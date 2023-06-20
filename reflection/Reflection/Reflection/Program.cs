using System;  
using System.Configuration;
using Reflection.classes;

namespace Reflection
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      var component = new ConfigurationComponent();
      component.LoadSettings();

      Console.WriteLine("Loaded settings:");
      Console.WriteLine($"MyFloatSetting:{component.MyFloatSetting}");
      Console.WriteLine($"MyStringSetting:{component.MyIntSetting}");
      Console.WriteLine($"MyIntSetting:{component.MyStringSetting}");
      Console.WriteLine($"MyTimeSpanSetting:{component.MyTimeSpanSetting}");

      component.MyIntSetting = 42;
      component.MyFloatSetting = 3.14f;
      component.MyStringSetting = "Hello World";
      component.MyTimeSpanSetting = TimeSpan.FromMinutes(0);

      component.SaveSettings();
      
      Console.WriteLine("Settings saved");
      Console.ReadLine();
    }
  }
}