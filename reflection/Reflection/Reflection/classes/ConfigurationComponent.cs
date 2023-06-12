using System;
using Reflection.attributes;

namespace Reflection.classes
{
    public class ConfigurationComponent : ConfigurationComponentBase
    {
        [ConfigurationItem("MyIntSetting",typeof(FileConfigurationProvider),"../../../../config.txt")]
        public int MyIntSetting { get; set; }
        
        [ConfigurationItem("MyFloatSetting",typeof(ConfigurationManagerConfigurationProvider),"config.txt")]
        public float MyFloatSetting { get; set; }
        
        [ConfigurationItem("MyStringSetting",typeof(FileConfigurationProvider),"config.txt")]
        public string MyStringSetting { get; set; }
        
        [ConfigurationItem("MyTimeSpanSetting",typeof(ConfigurationManagerConfigurationProvider),"config.txt")]
        public TimeSpan MyTimeSpanSetting { get; set; }
    }
}