using PIHelperSh.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.Core.Enums
{
    /// <summary>
    /// Варианты кастомного сравнения
    /// </summary>
    public enum CustiomComparationMode
    {
        /// <summary>
        /// Сравнение по умолчанию
        /// </summary>
        Default,
        /// <summary>
        /// Игнорируем при сравнении
        /// </summary>
        Ignore,
        /// <summary>
        /// WIP На данный момент не работает
        /// </summary>
        Comparator,
        /// <summary>
        /// Сравнение через стандартный оператор сравнения
        /// </summary>
        Equals
    }
}
