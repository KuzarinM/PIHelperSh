using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.Rendering;
using System.IO;
using MigraDoc.DocumentObjectModel.Tables;
using System.Reflection;
using MigraDoc.DocumentObjectModel.Shapes.Charts;
using PIHelperSh.Core.Extensions;
using PIHelperSh.PdfCreator.Interfases;
using PIHelperSh.PdfCreator.Enums;
using PIHelperSh.PdfCreator.Models.TableModels;
using PIHelperSh.PdfCreator.Models.TextModels;
using PIHelperSh.PdfCreator.Models.ImageModels;
using PIHelperSh.PdfCreator.Models.PiechartModel;
using HyperlinkType = MigraDoc.DocumentObjectModel.HyperlinkType;

namespace PIHelperSh.PdfCreator
{
    public class PdfCreator : IPdfCreator
    {
        private Document? _document;
        private Section? _section;

        private readonly Unit borderWidth = 0.5;

        public PdfCreator()
        {
            _document = new Document();
            DefineStyles(_document);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _section = _document.AddSection();
        }

        #region Внутренние методы
        private static string GetListLevel(int level)
        {
            try
            {
                return level.CreateEnumFromValue<PdfStyleType>().GetValue<string>();
            }
            catch (Exception ex)
            {
                return PdfStyleType.Basic.GetValue<string>();
            }
        }

        private void ConfigureChartLegend(Chart chart, PdfLegendPosition position)
        {
            switch (position)
            {
                case PdfLegendPosition.Bottom:
                    chart.FooterArea.AddLegend();
                    break;
                case PdfLegendPosition.Right:
                    chart.RightArea.AddLegend();
                    break;
                case PdfLegendPosition.Left:
                    chart.LeftArea.AddLegend();
                    break;
                case PdfLegendPosition.Top:
                    chart.TopArea.AddLegend();
                    break;
            }
        }

        private static void DefineStyles(Document document)
        {
            #region Базовый стиль
            var style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            #endregion

            #region Стиль заголовка
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
            style.Font.Size = 18;
            #endregion

            #region Стиль жирный
            style = document.Styles.AddStyle("NormalBold", "Normal");
            style.Font.Bold = true;
            #endregion

            #region Маркированный список уровень 1
            style = document.AddStyle("BulletList", "Normal");
            style.ParagraphFormat.LeftIndent = "1.5cm";
            style.ParagraphFormat.ListInfo = new ListInfo
            {
                ContinuePreviousList = true,//Продолжать список, который уже был ранее
                ListType = ListType.BulletList1//Маркер
            };
            style.ParagraphFormat.TabStops.ClearAll();
            style.ParagraphFormat.TabStops.AddTabStop(Unit.FromCentimeter(1.5), TabAlignment.Left);

            style.ParagraphFormat.FirstLineIndent = "-0.5cm";
            #endregion

            #region Маркированный список уровень 2
            style = document.AddStyle("BulletList2", "BulletList");
            style.ParagraphFormat.LeftIndent = "3.0cm";
            style.ParagraphFormat.ListInfo.ListType = ListType.BulletList2;
            style.ParagraphFormat.TabStops.ClearAll();
            style.ParagraphFormat.TabStops.AddTabStop(Unit.FromCentimeter(3.0), TabAlignment.Left);
            #endregion

            #region Маркированный список уровень 3
            style = document.AddStyle("BulletList3", "BulletList");
            style.ParagraphFormat.LeftIndent = "4.5cm";
            style.ParagraphFormat.ListInfo.ListType = ListType.BulletList3;
            style.ParagraphFormat.TabStops.ClearAll();
            style.ParagraphFormat.TabStops.AddTabStop(Unit.FromCentimeter(4.5), TabAlignment.Left);
            #endregion

            #region Мелкий шрифт
            style = document.Styles.AddStyle("NormalSmall", "Normal");
            style.Font.Size = 11;
            #endregion
        }

