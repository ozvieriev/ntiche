using Site.Identity;
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
            container.RegisterType<IAppSettings, AppSettings>(); 

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}