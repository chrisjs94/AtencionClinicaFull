using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AtencionClinica.Extensions;
using AtencionClinica.Models;
using AtencionClinica.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace AtencionClinica.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {
        private ClinicaContext _db = null;
        private readonly ILogger<BillsController> _logger;
        private IProductServices<Bill> _service;
        public BillsController(ClinicaContext db, IProductServices<Bill> service, ILogger<BillsController> logger)
        {
            this._db = db;
            _service = service;
            this._logger = logger;
        }

        [Route("api/bill/get")]
        public IActionResult Get(int skip, int take, IDictionary<string, string> values)
        {
            IQueryable<Bill> bills = _db.Bills
            .Include(x => x.PrivateCustomer)
           .OrderByDescending(x => x.CreateAt);

            if (values.ContainsKey("nombre"))
            {
                var name = Convert.ToString(values["nombre"]);
                bills = bills.Where(x => x.PrivateCustomer.FirstName.StartsWith(name) || x.PrivateCustomer.LastName.StartsWith(name));
            }

            if (values.ContainsKey("currencyId"))
            {
                var currencyId = Convert.ToInt32(values["currencyId"]);
                bills = bills.Where(x => x.CurrencyId == currencyId);
            }

            if (values.ContainsKey("createAt"))
            {
                var createAt = Convert.ToDateTime(values["createAt"]);
                bills = bills.Where(x => x.CreateAt > createAt && x.CreateAt < createAt.AddDays(1));
            }

            if (values.ContainsKey("isCredit"))
            {
                var isCredit = Convert.ToBoolean(values["isCredit"]);
                bills = bills.Where(x => (x.IsCredit ?? false) == isCredit);
            }

            var totalCount = bills.Count();

            if (values.ContainsKey("requireTotalCount"))
            {
                var requireTotalCount = Convert.ToBoolean(values["requireTotalCount"]);
                if (requireTotalCount)
                {
                    bills = bills.Skip(skip).Take(take);
                }
            }

            var items = bills.Select(x => new
            {
                x.Id,
                Nombre = x.PrivateCustomer.GetFullName(),
                x.AreaId,
                x.BillTypeId,
                x.CreateAt,
                x.CreateBy,
                x.Active,
                x.Total,
                x.CurrencyId,
                IsCredit = x.IsCredit ?? false
            });

            return Json(new
            {
                items,
                totalCount
            });

        }

        [HttpPost("api/bill/post")]
        public async Task<IActionResult> Post([FromBody] Bill bill)
        {
            var user = this.GetAppUser(_db);
            if (user == null)
                return BadRequest("La informacion del usuario cambio, inicie sesion nuevamente");

            var app = _db.Apps.FirstOrDefault();
            //admission.Inss = bene.Inss;
            bill.Active = true;
            bill.Total = bill.BillDetails.Sum(x => x.Total);
            bill.CreateAt = UserHelpers.GetTimeInfo();
            bill.CreateBy = user.Username;

            if (bill.PrivateCustomerId == (int)PrivateCustomers.ClienteContado)
            {
                var cliente = _db.PrivateCustomers.FirstOrDefault(x => x.Id == (int)PrivateCustomers.ClienteContado);
                if (cliente == null)
                    return BadRequest("La información del cliente de contado no se encontro en el sistema");

                bill.NameCustomer = string.IsNullOrEmpty(bill.NameCustomer) ? cliente.GetFullName() : bill.NameCustomer;
            }

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var result = _service.Create(bill);

                    if (!result.IsValid)
                        return BadRequest(result.Error);

                    await _db.SaveChangesAsync();

                    var lastOutPutProduct = _db.OutPutProducts.OrderByDescending(x => x.Id).FirstOrDefault(x => x.CreateBy == user.Username && x.AreaId == bill.AreaId);

                    if (lastOutPutProduct != null)
                        lastOutPutProduct.Reference = bill.Id.ToString();

                    if (bill.PrivateCustomerId != (int)PrivateCustomers.ClienteContado)
                    {
                        var areaDoctorId = bill.AreaDoctorId;

                        var sendTest = new PrivateSendTest()
                        {
                            Date = DateTime.Now,
                            DoctorId = areaDoctorId,
                            CreateAt = DateTime.Now,
                            CreateBy = user.Username
                        };

                        var follow = new FollowsPrivate
                        {
                            BillId = bill.Id,
                            AreaSourceId = 3, //caja
                            AreaTargetId = bill.AreaId,
                            Observation = "Tranferencia automatica desde caja",
                            CreateAt = UserHelpers.GetTimeInfo(),
                            CreateBy = user.Username
                        };

                        PrivateServiceTest serviceTest = new PrivateServiceTest()
                        {
                            SendTest = sendTest,
                            DoctorId = sendTest.DoctorId,
                            CreateAt = UserHelpers.GetTimeInfo(),
                            CreateBy = user.Username,
                            Date = UserHelpers.GetTimeInfo()
                        };
                        follow.PrivateServiceTests.Add(serviceTest);

                        if (bill.BillDetails.Any(x => x.ServiceId != null))
                        {
                            var work = new PrivateWorkOrder
                            {
                                Date = UserHelpers.GetTimeInfo(),
                                CreateAt = UserHelpers.GetTimeInfo(),
                                CreateBy = user.Username,
                                DoctorId = areaDoctorId,
                                Active = true,
                                Reference = bill.Id.ToString(),
                                FollowsPrivateId = follow.Id
                            };

                            follow.PrivateWorkOrders.Add(work);

                            foreach (var item in bill.BillDetails)
                            {
                                var service = _db.Services.FirstOrDefault(x => x.Id == item.ServiceId);
                                if (service.IsCultive)
                                {
                                    serviceTest.PrivateServiceTestCultives.Add(new PrivateServiceTestCultive()
                                    {
                                        ServiceTest = serviceTest,
                                        ServiceId = (int)item.ServiceId,
                                        Name = service.Name,
                                        ServiceTestId = serviceTest.Id
                                    });
                                }
                                else if (item.ServiceId == 8) //BAAR
                                {
                                    for (int i = 1; i <= 3; i++)
                                    {
                                        serviceTest.PrivateServiceTestBaarDetails.Add(new PrivateServiceTestBaarDetail()
                                        {
                                            ServiceTest = serviceTest,
                                            ServiceId = (int)item.ServiceId,
                                            TestNumber = i,
                                            ServiceTestId = serviceTest.Id
                                        });
                                    }
                                }
                                else
                                {
                                    var serviceDetails = _db.ServiceDetails.Where(x => x.ServiceId == item.ServiceId);

                                    if (serviceDetails.Any())
                                    {
                                        foreach (var item2 in serviceDetails)
                                        {
                                            serviceTest.PrivateServiceTestDetails.Add(new PrivateServiceTestDetail()
                                            {
                                                ServiceTest = serviceTest,
                                                ServiceId = (int)item.ServiceId,
                                                ServiceDetailId = item2.Id,
                                                Name = item2.Name,
                                                Um = item2.Um,
                                                Reference = item2.Reference,
                                                Result = "",
                                                ResultJson = "",
                                                ServiceTestId = serviceTest.Id
                                            });
                                        }
                                    }
                                }

                                sendTest.PrivateSendTestDetails.Add(new PrivateSendTestDetail()
                                {
                                    Serviceid = (int)item.ServiceId
                                });

                                work.PrivateWorkOrderDetails.Add(new PrivateWorkOrderDetail
                                {
                                    IsService = true,
                                    ServiceId = item.ServiceId,
                                    Quantity = Convert.ToDouble(item.Quantity),
                                    Price = item.Price,
                                    Total = item.Total,
                                    Costo = 0,
                                    PrivateWorkOrderId = work.Id
                                });
                            }
                        }

                        follow.PrivateSendTests.Add(sendTest);
                        _db.FollowsPrivates.Add(follow);
                    }

                    await _db.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    _logger.LogError("Logeando el modelo Bill: {0}", System.Text.Json.JsonSerializer.Serialize(bill));
                    transaction.Rollback();
                    return BadRequest(ex.Message);
                }
            }

            return Json(bill);

        }

        [HttpGet("api/bill/{id}/alta")]
        public IActionResult Alta(int id)
        {

            var bill = _db.Bills.FirstOrDefault(x => x.Id == id);

            if (bill != null)
            {
                bill.Finished = true;
                _db.SaveChanges();
            }

            return Json(new { n = id });
        }

        [HttpGet("api/bill/{id}/delete")]
        public IActionResult Delete(int id)
        {
            var bill = _db.Bills.FirstOrDefault(x => x.Id == id);
            //TODO verificar si no hay descargue de inventario para poder anular
            if (bill != null)
            {
                if (bill.Finished)
                    return BadRequest("No se puede anular una factura que ya fue procesada por las areas");

                var follow = _db.FollowsPrivates.Include(x => x.PrivateWorkOrders).ThenInclude(x => x.PrivateWorkOrderDetails)
                .Where(x => x.BillId == id);

                foreach (var item in follow)
                {

                    foreach (var work in item.PrivateWorkOrders)
                    {
                        if (work.PrivateWorkOrderDetails.Any(x => !x.IsService))
                            return BadRequest("No se puede anular la factura porque ya tiene ordenes de trabajo con productos realizadas");
                    }

                }

                bill.Active = false;
                _db.SaveChanges();
            }

            return Json(new { n = id });
        }
    }
}
