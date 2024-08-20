using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.Configuration.Attributes
{
    /// <summary>
    /// Атрибут конфигурации из Environment Variables
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FromEnvironmentAttribute: Attribute
    {
        /// <summary>
        /// Имя переменной окружения
        /// </summary>
        public string VariableName { get; set; } = string.Empty;
    }
}
