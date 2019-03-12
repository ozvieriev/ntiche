using Site.Identity;
using Site.UI.Core;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Site.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IAuthRepository, AuthRepository>();
            container.RegisterType<ITestRepository, TestRepository>();
            container.RegisterType<IAppSettings, AppSettings>();
            container.RegisterType<ILasyEmailSender, LasyEmailSender>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}