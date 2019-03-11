using Microsoft.AspNet.Identity;
using Site.Identity;
using Site.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IHttpActionResult> Get(string name, string languageIso2)
        {
            var vExam = await _test.vExamGetByNameAsync(name, languageIso2);

            return Ok(vExam);
        }

        [HttpGet, Route("results"), Authorize]
        public async Task<IHttpActionResult> GetResult(string name = null)
        {
            var accountId = Guid.Parse(User.Identity.GetUserId());
            var vExamResults = await _test.vExamResultByAccountIdAsync(accountId, name: name);

            return Ok(vExamResults);
        }
        [HttpPost, Route("{name:regex(pre-test|post-test)}"), Authorize]
        public async Task<IHttpActionResult> PostResult(string name, [FromBody] IEnumerable<Guid> answers)
        {
            var accountId = Guid.Parse(User.Identity.GetUserId());
            await _test.ExamResultInsertAsync(name, accountId, answers);

            var vExamResults = await _test.vExamResultByAccountIdAsync(accountId, name: name);
            var vExamResult = vExamResults
                    .OrderByDescending(entity => entity.CreateDateUtc)
                    .First();

            return Ok(new ExamPostViewModel("Thank you for your answers. Your test results have been saved.",
                vExamResult.IsSuccess));
        }

        //[HttpGet, Route("result"), Authorize]
        //public async Task<IHttpActionResult> GetResult(Guid examResultId)
        //{
        //    var languageIso2 = Request.Headers.GetLanguageIso2();
        //    var vExam = await _test.vExamGetByExamResultIdAsync(examResultId, languageIso2);

        //    return Ok(vExam);
        //}
    }
}