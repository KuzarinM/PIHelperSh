using PIHelperSh.WordCreater.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.WordCreater.Models
{
    /// <summary>
    /// Маркерованный(не нумерованный) список.
    /// </summary>
    public class MarkeredList : IRollElement
    {
        /// <summary>
        /// Элементы спсика(параграфы или другие списки)
        /// </summary>
        public List<IRollElement> childs { get; set; } = new();

        /// <summary>
        /// Уровень данного списка
        /// </summary>
        public int? RollLavel { get; set; }
    }
}
