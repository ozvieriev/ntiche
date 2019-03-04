using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Site.Data.Entities.Oauth;
using Site.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site.UI.Oauth
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        private static ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get { return Startup.OAuthBearerOptions.AccessTokenFormat; } }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var issued = DateTime.UtcNow;
            var expires = issued.Add(Startup.OAuthServerOptions.AccessTokenExpireTimeSpan);

            context.Ticket.Properties.IssuedUtc = issued;
            context.Ticket.Properties.ExpiresUtc = expires;

            var accountIdClaim = context.Ticket.Identity.FindFirst(ClaimTypes.NameIdentifier);

            var sessionToken = new SessionToken
            {
                Id = Guid.NewGuid(),
                IssuedUtc = issued,
                ExpiresUtc = expires,
                AccountId = Guid.Parse(accountIdClaim.Value),
                ProtectedTicket = AccessTokenFormat.Protect(context.Ticket)
            };

            using (var auth = new AuthRepository())
                await auth.SessionTokenInsertAsync(sessionToken);

            context.SetToken(sessionToken.Id.ToString("N"));
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            Guid id;
            if (!Guid.TryParseExact(context.Token, "N", out id))
                return;

            SessionToken token = null;
            using (var auth = new AuthRepository())
                token = await auth.SessionTokenGetAsync(id);

            if (!object.Equals(token, null))
            {
                var ticket = AccessTokenFormat.Unprotect(token.ProtectedTicket);

                var issued = DateTime.UtcNow;
                var expires = issued.Add(Startup.OAuthServerOptions.AccessTokenExpireTimeSpan);

                ticket.Properties.IssuedUtc = issued;
                ticket.Properties.ExpiresUtc = expires;

                context.SetTicket(ticket);
            }
            else
            {
                context.Response.Headers.Add("Refresh-Token-Expired", new[] { "token: " + (context.Token ?? string.Empty) });
                context.Response.Headers.Add("Access-Control-Expose-Headers", new[] { "Refresh-Token-Expired" });
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}