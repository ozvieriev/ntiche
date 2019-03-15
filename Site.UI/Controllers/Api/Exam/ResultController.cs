using Email.Templates;
using Site.Identity;
using Site.UI.Core;
using Site.UI.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Site.UI.Controllers.Api
{
    [RoutePrefix("api/exam")]
    public class ExamResultController : ApiController
    {
        private ITestRepository _test;
        private IAppSettings _appSettings;
        private ILasyEmailSender _lasyEmailSender;

        public ExamResultController(ITestRepository test, IAppSettings appSettings, ILasyEmailSender lasyEmailSender)
        {
            _test = test;
            _appSettings = appSettings;
            _lasyEmailSender = lasyEmailSender;
        }

        [HttpGet, Route("post-test/download")]
        public async Task<HttpResponseMessage> PostTestDownloadGet(Guid examResultId)
        {
            var examResult = await _test.ExamResultGetAsync(examResultId);
            if (object.Equals(examResult, null) || !examResult.ExamResultIsSuccess || !"post-test".Equals(examResult.ExamName))
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            var bytes = CertificateGenerator.LetterOfAttendance(examResult);
            var memoryStream = new MemoryStream(bytes);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(memoryStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = $"{examResult.AccountFirstName} {examResult.AccountLastName}.pdf"
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }

        [HttpPost, Route("post-test/send"), Authorize, ValidateModel, ValidateNullModel]
        public async Task<IHttpActionResult> PostTestSendPost(ExamResultViewModel model)
        {
            var examResult = await _test.ExamResultGetAsync(model.ExamResultId);
            if (object.Equals(examResult, null) || !examResult.ExamResultIsSuccess || !"post-test".Equals(examResult.ExamName))
                return NotFound();

            var bytes = CertificateGenerator.LetterOfAttendance(examResult);
            var attachment = new LasyEmailAttachment($"{examResult.AccountFirstName} {examResult.AccountLastName}.pdf", bytes);

            var lasyEmailViewModel = new LasyEmailViewModel("Congratulations", examResult.AccountEmail, EmailTemplate.PostTestCertificate,
                new Notification(new
                {
                    firstName = examResult.AccountFirstName,
                    lastName = examResult.AccountLastName
                }), attachment);

            _lasyEmailSender.Send(lasyEmailViewModel);

            return Ok(new DescriptionViewModel("An email has been sent to your account."));
        }
    }
}