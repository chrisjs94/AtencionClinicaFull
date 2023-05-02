using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Mvc;
using ReportsAPI.DbModels;
using ReportsAPI.Reports;
using ReportsAPI.Services;
using System.Linq;
using System;
using DevExpress.Xpo;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace ReportsAPI.Controllers
{
    public class TestServicesController : Controller
    {
        private ClinicaContext _db;

        public TestServicesController(ClinicaContext db)
        {
            this._db = db;
        }

        private static int CalculateEdad(DateTime birthDate)
        {
            DateTime now = DateTime.Now;
            return (int.Parse(now.ToString("yyyyMMdd")) - int.Parse(birthDate.ToString("yyyyMMdd"))) / 10000;
        }

        private IEnumerable<VwTestsResults> GetFiltered(IQueryable<VwTestsResults> result, int testId)
        {
            string[] strArray = new string[] { "VIH", "BAAR", "CULTIVO" };
            if (testId <= 0)
                result = result.Where(x => x.ReportName == null);
            else
                result = result.Where(x => x.ServiceId == testId);

            return result.ToList().Select(s =>
            {
                s.Edad = CalculateEdad(s.BirthDate);

                return s;
            });
        }

        public IActionResult Index(int id, int testId, string sizePage = "LETTER")
        {
            var queryable = (from st in _db.ServiceTests
                             join std in _db.ServiceTestDetails on st.Id equals std.ServiceTestId
                             join dc in _db.Doctors on st.DoctorId equals dc.Id
                             join srv in _db.Services on std.ServiceId equals srv.Id
                             join fll in _db.Follows on st.FollowId equals fll.Id
                             join adm in _db.Admissions on fll.AdmissionId equals adm.Id
                             join ben in _db.Beneficiaries on adm.BeneficiaryId equals ben.Id
                             join rel in _db.Relationships on ben.RelationshipId equals rel.Id
                             join sd in _db.ServiceDetails on std.ServiceDetailId equals sd.Id
                             join sty in _db.ServiceTypes on srv.TypeId equals sty.Id
                             where st.Id == id && srv.Id == testId
                             select new VwTestsResults()
                             {
                                 Id = st.Id,
                                 Date = st.Date,
                                 CreateBy = st.CreateBy,
                                 Doctor = dc.Name,
                                 Procedimiento = srv.Name,
                                 Examen = sd.Name,
                                 Result = std.Result,
                                 Reference = std.Reference,
                                 Um = std.Um,
                                 Inss = adm.Inss,
                                 Paciente = string.Format("{0} {1}", ben.FirstName, ben.LastName),
                                 Relationship = rel.Name,
                                 ServiceId = srv.Id,
                                 ReportName = srv.ReportName,
                                 Tipo = sty.Name,
                                 BirthDate = ben.BirthDate
                             });

            return this.Print(this.GetFiltered(queryable, testId), sizePage);
        }

        private FileStreamResult Print(IEnumerable<VwTestsResults> result, string sizePage)
        {
            VwTestsResults[] array = result.Where(x => !string.IsNullOrEmpty(x.Result)).ToArray();
            VwTestsResults vwTestsResult = Enumerable.FirstOrDefault<VwTestsResults>(array);
            XtraReport xtraReport = new XtraReport();
            if (vwTestsResult == null)
            {
                xtraReport = sizePage == "LETTER" ? new xrExamenes() : new xrExamenesTicket();
            }
            else
            {
                string reportName = vwTestsResult.ReportName;
                if (reportName == "VIH")
                {
                    xtraReport = sizePage == "LETTER" ? new xrExamenes() : new xrExamenesTicket();
                    VwTestsResults vwTestsResult1 = array[0];
                    string str = "";
                    string[] strArray = vwTestsResult1.Paciente.Split(" ", 0);
                    for (int i = 0; i < (int)strArray.Length; i++)
                    {
                        string str1 = strArray[i];
                        if (str1.Length > 0)
                        {
                            char chr = str1[0];
                            str = string.Concat(str, chr.ToString());
                        }
                    }
                    DateTime birthDate = vwTestsResult1.BirthDate;
                    vwTestsResult1.Paciente = string.Concat(str, birthDate.Year);
                }
                else if (reportName == "BAAR")
                {
                    xtraReport = sizePage == "LETTER" ? new xrExamenes() : new xrExamenesTicket();
                }
                else
                {
                    //xtraReport = (reportName == "CULTIVO" ? new xrExamenes() : new xrExamenes());
                    xtraReport = sizePage == "LETTER" ? new xrExamenes() : new xrExamenesTicket();
                }
            }
            xtraReport.DataSource = array;
            return (new PdfServices()).ExportPdf(xtraReport);
        }

        public IActionResult PrivateTest(int id, int testId = 0, string sizePage = "LETTER")
        {
            var queryable = (from x in _db.PrivateSendTests
                             join i in _db.PrivateServiceTests on x.Id equals i.SendTestId
                             join y in _db.PrivateServiceTestDetails on i.Id equals y.ServiceTestId
                             join z in _db.Services on y.ServiceId equals z.Id
                             join a in _db.ServiceTypes on z.TypeId equals a.Id
                             join b in _db.Doctors on x.DoctorId equals b.Id
                             join c in _db.FollowsPrivates on x.PrivateFollowId equals c.Id
                             join d in _db.Bills on c.BillId equals d.Id
                             join e in _db.PrivateCustomers on d.PrivateCustomerId equals e.Id
                             join f in _db.ServiceDetails on z.Id equals f.ServiceId
                             join g in _db.PrivateCustomerTypes on e.TypeId equals g.Id
                             where i.Id == id
                             select new VwTestsResults()
                             {
                                 Id = x.Id,
                                 Date = x.Date,
                                 CreateBy = x.CreateBy,
                                 Doctor = b.Name,
                                 Procedimiento = z.Name,
                                 Examen = f.Name,
                                 Result = y.Result,
                                 Reference = y.Reference,
                                 Um = y.Um,
                                 Inss = d.PrivateCustomerId,
                                 Paciente = string.Format("{0} {1}", e.FirstName, e.LastName),
                                 Relationship = g.Name,
                                 Edad = CalculateEdad(e.BirthDate),
                                 ServiceId = z.Id,
                                 ReportName = z.ReportName,
                                 Tipo = a.Name,
                                 BirthDate = e.BirthDate
                             });

            return this.Print(this.GetFiltered(queryable, testId), sizePage);
        }
    }
}
