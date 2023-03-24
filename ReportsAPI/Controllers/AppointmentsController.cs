using DevExpress.Export;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsAPI.DbModels;
using ReportsAPI.Models;
using ReportsAPI.Reports;
using ReportsAPI.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ReportsAPI.Controllers
{
    public class AppointmentsController : Controller
    {
        private ClinicaContext _db;

        public AppointmentsController(ClinicaContext db)
        {
            this._db = db;
        }

        public IActionResult Index(int id)
        {
            ReportsAPI.Models.Appointment[] appointmentArray = _db.Appointments.Where(cond => cond.Id == id)
                .Include(x => x.Beneficiary)
                .Include(y => y.Specialty)
                .Include(z => z.Doctor)
                .Include(a => a.Area)
                .Include(b => b.Beneficiary)
                .Select(x => new ReportsAPI.Models.Appointment()
                {
                    Id = x.Id,
                    Beneficiary = string.Format("{0} {1}", x.Beneficiary.FirstName, x.Beneficiary.LastName),
                    CreateAt = Helpers.GetTimeInfo(x.CreateAt),
                    Parentesco = x.Beneficiary.Relationship.Name,
                    Usuario = x.CreateBy,
                    INSS = x.Inss,
                    Especialidad = x.Specialty.Name,
                    DoctorId = x.DoctorId,
                    Doctor = x.Doctor.Name,
                    FechaCita = x.DateAppointment,
                    Observation = x.Observation
                }).ToArray();

            ReportsAPI.Models.Appointment appointment = appointmentArray.FirstOrDefault();

            DoctorTime doctorTime = _db.DoctorTimes
                .Where(x => x.DoctorId == appointment.DoctorId)
                .FirstOrDefault();

            for (int i = 0; i < (int)appointmentArray.Length; i++)
            {
                ReportsAPI.Models.Appointment appointment1 = appointmentArray[i];
                appointment1.HoraCita = doctorTime.StartHour.AddHours(-1);
            }

            Reports.Appointment appointment2 = new Reports.Appointment()
            {
                DataSource = appointmentArray
            };

            return (new PdfServices()).ExportPdf(appointment2);
        }
    }
}