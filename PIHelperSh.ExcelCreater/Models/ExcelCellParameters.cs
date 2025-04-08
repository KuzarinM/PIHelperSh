using PIHelperSh.ExcelCreator.Enums;

namespace PIHelperSh.ExcelCreator.Models
{
    /// <summary>
    /// Ячейка Excel c содержимым. При создании обязательно необходимо определить ячейку(Cell) и Text. 
    /// При необходимости объединения ячеек, нужно лишь заполнить EndCell(по умолчанию null)
    /// </summary>
    public class ExcelCellParameters
    {
        /// <summary>
        /// Ячейка, куда пишем(начало диапазона объединения в случае, если таковое необходимо)
        /// </summary>
        public ExcelCell Cell { get; set; } = new();

        /// <summary>
        /// Текст, который помещается в ячейку
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Тип стиля, который используется
        /// </summary>
        public ExcelStyleInfoType StyleInfo { get; set; } = ExcelStyleInfoType.SimpleBorder;

        /// <summary>
        /// В случае, если необходимо объединение ячеек, то данное поле заполняется конечной ячейкой. Иначе - не заполнять
        /// </summary>
        public ExcelCell? EndCell { get; set; } = null;

        /// <summary>
        /// Полное имя ячейки
        /// </summary>
        public string CellReference => Cell.CellReference;
    }
}
