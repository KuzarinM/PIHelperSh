using System.Reflection;
using PIHelperSh.Core.Attributes;

namespace PIHelperSh.Core.Extensions
{
	public static class EnumExtension
	{
		/// <summary>
		/// Получить значение из перечисления. Если такогового не найдено - вернётся default(T)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static T GetValue<T>(this Enum value)
		{
			Type type = value.GetType();

			FieldInfo? fieldInfo = type.GetField(value.ToString());

			if (fieldInfo != null)
			{
				var attribs = fieldInfo.GetCustomAttribute(typeof(TypeValueAttribute<T>), false) as TypeValueAttribute<T>;

				return attribs != null && attribs != null ? attribs.Value : default;
			}
			return default;
		}

		/// <summary>
		/// Создание объекта перечисления из зрачения. Если такового нет, то вернётся default(T)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		public static T CreateEnumFromValue<T>(this object target) where T : Enum
		{
			var targetType = typeof(TypeValueAttribute<>).MakeGenericType(target.GetType());

			foreach (var item in Enum.GetValues(typeof(T)))
			{
				var customAttr = item.GetType().GetCustomAttribute(targetType, false);
				if (customAttr != null && customAttr.GetType().GetProperty("Value")?.GetValue(customAttr) == target)
				{
					return (T)item;
				}
			}
			return default;
		}
	}
}
