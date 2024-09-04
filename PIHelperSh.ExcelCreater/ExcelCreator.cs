using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using PIHelperSh.Core.Extensions;
using PIHelperSh.ExcelCreator.Enums;
using PIHelperSh.ExcelCreator.Interfaces;
using PIHelperSh.ExcelCreator.Models;
using PIHelperSh.ExcelCreator.Helpers;

namespace PIHelperSh.ExcelCreator
{
    public class ExcelCreator : IExcelCreator
    {
        private SpreadsheetDocument? _spreadsheetDocument;
        private SharedStringTablePart? _shareStringPart;
        private Worksheet? _worksheet;
        private readonly MemoryStream _stream;

        public ExcelCreator()
        {
            _stream = new MemoryStream();

            _spreadsheetDocument = SpreadsheetDocument.Create(_stream, SpreadsheetDocumentType.Workbook);
            // Создаем книгу (в ней хранятся листы)

            var workbookpart = _spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            CreateStyles(workbookpart);

            // Получаем/создаем хранилище текстов для книги
            _shareStringPart = _spreadsheetDocument.WorkbookPart!.GetPartsOfType<SharedStringTablePart>().Any()
                ? _spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First()
                : _spreadsheetDocument.WorkbookPart.AddNewPart<SharedStringTablePart>();

            // Создаем SharedStringTable, если его нет
            _shareStringPart.SharedStringTable ??= new SharedStringTable();

            // Создаем лист в книгу
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Добавляем лист в книгу
            var sheets = _spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheet = new Sheet()
            {
                Id = _spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Лист 1"
            };
            sheets.Append(sheet);

            _worksheet = worksheetPart.Worksheet;
        }

