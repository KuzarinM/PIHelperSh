﻿using System.Collections;
using System.Text;

namespace PIHelperSh.Core.Extensions
{
	public static class PrintExtension
	{
		/// <summary>
		/// Превращает объект в некоторое подобие json для отладки и тестирования
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string toText(this object obj) => Print(obj, 0);

		private static string Print(object obj, int level)
		{
			if (obj == null) return "null";

			Type t = obj.GetType();

			if (t.IsPrimitive) return obj.ToString();
			if (t == typeof(string)) return $"\"{obj.ToString()}\"";
			if (t.IsEnum) return obj.ToString();
			if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
			{
				return $"{Print(t.GetProperty("Key").GetValue(obj), level)} : {Print(t.GetProperty("Value").GetValue(obj), level)}";
			}

			StringBuilder sb = new StringBuilder();

			if (t.GetInterfaces().Any(x => x.Name == "IEnumerable"))
			{
				var s = (IEnumerator)typeof(IEnumerable).GetMethod("GetEnumerator").Invoke(obj, null);
				string? tmp = _outputList(s, level);
				if (tmp != null) return tmp;
				return "null";
			}


			sb.AppendLine("{");
			foreach (var field in t.GetFields().Where(x => !x.Name.StartsWith("_") && x.IsStatic == false && x.FieldType != t))
			{
				var tmp = _outputData($"{field.FieldType.Name} {field.Name}", field.GetValue(obj), level + 1);
				if (tmp != null) sb.Append(tmp);

			}
			foreach (var property in t.GetProperties().Where(x => !x.Name.StartsWith("_") && x.PropertyType != t))
			{
				var tmp = _outputData($"{property.PropertyType.Name} {property.Name}", property.GetValue(obj), level + 1);
				if (tmp != null) sb.Append(tmp);
			}
			sb.Append(_printOnLavel("}", level));
			return sb.ToString();
		}

		private static string _printOnLavel(string str, int lavel, string lavelSumvol = "  ")
		{
			return $"{lavelSumvol.Multiply(lavel)}{str}";
		}

		private static string _outputList(IEnumerator enumerator, int lavel)
		{
			try
			{
				bool flag = false;
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("[");

				while (enumerator.MoveNext())
				{
					try
					{
						sb.AppendLine(_printOnLavel($"{Print(enumerator.Current, lavel + 1)},", lavel + 1));
						flag = true;
					}
					catch { }
				}
				if (!flag) return null;
				sb.Append(_printOnLavel("]", lavel - 1));
				return sb.ToString();
			}
			catch (Exception ex)
			{
				return "null";
			}
		}

		private static string? _outputData(string columnName, object data, int lavel)
		{
			StringBuilder sb = new StringBuilder();
			try
			{
				sb.AppendLine(_printOnLavel($"{columnName}: {Print(data, lavel + 1)},", lavel));
			}
			catch
			{
				return null;
			}
			return sb.ToString();
		}
	}
}
