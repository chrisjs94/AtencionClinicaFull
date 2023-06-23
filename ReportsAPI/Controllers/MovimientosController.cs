using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsAPI.DbModels;
using ReportsAPI.Models;
using ReportsAPI.Reports;
using ReportsAPI.Services;
using System.Linq;

namespace ReportsAPI.Controllers
{
    public class MovimientosController : Controller
    {
        private ClinicaContext _db;

        public MovimientosController(ClinicaContext db)
        {
            this._db = db;
        }

        //public IActionResult AjusteEntrada(int id)
        //{
        //    return this.Movimientos(id, "Ajuste de Entrada");
        //}

        public IActionResult AjusteSalida(int id)
        {
            var array = (from a in _db.OutPutProducts
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
                         });
            xrSaldoInicial _xrSaldoInicial = new xrSaldoInicial()
            {
                DataSource = array
            };
            return (new PdfServices()).ExportPdf(_xrSaldoInicial);
        }

        //public IActionResult Compras(int id)
        //{
        //    Movimiento[] array = Enumerable.ToArray<Movimiento>(Queryable.Select(Queryable.Where(Queryable.Join(Queryable.Join(Queryable.Join(Queryable.Join(Queryable.Join(this._db.InPutProducts, this._db.InPutProductDetails, (InPutProducts ipp) => ipp.Id, (InPutProductDetails ippd) => ippd.InPutProductId, (InPutProducts ipp, InPutProductDetails ippd) => new { ipp = ipp, ippd = ippd }), this._db.Providers, (u003cu003eh__TransparentIdentifier0) => u003cu003eh__TransparentIdentifier0.ipp.ProviderId, (Providers p) => (int?)p.Id, (u003cu003eh__TransparentIdentifier0, p) => new { <> h__TransparentIdentifier0 = u003cu003eh__TransparentIdentifier0, p = p }), this._db.Areas, (u003cu003eh__TransparentIdentifier1) => u003cu003eh__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.AreaId, (Areas a) => a.Id, (u003cu003eh__TransparentIdentifier1, a) => new { <> h__TransparentIdentifier1 = u003cu003eh__TransparentIdentifier1, a = a }), this._db.Products, (u003cu003eh__TransparentIdentifier2) => u003cu003eh__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ippd.ProductId, (Products pro) => pro.Id, (u003cu003eh__TransparentIdentifier2, pro) => new { <> h__TransparentIdentifier2 = u003cu003eh__TransparentIdentifier2, pro = pro }), this._db.Users, (u003cu003eh__TransparentIdentifier3) => u003cu003eh__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.CreateBy, (Users u) => u.Username, (u003cu003eh__TransparentIdentifier3, u) => new { <> h__TransparentIdentifier3 = u003cu003eh__TransparentIdentifier3, u = u }), (u003cu003eh__TransparentIdentifier4) => u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.Id == id), (u003cu003eh__TransparentIdentifier4) => new Movimiento()
        //    {
        //        Id = string.Format("#{0}", (object)u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.Id),
        //        Date = u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.Date,
        //        ProviderOrClient = u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.p.Name,
        //        Area = u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.a.Name,
        //        Username = u003cu003eh__TransparentIdentifier4.u.FullName,
        //        Tipo = "Orden de Compra",
        //        Reference = u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.Reference,
        //        Observation = u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.Observation,
        //        ProductName = u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.pro.Name,
        //        ProductCost = u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ippd.Cost,
        //        ProductQuantity = u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ippd.Quantity,
        //        ProductTotal = u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ippd.Total,
        //        ProductId = u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ippd.ProductId,
        //        Total = u003cu003eh__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.Total
        //    }));
        //    xrMovimientos xrMovimiento = new xrMovimientos()
        //    {
        //        DataSource = array
        //    };
        //    return (new PdfServices()).ExportPdf(xrMovimiento);
        //}

        //public IActionResult Movimientos(int id, string tipo)
        //{
        //    MovimientoBase[] array = Enumerable.ToArray<MovimientoBase>(Queryable.Select(Queryable.Where(Queryable.Join(Queryable.Join(Queryable.Join(Queryable.Join(this._db.InPutProducts, this._db.InPutProductDetails, (InPutProducts ipp) => ipp.Id, (InPutProductDetails ippd) => ippd.InPutProductId, (InPutProducts ipp, InPutProductDetails ippd) => new { ipp = ipp, ippd = ippd }), this._db.Areas, (u003cu003eh__TransparentIdentifier0) => u003cu003eh__TransparentIdentifier0.ipp.AreaId, (Areas a) => a.Id, (u003cu003eh__TransparentIdentifier0, a) => new { <> h__TransparentIdentifier0 = u003cu003eh__TransparentIdentifier0, a = a }), this._db.Products, (u003cu003eh__TransparentIdentifier1) => u003cu003eh__TransparentIdentifier1.<> h__TransparentIdentifier0.ippd.ProductId, (Products pro) => pro.Id, (u003cu003eh__TransparentIdentifier1, pro) => new { <> h__TransparentIdentifier1 = u003cu003eh__TransparentIdentifier1, pro = pro }), this._db.Users, (u003cu003eh__TransparentIdentifier2) => u003cu003eh__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.CreateBy, (Users u) => u.Username, (u003cu003eh__TransparentIdentifier2, u) => new { <> h__TransparentIdentifier2 = u003cu003eh__TransparentIdentifier2, u = u }), (u003cu003eh__TransparentIdentifier3) => u003cu003eh__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.Id == id), (u003cu003eh__TransparentIdentifier3) => new MovimientoBase()
        //    {
        //        Id = string.Format("#{0}", (object)u003cu003eh__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.Id),
        //        Date = u003cu003eh__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.Date,
        //        Area = u003cu003eh__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.a.Name,
        //        Username = u003cu003eh__TransparentIdentifier3.u.FullName,
        //        Tipo = tipo,
        //        Observation = u003cu003eh__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.Observation,
        //        ProductName = u003cu003eh__TransparentIdentifier3.<> h__TransparentIdentifier2.pro.Name,
        //        ProductCost = u003cu003eh__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ippd.Cost,
        //        ProductQuantity = u003cu003eh__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ippd.Quantity,
        //        ProductTotal = u003cu003eh__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ippd.Total,
        //        ProductId = u003cu003eh__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ippd.ProductId,
        //        Total = u003cu003eh__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.ipp.Total
        //    }));
        //    xrSaldoInicial _xrSaldoInicial = new xrSaldoInicial()
        //    {
        //        DataSource = array
        //    };
        //    return (new PdfServices()).ExportPdf(_xrSaldoInicial);
        //}

