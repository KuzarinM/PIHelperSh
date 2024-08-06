using PIHelperSh.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.Core.Extentions
{
    public static class ConfigurationExtention
    {
        public static void LoadConfiguration()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var property in type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                    {
                        try
                        {
                            var attr = property.GetCustomAttribute<FromConfigAttribute>();

                            if (attr != null)
                            {
                                var targetType = property.PropertyType;
                                string? value = null;
                                if (value == null && attr.EnvirmentValueName != null)
                                {
                                    value = Environment.GetEnvironmentVariable(attr.EnvirmentValueName);
                                }

                                if (value == null && attr.ConfigValueName != null)
                                {
                                    value = "234";
                                }

                                if (value != null)
                                {
                                    TypeConverter typeConverter = TypeDescriptor.GetConverter(targetType);
                                    property.SetValue(null, typeConverter.ConvertFrom(value));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            int a = 0;
                        }
                    }
                }
            }
        }
    }
}
