using PIHelperSh.PdfCreator.Enums;

namespace PIHelperSh.PdfCreator.Models.Properties;


/// <summary>
/// Свойства гиперссылки
/// </summary>
public class HyperlinkProperties(string text, string link, PIHyperlinkType type)
{
    /// <summary>
    /// Текст, в который будет встроена гиперссылка
    /// </summary>
    public string Text { get; set; } = text;
    
    /// <summary>
    /// Ссылка
    /// </summary>
    public string Link { get; set; } = link;
    
    /// <summary>
    /// Тип гиперссылки
    /// </summary>
    public PIHyperlinkType Type { get; set; } = type;
}