# Библиотека PDF
Данная библиотека является надстройкой над MigraDoc  и упрощает работу с PDF  в контексте ряда операций.
Основным интерфейсом системы является **IPdfCreator**. На данный момент имеется единственная реализация - **PdfCreator**
## Основные методы IPdfCreator
 - **AddParagraph(PdfParagraph paragraph)** данный метод предназначен для добавления на лист PDF параграфа текста. Принимает стандартную модель параграфа (о ней позже)
 - **AddList(PdfList List)** данный метод предназначен для добавления на лист PDF маркированного списка. Принимает стандартныую модель списка ( о ней позже). `Поддерживается не более 3-х уровней!`
 - **AddChart(PdfPieChartModel pieChart)** данный метод предназначен для добавления на лист PDF круговой диаграммы. Принимает модель диаграммы (о ней позже)
 - **AddImage(PdfImage image)** данный метод предназначен для добавления на лист изображения. Принимает стандартную модель (о ней позже). 
 - **MemoryStream SavePdf()** данный метод сохраняет PDF документ и возвращает поток данных файла.
 - **SavePdf(string filename)** данный метод сохраняет PDF документ на диске по пути из filename
 - **AddTable\<T>(PdfTable\<T> header, bool rowHeaded = false)** данный метод позволяет добавлять на лист таблицу с шапкой и значениями. **rowHeaded** позволяет описать будет ли шапка в строках или в столбцах.
 - **AddSimpleTable(PDFSimpleTable tableData)** данный метод позволяет добавть на лист простую табличку, строки в которой задаются пользователем самостоятельно.
 ## Основные модели
 ### PdfParagraph 
 Модель имеет следующие поля:
 - **PdfMargin MarginAfter** - Отступ после параграфа. По умолчанию значение *Medium*
 - **PdfAlignmentType ParagraphAlignment** - Выравнивание параграфа. По умолчанию значение *Left*
 - **PdfStyleType Style** - Стиль параграфа. По сути объединение шрифта, жирности и отступов. По умолчанию - Basic. На данный момент имеются следующие (В дальнейшем планируется добавить возможность добавлять свои стили):
	- *Basic*: Times New Roman 14
	- *Title*: Times New Roman 18 жирный
	- *Bold*: Times New Roman 14 жирный
	- *ListLevel1*:  Times New Roman 14 отступ 1.5cm
	- *ListLevel2*: Times New Roman 14 отступ 3.0cm
	- *ListLevel3*: Times New Roman 14 отступ 4.5cm
	- *Small*: Times New Roman 11
 - **string Text** - Текст параграфа
 - **HyperlinkProperties Hyperlink** - Свойства гиперссылки. По умолчанию отсутствует
### PdfList 
Данная модель позволяет создать многоуровневый список. 
 - **PdfMargin MarginAfter** - подробно описан ранее
 - **List\<IPdfElement> Content** - элементы списка. Это могут быть как параграфы, так и другие списки
### PdfPieChartModel 
Данная модель позволяет добавлять диаграмму на лист
 - **double Width** - Ширина диаграммы на листе. По умолчанию 16.
 - **double Height** - Высота диаграммы на листе. По умолчанию 12.
 - **string ChartName** - Надпись над диаграммой. По сути её имя.
 -  **PdfStyleType HeaderStyle** - Стиль заголовка диаграммы.
 - **PdfAlignmentType HeaderAlignment** - Выравнивание заголовка диаграммы.
 -  **PdfLegendPosition LegendPosition** - Местоположение легенды диаграммы.
 - **List\<PdfPieChartData> DataSet** - Набор данных для диаграммы
### PdfPieChartData:
 - **string DisplayName** - Имя для значения
 - **double Value** - Значение 
 - **Color? Color** - Если нужно, цвет для значения
### PdfImage
Моделька для вывода изображения на лист. Имеет поля:
 - **MigradocImage Image** - само изображение (обёртка)
 - **int? Width** - Ширина изображения (если null - по размеру изображения)
 - **int? Height** - Высота сообщения (если null - по соотношению сторон к длине)
 - **PdfAlignmentType ImageAlignment** - Выравнивание изображения (по умолчанию - Left)
 - **PdfMargin MarginAfter** - Отступ после. По умолчанию Medium
### MigradocImage
Обёртка, предназначенная для вывода изображений в MigraDock
 Объект создаётся только с помощью статических методов:
 - **CreateFromPath(string path)** - создаём из картинки, расположенной на компьютере по пути
 - **MigradocImage CreateFromBase64(string base64)** - создаём из base64 строки, в которой зашифрована картинка
 - **MigradocImage CreateFromStream(Stream stream)** - создаём из потока картинки
### PdfTable
Сейчас будет немного сложно. Данная модель предназначена для создания таблицы из коллекции объектов определённого типа, при этом шапка таблицы поддерживает 2 уровня(то есть, ряд столбцов могут быть сгруппированы общим заголовком). Поля:
 - **List\<IPdfColumnItem>? Header** - модельки заголовков таблицы
 - **PdfStyleType HeaderStyle** - Стиль заголовка таблицы. По умолчанию - Bold
 - **PdfAlignmentType HeaderHorisontalAlignment**  - Выравнивание текста внутри заголовков. По умолчанию - по центру
 - **PdfStyleType RecordStyle** - Стиль текста записей строк таблицы. По умолчанию - Basic 
 - **PdfAlignmentType RecordHorisontalAlignment** - Выравнивае записей строк таблицы внутри ячейки. По умолчанию - Left
 - **List\<T> Records** - список записей, из которых формируем таблицу
 ### PdfTableColumn
 Столбец таблицы:
 - **string? Title** - заголовок 
 - **float? Size** - ширина/высота ячейки
 - **string PropertyName** - Имя свойства, которое будет заполнять данный столбец/строку
 ### PdfTableColumnGroup
 Сгруппированные столбцы/строки заголовка
 Поля:
 - **string? Title** - заголовок этой группы
 - **List\<PdfTableColumn> InnerColumns** - внутренние столбцы/строки
 ### PDFSimpleTable
 - **List<IPdfColumnItem>? Header** - Заголовок таблицы. Уже был ранее описан
 - **PdfStyleType HeaderStyle** - Стиль заголовка таблицы. По умолчанию - Bold
 - **PdfAlignmentType HeaderHorisontalAlignment**  - Выравнивание текста внутри заголовков. По умолчанию - по центру
 - **List<PDFSimpleTableRow> Rows** - непосредственно строки таблицы
 - **PdfStyleType RowStyle** - ситль текста таблицы по умолчанию. Может быть переопределён в каждой строке
 - **PdfAlignmentType RowHorisontalAlignment** - выравнивание текста строки таблицы по умолчанию. Может быть переопределён
 - **PdfMargin MarginAfter** - Отступ после. По умолчанию None
 - **bool ChangePageOrientation** - флаг, меняем ли мы ориентацию листа на альбомную
### PDFSimpleTableRow
 - **List<string>** Items - элементы строки таблицы
 - **PdfStyleType? Style** - возможность переопределить стиль данной строки. Если Null - берётся стиль по умолчанию
 - **PdfAlignmentType? Alignment** - возможность переопределить выравниваение данной строки. Если Null - берётся стиль по умолчанию