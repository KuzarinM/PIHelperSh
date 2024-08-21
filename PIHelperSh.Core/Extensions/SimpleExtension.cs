using System.Text;

namespace PIHelperSh.Core.Extensions
{
	public static class SimpleExtension
	{
		/// <summary>
		/// "Умножение" строк
		/// </summary>
		/// <param name="str"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public static string Multiply(this string str, int count)
		{
			StringBuilder sb = new StringBuilder(str.Length * count);
			for (int i = 0; i < count; i++)
			{
				sb.Append(str);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Целочисленная положительная степень
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static int PowInt(int a, int b)
		{
			if (b == 0)
				return 1;
			if (b == 1)
				return a;
			int res = PowInt(a, b / 2);
			if (b % 2 == 0)
				return res * res;
			else
				return res * res * a;
		}

        /// <summary>
        /// Обёртка над string.IsNullOrEmpty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
	}
}
