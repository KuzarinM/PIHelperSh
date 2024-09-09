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
            uint result = 0;

            foreach (char c in excelColumn)
            {
                result = (result + 1) * ALPHABET_LEN;

                result += c - ALPHABET_START;
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
    }
}
