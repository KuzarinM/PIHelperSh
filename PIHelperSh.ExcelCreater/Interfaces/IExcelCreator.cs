using PIHelperSh.ExcelCreator.Models;

namespace PIHelperSh.ExcelCreator.Interfaces
{
	public interface IExcelCreator
    {
        /// <summary>
        /// Позволяет настроить фиксированную ширину для конкретных столбцов. 
        /// </summary>
        /// <param name="settings">Список столбцов с необходимыми значениями ширины</param>
        public void ConfigureColumns(List<ExcelColumnSettings> settings);

        /// <summary>
        /// Позволяет вставить на лист Excel целую строку(последовательно размещается)
        /// </summary>
        /// <param name="row"></param>
        public void InsertRow(ExcelRowParameters row);

        /// <summary>
        /// Вставка ячейки на лист.
        /// </summary>
        /// <param name="excelParams"></param>
        public void InsertCell(ExcelCellParameters excelParams);

        /// <summary>
        /// (Устарел) Установка объединения ячеек (Если нужно писать туда текст, то метод InsertCell с установкой значения EndCell удобнее) 
        /// </summary>
        /// <param name="excelParams"></param>
        public void MergeCells(ExcelMergeParameters excelParams);

        /// <summary>
        /// Метод, сохраняющий документ и возвращающий поток MemoryStream
        /// </summary>
        /// <param name="info"></param>
        /// <returns>Поток конечного файла</returns>
        public MemoryStream SaveExcel();

        /// <summary>
        /// Метод, сохраняющий документ в файл
        /// </summary>
        /// <param name="filename">Путь до файла. Проверок никаких нет</param>
        public void SaveExcel(string filename);
    }
}
