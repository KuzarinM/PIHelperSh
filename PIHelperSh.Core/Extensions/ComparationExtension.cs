using PIHelperSh.Core.Attributes;
using PIHelperSh.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.Core.Extensions
{
    public static class ComparationExtension
    {
        public static bool Compare<T>(this T source, T target) => Compare((object?)source, (object?)target);

        public static bool Compare(this object? source, object? target)
        {
            if(source == null || target == null) 
                return false;

            if (source.GetType() != target.GetType())
                return false;

            var type = source.GetType();

            if (type.IsValueType || type == typeof(string))
                return object.Equals(source, target);

            var fields = type.GetFields();
            var props = type.GetProperties();

            if(fields.Length == 0 && props.Length == 0)
                return object.Equals(source, target);

            return
                fields.All(x =>
                {
                    var custom = x.GetCustomAttribute<CustomComparationAttribute>();

                    if (custom == null || custom.Mode == CustiomComparationMode.Default)
                        return Compare(x.GetValue(source), x.GetValue(target));
                    switch (custom.Mode)
                    {
                        case CustiomComparationMode.Ignore:
                            return true;
                        case CustiomComparationMode.Comparator:
                            var a = x.GetValue(source);
                            var b = x.GetValue(target);
                            if (a == null || b == null)
                                return false;
                            return custom.Comparer(a, b);
                        case CustiomComparationMode.Equals:
                            return object.Equals(x.GetValue(source), x.GetValue(target));
                    }
                    return false;
                }) &&
                props.All(x =>
                {
                    var custom = x.GetCustomAttribute<CustomComparationAttribute>();

                    if (custom == null || custom.Mode == CustiomComparationMode.Default)
                        return Compare(x.GetValue(source), x.GetValue(target));
                    switch (custom.Mode)
                    {
                        case CustiomComparationMode.Ignore:
                            return true;
                        case CustiomComparationMode.Comparator:
                            var a = x.GetValue(source);
                            var b = x.GetValue(target);
                            if(a == null || b == null)
                                return false;
                            return custom.Comparer(a,b);
                        case CustiomComparationMode.Equals:
                            return object.Equals(x.GetValue(source), x.GetValue(target));
                    }
                    return false;
                });
        }
    }
}
