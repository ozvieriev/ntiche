using Email.Templates;
using NLog;
using Site.Identity;
using Site.UI.Core;
using Site.UI.Models;
using System;
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
            {
                var response = Request.CreateExceptionResponse(
                    error_description: "The account with this email does not exist.",
                    statusCode: System.Net.HttpStatusCode.NotFound);

                return ResponseMessage(response);
            }

            var recoverPasswordLink = await _auth.GenerateRecoverPasswordTokenLinkAsync(account, _appSettings.Oauth.RecoverPasswordLink);

            Sender.Send("Recover password", account.Email, EmailTemplate.RecoverPassword, new Notification(new
            {
                account.FirstName,
                account.LastName,
                account.UserName,
                recoverPasswordLink
            }));

            return Ok(new
            {
                email,
                description = "An email has been sent to your account to reset your password."
            });
        }

        [HttpPost, Route("reset-password"), ValidateNullModel, ValidateModel]
        public async Task<IHttpActionResult> RecoverPassword(ResetPasswordViewModel model)
        {
            var account = await _auth.AccountGetAsync(model.AccountId);

            if (object.Equals(account, null))
            {
                var response = Request.CreateExceptionResponse(
                    error_description: "The account with this id does not exist.",
                    statusCode: System.Net.HttpStatusCode.NotFound);

                return ResponseMessage(response);
            }

            var identityResult = await _auth.AccountResetPasswordAsync(model.AccountId, model.ResetPasswordToken, model.Password);

            Exception identityResultException = null;
            if (!identityResult.Validate(ref identityResultException))
                return Request.HttpExceptionResult(identityResultException);

            await _auth.AccountActivateAsync(model.AccountId);

            return Ok(new
            {
                description = "Your password has been successfully changed. You can use the new password to sign in."
            });
        }
    }
}
