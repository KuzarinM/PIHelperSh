using PIHelperSh.PdfCreater.Enums;
using PIHelperSh.PdfCreater.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreater.Models.TextModels
{
    /// <summary>
    /// Параграф в PDF
    /// </summary>
    public class PdfParagraph : IPdfElement
    {
        /// <summary>
        /// Текст параграфа
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Стиль параграфа (по умолчанию - базовый)
        /// </summary>
        public PdfStyleType Style { get; set; } = PdfStyleType.Basic;

        /// <summary>
        /// Выравнивание текста внутри параграфа (по умолчанию - по левой стороне)
        /// </summary>
        public PdfAlignmentType ParagraphAlignment { get; set; } = PdfAlignmentType.Left;

        /// <summary>
        /// Отступ после параграфа (по умолчанию средний)
        /// </summary>
        public PdfMargin MarginAfter { get; set; } = PdfMargin.Medium;
    }
}