        private Paragraph? MakeParagraph(PdfParagraph pdfParagraph)
        {
            if (_section == null)
                return null;
            var paragraph = _section.AddParagraph(pdfParagraph.Text);
            paragraph.Format.Alignment = pdfParagraph.ParagraphAlignment.GetValue<ParagraphAlignment>();
            //Т.к стили могут назначаться по иному, тут будет вот так вот
            return paragraph;
        }

        private Paragraph? MakeList(PdfList pdfList, int level)
        {
            Paragraph? last = null;
            foreach (IPdfElement element in pdfList.Content)
            {
                if (element is PdfParagraph par)
                {
                    var paragraph = MakeParagraph(par);
                    if (paragraph == null)
                        continue;

                    paragraph.Format.SpaceAfter = "0.3cm";
                    paragraph.Style = GetListLevel(level);
                    last = paragraph;
                }
                else if (element is PdfList ls)
                {
                    last = MakeList(ls, level + 1);
                }
            }
            return last;
        }

        private void ConfigureParagraph(Paragraph paragraph, PdfParagraph properties)
        {
            paragraph.Format.Alignment = properties.ParagraphAlignment.GetValue<ParagraphAlignment>(); ;
            if (properties.MarginAfter != PdfMargin.None) paragraph.Format.SpaceAfter = properties.MarginAfter.GetValue<string>();
            paragraph.Style = properties.Style.GetValue<string>();

            if (properties.Hyperlink.Type != PIHyperlinkType.None)
            {
                paragraph.AddHyperlink(properties.Hyperlink.Link, properties.Hyperlink.Type.GetValue<HyperlinkType>());
                paragraph.AddFormattedText(properties.Text);
            }
        }

        private void ConfigureCell(Cell cell, string text, PdfAlignmentType alignment, PdfStyleType style, int? rightMerge = null, int? downMerge = null, bool dcw = false)
        {
            if (rightMerge.HasValue) cell.MergeRight = rightMerge.Value;
            if (downMerge.HasValue)
            {
                cell.MergeDown = downMerge.Value;
                cell.VerticalAlignment = VerticalAlignment.Center;
            }

            Paragraph paragraph = cell.AddParagraph(text);
            ConfigureParagraph(paragraph, new()
            {
                ParagraphAlignment = alignment,
                Style = style,
                MarginAfter = PdfMargin.None
            });

            cell.Borders.Left.Width = borderWidth;
            cell.Borders.Right.Width = borderWidth;
            cell.Borders.Top.Width = borderWidth;
            cell.Borders.Bottom.Width = borderWidth;

            if (dcw)
            {
                float columnNeeds = text.Length / 3.8f;
                if (cell.Column.Width.Centimeter < columnNeeds)
                    cell.Column.Width = Unit.FromCentimeter(columnNeeds);
            }
        }

        private Func<object?, object?> GetGetter(FieldInfo[] fields, PropertyInfo[] props, string Name)
        {
            var a = fields.FirstOrDefault(x => x.Name == Name);
            if (a != null) return a.GetValue;

            var b = props.FirstOrDefault(x => x.Name == Name);
            if (b != null) return b.GetValue;

            throw new KeyNotFoundException($"У объекта не найдено поле/свойство {Name}");
        }

