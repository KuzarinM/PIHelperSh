using PIHelperSh.PdfCreator.Enums;

namespace PIHelperSh.PdfCreator.Models.Properties;


/// <summary>
/// Свойства гиперссылки
/// </summary>
public class HyperlinkProperties(string link, PIHyperlinkType type)
{
    /// <summary>
    /// Ссылка
    /// </summary>
    public string Link { get; set; } = link;
    
    /// <summary>
    /// Тип гиперссылки
    /// </summary>
    public PIHyperlinkType Type { get; set; } = type;
}