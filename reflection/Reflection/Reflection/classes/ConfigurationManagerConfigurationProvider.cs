using System;
using System.Configuration;

namespace Reflection.classes
{
    public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
        public void SaveSetting(string settingName, object value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationElement setting = config.AppSettings.Settings[settingName];
                if (setting != null)
                {
                    setting.Value = value.ToString();
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
                else
                {
                    Console.WriteLine($"Setting '{settingName}' not found in configuration file.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error saving setting: {e.Message}");
            }
        }

        public object GetSetting(string settingName, Type type)
        {
            try
            {
                string value = ConfigurationManager.AppSettings[settingName];
                
                type == typeof(TimeSpan) ? TimeSpan.Parse(value) : Convert.ChangeType(value, type);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error retrieving setting: {e.Message}");
                return type.IsValueType ? Activator.CreateInstance(type) : null;
            }
        }
    }

}