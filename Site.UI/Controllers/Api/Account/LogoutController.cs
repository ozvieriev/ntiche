using Site.Identity;
using System.Web.Http;

namespace Site.UI.Controllers.Api
{
    [RoutePrefix("api/account")]
    public class AccountLogoutController : ApiController
    {
        private IAuthRepository _auth;

        public AccountLogoutController(IAuthRepository auth)
        {
            _auth = auth;
        }
    }
}
