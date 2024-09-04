using DocumentFormat.OpenXml.Spreadsheet;

namespace PIHelperSh.ExcelCreator.Helpers
{
    internal static class CellHelper
    {
        const uint ALPHABET_START = 'A';
        const uint ALPHABET_END = 'Z';
        const uint ALPHABET_LEN = ALPHABET_END - ALPHABET_START + 1;

        public static uint FromExcelFormat(string excelColumn)
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

        public static string ToExcelFormat(uint column)
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

        public static CellValues GetCellValueType(object value)
        {
            if (value.GetType() == typeof(int) || value.GetType() == typeof(decimal) || value.GetType() == typeof(double))
                return CellValues.Number;

            if (value.GetType() == typeof(bool))
                return CellValues.Boolean;

            if (value.GetType() == typeof(DateTime) || value.GetType() == typeof(DateTimeOffset))
                return CellValues.Date;

            if (value.GetType() == typeof(string))
                return CellValues.SharedString;

            throw new NotImplementedException("CellTypeValue undefined");
        }
    }
}
