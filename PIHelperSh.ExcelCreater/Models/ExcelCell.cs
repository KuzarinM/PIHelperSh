using PIHelperSh.ExcelCreator.Helpers;

namespace PIHelperSh.ExcelCreator.Models
{
    /// <summary>
    /// Ячейка в таблице. Проще всего создавать через конструктор.
    /// </summary>
    public class ExcelCell
    {
        /// <summary>
        /// Номер столбца
        /// </summary>
        public uint Column { get; set; } = 0;

        /// <summary>
        /// Номер строки
        /// </summary>
        public uint Row { get; set; } = 0;
        
        /// <summary>
        /// Номер столбца в стиле excel
        /// </summary>
        public string ExcelColumn 
        { 
            get => CellHelper.ToExcelFormat(Column);
            set => Column = CellHelper.FromExcelFormat(value); 
        }
                
        /// <summary>
        /// Номер строки в стиле excel
        /// </summary>
        public uint ExcelRow { get => Row + 1; set => Row = value - 1; }

        /// <summary>
        /// Полное имя ячейки
        /// </summary>
        public string CellReference => $"{ExcelColumn}{ExcelColumn}";

        /// <summary>
        /// </summary>
        public ExcelCell() { }

        /// <summary>
        /// </summary>
        /// <param name="column">Номер столбца</param>
        /// <param name="row">Номер строки</param>
        public ExcelCell(uint column, uint row)
        {
            Column = column;
            Row = row;
        }

        /// <summary>
        /// </summary>
        /// <param name="excelColumn">Номер столбца в стиле excel</param>
        /// <param name="excelRow">Номер строки в стиле excel</param>
        public ExcelCell(string excelColumn, uint excelRow)
        {
            ExcelColumn = excelColumn;
            ExcelRow = excelRow;
        }
    }
}
