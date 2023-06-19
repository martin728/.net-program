using System;
using Reflection.attributes;

namespace Reflection.classes
{
    public class ConfigurationComponent : ConfigurationComponentBase
    {
        [ConfigurationItem("MyIntSetting", typeof(FileConfigurationProvider),"../../../Reflection/config.txt")]
        public int MyIntSetting { get; set; }
        
        [ConfigurationItem("MyFloatSetting", typeof(ConfigurationManagerConfigurationProvider))]
        public float MyFloatSetting { get; set; }
        
        [ConfigurationItem("MyStringSetting", typeof(FileConfigurationProvider),"./../../Reflection/config.txt")]
        public string MyStringSetting { get; set; }
        
        [ConfigurationItem("MyTimeSpanSetting", typeof(ConfigurationManagerConfigurationProvider))]
        public TimeSpan MyTimeSpanSetting { get; set; }
    }
}