using PIHelperSh.ExcelCreater.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.ExcelCreater.Models
{
    /// <summary>
    /// Данный класс позволяет выводить список текстов послледовательно в одну строку. Для этого обязательно указать начальную ячейку. Конечную можно не указывать, она сама будет вычислена исходя из длинны списка
    /// </summary>
    public class ExcelRowParameters : ExcelCell
    {
        /// <summary>
        /// Список строк, который будут последовательно выведены в ячейки
        /// </summary>
        public List<string> CelsTexts { get; set; } = new();

        /// <summary>
        /// Общее стилистическое оформление строки
        /// </summary>
        public ExcelStyleInfoType StyleInfo { get; set; }
    }
}
