using PIHelperSh.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.ExcelCreator.Enums
{
    /// <summary>
    /// Данное перечисление отвечает за тип текста(настройки шрифта и позиционирования)
    /// </summary>
    public enum ExcelStyleInfoType
    {
        /// <summary>
        /// Заголовок. По центру и Жирно
        /// </summary>
        [TypeValue<uint>(0U)]
        Title,
        /// <summary>
        /// Жирный текст с рамкой
        /// </summary>
        [TypeValue<uint>(1U)]
        BoldBorder,
        /// <summary>
        /// Обыкновенный текст с рамкой
        /// </summary>
        [TypeValue<uint>(2U)]
        SimpleBorder
    }
}
