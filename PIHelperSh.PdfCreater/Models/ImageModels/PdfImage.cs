using PIHelperSh.PdfCreater.Enums;
using PIHelperSh.PdfCreater.Interfases;

namespace PIHelperSh.PdfCreater.Models.ImageModels
{
	/// <summary>
	/// Информация о изображении
	/// </summary>
	public class PdfImage : IPdfElement
    {
        /// <summary>
        /// Путь до изображения
        /// </summary>
        public MigradocImage Image { get; set; }

        /// <summary>
        /// Ширина изображения
        /// </summary>
        public int? Width { get; set; } = null;

        /// <summary>
        /// Высота изображения
        /// </summary>
        public int? Height { get; set; } = null;

        /// <summary>
        /// Выравнивание текста внутри параграфа (по умолчанию - по левой стороне)
        /// </summary>
        public PdfAlignmentType ImageAlignment { get; set; } = PdfAlignmentType.Left;

        /// <summary>
        /// Отступ после параграфа (по умолчанию средний)
        /// </summary>
        public PdfMargin MarginAfter { get; set; } = PdfMargin.Medium;
    }
}
