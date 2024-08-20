using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.Configuration.Attributes
{
    /// <summary>
    /// Атрибут конфигурации инициализирующийся при запуске AddConfigurations
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoConfigurationAttribute : Attribute
    {
    }
}
