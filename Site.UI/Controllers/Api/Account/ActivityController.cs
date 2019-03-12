using Microsoft.AspNet.Identity;
using Site.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Site.UI.Controllers.Api
{
    [RoutePrefix("api/account")]
    public class AccountActivityController : ApiController
    {
        private ITestRepository _test;

        public AccountActivityController(ITestRepository test)
        {
            _test = test;
        }

        [HttpGet, Route("activity")]
        public async Task<IHttpActionResult> Activity()
        {
            var accountId = Guid.Parse(User.Identity.GetUserId());
            var accoutnActivity = await _test.AccountActivityGetAsync(accountId);

            return Ok(accoutnActivity);
        }
    }
}
