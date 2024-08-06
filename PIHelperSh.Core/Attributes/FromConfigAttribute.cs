namespace PIHelperSh.Core.Attributes
{
    /// <summary>
    /// Атрибут для получения значения из конфигурации или переменных среды
    /// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FromConfigAttribute: Attribute
    {
        public string? EnvironmentValueName = null;

        public string? ConfigValueName = null;
    }
}
