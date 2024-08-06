namespace PIHelperSh.ExcelCreator.Models
{
	/// <summary>
	/// Класс служит для объединения ячеек. Поля CellFrom и CellTo обязательные
	/// </summary>
	public class ExcelMergeParameters
    {
        /// <summary>
        /// Начиная с какой ячейки объединяем
        /// </summary>
        public ExcelCell CellFrom { get; set; } = new();

        /// <summary>
        /// До какой ячейки объединяем
        /// </summary>
        public ExcelCell CellTo { get; set; } = new();

        /// <summary>
        /// Текстовое представление объединения
        /// </summary>
        public string Merge => $"{CellFrom.CellReference}:{CellTo.CellReference}";

        /// <summary>
        /// Объединяет ли диапазон несколько строк
        /// </summary>
        public bool isRowMerge => CellFrom.Row != CellTo.Row;

        /// <summary>
        /// Объединяет ли диапазон несколько столбцов
        /// </summary>
        public bool isCollumnMerge => CellFrom.Column != CellTo.Column;
    }
}
