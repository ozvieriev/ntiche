using Email.Templates;
using Site.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace Site.UI.Controllers.Api
{
    [RoutePrefix("api/account")]
    public class RecoverPasswordController : ApiController
    {
        private IAuthRepository _auth;
        private IAppSettings _appSettings;

        public RecoverPasswordController(IAuthRepository auth, IAppSettings appSettings)
        {
            _auth = auth;
            _auth.SetDataProtectorProvider(Startup.DataProtectionProvider);

            _appSettings = appSettings;
        }

        [HttpGet, Route("recover-password")]
        public async Task<IHttpActionResult> RecoverPassword([FromUri] string email)
        {
            var account = await _auth.AccountGetByEmailAsync(email);

            if (object.Equals(account, null))
                return NotFound();

            var recoverPasswordLink = await _auth.GenerateRecoverPasswordTokenLinkAsync(account, _appSettings.Oauth.RecoverPasswordLink);

            Sender.Send(account.Email, EmailTemplate.RecoverPassword, new Notification(new
            {
                account.FirstName,
                account.LastName,
                recoverPasswordLink
            }));

            return Ok(new { email });
        }
    }
}
