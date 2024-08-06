using PIHelperSh.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreator.Enums
{
    /// <summary>
    /// Типы отступов после элементов (вертикальный)
    /// </summary>
    public enum PdfMargin
    {
        /// <summary>
        /// Отступа нет
        /// </summary>
        [TypeValue<string>("ocm")]
        None,
        /// <summary>
        /// Отступ небольшой
        /// </summary>
        [TypeValue<string>("0.3cm")]
        Smal,
        /// <summary>
        /// Отступ средний
        /// </summary>
        [TypeValue<string>("1cm")]
        Medium,
        /// <summary>
        /// Отступ большой
        /// </summary>
        [TypeValue<string>("1.6cm")]
        Big
    }
}
