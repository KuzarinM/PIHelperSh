﻿using PIHelperSh.ExcelCreater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.ExcelCreater.Interfaces
{
    public interface IExcelCreater
    {
        /// <summary>
        /// Метод, непосредственно создающий Excel документ
        /// </summary>
        /// <param name="info"></param>
        public void CreateExcel();

        /// <summary>
        /// Позволяет настроить фиксированную ширину для конкретных столбцов. 
        /// </summary>
        /// <param name="settings">Список столбцов с необходимиыми значениями ширины</param>
        public void ConfigurateColumns(List<ExcelColumnSettings> settings);

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