        private List<Func<object?, object?>> MakeTableHeader<T>(Table table, PdfTable<T> header)
        {
            foreach (var item in header.Header)
            {
                if (item is PdfTableColumnGroup group)
                {
                    group.InnerColumns.ForEach(x => table.AddColumn(Unit.FromCentimeter(x.Size.Value)));
                }
                else
                {
                    table.AddColumn(Unit.FromCentimeter(item.Size.Value));
                }
            }

            Row upRow = table.AddRow();
            Row downRow = table.AddRow();
            upRow.HeadingFormat = downRow.HeadingFormat = true;
            upRow.Format.Font.Bold = downRow.Format.Font.Bold = true;

            int upColumn = 0;
            int downColumn = 0;

            Type eType = typeof(T);

            var fields = eType.GetFields();
            var prop = eType.GetProperties();

            List<Func<object?, object?>> objectFields = new();

            foreach (var item in header.Header)
            {
                if (item is PdfTableColumnGroup group)
                {
                    ConfigureCell(upRow.Cells[upColumn], group.Title, header.HeaderHorisontalAlignment, header.HeaderStyle, rightMerge: group.InnerColumns.Count - 1);
                    upColumn += group.InnerColumns.Count;
                    foreach (var ic in group.InnerColumns)
                    {
                        ConfigureCell(downRow.Cells[downColumn], ic.Title, header.HeaderHorisontalAlignment, header.HeaderStyle);
                        objectFields.Add(GetGetter(fields, prop, ic.PropertyName));
                        downColumn++;
                    }
                }
                else
                {
                    ConfigureCell(upRow.Cells[upColumn], item.Title, header.HeaderHorisontalAlignment, header.HeaderStyle, downMerge: 1);
                    ConfigureCell(downRow.Cells[downColumn], item.Title, header.HeaderHorisontalAlignment, header.HeaderStyle);
                    objectFields.Add(GetGetter(fields, prop, ((PdfTableColumn)item).PropertyName));
                    upColumn++;
                    downColumn++;
                }
            }

            return objectFields;
        }

        private void MakeSimpleTableHeader(Table table, PDFSimpleTable header)
        {
            foreach (var item in header.Header)
            {
                if (item is PdfTableColumnGroup group)
                {
                    group.InnerColumns.ForEach(x => table.AddColumn(Unit.FromCentimeter(x.Size.Value)));
                }
                else
                {
                    table.AddColumn(Unit.FromCentimeter(item.Size.Value));
                }
            }

            Row upRow = table.AddRow();
            Row downRow = table.AddRow();
            upRow.HeadingFormat = downRow.HeadingFormat = true;
            upRow.Format.Font.Bold = downRow.Format.Font.Bold = true;

            int upColumn = 0;
            int downColumn = 0;


            foreach (var item in header.Header)
            {
                if (item is PdfTableColumnGroup group)
                {
                    ConfigureCell(upRow.Cells[upColumn], group.Title, header.HeaderHorisontalAlignment, header.HeaderStyle, rightMerge: group.InnerColumns.Count - 1);
                    upColumn += group.InnerColumns.Count;
                    foreach (var ic in group.InnerColumns)
                    {
                        ConfigureCell(downRow.Cells[downColumn], ic.Title, header.HeaderHorisontalAlignment, header.HeaderStyle);
                        downColumn++;
                    }
                }
                else
                {
                    ConfigureCell(upRow.Cells[upColumn], item.Title, header.HeaderHorisontalAlignment, header.HeaderStyle, downMerge: 1);
                    ConfigureCell(downRow.Cells[downColumn], item.Title, header.HeaderHorisontalAlignment, header.HeaderStyle);
                    upColumn++;
                    downColumn++;
                }
            }
        }

