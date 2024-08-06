namespace PIHelperSh.ExcelCreator
{
	/// <summary>
	/// Методы расширения чисто для Excel
	/// </summary>
	internal static class OfficePackageExtentions
    {
        private static readonly int LetterCount = 26;

        /// <summary>
        /// Получить следующий столбец в Excel (a->z->aa->zz->...)
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        internal static string NextColumn(this string column)
        {
            int len = column.Length;
            char[] resout = new char[len];
            bool flag = true;
            for (int i = len - 1; i >= 0; i--)
            {
                if (flag)
                {
                    char letter = column[i];
                    letter++;
                    if (letter <= 'Z')
                    {
                        resout[i] = letter;
                        flag = false;
                    }
                    else
                        resout[i] = 'A';
                }
                else
                    resout[i] = column[i];
            }
            if (flag)
                return $"A{new string(resout)}";
            return new string(resout);
        }

        /// <summary>
        /// Преобразует буквенное обозначение столбца Excel в его номер
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        internal static uint ConvertToInt(this string column)
        {
            uint id = 0;
            int pow = 0;
            for (int i = column.Length - 1; i >= 0; i--)
            {
                id += (uint)(powInt(LetterCount, pow) * (column[i] - 'A' + 1));
                pow++;
            }
            return id;
        }

        /// <summary>
        /// Возведение целых числе в степень. Необходимо для работы ConvertToInt
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        internal static int powInt(int a, int b)
        {
            if (b == 0)
                return 1;
            if (b == 1)
                return a;
            int res = powInt(a, b / 2);
            if (b % 2 == 0)
                return res * res;
            else
                return res * res * a;
        }
    }
}
