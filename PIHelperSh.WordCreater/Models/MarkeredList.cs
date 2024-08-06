using PIHelperSh.WordCreater.Interfaces;

namespace PIHelperSh.WordCreater.Models
{
	/// <summary>
	/// Маркированный(не нумерованный) список.
	/// </summary>
	public class MarkeredList : IRollElement
    {
        /// <summary>
        /// Элементы списка(параграфы или другие списки)
        /// </summary>
        public List<IRollElement> childs { get; set; } = new();

        /// <summary>
        /// Уровень данного списка
        /// </summary>
        public int? RollLavel { get; set; }
    }
}