        private void MakeTableWithHeaderInRow<T>(Table table, PdfTable<T> header)
        {
            for (int i = 0; i < header.Records.Count + 2; i++)
            {
                var c = table.AddColumn("0.5cm");
            }

            Type tp = typeof(T);
            var fields = tp.GetFields();
            var props = tp.GetProperties();

            foreach (var item in header.Header!)
            {
                Row row = table.AddRow();
                if (item is PdfTableColumnGroup group)
                {
                    ConfigureCell(row.Cells[0], group.Title!, header.HeaderHorisontalAlignment, header.HeaderStyle, downMerge: group.InnerColumns.Count - 1, dcw: true);
                    foreach (var innerParam in group.InnerColumns)
                    {
                        if (row == null) row = table.AddRow();//Первая(а точнее вторая) часть костыля с первой строкой
                        row.HeightRule = RowHeightRule.Exactly;
                        row.Height = Unit.FromCentimeter(innerParam.Size.Value);//Устанавливаем высоту строки. Так просили...

                        ConfigureCell(row.Cells[1], innerParam.Title!, header.HeaderHorisontalAlignment, header.HeaderStyle, dcw: true);

                        Func<object?, object?> getter = GetGetter(fields, props, innerParam.PropertyName);
                        for (int i = 0; i < header.Records.Count; i++)
                        {
                            ConfigureCell(row.Cells[i + 2], getter(header.Records[i]).ToString(), header.RecordHorisontalAlignment, header.RecordStyle, dcw: true);
                        }
                        row = null; //Это небольшой костыль, который позволяет не создавать новую строчку в самый первый раз(т.к. она же есть)
                    }
                }
                else if (item is PdfTableColumn column)
                {
                    row.HeightRule = RowHeightRule.Exactly;
                    row.Height = Unit.FromCentimeter(column.Size.Value);
                    ConfigureCell(row.Cells[0], column.Title!, header.HeaderHorisontalAlignment, header.HeaderStyle, rightMerge: 1, dcw: true);

                    Func<object?, object?> getter = GetGetter(fields, props, column.PropertyName);
                    for (int i = 0; i < header.Records.Count; i++)
                    {
                        ConfigureCell(row.Cells[i + 2], getter(header.Records[i]).ToString(), header.RecordHorisontalAlignment, header.RecordStyle, dcw: true);
                    }
                }
            }
        }

        private void ConfigurePieChart(Chart chart, PdfPieChartModel pieChart)
        {
            chart.Width = Unit.FromCentimeter(pieChart.Width);
            chart.Height = Unit.FromCentimeter(pieChart.Height);

            Paragraph p = chart.HeaderArea.AddParagraph(pieChart.ChartName);
            ConfigureParagraph(p, new()
            {
                Style = pieChart.HeaderStyle,
                ParagraphAlignment = pieChart.HeaderAlignment,
                MarginAfter = PdfMargin.None
            });

            chart.LineFormat.Visible = true;
            ConfigureChartLegend(chart, pieChart.LegendPosition);
            chart.DataLabel.Position = DataLabelPosition.OutsideEnd;
        }

        #endregion

        /// <summary>
        /// Создаём параграф
        /// </summary>
        /// <param name="paragraph">Модель параграфа, который вставляем</param>
        public void AddParagraph(PdfParagraph pdfParagraph)
        {
            var paragraph = MakeParagraph(pdfParagraph);
            
            if (paragraph == null)
                return;

            ConfigureParagraph(paragraph, pdfParagraph);
        }

        /// <summary>
        /// Создаём разрыв страницы
        /// </summary>
        /// <param name="pageBreak">Вставить разрыв страницы</param>
        public void AddPageBreak()
        {
            _section?.AddPageBreak();
        }

        /// <summary>
        /// Создаём маркированный список
        /// </summary>
        /// <param name="List">Модель списка(может быть многоуровневым). Max - 3 уровня</param>
        public void AddList(PdfList pdfList)
        {
            if (_section == null)
            {
                return;
            }
            var lastP = MakeList(pdfList, 1);
            if (lastP != null) lastP.Format.SpaceAfter = pdfList.MarginAfter.GetValue<string>();
        }

        /// <summary>
        /// Создаёт таблицу, с шапкой из 2-х строк(с группировками)
        /// </summary>
        /// <typeparam name="T">Тип DTO, из которой берутся данные в таблицу</typeparam>
        /// <param name="header">Модель самой таблицы</param>
        public void AddTable<T>(PdfTable<T> header, bool rowHeaded = false)
        {
            if (_document == null)
            {
                return;
            }

            if (rowHeaded)
            {
                _section.PageSetup.Orientation = Orientation.Landscape;
                _section.PageSetup.LeftMargin = 10;
                MakeTableWithHeaderInRow(_document.LastSection.AddTable(), header);
                return;
            }

            var table = _document.LastSection.AddTable();
            if (rowHeaded)
            {
                MakeTableWithHeaderInRow(table, header);
                return;
            }

            var mapper = MakeTableHeader(table, header);

            foreach (var item in header.Records)
            {
                var row = table.AddRow();
                for (int i = 0; i < mapper.Count; i++)
                {
                    ConfigureCell(row.Cells[i], mapper[i](item)?.ToString(), header.RecordHorisontalAlignment, header.RecordStyle);
                }
            }

        }

