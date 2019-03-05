using Site.Data.Entities.Oauth;
using Site.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace Site.UI.Controllers.Api
{
    [RoutePrefix("api/account")]
    public class IndexController : ApiController
    {
        private IAuthRepository _auth;

        public IndexController(IAuthRepository auth)
        {
            _auth = auth;
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get(string email = null, string userName = null)
        {
            Account account = null;
            if (!string.IsNullOrEmpty(userName))
            {
                account = await _auth.AccountGetAsync(userName);

                if (!object.Equals(account, null))
                    return Ok(new { account.UserName });
            }

            if (!string.IsNullOrEmpty(email))
            {
                account = await _auth.AccountGetByEmailAsync(email);

                if (!object.Equals(account, null))
                    return Ok(new { account.Email });
            }

            return NotFound();
        }
    }
}
