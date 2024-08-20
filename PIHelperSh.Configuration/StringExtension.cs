using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PIHelperSh.Configuration
{
    public static class StringExtension
    {
        /// <summary>
        /// Преобразовать строку в константный регистр
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToConstantCase(this string value)
            => Regex.Replace(value, @"(?!^)([A-Z]|( [a-z]))", c => $"_{c}", RegexOptions.None).ToUpper();

        /// <summary>
        /// Преобразует строку в паскальный регистр
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string value)
            => Regex.Replace(value, @"(_([a-z]|[A-Z]))|(^[a-z])", c => c.Value.Trim(['_']).ToUpper(), RegexOptions.None);
    }
}
