using PIHelperSh.WordCreater.Models;

namespace PIHelperSh.WordCreater.Interfaces
{
	public interface IWordCreater
    {
        /// <summary>
        /// Настройки текст по умолчанию для ВСЕГО документа
        /// </summary>
        public WordTextProperties DefaultTextProperies { get; }

        /// <summary>
        /// Метод создания документа.
        /// </summary>
        /// <param name="info"></param>
        public void CreateWord(ListInfo info);

        /// <summary>
        /// Метод создания параграфа
        /// </summary>
        /// <param name="paragraph">Параграф</param>
        public void AddParagraph(WordParagraph paragraph);

        /// <summary>
        /// Метод создания маркированного списка
        /// </summary>
        /// <param name="list"></param>
        public void AddMarkeredList(MarkeredList list);

        /// <summary>
        /// Метод сохранения созданного документа
        /// </summary>
        /// <param name="info"></param>
        /// <returns>Возвращает MemoryStream с файлом</returns>
        public MemoryStream SaveWord();

        /// <summary>
        /// Метод сохранения созданного документа в виде файла
        /// </summary>
        /// <param name="filename">Путь к файлу. Проверок типа нет</param>
        public void SaveWord(string filename);
    }
}
