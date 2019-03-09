using Newtonsoft.Json.Serialization;
using Site.UI.Handlers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Site.UI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Replace(typeof(IExceptionHandler), new OopsExceptionHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Add(new LanguageHandler());
            config.MessageHandlers.Add(new CancelHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}
