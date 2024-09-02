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
        public string ExcelColumn { get => ToExcelFormat(Column); set => Column = FromExcelFormat(value); }
                
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
        /// <param name="excelColumn">Номер столбца в стиле excel</param>
        /// <param name="excelRow">Номер строки в стиле excel</param>
        public ExcelCell(string excelColumn, uint excelRow)
        {
            ExcelColumn = excelColumn;
            ExcelRow = excelRow;
        }

        /// <summary>
        /// </summary>
        /// <param name="column">Номер столбца</param>
        /// <param name="row">Номер строки</param>
        public ExcelCell(uint column, uint row)
        {
            Column = column;
            Row = row;
        }

        const uint ALPHABET_START = 'A';
        const uint ALPHABET_END = 'Z';
        const uint ALPHABET_LEN = ALPHABET_END - ALPHABET_START + 1;

        private static uint FromExcelFormat(string excelColumn)
        {
            const uint alphabetStart = 'A';
            const uint alphabetEnd = 'Z';
            const uint alphabetLen = alphabetEnd - alphabetStart + 1;

            uint result = 0;    

            foreach (char c in excelColumn)
            {
                result = (result + 1) * alphabetLen;

                result += c - alphabetStart;
            }

            return result;
        }

        private static string ToExcelFormat(uint column)
        {
            List<char> result = new List<char>();

            while (column >= 0)
            {
                result.Add((char)(ALPHABET_START + (column % ALPHABET_LEN)));
                column = column / ALPHABET_LEN - 1;
            }

            result.Reverse();
            return string.Join("", result);
        }
    }
}
