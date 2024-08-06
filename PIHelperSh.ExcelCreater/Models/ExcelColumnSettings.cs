namespace PIHelperSh.ExcelCreator.Models
{
	/// <summary>
	/// Данный класс необходим для настройки ширины столбцов. Более нигде не применяется
	/// </summary>
	public class ExcelColumnSettings
    {
        /// <summary>
        /// Название столбца
        /// </summary>
        public string Column { get; set; } = string.Empty;

        /// <summary>
        /// Желаемая ширина столбца
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Индекс столбца в системе
        /// </summary>
        public uint ColumnNumber => Column.ConvertToInt();
    }
}
