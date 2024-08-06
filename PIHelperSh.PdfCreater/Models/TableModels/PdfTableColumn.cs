using PIHelperSh.PdfCreator.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreator.Models.TableModels
{
    /// <summary>
    /// Столбец(строка) таблицы, определяющий дальнейшее содержимое
    /// </summary>
    public class PdfTableColumn : IPdfColumnItem
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Размер (Ширина столбца/Высота строки)
        /// </summary>
        public float? Size { get; set; }

        /// <summary>
        /// Название свойства, которое будет заполнять столбец
        /// </summary>
        public string PropertyName { get; set; }
    }
}
