using PIHelperSh.PdfCreator.Enums;
using PIHelperSh.PdfCreator.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreator.Models.TableModels
{
    /// <summary>
    /// Простая табличка в PDF
    /// </summary>
    public class PDFSimpleTable
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public List<IPdfColumnItem>? Header { get; set; } = null;

        /// <summary>
        /// Стиль заголовка (по умолчанию - жирный)
        /// </summary>
        public PdfStyleType HeaderStyle { get; set; } = PdfStyleType.Bold;

        /// <summary>
        /// Выравнивание текста внутри заголовка (по умолчанию - по центру)
        /// </summary>
        public PdfAlignmentType HeaderHorisontalAlignment { get; set; } = PdfAlignmentType.Center;

        /// <summary>
        /// Набор строк
        /// </summary>
        public List<PDFSimpleTableRow> Rows { get; set; }

        /// <summary>
        /// Базовый стиль строк
        /// </summary>
        public PdfStyleType RowStyle = PdfStyleType.Basic;

        /// <summary>
        /// Базовое выравнивание элементов сторок
        /// </summary>
        public PdfAlignmentType RowHorisontalAlignment = PdfAlignmentType.Right;

        /// <summary>
        /// Отступ после таблицы
        /// </summary>
        public PdfMargin MarginAfter = PdfMargin.None;

        /// <summary>
        /// Меняем ли ориентацию страницы на альбомную
        /// </summary>
        public bool ChangePageOrientation = false;
    }
}
