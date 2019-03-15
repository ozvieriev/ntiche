using Site.Identity;
using Site.UI.Core;
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
    public class ExamDownloadController : ApiController
    {
        private ITestRepository _test;
        private IAppSettings _appSettings;

        public ExamDownloadController(ITestRepository test, IAppSettings appSettings)
        {
            _test = test;
            _appSettings = appSettings;
        }

        [HttpGet, Route("post-test/download")]
        public async Task<HttpResponseMessage> PostTestDownloadGet(Guid examResultId)
        {
            var examResult = await _test.ExamResultGetAsync(examResultId);
            if (object.Equals(examResult, null) || !examResult.ExamResultIsSuccess)
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
    }
}