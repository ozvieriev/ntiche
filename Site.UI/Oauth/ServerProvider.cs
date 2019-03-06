﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Site.Data.Entities.Oauth;
using Site.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Site.UI.Oauth
{
    public class ServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => { context.Validated(); });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var emailConfirmationToken = string.Empty;
            var accountId = Guid.Empty;

            var formCollection = await context.Request.ReadFormAsync();
            if (!object.Equals(formCollection, null))
            {
                emailConfirmationToken = formCollection["emailConfirmationToken"];
                Guid.TryParse(formCollection["accountId"], out accountId);
            }
            Account account = null;
            using (var auth = new AuthRepository())
            {
                if (string.IsNullOrEmpty(emailConfirmationToken))
                    account = await auth.AccountGetAsync(context.UserName, context.Password);
                else
                {
                    account = await auth.AccountGetAsync(accountId);

                    if (!object.Equals(account, null))
                    {
                        auth.SetDataProtectorProvider(Startup.DataProtectionProvider);

                        var bytes = Convert.FromBase64String(emailConfirmationToken);
                        emailConfirmationToken = Encoding.UTF8.GetString(bytes);

                        var identityResult = await auth.AccountConfirmEmailAsync(accountId, emailConfirmationToken);

                        if (!identityResult.Succeeded)
                        {
                            context.SetError("invalid_grant", "Email confirmation link is incorrect.");
                            return;
                        }
                    }
                }
            }
            if (object.Equals(account, null))
            {
                context.SetError("invalid_grant", "Your user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var properties = new Dictionary<string, string> { };

            identity.AddClaims(account);
            context.Validated(new AuthenticationTicket(identity, new AuthenticationProperties(properties)));
        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var identity = new ClaimsIdentity(context.Ticket.Identity);
            var properties = context.Ticket.Properties.Dictionary;

            Account account = null;
            var accountId = Guid.Parse(identity.GetUserId());
            using (var auth = new AuthRepository())
                account = await auth.AccountGetAsync(accountId);

            if (object.Equals(account, null))
                return;

            identity.AddClaims(account);
            context.Validated(new AuthenticationTicket(identity, context.Ticket.Properties));
        }
    }
}