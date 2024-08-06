namespace PIHelperSh.WordCreater.Interfaces
{
	/// <summary>
	/// Данный интерфейс необходим только для нормальной работы списков(чтобы можно было делать многоуровневые списки) Слово List в названии заменено на Roll чтобы не создавать путаницы с List как отчётом в виде списка.
	/// </summary>
	public interface IRollElement
    {
        /// <summary>
        /// Уровень элемента списка.
        /// </summary>
		public int? RollLavel { get; set; }
    }
}
