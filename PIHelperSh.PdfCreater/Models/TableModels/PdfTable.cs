using PIHelperSh.PdfCreator.Enums;
using PIHelperSh.PdfCreator.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreator.Models.TableModels
{
    /// <summary>
    /// Таблица для вставки на лист
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PdfTable<T>
    {
        /// <summary>
        /// Шапка таблицы(2 строки/2 столбца)
        /// </summary>
        public List<IPdfColumnItem>? Header { get; set; } = null;

        /// <summary>
        /// Стиль заголовка (по умолчанию - жирный)
        /// </summary>
        public PdfStyleType HeaderStyle { get; set; } = PdfStyleType.Bold;

        /// <summary>
        /// Выравнивание текста внутри заголовка (по умолчанию - по центру)
        /// </summary>
        public PdfAlignmentType HeaderHorisontalAlignment { get; set; } = PdfAlignmentType.Center;

        /// <summary>
        /// Список объектов, информация о которых будет в таблице. 
        /// </summary>
        public List<T> Records { get; set; } = new();

        /// <summary>
        /// Стиль объектов в таблице (по умолчанию - базовый)
        /// </summary>
        public PdfStyleType RecordStyle { get; set; } = PdfStyleType.Basic;

        /// <summary>
        /// Выравнивание текста объектов в таблице (по умолчанию - по левой строне)
        /// </summary>
        public PdfAlignmentType RecordHorisontalAlignment { get; set; } = PdfAlignmentType.Left;
    }
}
