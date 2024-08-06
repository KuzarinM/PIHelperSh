using PIHelperSh.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreater.Enums
{
    /// <summary>
    /// Типы стилей в документе
    /// </summary>
    public enum PdfStyleType
    {
        /// <summary>
        /// Основная часть документа
        /// </summary>
        [TypeValue<string>("Normal")]
        Basic,
        /// <summary>
        /// Название документа
        /// </summary>
        [TypeValue<string>("NormalTitle")]
        Title,
        /// <summary>
        /// Уровень в списке 1
        /// </summary>
        [TypeValue<string>("BulletList")]
        [TypeValue<int>(1)]
        ListLevel1,
        /// <summary>
        /// Уровень в списке 2
        /// </summary>
        [TypeValue<string>("BulletList2")]
        [TypeValue<int>(2)]
        ListLevel2,
        /// <summary>
        /// Уровень в списке 3
        /// </summary>
        [TypeValue<string>("BulletList3")]
        [TypeValue<int>(3)]
        ListLevel3,
        /// <summary>
        /// Жирный шрифт
        /// </summary>
        [TypeValue<string>("NormalBold")]
        Bold
    }
}
