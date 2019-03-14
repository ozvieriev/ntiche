using SelectPdf;
using System;
using System.Text;

namespace Certificate.Templates
{
    public static class PdfCertificate
    {
        private static readonly string LinkCssStyle = "<link href='css/style.css' rel='stylesheet' />";
        private static readonly string CssStyle = $"<style type'text/css'>{App_LocalResources.LetterOfAttendance.local.style}</style>";

        public static byte[] CreatePdf(CertificateTemplate certificateTemplate, Notification notification)
        {
            var htmlTemplate = App_LocalResources.LetterOfAttendance.local.html;

            var builder = new StringBuilder(htmlTemplate);
            builder.Replace(LinkCssStyle, CssStyle);

            var properties = notification.GetProperties();
            foreach (var property in properties)
                builder.Replace($"<{property.Name}>", notification[property.Name]);

            var converter = new HtmlToPdf();

            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 800;
            converter.Options.PdfCompressionLevel = PdfCompressionLevel.Best;
            converter.Options.SecurityOptions.CanEditContent = false;
            converter.Options.SecurityOptions.OwnerPassword = "ntiche";
                 

            var pdfDocument = converter.ConvertHtmlString(builder.ToString());
            var bytes = pdfDocument.Save();

            pdfDocument.Close();

            return bytes;
        }
    }
}
