namespace System.Utilities.Common
{
    using System;
    using System.Configuration;

    public class ConfigHelper
    {
        public static string GetAppSettingsValue(string key)
        {
            ConfigurationManager.RefreshSection("appSettings");
            return (ConfigurationManager.AppSettings[key] ?? string.Empty);
        }

        public static bool UpdateAppSettings(string key, string value)
        {
            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!configuration.HasFile)
            {
                throw new ArgumentException("程序配置文件缺失！");
            }
            if (configuration.AppSettings.Settings[key] == null)
            {
                configuration.AppSettings.Settings.Add(key, value);
            }
            else
            {
                configuration.AppSettings.Settings[key].Value = value;
            }
            configuration.Save(ConfigurationSaveMode.Modified);
            return true;
        }
    }
}

