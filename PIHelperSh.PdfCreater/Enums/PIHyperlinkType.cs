using MigraDoc.DocumentObjectModel;
using PIHelperSh.Core.Attributes;
using HyperlinkType = MigraDoc.DocumentObjectModel.HyperlinkType;
namespace PIHelperSh.PdfCreator.Enums;


/// <summary>
/// Тип гиперссылки
/// </summary>
public enum PIHyperlinkType
{
    /// <summary>
    /// Отсутствие гиперссылки
    /// </summary>
    None,
    
    /// <summary>
    /// Ссылка на закладку
    /// </summary>
    [TypeValue<HyperlinkType>(HyperlinkType.Bookmark)]
    Bookmark,
    
    /// <summary>
    /// Ссылка на внешнюю закладку
    /// </summary>
    [TypeValue<HyperlinkType>(HyperlinkType.ExternalBookmark)]
    ExternalBookmark,
    
    /// <summary>
    /// Ссылка на внешний документ
    /// </summary>
    [TypeValue<HyperlinkType>(HyperlinkType.EmbeddedDocument)]
    EmbeddedDocument,
    
    /// <summary>
    /// Ссылка на интернет-ресурс
    /// </summary>
    [TypeValue<HyperlinkType>(HyperlinkType.Url)]
    Url,
    
    /// <summary>
    /// Ссылка на файл
    /// </summary>
    [TypeValue<HyperlinkType>(HyperlinkType.File)]
    File
}