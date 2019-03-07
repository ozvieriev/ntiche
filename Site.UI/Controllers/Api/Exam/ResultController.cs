using Microsoft.AspNet.Identity;
using Site.Identity;
using Site.UI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Site.UI.Controllers.Api
{
    [RoutePrefix("api/exam")]
    public class ExamResultController : ApiController
    {
        private ITestRepository _test;
        private IAppSettings _appSettings;

        public ExamResultController(ITestRepository test, IAppSettings appSettings)
        {
            _test = test;
            _appSettings = appSettings;
        }

        [HttpPost, Route("result"), Authorize]
        public async Task<IHttpActionResult> PostResult([FromBody] IEnumerable<Guid> answers)
        {
            var accountId = Guid.Parse(User.Identity.GetUserId());
            await _test.ExamResultInsertAsync(accountId, answers);

            return Ok(new DescriptionViewModel("Your test results have been saved."));
        }
    }
}
