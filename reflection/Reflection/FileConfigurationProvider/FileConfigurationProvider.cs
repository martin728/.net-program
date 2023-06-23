using System;
using System.IO;
using System.Linq;

namespace FileConfigurationProvider
{
    public class FileConfigurationProvider : IConfigurationProvider.IConfigurationProvider
    {
        private string _filePath;

        public FileConfigurationProvider(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public void SaveSetting(string settingName, object value)
        {
            string[] lines = File.ReadAllLines(_filePath);
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(settingName + "="))
                {
                    lines[i] = $"{settingName}={value}";
                    break;
                }
            }
            File.WriteAllLines(_filePath, lines);
        }

        public object GetSetting(string settingName, Type type)
        {
            if (!File.Exists(_filePath))
            {
                return type.IsValueType ? Activator.CreateInstance(type) : null;
            }

            string[] lines = File.ReadAllLines(_filePath);
            string line = lines.FirstOrDefault(l => l.StartsWith(settingName + "=", StringComparison.OrdinalIgnoreCase));
            if (line != null)
            {
                string[] parts = line.Split('=');
                if (parts.Length == 2 && parts[0].Equals(settingName, StringComparison.OrdinalIgnoreCase))
                {
                    string value = parts[1];
                    return Convert.ChangeType(value, type);
                }
            }

            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }

}