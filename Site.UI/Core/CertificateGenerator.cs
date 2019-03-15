using Certificate.Templates;
using Site.Data.Entities.Test;

namespace Site.UI.Core
{
    public static class CertificateGenerator
    {
        public static byte[] LetterOfAttendance(vExamResult examResult)
        {
            var notification = new Notification(new
            {
                accountFullName = $"{examResult.AccountFirstName} {examResult.AccountLastName}",
                accountPharmacistLicense = examResult.AccountPharmacistLicense,
                examResultCreateDate = examResult.ExamResultCreateDate.ToString("MM-dd-yyyy")
            });
            return PdfCertificate.CreatePdf(CertificateTemplate.LetterOfAttendance, notification);
        }
    }
}