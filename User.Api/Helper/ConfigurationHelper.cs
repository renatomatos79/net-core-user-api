using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Helper
{
    public static class ConfigurationHelper
    {
        public static string GetStringValue(this IConfiguration configuration, string key)
        {
            return configuration.GetValue(typeof(string), key)?.ToString() ?? string.Empty;
        }

        public static string GetConnectionStringValue(this IConfiguration configuration, string key)
        {
            return configuration.GetConnectionString(key);
        }

        public static AppSettings GetAppSettings(this IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            return appSettingsSection.Get<AppSettings>();
        }
    }
}
