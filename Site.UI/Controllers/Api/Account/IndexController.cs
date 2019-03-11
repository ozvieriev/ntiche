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
        public async Task<IHttpActionResult> Get(string email = null)
        {
            var account = await _auth.AccountGetByEmailAsync(email);

            if (!object.Equals(account, null))
                return Ok(new { account.Email });

            return NotFound();
        }
    }
}
