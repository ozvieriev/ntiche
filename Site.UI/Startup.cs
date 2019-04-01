using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Site.UI.Startup))]
namespace Site.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var appData = HttpContext.Current.Server.MapPath("~/App_Data");
            //AppDomain.CurrentDomain.SetData("App_Data", appData);

            Oauth(app);
            AutoMapper(app);
        }
    }
}