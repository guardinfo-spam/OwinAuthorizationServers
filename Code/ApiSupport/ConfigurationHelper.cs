using System.Configuration;

namespace ApiSupport
{
    public static class ConfigurationHelper
    {
        public static string GetAppSetting(string key)
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(key))
            {
                if (ConfigurationManager.AppSettings[key] != null)
                {
                    result = ConfigurationManager.AppSettings[key];
                }
            }

            return result;
        }
    }
}
