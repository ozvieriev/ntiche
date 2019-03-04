using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Site.UI.Oauth;
using System;

namespace Site.UI
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthServerOptions { get; private set; }
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static IDataProtectionProvider DataProtectionProvider { get; set; }

        public void Oauth(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();

            OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //#if DEBUG
                AllowInsecureHttp = true,
                //#endif

                TokenEndpointPath = new PathString("/api/token"),
                Provider = new ServerProvider(),
                RefreshTokenProvider = new RefreshTokenProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1)
            };

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions { };
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}