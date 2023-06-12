using System;
using System.Configuration;
using System.Reflection;
using Reflection.attributes;

namespace Reflection.classes
{
    public class ConfigurationComponentBase
    {
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

                if (attribute.ProviderType != null && typeof(IConfigurationProvider).IsAssignableFrom(attribute.ProviderType))
                {
                    try
                    {
                        IConfigurationProvider provider = (IConfigurationProvider)Activator.CreateInstance(attribute.ProviderType);
                        provider?.SaveSetting(attribute.SettingName, value);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error creating instance of {attribute.ProviderType}: {e.Message}");
                    }
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

                IConfigurationProvider provider = null;

                try
                {
                    if (attribute.ProviderType == typeof(FileConfigurationProvider))
                    {
                        provider = new FileConfigurationProvider(attribute.FilePath);
                    }
                    else if (attribute.ProviderType == typeof(ConfigurationManagerConfigurationProvider))
                    {
                        provider = new ConfigurationManagerConfigurationProvider();
                    }
                    else
                    {
                        Console.WriteLine($"Unsupported provider type: {attribute.ProviderType}");
                        continue;
                    }

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