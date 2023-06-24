using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsAPI.DbModels;
using ReportsAPI.Models;
using ReportsAPI.Reports;
using ReportsAPI.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsAPI.Controllers
{
    public class MovimientosController : Controller
    {
        private ClinicaContext _db;

        public MovimientosController(ClinicaContext db)
        {
            this._db = db;
        }

        public async Task<IActionResult> AjusteEntrada(int id)
        {
            return await Movimientos(id, "Ajuste de Entrada");
        }

        public async Task<IActionResult> AjusteSalida(int id)
        {
            var array = await (from a in _db.OutPutProducts
                         join b in _db.OutPutProductDetails on a.Id equals b.OutPutProductId
                         join c in _db.Areas on a.AreaId equals c.Id
                         join d in _db.Products on b.ProductId equals d.Id
                         join e in _db.Users on a.CreateBy equals e.Username
                         where a.Id == id
                         select new MovimientoBase()
                         {
                             Id = string.Format("#{0}", a.Id),
                             Date = a.Date,
                             Area = c.Name,
                             Username = e.FullName,
                             Tipo = "Ajuste de Salida",
                             Observation = a.Observation,
                             ProductName = d.Name,
                             ProductCost = b.Cost,
                             ProductQuantity = b.Quantity,
                             ProductTotal = b.Total,
                             ProductId = b.ProductId,
                             Total = a.Total
                         }).ToArrayAsync();
            xrSaldoInicial _xrSaldoInicial = new xrSaldoInicial()
            {
                DataSource = array
            };
            return (new PdfServices()).ExportPdf(_xrSaldoInicial);
        }

        public async Task<IActionResult> Compras(int id)
        {
            Movimiento[] array =
                await (from ippd in _db.InPutProductDetails
                .Include(b => b.InPutProduct)
                    .ThenInclude(c => c.Provider)
                .Include(b => b.InPutProduct)
                    .ThenInclude(c => c.Area)
                .Include(b => b.Product)
                join u in _db.Users on ippd.InPutProduct.CreateBy equals u.Username
                where ippd.InPutProductId == id
                select new Movimiento()
                {
                    Id = string.Format("#{0}", ippd.InPutProductId),
                    Date = ippd.InPutProduct.Date,
                    ProviderOrClient = ippd.Product.Name,
                    Area = ippd.InPutProduct.Area.Name,
                    Username = u.FullName,
                    Tipo = "Orden de Compra",
                    Reference = ippd.InPutProduct.Reference,
                    Observation = ippd.InPutProduct.Observation,
                    ProductName = ippd.Product.Name,
                    ProductCost = ippd.Cost,
                    ProductQuantity = ippd.Quantity,
                    ProductTotal = ippd.Total,
                    ProductId = ippd.ProductId,
                    Total = ippd.InPutProduct.Total
                }).ToArrayAsync();
            xrMovimientos xrMovimiento = new xrMovimientos()
            {
                DataSource = array
            };
            return (new PdfServices()).ExportPdf(xrMovimiento);
        }

        public async Task<IActionResult> Movimientos(int id, string tipo)
        {
            MovimientoBase[] array =
                await (from ipp in _db.InPutProducts.Include(c => c.Area)
                 join ippd in _db.InPutProductDetails.Include(p => p.Product) on ipp.Id equals ippd.InPutProductId
                 join u in _db.Users on ipp.CreateBy equals u.Username
                 where ipp.Id == id
                 select new MovimientoBase()
                 {
                     Id = string.Format("#{0}", ipp.Id),
                     Date = ipp.Date,
                     Area = ipp.Area.Name,
                     Username = u.FullName,
                     Tipo = tipo,
                     Observation = ipp.Observation,
                     ProductName = ippd.Product.Name,
                     ProductCost = ippd.Cost,
                     ProductQuantity = ippd.Quantity,
                     ProductTotal = ippd.Total,
                     ProductId = ippd.ProductId,
                     Total = ipp.Total
                 }).ToArrayAsync();

            xrSaldoInicial _xrSaldoInicial = new xrSaldoInicial()
            {
                DataSource = array
            };
            return (new PdfServices()).ExportPdf(_xrSaldoInicial);
        }

        private MovimientoRequisaDespacho[] QueryRequisa(int id, string tipo)
        {
            var array = from t in _db.Traslates
                .Where(cond => cond.Id == id)
                .Include(b => b.AreaSource)
                .Include(c => c.AreaTarget)
                .Include(d => d.Stage)
                        join y in _db.Users on t.CreateBy equals y.Username
                        join td in _db.TraslateDetails on t.Stage.Id equals td.TraslateId
                        join p in _db.Products on td.ProductId equals p.Id
                        select new MovimientoRequisaDespacho()
                        {
                            Id = string.Format("#{0}", id),
                            Date = t.Date,
                            Estado = t.Stage.Name,
                            AreaSource = t.AreaSource.Name,
                            AreaTarget = t.AreaTarget.Name,
                            Username = y.FullName,
                            Tipo = tipo,
                            Observation = t.Observation,
                            ProductName = p.Name,
                            ProductCost = td.Cost,
                            ProductQuantity = td.QuantityRequest,
                            ProductResponse = td.QuantityResponse,
                            ProductTotal = td.Total,
                            ProductId = td.ProductId,
                            Total = t.Total,
                            ModifyBy = t.LastModificationBy
                        };

            return array.ToArray();
        }

        public IActionResult RequisaDespacho(int id)
        {
            MovimientoRequisaDespacho[] movimientoRequisaDespachoArray = this.QueryRequisa(id, "Despacho de Requisa");
            xrRequisaResponse _xrRequisaResponse = new xrRequisaResponse()
            {
                DataSource = movimientoRequisaDespachoArray
            };
            return (new PdfServices()).ExportPdf(_xrRequisaResponse);
        }

        public IActionResult RequisaSolicitud(int id)
        {
            MovimientoRequisaDespacho[] movimientoRequisaDespachoArray = this.QueryRequisa(id, "Solicitud de requisa");
            RequisaSolicitud requisaSolicitud = new RequisaSolicitud()
            {
                DataSource = movimientoRequisaDespachoArray
            };
            return (new PdfServices()).ExportPdf(requisaSolicitud);
        }

        public async Task<IActionResult> SaldoInicial(int id)
        {
            return await Movimientos(id, "Saldo Inicial");
        }
    }
}
