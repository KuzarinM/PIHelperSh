using PIHelperSh.ExcelCreator.Enums;

namespace PIHelperSh.ExcelCreator.Models
{
	/// <summary>
	/// Данный класс позволяет выводить список текстов последовательно в одну строку. Для этого обязательно указать начальную ячейку. Конечную можно не указывать, она сама будет вычислена исходя из длинны списка
	/// </summary>
	public class ExcelRowParameters : ExcelCell
    {
        /// <summary>
        /// Список строк, который будут последовательно выведены в ячейки
        /// </summary>
        public List<string> CellsTexts { get; set; } = new();

        /// <summary>
        /// Общее стилистическое оформление строки
        /// </summary>
        public ExcelStyleInfoType StyleInfo { get; set; }
    }
}
