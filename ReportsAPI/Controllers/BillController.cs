using DevExpress.CodeParser;
using Microsoft.AspNetCore.Mvc;
using ReportsAPI.DbModels;
using ReportsAPI.Models;
using ReportsAPI.Reports;
using ReportsAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ReportsAPI.Controllers
{
    public class BillController : Controller
    {
        private ClinicaContext _db;

        public BillController(ClinicaContext db)
        {
            this._db = db;
        }

        public IActionResult BillByClient(int id)
        {
            IEnumerable<vwBillFinishedByClient> vwBillFinishedByClientArray = _db.vwBillFinishedByClients
                .Where(x => x.Id == id)
                .ToList()
                .Select(x =>
            {
                string str;
                string str1;
                vwBillFinishedByClient _vwBillFinishedByClient = new vwBillFinishedByClient()
                {
                    Id = x.Id,
                    CreateBy = x.CreateBy,
                    CreateAt = x.CreateAt
                };
                string firstName = x.FirstName;
                if (firstName != null)
                {
                    str = firstName.Trim();
                }
                else
                {
                    str = null;
                }
                string lastName = x.LastName;
                if (lastName != null)
                {
                    str1 = lastName.Trim();
                }
                else
                {
                    str1 = null;
                }
                _vwBillFinishedByClient.FirstName = string.Concat(str, " ", str1);
                _vwBillFinishedByClient.TypeCustomer = x.TypeCustomer;
                _vwBillFinishedByClient.ContractType = x.ContractType;
                _vwBillFinishedByClient.Area = x.Area;
                _vwBillFinishedByClient.ServiceName = x.ServiceName;
                _vwBillFinishedByClient.Quantity = x.Quantity;
                _vwBillFinishedByClient.Price = x.Price;
                _vwBillFinishedByClient.SubTotal = Convert.ToDouble(x.Price * Convert.ToDecimal(x.Quantity));
                _vwBillFinishedByClient.Active = x.Active;

                return _vwBillFinishedByClient;
            });

            xrBalanceByClient _xrBalanceByClient = new xrBalanceByClient()
            {
                DataSource = vwBillFinishedByClientArray
            };
            if (vwBillFinishedByClientArray.Count() != 0 && vwBillFinishedByClientArray.FirstOrDefault().Active)
            {
                _xrBalanceByClient.Parameters["Anulado"].Value = "ANULADO";
                _xrBalanceByClient.Parameters["Anulado"].Visible = false;
            }
            return (new PdfServices()).ExportPdf(_xrBalanceByClient);
        }

        public IActionResult Index(int id)
        {
            IEnumerable<vwBillProductsServices> array = _db.VwBillProductsServices.Where(x => x.Id == id).ToArray();
            string str = "Servicios por consulta y examenes";
            Reports.Bill bill = new Reports.Bill()
            {
                DataSource = array
            };

            var bill1 = _db.Bills.Where(x => x.Id == id).FirstOrDefault();
            if (bill1 != null)
            {
                if (bill1.AreaId == 7)
                {
                    str = "Servicios y medicamentos de Farmacia";
                }
                if (bill1.AreaId == 9)
                {
                    str = "Servicios y medicamentos de Farmacia Privada";
                }
            }
            bill.Parameters["titulo"].Value = str;
            bill.Parameters["titulo"].Visible = false;
            return (new PdfServices()).ExportPdf(bill);
        }

        //public IActionResult Resumen(DateTime start, DateTime end, bool onlyIngreso, int customerId, int serviceId)
        //{
            //IQueryable<VwBill> queryable = _db.VwBills.Where(x => (x.RegisterAt >= start) && (x.RegisterAt <= end));
            //if (onlyIngreso)
            //    queryable = queryable.Where(x => x.DetalleId != null);
            //if (customerId > 0)
            //    queryable = queryable.Where(x => x.ClientId == customerId);
            //if (serviceId > 0)
            //    queryable = queryable.Where(x => x.ServiceId == serviceId);

            //vwBills[] array = Enumerable.ToArray<vwBills>(queryable);
            //vwBills[] vwBillsArray = array;
            //Func<vwBills, int> u003cu003e9_34 = BillController.<> c.<> 9__3_4;
            //if (u003cu003e9_34 == null)
            //{
            //    u003cu003e9_34 = new Func<vwBills, int>(BillController.<> c.<> 9, (vwBills x) => x.ClientId);
            //    BillController.<> c.<> 9__3_4 = u003cu003e9_34;
            //}
            //IEnumerable<int> enumerable = Enumerable.Distinct<int>(Enumerable.Select<vwBills, int>((IEnumerable<vwBills>)vwBillsArray, u003cu003e9_34));
            //int num = 1;
            //foreach (int num1 in enumerable)
            //{
            //    IEnumerable<vwBills> enumerable1 = Enumerable.Where<vwBills>(array, new Func<vwBills, bool>(variable, (vwBills x) => x.ClientId == this.clientId));
            //    Func<vwBills, DateTime> u003cu003e9_37 = BillController.<> c.<> 9__3_7;
            //    if (u003cu003e9_37 == null)
            //    {
            //        u003cu003e9_37 = new Func<vwBills, DateTime>(BillController.<> c.<> 9, (vwBills x) => x.RegisterAt);
            //        BillController.<> c.<> 9__3_7 = u003cu003e9_37;
            //    }
            //    foreach (vwBills vwBill in Enumerable.OrderBy<vwBills, DateTime>(enumerable1, u003cu003e9_37))
            //    {
            //        vwBill.RowNumber = (long)num;
            //        num++;
            //    }
            //    num = 1;
            //}
            //vwBills[] vwBillsArray1 = array;
            //Func<vwBills, BillHemodialisis> u003cu003e9_35 = BillController.<> c.<> 9__3_5;
            //if (u003cu003e9_35 == null)
            //{
            //    u003cu003e9_35 = new Func<vwBills, BillHemodialisis>(BillController.<> c.<> 9, (vwBills x) => {
            //        string str;
            //        string str1;
            //        BillHemodialisis billHemodialisi = new BillHemodialisis()
            //        {
            //            Id = x.Id,
            //            GrantTotal = "",
            //            TypeCustomer = x.TypeCustomer,
            //            Contract = x.ContractType
            //        };
            //        string firstName = x.FirstName;
            //        if (firstName != null)
            //        {
            //            str = firstName.Trim();
            //        }
            //        else
            //        {
            //            str = null;
            //        }
            //        string lastName = x.LastName;
            //        if (lastName != null)
            //        {
            //            str1 = lastName.Trim();
            //        }
            //        else
            //        {
            //            str1 = null;
            //        }
            //        billHemodialisi.Name = string.Concat(str, " ", str1);
            //        billHemodialisi.Direccion = "";
            //        billHemodialisi.CreateAt = x.CreateAt;
            //        billHemodialisi.CreateBy = x.CreateBy;
            //        billHemodialisi.TipoIngreso = x.Observation;
            //        billHemodialisi.ServiceName = x.ServiceName;
            //        billHemodialisi.Quantity = Convert.ToDecimal(x.Quantity);
            //        billHemodialisi.Price = x.Price;
            //        billHemodialisi.Total = Math.Round(Convert.ToDecimal(x.Quantity) * x.Price, 2);
            //        billHemodialisi.RegisterAt = x.RegisterAt;
            //        billHemodialisi.Inss = x.Inss;
            //        billHemodialisi.RowNumber = x.RowNumber;
            //        billHemodialisi.AddAt = x.AddAt;
            //        billHemodialisi.ClientId = x.ClientId;
            //        return billHemodialisi;
            //    });
            //    BillController.<> c.<> 9__3_5 = u003cu003e9_35;
            //}
            //IEnumerable<BillHemodialisis> enumerable2 = Enumerable.Select<vwBills, BillHemodialisis>((IEnumerable<vwBills>)vwBillsArray1, u003cu003e9_35);
            //xrHemodialisis xrHemodialisi = new xrHemodialisis()
            //{
            //    DataSource = enumerable2
            //};
            //return (new PdfServices()).ExportPdf(xrHemodialisi);
        //}
    }
}
