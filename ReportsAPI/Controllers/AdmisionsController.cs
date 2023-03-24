using DevExpress.XtraRichEdit.Import.Rtf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsAPI.DbModels;
using ReportsAPI.Models;
using ReportsAPI.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsAPI.Controllers
{
    public class AdmisionsController : Controller
    {
        private ClinicaContext _db;

        public AdmisionsController(ClinicaContext db)
        {
            this._db = db;
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            AdmisionTicket[] array = await _db.Admissions.Where(cond => cond.Id == id)
                .Include(a => a.Specialty)
                .Include(b => b.Area)
                .Include(c => c.Beneficiary)
                .ThenInclude(d => d.Sex)
                .Select(x => new AdmisionTicket()
                {
                    Id = x.Id,
                    Numero = x.NumberOfDay,
                    Nace = x.Beneficiary.BirthDate,
                    Sexo = x.Beneficiary.Sex.Name,
                    Beneficiary = string.Format("{0} {1}", x.Beneficiary.FirstName, x.Beneficiary.LastName),
                    CreateAt = x.CreateAt,
                    Area = x.Area.Name,
                    Parentesco = x.Beneficiary.Relationship.Name,
                    Usuario = x.CreateBy,
                    INSS = x.Inss,
                    Especialidad = x.Specialty.Name,
                    TipoIngreso = x.Type.Name
                }).ToArrayAsync();

            Reports.AdmisionTicket admisionTicket = new Reports.AdmisionTicket()
            {
                DataSource = array
            };
            try
            {
                return (new PdfServices()).ExportPdf(admisionTicket);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
