using PIHelperSh.PdfCreator.Enums;
using PIHelperSh.PdfCreator.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreator.Models.ImageModels
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
        /// Выравнивание текста внутри параграфа (по умолчанию - по левой строне)
        /// </summary>
        public PdfAlignmentType ImageAlignment { get; set; } = PdfAlignmentType.Left;

        /// <summary>
        /// Отступ после параграфа (по умолчанию средний)
        /// </summary>
        public PdfMargin MarginAfter { get; set; } = PdfMargin.Medium;
    }
}
