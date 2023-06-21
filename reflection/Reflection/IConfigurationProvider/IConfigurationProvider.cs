using System;

namespace IConfigurationProvider
{
    public interface IConfigurationProvider
    {
        void SaveSetting(string settingName, object value);
        object GetSetting(string settingName, Type type);
    }
}