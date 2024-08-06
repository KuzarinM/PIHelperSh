using PIHelperSh.Core.Attributes;

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
