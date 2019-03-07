using Site.Identity;
using System.Web.Http;

namespace Site.UI.Controllers.Api
{
    [RoutePrefix("api/account")]
    public class LogoutController : ApiController
    {
        private IAuthRepository _auth;

        public LogoutController(IAuthRepository auth)
        {
            _auth = auth;
        }
    }
}
