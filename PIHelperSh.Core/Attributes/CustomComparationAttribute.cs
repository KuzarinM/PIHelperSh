using PIHelperSh.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.Core.Attributes
{
    /// <summary>
    /// Атрибут для работы кастомного компаратора
    /// </summary>
    public class CustomComparationAttribute:Attribute
    {
        /// <summary>
        /// Режим сравнения
        /// </summary>
        public CustiomComparationMode Mode { get; set; } = CustiomComparationMode.Default;
    }
}
