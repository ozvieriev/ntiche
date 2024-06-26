﻿using AutoMapper;
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
    public class AccountRegisterController : ApiController
    {
        private IAuthRepository _auth;
        private IAppSettings _appSettings;
        private ILasyEmailSender _lasyEmailSender;

        public AccountRegisterController(IAuthRepository auth, IAppSettings appSettings, ILasyEmailSender lasyEmailSender)
        {
            _auth = auth;
            _auth.SetDataProtectorProvider(Startup.DataProtectionProvider);

            _appSettings = appSettings;

            _lasyEmailSender = lasyEmailSender;
        }

        [HttpPost, Route("register"), ValidateNullModel, ValidateModel]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            var account = new Account();
            account = Mapper.Map(model, account);

            var identityResult = await _auth.AccountCreateAsync(account, model.Password);

            Exception identityResultException = null;
            if (!identityResult.Validate(ref identityResultException))
                return Request.HttpErrorResult(identityResultException.Message);

            var emailConfirmationLink = await _auth.GenerateEmailConfirmationTokenLinkAsync(account, _appSettings.Oauth.EmailConfirmationLink);
            var lasyEmailViewModel = new LasyEmailViewModel("Email confirmation", model.Email, EmailTemplate.EmailConfirmation, new Notification(new
            {
                model.FirstName,
                model.LastName,
                emailConfirmationLink
            }));

            _lasyEmailSender.Send(lasyEmailViewModel);

            return Ok(new DescriptionViewModel("Please click on the link that has just been sent to your email account to verify your email and continue the registration process."));
        }
    }
}
