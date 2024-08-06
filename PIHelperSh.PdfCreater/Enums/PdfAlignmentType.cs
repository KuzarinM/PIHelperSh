using MigraDoc.DocumentObjectModel;
using PIHelperSh.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreator.Enums
{
    /// <summary>
    /// Тип выравнивания элементов
    /// </summary>
    public enum PdfAlignmentType
    {
        /// <summary>
        /// По центру
        /// </summary>
        [TypeValue<ParagraphAlignment>(ParagraphAlignment.Center)]
        Center,
        /// <summary>
        /// По левому краю
        /// </summary>
        [TypeValue<ParagraphAlignment>(ParagraphAlignment.Left)]
        Left,
        /// <summary>
        /// По правому краю
        /// </summary>
        [TypeValue<ParagraphAlignment>(ParagraphAlignment.Right)]
        Rigth
    }
}
