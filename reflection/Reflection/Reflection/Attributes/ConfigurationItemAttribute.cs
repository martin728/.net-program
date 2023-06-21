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
        public string AssemblyName { get; set; }

        public ConfigurationItemAttribute(string settingName, Type providerType,string assemblyName, string filePath = null)
        {
            SettingName = settingName;
            ProviderType = providerType;
            AssemblyName = assemblyName;
            FilePath = filePath;
        }
    }
}