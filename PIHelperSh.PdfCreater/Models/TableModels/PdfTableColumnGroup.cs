using PIHelperSh.PdfCreator.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreator.Models.TableModels
{
    /// <summary>
    /// Группа столбцов(строк) таблицы, определяющий дальнейшее содержимое
    /// </summary>
    public class PdfTableColumnGroup : IPdfColumnItem
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Колличество элементов в группе
        /// </summary>
        public float? Size => InnerColumns.Sum(x => x.Size);

        /// <summary>
        /// Столбцы/строки в группе
        /// </summary>
        public List<PdfTableColumn> InnerColumns { get; set; } = new();
    }
}
