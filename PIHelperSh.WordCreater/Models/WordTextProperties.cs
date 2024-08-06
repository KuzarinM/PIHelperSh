using PIHelperSh.WordCreator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.WordCreator.Models
{
    /// <summary>
    /// Настроки текст
    /// </summary>
    public class WordTextProperties
    {
        /// <summary>
        /// Шрифт. В оригинальном OpenXML есть, не знаю, то ли баг, то ли фича, в общем 10 шрифт в коде, был 5 в реальном документе. В моём случае это ИСПРАВЛЕНО. 
        /// </summary>
        public int? Size { get; set; } = null;

        /// <summary>
        /// Признак жирности текста
        /// </summary>
        public bool? Bold { get; set; } = null;

        /// <summary>
        /// Тип выравнивания текста
        /// </summary>
        public WordJustificationType? JustificationType { get; set; } = null;
    }
}
