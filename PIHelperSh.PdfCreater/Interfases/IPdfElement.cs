using PIHelperSh.PdfCreater.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreater.Interfases
{
    /// <summary>
    /// Аналогично word этот интерфейс нужен для работы списков. 
    /// </summary>
    public interface IPdfElement
    {
        /// <summary>
        /// //Отступ после(может быть и у списков и у параграфов)
        /// </summary>
        PdfMargin MarginAfter { get; }
    }
}
