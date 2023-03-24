using System;
using System.Runtime.CompilerServices;

namespace ReportsAPI.Models
{
    public class Appointment
    {
        public string Beneficiary
        {
            get;
            set;
        }

        public DateTime CreateAt
        {
            get;
            set;
        }

        public string Doctor
        {
            get;
            set;
        }

        public int DoctorId
        {
            get;
            set;
        }

        public string Especialidad
        {
            get;
            set;
        }

        public DateTime FechaCita
        {
            get;
            set;
        }

        public DateTime HoraCita
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        public int INSS
        {
            get;
            set;
        }

        public string Observation
        {
            get;
            set;
        }

        public string Parentesco
        {
            get;
            set;
        }

        public string Usuario
        {
            get;
            set;
        }

        public Appointment()
        {
        }
    }
}