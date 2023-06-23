using System;
using System.Configuration;
using System.Reflection;
using Reflection.attributes;

namespace Reflection.classes
{
    public class ConfigurationComponentBase
    {
        private IConfigurationProvider.IConfigurationProvider CreateProvider(ConfigurationItemAttribute configAttribute)
        {
            IConfigurationProvider.IConfigurationProvider provider = null;
            
            var assembly = Assembly.Load(configAttribute.AssemblyName);
            var type = assembly.GetType(configAttribute.ProviderType.FullName);
            
            try
            {
                if (typeof(FileConfigurationProvider.FileConfigurationProvider) == type)
                {
                    provider = new FileConfigurationProvider.FileConfigurationProvider(configAttribute.FilePath);
                }
                else if (typeof(ConfigurationManagerConfigurationProvider.ConfigurationManagerConfigurationProvider) == type)
                {
                    provider = new ConfigurationManagerConfigurationProvider.ConfigurationManagerConfigurationProvider();
                    
                }
                else
                {
                    Console.WriteLine($"Unsupported provider type: {configAttribute.ProviderType}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error creating instance of {configAttribute.ProviderType}: {e.Message}");
            }

            return provider;
        }

        public void SaveSettings()
        {
            Type type = GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                ConfigurationItemAttribute attribute = property.GetCustomAttribute<ConfigurationItemAttribute>();
                if (attribute == null)
                    continue;

                object value = property.GetValue(this);

                if (attribute.ProviderType != null && typeof(IConfigurationProvider.IConfigurationProvider).IsAssignableFrom(attribute.ProviderType))
                {
                    IConfigurationProvider.IConfigurationProvider provider = CreateProvider(attribute);
                    provider?.SaveSetting(attribute.SettingName, value);
                }
                else
                {
                    Console.WriteLine($"Invalid provider type: {attribute.ProviderType}");
                }
            }
        }

        public void LoadSettings()
        {
            Type type = GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                ConfigurationItemAttribute attribute = property.GetCustomAttribute<ConfigurationItemAttribute>();
                if (attribute == null)
                    continue;

                IConfigurationProvider.IConfigurationProvider provider = CreateProvider(attribute);

                if (provider != null)
                {
                    try
                    {
                        object value = provider.GetSetting(attribute.SettingName, property.PropertyType);

                        if (value != null)
                        {
                            property.SetValue(this, value);
                        }
                        else if (property.PropertyType.IsValueType)
                        {
                            property.SetValue(this, Activator.CreateInstance(property.PropertyType));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error creating instance of {attribute.ProviderType}: {e.Message}");
                    }
                }
            }
        }


    }
    
}