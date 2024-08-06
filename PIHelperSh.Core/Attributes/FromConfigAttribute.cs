namespace PIHelperSh.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FromConfigAttribute: Attribute
    {
        public string? EnvironmentValueName = null;

        public string? ConfigValueName = null;
    }
}
