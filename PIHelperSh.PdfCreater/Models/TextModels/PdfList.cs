using PIHelperSh.PdfCreator.Enums;
using PIHelperSh.PdfCreator.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreator.Models.TextModels
{
    /// <summary>
    /// Список элементов в PDF
    /// </summary>
    public class PdfList : IPdfElement
    {
        /// <summary>
        /// Элементы списка (параграфы или иные спсики)
        /// </summary>
        public List<IPdfElement> Content { get; set; } = new();

        /// <summary>
        /// Отступ после списка (по умолчанию - средний)
        /// </summary>
        public PdfMargin MarginAfter { get; set; } = PdfMargin.Medium;
    }
}
