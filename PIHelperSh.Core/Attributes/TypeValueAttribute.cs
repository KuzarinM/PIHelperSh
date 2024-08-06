namespace PIHelperSh.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class TypeValueAttribute<T> : Attribute
    {
        public T Value { get; private set; }

        public TypeValueAttribute(T value)
        {
            Value = value;
        }
    }
}
