using PIHelperSh.PdfCreator.Models.ImageModels;
using PIHelperSh.PdfCreator.Models.PiechartModel;
using PIHelperSh.PdfCreator.Models.TableModels;
using PIHelperSh.PdfCreator.Models.TextModels;

namespace PIHelperSh.PdfCreator.Interfaces
{
    public interface IPdfCreator
    {
        /// <summary>
        /// Создаём разрыв страницы
        /// </summary>
        /// <param name="pageBreak">Вставить разрыв страницы</param>
        public void AddPageBreak();
        
        /// <summary>
        /// Создаём параграф
        /// </summary>
        /// <param name="paragraph">Модель параграфа, который вставляем</param>
        public void AddParagraph(PdfParagraph paragraph);

        /// <summary>
        /// Создаём маркированный список
        /// </summary>
        /// <param name="List">Модель списка(может быть многоуровневым). Max - 3 уровня</param>
        public void AddList(PdfList List);

        /// <summary>
        /// Создаёт таблицу, с шапкой из 2-х строк(с группировками)
        /// </summary>
        /// <typeparam name="T">Тип DTO, из которой берутся данные в таблицу</typeparam>
        /// <param name="header">Модель самой таблицы</param>
        public void AddTable<T>(PdfTable<T> header, bool rowHeaded = false);


        /// <summary>
        /// Создаёт табличку, наподобие той, что с T, но проще
        /// </summary>
        /// <param name="tableData"></param>
        public void AddSimpleTable(PDFSimpleTable tableData);

        /// <summary>
        /// Создаёт круговую диаграмму.
        /// </summary>
        /// <param name="pieChart">Модель для круговой диаграммы</param>
        public void AddChart(PdfPieChartModel pieChart);

        /// <summary>
        /// Добавляем на лист изображение. Можно по пути, можно по потоку, можно по Base64 строке
        /// </summary>
        /// <param name="image">Модель одного изображения</param>
        public void AddImage(PdfImage image);

        /// <summary>
        /// Метод сохранения созданного PDF документа
        /// </summary>
        /// <returns>Поток MemoryStream с документом</returns>
        public MemoryStream SavePdf();

        /// <summary>
        /// Метод сохранения созданного PDF документа в файл
        /// </summary>
        /// <param name="filename">Имя файла и путь до него. Проверки на расширение нет</param>
        public void SavePdf(string filename);

    }
}
