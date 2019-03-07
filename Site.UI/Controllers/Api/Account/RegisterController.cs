using AutoMapper;
using Email.Templates;
using Site.Data.Entities.Oauth;
using Site.Identity;
using Site.UI.Core;
using Site.UI.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Site.UI.Controllers.Api
{
    [RoutePrefix("api/account")]
    public class RegisterController : ApiController
    {
        private IAuthRepository _auth;
        private IAppSettings _appSettings;

        public RegisterController(IAuthRepository auth, IAppSettings appSettings)
        {
            _auth = auth;
            _auth.SetDataProtectorProvider(Startup.DataProtectionProvider);

            _appSettings = appSettings;
        }

        [HttpPost, Route("register"), ValidateNullModel, ValidateModel]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            var account = new Account();
            account = Mapper.Map(model, account);

            var identityResult = await _auth.AccountCreateAsync(account, model.Password);

            Exception identityResultException = null;
            if (!identityResult.Validate(ref identityResultException))
                return Request.HttpExceptionResult(identityResultException);

            var emailConfirmationLink = await _auth.GenerateEmailConfirmationTokenLinkAsync(account, _appSettings.Oauth.EmailConfirmationLink);

            Sender.Send("Email confirmation", model.Email, EmailTemplate.EmailConfirmation, new Notification(new
            {
                model.FirstName,
                model.LastName,
                model.UserName,
                emailConfirmationLink
            }));

            return Ok(new DescriptionViewModel("Please click on the link has just been sent to your email account to verify your email and continue the registration process."));
        }
    }
}
