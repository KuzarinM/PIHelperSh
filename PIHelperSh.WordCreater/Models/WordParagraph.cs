using PIHelperSh.WordCreator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.WordCreator.Models
{
    /// <summary>
    /// Класс, ответственный за параграфы.
    /// </summary>
    public class WordParagraph : IRollElement
    {
        /// <summary>
        /// Весь таекст параграфа состоит из прогнов (у каждого могут быть свои настройки). Если их нет, то применяются настройки по умолчанию
        /// </summary>
        public List<(string run, WordTextProperties? properties)> Texts { get; set; } = new();

        /// <summary>
        /// Настройки по умолчанию для параграфа
        /// </summary>
        public WordTextProperties? TextProperties { get; set; }

        /// <summary>
        /// Если этот параграф - элемент списка, то тут указывается уровень
        /// </summary>
        public int? RollLavel { get; set; }
    }
}