        private MovimientoRequisaDespacho[] QueryRequisa(int id, string tipo)
        {
            var array = _db.Traslates
                .Where(cond => cond.Id == id)
                .Include(b => b.AreaSource)
                .Include(c => c.AreaTarget)
                .Include(d => d.Stage)
                .Include(a => a.TraslateDetails)
                .Include(f => f.TraslateDetails.)
                .ThenInclude(e => e.Product)
                .Select(s => new MovimientoRequisaDespacho()
                {
                    Id = string.Format("#{0}", s,id),
                    Date = s.Date,
                    Estado = .Name,
                    AreaSource = u003cu003eh__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.aSource.Name,
                    AreaTarget = u003cu003eh__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.aTarget.Name,
                    Username = u003cu003eh__TransparentIdentifier5.u.FullName,
                    Tipo = tipo,
                    Observation = u003cu003eh__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.t.Observation,
                    ProductName = u003cu003eh__TransparentIdentifier5.<> h__TransparentIdentifier4.pro.Name,
                    ProductCost = u003cu003eh__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.td.Cost,
                    ProductQuantity = u003cu003eh__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.td.QuantityRequest,
                    ProductResponse = u003cu003eh__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.td.QuantityResponse,
                    ProductTotal = u003cu003eh__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.td.Total,
                    ProductId = u003cu003eh__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.td.ProductId,
                    Total = u003cu003eh__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.t.Total,
                    ModifyBy = u003cu003eh__TransparentIdentifier5.<> h__TransparentIdentifier4.<> h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.t.LastModificationBy
                })


        MovimientoRequisaDespacho[] array = Enumerable.ToArray<MovimientoRequisaDespacho>(
            Queryable.Select(Queryable.Where(Queryable.Join(
                Queryable.Join(Queryable.Join(Queryable.Join(
                    Queryable.Join(Queryable.Join(this._db.Traslates, this._db.TraslateDetails,
                    (Traslates t) => t.Id, (TraslateDetails td) => td.TraslateId,
                    (Traslates t, TraslateDetails td) => new { t = t, td = td }),
                    this._db.TraslateStages, (u003cu003eh__TransparentIdentifier0) =>
                    u003cu003eh__TransparentIdentifier0.t.StageId, (TraslateStages e) => e.Id,
                    (u003cu003eh__TransparentIdentifier0, e) => new { <> h__TrarentIdenansptifier0 = u003cu003eh__TransparentIdentifier0, e = e }),
                    this._db.Areas, (u003cu003eh__TransparentIdentifier1) =>
                    u003cu003eh__TransparentIdentifier1.<> h__TransparentIdentifier0.t.AreaSourceId,
                    (Areas aSource) => aSource.Id, (u003cu003eh__TransparentIdentifier1, aSource) =>
                    new { <> h__TransparentIdentifier1 = u003cu003eh__TransparentIdentifier1, aSource = aSource }),
                    this._db.Areas, (u003cu003eh__TransparentIdentifier2) =>
                    u003cu003eh__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.t.AreaTargetId,
                    (Areas aTarget) => aTarget.Id, (u003cu003eh__TransparentIdentifier2, aTarget) =>
                    new { <> h__TransparentIdentifier2 = u003cu003eh__TransparentIdentifier2, aTarget = aTarget }),
                    this._db.Products, (u003cu003eh__TransparentIdentifier3) => u003cu003eh__TransparentIdentifier3.<>
                    h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.td.ProductId,
                    (Products pro) => pro.Id, (u003cu003eh__TransparentIdentifier3, pro) => new
                    { <> h__TransparentIdentifier3 = u003cu003eh__TransparentIdentifier3, pro = pro }),
                this._db.Users, (u003cu003eh__TransparentIdentifier4) => u003cu003eh__TransparentIdentifier4.<>
                h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.t.CreateBy,
                (Users u) => u.Username, (u003cu003eh__TransparentIdentifier4, u) => new { <> h__TransparentIdentifier4 = u003cu003eh__TransparentIdentifier4, u = u }),
                (u003cu003eh__TransparentIdentifier5) => u003cu003eh__TransparentIdentifier5.<> h__TransparentIdentifier4.<>
                h__TransparentIdentifier3.<> h__TransparentIdentifier2.<> h__TransparentIdentifier1.<> h__TransparentIdentifier0.t.Id == id),
                (u003cu003eh__TransparentIdentifier5) => new MovimientoRequisaDespacho()
                {
                    
                }));
            return array;
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

        public IActionResult SaldoInicial(int id)
        {
            return this.Movimientos(id, "Saldo Inicial");
        }
    }
}
