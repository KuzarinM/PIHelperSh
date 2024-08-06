using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using PIHelperSh.WordCreator.Enums;
using PIHelperSh.WordCreator.Interfaces;
using PIHelperSh.WordCreator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.WordCreator
{
    public class WordCreator : IWordCreator
    {
        private WordprocessingDocument? _wordDocument;
        protected NumberingDefinitionsPart? _numberingPart;//Тут хранится списки (Заполняется только по необходимости см. InitLists() )
        private Body? _docBody;
        private MemoryStream? stream;
        private WordTextProperties _defaultTextProperties = new()
        {
            Size = 14,
            Bold = false,
            JustificationType = WordJustificationType.Both
        };

        public WordCreator()
        {
            stream = new MemoryStream();

            _wordDocument = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document);
            MainDocumentPart mainPart = _wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();
            _docBody = mainPart.Document.AppendChild(new Body());
        }

        #region Служебные методы, необходимые для записи в word документ
        private static JustificationValues GetJustificationValues(WordJustificationType type) => type switch
        {
            WordJustificationType.Both => JustificationValues.Both,
            WordJustificationType.Center => JustificationValues.Center,
            _ => JustificationValues.Left,
        };

        private static SectionProperties CreateSectionProperties()
        {
            return new SectionProperties(new PageSize
            {
                Orient = PageOrientationValues.Portrait
            });
        }

        private static ParagraphProperties? CreateParagraphProperties(WordTextProperties? paragraphProperties)
        {
            if (paragraphProperties == null)
            {
                return null;
            }

            var properties = new ParagraphProperties();
            if (paragraphProperties.JustificationType.HasValue)
            {
                properties.AppendChild(new Justification()
                {
                    Val = GetJustificationValues(paragraphProperties.JustificationType.Value)
                });
            }

            properties.AppendChild(new SpacingBetweenLines
            {
                LineRule = LineSpacingRuleValues.Auto
            });

            properties.AppendChild(new Indentation());

            var paragraphMarkRunProperties = new ParagraphMarkRunProperties();
            if (paragraphProperties.Size.HasValue)
            {
                paragraphMarkRunProperties.AppendChild(new FontSize { Val = (paragraphProperties.Size.Value * 2).ToString() });//todo ERROR По неизвестной науке причине 10 шрифт в коде, это 5 в word.
            }
            properties.AppendChild(paragraphMarkRunProperties);

            return properties;
        }

        private Paragraph? MakeParagraph(WordParagraph paragraph)
        {
            var docParagraph = new Paragraph();

            docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));

            foreach (var run in paragraph.Texts)
            {
                var docRun = new Run();
                var properties = new RunProperties();

                //todo WARNING По не совсем ясной причине, настройки шрифтов не применяются к прогонам параграфа. Поэтому, их необходимо выставлять вручную.
                if (run.properties != null && run.properties.Size.HasValue)
                {
                    properties.AppendChild(new FontSize { Val = (run.properties.Size.Value * 2).ToString() });
                }
                else if (paragraph.TextProperties != null && paragraph.TextProperties.Size.HasValue)
                {
                    properties.AppendChild(new FontSize { Val = (paragraph.TextProperties.Size.Value * 2).ToString() });
                }
                if (run.properties != null && run.properties.Bold.HasValue && run.properties.Bold.Value ||
                    paragraph.TextProperties != null && paragraph.TextProperties.Bold.HasValue && paragraph.TextProperties.Bold.Value)
                {
                    properties.AppendChild(new Bold());
                }
                docRun.AppendChild(properties);

                docRun.AppendChild(new Text { Text = run.Item1, Space = SpaceProcessingModeValues.Preserve });

                docParagraph.AppendChild(docRun);
            }
            return docParagraph;
        }
        //Если что, именно тут настраиваются уровни
        private void InitLists()
        {
            if (_wordDocument == null)
            {
                throw new InvalidOperationException("Сначала создайте документ!");
            }
            _numberingPart = _wordDocument.MainDocumentPart.AddNewPart<NumberingDefinitionsPart>("documentsLIsts");//Ну, тут не может она null быть, так как иначе это совсем что-то странное

            Level firstLevel = new Level(
                new NumberingFormat() { Val = NumberFormatValues.Decimal },
                new LevelText() { Val = "%1." }
                )
            {
                LevelIndex = 0,
                StartNumberingValue = new StartNumberingValue() { Val = 1 }
            };
            Level secondLevel = new Level(
                new NumberingFormat() { Val = NumberFormatValues.Bullet },
                new LevelText() { Val = "●" }
                )
            {
                LevelIndex = 1,
            };
            Level thridLavel = new Level(
                new NumberingFormat() { Val = NumberFormatValues.Bullet }
                )
            {
                LevelIndex = 2,
            };
            //todo INFO Иного способа сделать списки Нормальными(с отступами) не существует. openXML сам такое не умеет
            //Все данные взятия из документа word(созданного силами самого редактора word) со списками(пришлось лесть в xml структуру).
            firstLevel.AppendChild(new ParagraphProperties(new Indentation
            {
                Left = "720",
                Hanging = "360"
            }));
            secondLevel.AppendChild(new ParagraphProperties(new Indentation
            {
                Left = "1440",
                Hanging = "360"
            }));
            thridLavel.AppendChild(new ParagraphProperties(new Indentation
            {
                Left = "2160",
                Hanging = "360"
            }));

            var num = new AbstractNum(firstLevel, secondLevel, thridLavel);
            num.AbstractNumberId = 1;

            var element = new Numbering(num, new NumberingInstance(new AbstractNumId { Val = 1 }) { NumberID = 1 });
            element.Save(_numberingPart);
        }

        private void MakeList(MarkeredList list, int level = 0)
        {
            if (_docBody == null)
                return;

            int listId = 1;

            foreach (var line in list.childs)
            {
                if (line is WordParagraph pLine)
                {
                    var paragraph = MakeParagraph(pLine);
                    if (paragraph == null)
                        continue;
                    var numProp = new NumberingProperties(
                        new NumberingLevelReference() { Val = line.RollLavel.HasValue ? line.RollLavel : level },
                        new NumberingId() { Val = listId }); ;
                    if (paragraph.ParagraphProperties == null)
                    {
                        throw new ArgumentNullException("This situation is impossible, if you create paragraph with TextProperties properties");
                    }
                    paragraph.ParagraphProperties.AppendChild(numProp);
                    _docBody.AppendChild(paragraph);
                }
                else if (line is MarkeredList mList)
                {
                    MakeList(mList, level + 1);
                }
            }

        }

        #endregion

        public WordTextProperties DefaultTextProperies
        {
            get => _defaultTextProperties;
            set => _defaultTextProperties = value;
        }

        public void AddMarkeredList(MarkeredList list)
        {
            if (_docBody == null)
            {
                throw new InvalidOperationException("Сначала создайте документ");
            }
            if (_numberingPart == null)
            {
                InitLists();
            }
            MakeList(list);
        }

        public void AddParagraph(WordParagraph paragraph)
        {
            if (_docBody == null || paragraph == null)
            {
                return;
            }
            var docParagraph = MakeParagraph(paragraph);
            if (paragraph == null)
                return;
            _docBody.AppendChild(docParagraph);

        }

        public MemoryStream SaveWord()
        {
            if (_docBody == null || _wordDocument == null)
            {
                throw new InvalidOperationException("To save a document, it must first be created!");
            }
            _docBody.AppendChild(CreateSectionProperties());

            _wordDocument.MainDocumentPart!.Document.Save();

            _wordDocument.Dispose();

            if (stream == null)
                throw new InvalidOperationException("The return stream was empty!");

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public void SaveWord(string filename)
        {
            using Stream streamToWriteTo = File.Open(filename, FileMode.Create);
            MemoryStream ms = SaveWord();
            ms.Position = 0;
            ms.CopyTo(streamToWriteTo);
        }
    }
}
