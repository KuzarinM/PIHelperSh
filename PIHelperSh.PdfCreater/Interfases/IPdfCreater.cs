using PIHelperSh.PdfCreater.Models.ImageModels;
using PIHelperSh.PdfCreater.Models.PiechartModel;
using PIHelperSh.PdfCreater.Models.TableModels;
using PIHelperSh.PdfCreater.Models.TextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreater.Interfases
{
    public interface IPdfCreater
    {
        /// <summary>
        /// Метод начального создания документа PDF
        /// </summary>
        public void CreatePdf();

        /// <summary>
        /// Создаём параграф
        /// </summary>
        /// <param name="paragraph">Модель параграфа, который вставляем</param>
        public void AddParagraph(PdfParagraph paragraph);

        /// <summary>
        /// Создаём маркерованный список
        /// </summary>
        /// <param name="List">Модель списка(может быть многоуровневым). Max - 3 уровня</param>
        public void AddList(PdfList List);

        /// <summary>
        /// Создаёт таблицу, с шапкой из 2-х строк(с группировками)
        /// </summary>
        /// <typeparam name="T">Тип DTO, из которой берё=утся данные в таблицу</typeparam>
        /// <param name="header">Модель самой таблицы</param>
        public void AddTable<T>(PdfTable<T> header, bool rowHeaded = false);

        /// <summary>
        /// Создаёт круговую диаграму.
        /// </summary>
        /// <param name="pieChart">Модель для круговой диаграмы</param>
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