        #region Служебные методы, необходимые для записи в excel документ
        private static void CreateStyles(WorkbookPart workbookpart)
        {
            var sp = workbookpart.AddNewPart<WorkbookStylesPart>();
            sp.Stylesheet = new Stylesheet();

            #region Шрифты
            var fonts = new Fonts() { Count = 2U, KnownFonts = true };

            var fontUsual = new Font(
                new FontSize() { Val = 12D },
                new DocumentFormat.OpenXml.Office2010.Excel.Color() { Theme = 1U },
                new FontName() { Val = "Times New Roman" },
                new FontFamilyNumbering() { Val = 2 },
                new FontScheme() { Val = FontSchemeValues.Minor });

            var fontTitle = new Font(
                new Bold(),
                new FontSize() { Val = 14D },
                new DocumentFormat.OpenXml.Office2010.Excel.Color() { Theme = 1U },
                new FontName() { Val = "Times New Roman" },
                new FontFamilyNumbering() { Val = 2 },
                new FontScheme() { Val = FontSchemeValues.Minor });

            fonts.Append(fontUsual);
            fonts.Append(fontTitle);
            #endregion

            #region Заполнение
            var fills = new Fills() { Count = 2U };

            var fill1 = new Fill(
                new PatternFill() { PatternType = PatternValues.None });

            var fill2 = new Fill(
                new PatternFill() { PatternType = PatternValues.Gray125 });

            fills.Append(fill1);
            fills.Append(fill2);
            #endregion

            #region Границы ячеек
            var borders = new Borders() { Count = 3U };

            var borderNoBorder = new Border(
                new LeftBorder(),
                new RightBorder(),
                new TopBorder(),
                new BottomBorder(),
                new DiagonalBorder()
                );
            var borderThin = new Border(
                new LeftBorder(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U })
                {
                    Style = BorderStyleValues.Thin
                },
                new RightBorder(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U })
                {
                    Style = BorderStyleValues.Thin
                },
                new TopBorder(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U })
                {
                    Style = BorderStyleValues.Thin
                },
                new BottomBorder(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U })
                {
                    Style = BorderStyleValues.Thin
                },
                new DiagonalBorder()
            );
            var borderMedium = new Border(
                new LeftBorder(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U })
                {
                    Style = BorderStyleValues.Medium
                },
                new RightBorder(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U })
                {
                    Style = BorderStyleValues.Medium
                },
                new TopBorder(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U })
                {
                    Style = BorderStyleValues.Medium
                },
                new BottomBorder(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U })
                {
                    Style = BorderStyleValues.Medium
                },
                new DiagonalBorder()
            );

            borders.Append(borderNoBorder);
            borders.Append(borderThin);
            borders.Append(borderMedium);
            #endregion

            #region Какие-то пред стилевые настройки
            var cellStyleFormats = new CellStyleFormats() { Count = 1U };

            var cellFormatStyle = new CellFormat() { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 0U };

            cellStyleFormats.Append(cellFormatStyle);
            #endregion

            #region Создание итоговых стилей
            var cellFormats = new CellFormats() { Count = 3U };

            var cellFormatHeader = new CellFormat() { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 1U, FormatId = 0U, Alignment = new Alignment() { Vertical = VerticalAlignmentValues.Center, WrapText = true, Horizontal = HorizontalAlignmentValues.Center }, ApplyFont = true, ApplyBorder = true };
            var cellFormatBody = new CellFormat() { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 1U, FormatId = 0U, ApplyFont = true, ApplyBorder = true };
            var cellFormatTitle = new CellFormat() { NumberFormatId = 0U, FontId = 1U, FillId = 0U, BorderId = 0U, FormatId = 0U, Alignment = new Alignment() { Vertical = VerticalAlignmentValues.Center, WrapText = true, Horizontal = HorizontalAlignmentValues.Center }, ApplyFont = true };

            cellFormats.Append(cellFormatTitle);
            cellFormats.Append(cellFormatHeader);
            cellFormats.Append(cellFormatBody);
            #endregion

            #region Дополнительные настройки

            var cellStyles = new CellStyles() { Count = 1U };

            cellStyles.Append(new CellStyle() { Name = "Normal", FormatId = 0U, BuiltinId = 0U });

            var differentialFormats = new DocumentFormat.OpenXml.Office2013.Excel.DifferentialFormats() { Count = 0U };

            var tableStyles = new TableStyles() { Count = 0U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleLight16" };

            var stylesheetExtensionList = new StylesheetExtensionList();

            var stylesheetExtension1 = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            stylesheetExtension1.Append(new SlicerStyles() { DefaultSlicerStyle = "SlicerStyleLight1" });

            var stylesheetExtension2 = new StylesheetExtension() { Uri = "{9260A510-F301-46a8-8635-F512D64BE5F5}" };
            stylesheetExtension2.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");
            stylesheetExtension2.Append(new TimelineStyles() { DefaultTimelineStyle = "TimeSlicerStyleLight1" });

            stylesheetExtensionList.Append(stylesheetExtension1);
            stylesheetExtensionList.Append(stylesheetExtension2);

            sp.Stylesheet.Append(fonts);
            sp.Stylesheet.Append(fills);
            sp.Stylesheet.Append(borders);
            sp.Stylesheet.Append(cellStyleFormats);
            sp.Stylesheet.Append(cellFormats);
            sp.Stylesheet.Append(cellStyles);
            sp.Stylesheet.Append(differentialFormats);
            sp.Stylesheet.Append(tableStyles);
            sp.Stylesheet.Append(stylesheetExtensionList);

            #endregion
        }
       
        private void AddCellMergeBorder(ExcelMergeParameters merge, ExcelStyleInfoType style)
        {
            if (_worksheet == null || _shareStringPart == null) return;
            var sheetData = _worksheet.GetFirstChild<SheetData>();
            if (sheetData == null) return;

            for (uint rowIndex = merge.CellFrom.ExcelRow; rowIndex <= merge.CellTo.ExcelRow; ++rowIndex)
            {
                // Ищем строку, либо добавляем ее
                Row? row = sheetData.Elements<Row>().Where(r => r.RowIndex! == rowIndex).FirstOrDefault();
                if (row == null)
                {
                    row = new Row() { RowIndex = rowIndex };
                    sheetData.Append(row);
                }

                // Все ячейки должны быть последовательно друг за другом расположены
                // нужно определить, после какой вставлять
                Cell? preveousCell = 
                    preveousCell = row
                        .Elements<Cell>()
                        .Last(rc => string.Compare(rc.CellReference!.Value, $"{merge.CellFrom.ExcelColumn}{rowIndex}", true) < 0);

                for (ExcelCell currElement = new (merge.CellFrom.ExcelColumn, rowIndex); currElement.Column <= merge.CellTo.Column; ++currElement.Column)
                {
                    string cellReference = currElement.CellReference;

                    // Ищем нужную ячейку  
                    Cell? cell = row.Elements<Cell>().Where(c => c.CellReference!.Value == cellReference).FirstOrDefault();

                    cell ??= row.InsertBefore(new Cell() { CellReference = cellReference }, preveousCell);

                    cell.StyleIndex = style.GetValue<uint>();
                    preveousCell = cell;
                }
            }
        }
        #endregion

        public void ConfigureColumns(List<ExcelColumnSettings> settings)
        {
            if (_worksheet == null || _shareStringPart == null)
            {
                return;
            }

            Columns? columns = _worksheet.GetFirstChild<Columns>();

            if (columns == null)
            {
                columns = new Columns();
                _ = _worksheet.InsertAt(columns, 0);
            }

            foreach (var column in settings)
            {
                uint cid = column.ColumnNumber;
                columns.Append(new Column { Min = cid, Max = cid, Width = column.Width, CustomWidth = true });
            }
        }

        public void InsertRow(ExcelRowParameters excelRow)
        {
            if (_worksheet == null || _shareStringPart == null)
            {
                return;
            }

            var sheetData = _worksheet.GetFirstChild<SheetData>();
            
            if (sheetData == null)
            {
                return;
            }

            // Ищем строку, либо добавляем ее
            Row? targetRow = sheetData.Elements<Row>().Where(r => r.RowIndex! == excelRow.ExcelRow).FirstOrDefault();
            if (targetRow == null)
            {
                targetRow = new Row() { RowIndex = excelRow.ExcelRow };
                sheetData.Append(targetRow);
            }

            ExcelCell currentCell = new(excelRow.Column, excelRow.Row);
            Cell preveousCell = targetRow
                .Elements<Cell>()
                .Last(rc => string.Compare(rc.CellReference!.Value, currentCell.CellReference, true) < 0);

            int sharedStringCount = _shareStringPart.SharedStringTable.Elements<SharedStringItem>().Count();

            foreach (var item in excelRow.CellsValues)
            {
                Cell? cell = targetRow
                    .Elements<Cell>()
                    .FirstOrDefault(c => c.CellReference!.Value == currentCell.CellReference);

                cell ??= targetRow.InsertAfter(new Cell() { CellReference = currentCell.CellReference }, preveousCell);

                // Получаем данные для вставки
                
                var excelValue = (CellValue)typeof(CellValue).GetConstructor([item.GetType()])!.Invoke([item]);

                // вставляем новый текст

                cell.DataType = CellHelper.GetCellValueType(item);
                cell.CellValue = excelValue;
                cell.StyleIndex = excelRow.StyleInfo.GetValue<uint>();

                _shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(excelValue));
                _shareStringPart.SharedStringTable.Save();

                preveousCell = cell;
                ++sharedStringCount;
                ++currentCell.Column;
            }
        }

        public void InsertCell(ExcelCellParameters cellParams)
        {
            if (_worksheet == null || _shareStringPart == null)
            {
                return;
            }

            var sheetData = _worksheet.GetFirstChild<SheetData>();

            if (sheetData == null)
            {
                return;
            }

            // Ищем строку, либо добавляем ее
            Row? row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex! == cellParams.Cell.ExcelRow);

            if (row == null)
            {
                row = new Row() { RowIndex = cellParams.Cell.Row };
                sheetData.Append(row);
            }

            // Ищем нужную ячейку  
            Cell? cell = row.Elements<Cell>().FirstOrDefault(c => c.CellReference!.Value == cellParams.CellReference);

            if (cell == null)
            {
                // Все ячейки должны быть последовательно друг за другом расположены
                // нужно определить, после какой вставлять
                Cell? preveousCell = row
                    .Elements<Cell>()
                    .Last(rc => string.Compare(rc.CellReference!.Value, cellParams.CellReference, true) < 0);

                cell = row.InsertBefore(
                    new Cell() { CellReference = cellParams.CellReference },
                    preveousCell);
            }

            // вставляем новый текст
            _shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new Text(cellParams.Text)));
            _shareStringPart.SharedStringTable.Save();

            if (cellParams.EndCell != null)
            {
                var merge = new ExcelMergeParameters
                {
                    CellFrom = cellParams.Cell,
                    CellTo = cellParams.EndCell
                };
                MergeCells(merge);
                AddCellMergeBorder(merge, cellParams.StyleInfo);//Стиль текущей ячейки будет применён тут
            }
            else cell!.StyleIndex = cellParams.StyleInfo.GetValue<uint>(); //Если это не объединение - стиль надо применить

            cell!.CellValue = new CellValue((_shareStringPart.SharedStringTable.Elements<SharedStringItem>().Count() - 1).ToString()); //Устанавливаем текст
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString); //Устанавливаем тип
        }

        public void MergeCells(ExcelMergeParameters excelParams)
        {
            if (_worksheet == null)
            {
                return;
            }

            MergeCells? mergeCells = _worksheet.Elements<MergeCells>().FirstOrDefault();

            if (mergeCells == null)
            {
                mergeCells = new MergeCells();

                if (_worksheet.Elements<CustomSheetView>().Any())
                {
                    _worksheet.InsertAfter(mergeCells, _worksheet.Elements<CustomSheetView>().First());
                }
                else
                {
                    _worksheet.InsertAfter(mergeCells, _worksheet.Elements<SheetData>().First());
                }
            }

            var mergeCell = new MergeCell()
            {
                Reference = new StringValue(excelParams.Merge)
            };
            mergeCells.Append(mergeCell);
        }

        public MemoryStream SaveExcel()
        {
            if (_spreadsheetDocument == null)
            {
                throw new InvalidOperationException("To save a document, it must first be created!");
            }

            _spreadsheetDocument.WorkbookPart!.Workbook.Save();
            _spreadsheetDocument.Dispose();

            if (_stream == null)
                throw new InvalidOperationException("The return stream was empty!");

            _stream.Seek(0, SeekOrigin.Begin);
            return _stream;
        }

        public void SaveExcel(string filename)
        {
            using Stream streamToWriteTo = File.Open(filename, FileMode.Create);
            MemoryStream ms = SaveExcel();
            ms.Position = 0;
            ms.CopyTo(streamToWriteTo);
        }
    }
}
