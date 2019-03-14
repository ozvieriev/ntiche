using Certificate.Templates.CertificateTemplates.LetterOfAttendance;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Certificate.Templates
{
    public static class PdfCertificate
    {
        private static readonly string LinkCssStyle = "<link href='css/style.css' rel='stylesheet' />";
        private static readonly string CssStyle = $"<style type'text/css'>{Resources.style}</style>";

        private static readonly Dictionary<string, string> _htmlTemplates;

        static PdfCertificate()
        {
            _htmlTemplates = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            _htmlTemplates.Add("en", Resources.en);
            _htmlTemplates.Add("fr", Resources.fr);
        }

        public static byte[] CreatePdf(CertificateTemplate certificateTemplate, Notification notification, string languageIso2 = "en")
        {
            var htmlTemplate = _htmlTemplates.ContainsKey(languageIso2) ?
                _htmlTemplates[languageIso2] :
                _htmlTemplates.First().Value;

            var builder = new StringBuilder(htmlTemplate);
            builder.Replace(LinkCssStyle, CssStyle);

            var properties = notification.GetProperties();
            foreach (var property in properties)
                builder.Replace($"<{property.Name}>", notification[property.Name]);

            var converter = new HtmlToPdf();

            converter.Options.PdfPageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize), "A4", true);
            converter.Options.PdfPageOrientation = (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation), "Portrait", true);
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;
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
