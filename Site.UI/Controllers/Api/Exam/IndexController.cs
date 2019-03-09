using Microsoft.AspNet.Identity;
using Site.Identity;
using Site.UI.Helpers;
using Site.UI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2;

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

        [HttpGet, Route(""), Authorize, CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        public async Task<IHttpActionResult> Get(string languageIso2, string name = "drugs")
        {
            var vExam = await _test.vExamGetByNameAsync(name, languageIso2);

            return Ok(vExam);
        }

        [HttpPost, Route(""), Authorize]
        public async Task<IHttpActionResult> PostResult([FromBody] IEnumerable<Guid> answers)
        {
            var accountId = Guid.Parse(User.Identity.GetUserId());
            await _test.ExamResultInsertAsync(accountId, answers);

            return Ok(new DescriptionViewModel("Thank you for your answers. Your test results have been saved."));
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