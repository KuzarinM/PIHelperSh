using System.ComponentModel;
using System;
using Newtonsoft.Json;

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

        /// <summary>
        /// В случае существования конвертации из string - она будет применена
        /// </summary>
        /// <param name="value"></param>
        public TypeValueAttribute(string value)
        {
            try
            {
                var convert = TypeDescriptor.GetConverter(typeof(T));
                var res = convert.ConvertFromString(value);
                if (res != null)
                    Value = (T)res!;
            }
            catch (Exception ex) 
            {
                //todo пока не придумал, как такое отруливать
            }
            try
            {
                if (Value == null)
                {
                    var obj = JsonConvert.DeserializeObject<T>(value);
                    if (obj != null)
                        Value = obj!;
                    else
                        Value = default(T);
                }
            }
            catch (Exception ex)
            {
                //todo пока не придумал, как такое отруливать
            }
        }
    }
}
