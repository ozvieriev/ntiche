using System;
using System.Configuration;
using System.Globalization;

namespace Site.UI
{
    public interface IAppSettings
    {
        AppSettingsOauth Oauth { get; }
    }
    public class AppSettings : IAppSettings
    {
        public AppSettingsOauth Oauth { get; private set; }

        public AppSettings()
        {
            Oauth = new AppSettingsOauth
            {
                EmailConfirmation = new Uri(Setting<string>("oauth:emailConfirmationLink"))
            };
        }

        private static T Setting<T>(string name)
        {
            string value = ConfigurationManager.AppSettings[name];

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }



    public class AppSettingsOauth
    {
        public Uri EmailConfirmation;
    }
}