using System;
using System.Configuration;

namespace Reflection.attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurationItemAttribute : Attribute
    {
        public string SettingName { get; set; }
        public Type ProviderType { get; set; }
        public string FilePath { get; set; }

        public ConfigurationItemAttribute(string settingName, Type providerType, string filePath = null)
        {
            SettingName = settingName;
            ProviderType = providerType;
            FilePath = filePath;
        }
    }
}