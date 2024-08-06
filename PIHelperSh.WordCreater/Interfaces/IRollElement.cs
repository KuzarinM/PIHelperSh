using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.WordCreator.Interfaces
{
    /// <summary>
    /// Данный интерфейс необходим тольео для нормальной работы списков(чтобы можно было делать многоуровневые списки) Слово List в навазнии заменено на Roll чтобы не создавать путаницы с List как отчётом в иде списка.
    /// </summary>
    public interface IRollElement
    {
        /// <summary>
        /// Уровень элемента списка.
        /// </summary>
		public int? RollLavel { get; set; }
    }
}
