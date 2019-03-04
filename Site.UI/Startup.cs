using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Site.UI.Startup))]
namespace Site.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Oauth(app);
            AutoMapper(app);
        }
    }
}