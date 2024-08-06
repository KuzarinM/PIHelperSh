using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.ExcelCreator.Models
{
    /// <summary>
    /// Ячейка в таблице. Проще всего создавать через конструктор.
    /// </summary>
    public class ExcelCell
    {
        /// <summary>
        /// Название столбца
        /// </summary>
        public string Column { get; set; } = string.Empty;

        /// <summary>
        /// Номер строки
        /// </summary>
        public uint Row { get; set; }

        /// <summary>
        /// Полное имя ячейки
        /// </summary>
        public string CellReference => $"{Column}{Row}";

        public ExcelCell() { }

        public ExcelCell(string ColumnName, uint RowNumber)
        {
            Column = ColumnName;
            Row = RowNumber;
        }
    }
}
