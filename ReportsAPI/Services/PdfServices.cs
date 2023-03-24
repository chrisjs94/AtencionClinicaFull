using DevExpress.Pdf.Native.BouncyCastle.Utilities.IO;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ReportsAPI.Services
{
    public class PdfServices
    {
        public PdfServices()
        {
        }

        public FileStreamResult ExportPdf(XtraReport report)
        {
            MemoryStream memoryStream = new MemoryStream();
            report.CreateDocument();
            report.PrintingSystem.ExportToPdf(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(memoryStream, "application/pdf");
        }
    }
}
