using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
