using PIHelperSh.PdfCreator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreator.Models.TableModels
{
    /// <summary>
    /// Строка простой таблицы
    /// </summary>
    public class PDFSimpleTableRow
    {
        /// <summary>
        /// Элемменты данной стоки
        /// </summary>
        public List<string> Items = new List<string>();

        /// <summary>
        /// Стиль элементов строки, если он отличается от стиля строк, определённого в таблице
        /// </summary>
        public PdfStyleType? Style = null;

        /// <summary>
        /// Выравнивание элементов строки, если оно отличается от выравнивания,  определённого в таблице
        /// </summary>
        public PdfAlignmentType? Alignment = null;
    }
}
