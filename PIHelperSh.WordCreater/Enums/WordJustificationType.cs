using DocumentFormat.OpenXml.Wordprocessing;
using PIHelperSh.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.WordCreator.Enums
{
    /// <summary>
    /// Тип выравнивания содержимого для Word
    /// </summary>
    public enum WordJustificationType
    {
        /// <summary>
        /// По центру
        /// </summary>
        Center,
        /// <summary>
        /// На всю ширину страницы
        /// </summary>
        Both,
        /// <summary>
        /// По левому краю
        /// </summary>
        Left
    }
}
