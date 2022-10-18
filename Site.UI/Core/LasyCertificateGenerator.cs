using Certificate.Templates;
using NLog;
using Site.Data.Entities.Test;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Site.UI.Core
{
    public interface ILasyCertificateGenerator
    {
        void Save(LasyCertificateGeneratorViewModel model);
        FileInfo GetCertificate(LasyCertificateGeneratorViewModel model);
    }

    public class LasyCertificateGenerator : ILasyCertificateGenerator
    {
        private static ConcurrentQueue<LasyCertificateGeneratorViewModel> _lasyExamResults;
        private static Logger _oauthLoggerExam = LogManager.GetLogger("oauth-logger-exam");

        static LasyCertificateGenerator()
        {
            _lasyExamResults = new ConcurrentQueue<LasyCertificateGeneratorViewModel>();

            var cancelationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        LasyCertificateGeneratorViewModel model;
                        if (_lasyExamResults.TryDequeue(out model))
                            GenerateCertificate(model);
                    }

                    catch (Exception exc) { _oauthLoggerExam.Error(exc); }
                    Task.Delay(TimeSpan.FromSeconds(5), cancelationTokenSource.Token).Wait();
                }

            }, cancelationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Save(LasyCertificateGeneratorViewModel model)
        {
            _lasyExamResults.Enqueue(model);
        }

        public FileInfo GetCertificate(LasyCertificateGeneratorViewModel model)
        {
            var examResult = model.ExamResult;
            var filePath = GetFilePath(examResult);
            var fileInfo = new FileInfo(filePath);

            if (fileInfo.Exists)
                return fileInfo;

            return GenerateCertificate(model);
        }

        private static FileInfo GenerateCertificate(LasyCertificateGeneratorViewModel model)
        {
            var examResult = model.ExamResult;
            var notification = new Notification(new
            {
                accountFullName = $"{examResult.AccountFirstName} {examResult.AccountLastName}",
                accountPharmacistLicense = examResult.AccountPharmacistLicense,
                examResultCreateDate = examResult.ExamResultCreateDate.ToString("MM-dd-yyyy")
            });
            var filePath = GetFilePath(examResult);
            PdfCertificate.Save(filePath, CertificateTemplate.LetterOfAttendance, notification);

            return new FileInfo(filePath);
        }
        private static string GetFilePath(vExamResult examResult)
        {
            var id = examResult.Id.ToString("N");
            var folder = id.Substring(0, 2);

            return HostingEnvironment.MapPath($"~/pdf/{examResult.ExamName}/{folder}/{id}-{Thread.CurrentThread.CurrentUICulture.Name}.pdf");
        }
    }

    public class LasyCertificateGeneratorViewModel
    {
        public LasyCertificateGeneratorViewModel(vExamResult examResult, CertificateTemplate certificateTemplate)
        {
            ExamResult = examResult;
            CertificateTemplate = certificateTemplate;
        }

        public vExamResult ExamResult { get; private set; }
        public CertificateTemplate CertificateTemplate { get; private set; }
    }
}