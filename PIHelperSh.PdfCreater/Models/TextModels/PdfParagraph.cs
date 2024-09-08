using PIHelperSh.PdfCreator.Enums;
using PIHelperSh.PdfCreator.Interfases;
using PIHelperSh.PdfCreator.Models.Properties;

namespace PIHelperSh.PdfCreator.Models.TextModels
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
        /// Выравнивание текста внутри параграфа (по умолчанию - по левой строне)
        /// </summary>
        public PdfAlignmentType ParagraphAlignment { get; set; } = PdfAlignmentType.Left;

        /// <summary>
        /// Отступ после параграфа (по умолчанию средний)
        /// </summary>
        public PdfMargin MarginAfter { get; set; } = PdfMargin.Medium;

        /// <summary>
        /// Свойства гиперссылки (по умолчанию отсутствует)
        /// </summary>
        public HyperlinkProperties Hyperlink { get; set; } = new();
    }
}
