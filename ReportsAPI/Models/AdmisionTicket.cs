using System;
using System.Runtime.CompilerServices;

namespace ReportsAPI.Models
{
    public class AdmisionTicket
    {
        public string Area
        {
            get;
            set;
        }

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

        public string Especialidad
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

        public DateTime Nace
        {
            get;
            set;
        }

        public int Numero
        {
            get;
            set;
        }

        public string Parentesco
        {
            get;
            set;
        }

        public string Sexo
        {
            get;
            set;
        }

        public string TipoIngreso
        {
            get;
            set;
        }

        public string Usuario
        {
            get;
            set;
        }

        public AdmisionTicket()
        {
        }
    }
}