        /// <summary>
        /// Создаёт табличку, наподобие той, что с T, но проще
        /// </summary>
        /// <param name="tableData"></param>
        public void AddSimpleTable(PDFSimpleTable tableData)
        {
            if (_document == null)
            {
                return;
            }

            if (tableData.ChangePageOrientation)
            {
                _section.PageSetup.Orientation = Orientation.Landscape;
                _section.PageSetup.LeftMargin = 10;
                _section.PageSetup.BottomMargin = 5;
            }

            var table = _document.LastSection.AddTable();

            MakeSimpleTableHeader(table, tableData);

            foreach (var item in tableData.Rows)
            {
                var row = table.AddRow();

                for (int i = 0; i < item.Items.Count; i++)
                {
                    ConfigureCell(row.Cells[i], item.Items[i], item.Alignment ?? tableData.RowHorisontalAlignment, item.Style ?? tableData.RowStyle);
                }
            }
            if (tableData.MarginAfter != PdfMargin.None) table.Format.SpaceAfter = tableData.MarginAfter.GetValue<string>();
        }

        /// <summary>
        /// Создаёт круговую диаграмму.
        /// </summary>
        /// <param name="pieChart">Модель для круговой диаграммы</param>
        public void AddChart(PdfPieChartModel pieChart)
        {
            if (_document == null)
            {
                return;
            }

            Chart chart = new Chart(ChartType.Pie2D);
            ConfigurePieChart(chart, pieChart);

            Series series = chart.SeriesCollection.AddSeries();
            XSeries xseries = chart.XValues.AddXSeries();

            foreach (var item in pieChart.DataSet)
            {
                var p = series.Add(item.Value);
                xseries.Add(item.DisplayName);
                if (item.Color.HasValue)
                {
                    p.FillFormat.Color = new Color((uint)item.Color.Value.ToArgb());
                }
            }

            _section.Add(chart);
        }

        /// <summary>
        /// Добавляем на лист изображение. Можно по пути, можно по потоку, можно по Base64 строке
        /// </summary>
        /// <param name="image">Модель одного изображения</param>
        public void AddImage(PdfImage img)
        {
            if (_section == null) return;
            var paragraph = _section.AddParagraph();//Это некоторый костыли. у изображения настроить выравнивание - это очень непросто. А вот так вот - можно
            var image = paragraph.AddImage(img.Image.GetImageForMigraDoc());

            if (img.Width.HasValue) image.Width = img.Width.Value;
            if (img.Height.HasValue) image.Width = img.Height.Value;

            paragraph.Format.Alignment = img.ImageAlignment.GetValue<ParagraphAlignment>();
            paragraph.Format.SpaceAfter = img.MarginAfter.GetValue<string>();
        }

        /// <summary>
        /// Метод сохранения созданного PDF документа
        /// </summary>
        /// <returns>Поток MemoryStream с документом</returns>
        public MemoryStream SavePdf()
        {
            var renderer = new PdfDocumentRenderer(true)
            {
                Document = _document
            };
            renderer.RenderDocument();

            MemoryStream file = new MemoryStream();
            renderer.PdfDocument.Save(file, false);

            file.Seek(0, SeekOrigin.Begin);
            return file;
        }

        /// <summary>
        /// Метод сохранения созданного PDF документа в файл
        /// </summary>
        /// <param name="filename">Имя файла и путь до него. Проверки на расширение нет</param>
        public void SavePdf(string filename)
        {
            using Stream streamToWriteTo = File.Open(filename, FileMode.Create);
            MemoryStream ms = SavePdf();
            ms.Position = 0;
            ms.CopyTo(streamToWriteTo);
        }
    }
}
