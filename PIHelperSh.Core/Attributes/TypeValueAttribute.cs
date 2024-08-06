namespace PIHelperSh.Core.Attributes
{
    /// <summary>
    /// Позволяет привязывать к enum объекты любых типов
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class TypeValueAttribute<T> : Attribute
    {
        /// <summary>
        /// Значение
        /// </summary>
        public T Value { get; private set; }

        public TypeValueAttribute(T value)
        {
            Value = value;
        }
    }
}
