﻿using PIHelperSh.ExcelCreator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.ExcelCreator.Models
{
    //Ячейка Excel c содержимым. При создании обязательно необходимо определить ячейку(Cell) и Text. При необходимости объединения ячеек, нужно лишь заполнить EndCell(по умолчанию null)
    public class ExcelCellParameters
    {
        /// <summary>
        /// Ячейка, куда пишем(начало диапозона объединения в случае, если таковое необходимо)
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

        public string CellReference => Cell.CellReference;
    }
}
