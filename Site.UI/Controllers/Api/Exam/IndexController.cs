using Site.Identity;
using Site.UI.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Site.UI.Controllers.Api
{
    [RoutePrefix("api/exam")]
    public class ExamController : ApiController
    {
        private ITestRepository _test;
        private IAppSettings _appSettings;

        public ExamController(ITestRepository test, IAppSettings appSettings)
        {
            _test = test;
            _appSettings = appSettings;
        }

        [HttpGet, Route(""), Authorize]
        public async Task<IHttpActionResult> Get(string name = "drugs")
        {
            var languageIso2 = Request.Headers.GetLanguageIso2();
            var vExam = await _test.vExamGetByNameAsync(name, languageIso2);

            return Ok(vExam);
        }

        [HttpGet, Route("result"), Authorize]
        public async Task<IHttpActionResult> GetResult(Guid examResultId)
        {
            var languageIso2 = Request.Headers.GetLanguageIso2();
            var vExam = await _test.vExamGetByExamResultIdAsync(examResultId, languageIso2);

            return Ok(vExam);
        }
    }
}
