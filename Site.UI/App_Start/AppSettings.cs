using System;
using System.Configuration;
using System.Globalization;

namespace Site.UI
{
    public interface IAppSettings
    {
        AppSettingsOauth Oauth { get; }
        AppSettingsEmail Email { get; }
    }
    public class AppSettings : IAppSettings
    {
        public AppSettingsOauth Oauth { get; private set; }
        public AppSettingsEmail Email { get; private set; }

        public AppSettings()
        {
            Oauth = new AppSettingsOauth
            {
                EmailConfirmationLink = new Uri(Setting<string>("oauth:emailConfirmationLink")),
                RecoverPasswordLink = new Uri(Setting<string>("oauth:recoverPasswordLink"))
            };
            Email = new AppSettingsEmail
            {
                Admin = Setting<string>("email:admin"),
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
        public Uri EmailConfirmationLink;
        public Uri RecoverPasswordLink;
    }
    public class AppSettingsEmail
    {
        public string Admin { get; set; }
    }
}