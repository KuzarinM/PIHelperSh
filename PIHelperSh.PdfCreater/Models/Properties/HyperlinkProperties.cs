using PIHelperSh.PdfCreator.Enums;

namespace PIHelperSh.PdfCreator.Models.Properties;


/// <summary>
/// Свойства гиперссылки
/// </summary>
public class HyperlinkProperties()
{
    /// <summary>
    /// Текст, в который будет встроена гиперссылка
    /// </summary>
    public string Text { get; set; } = string.Empty;
    
    /// <summary>
    /// Ссылка
    /// </summary>
    public string Link { get; set; } = string.Empty;
    
    /// <summary>
    /// Тип гиперссылки
    /// </summary>
    public PIHyperlinkType Type { get; set; } = PIHyperlinkType.None;
}