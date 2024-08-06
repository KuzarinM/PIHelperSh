using PIHelperSh.Core.Attributes;
using System.ComponentModel;
using System.Reflection;

namespace PIHelperSh.Core.Extensions
{
	public static class ConfigurationExtension
	{
		public static void LoadConfiguration()
		{
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (var type in assembly.GetTypes())
				{
					foreach (var property in type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
					{
						try
						{
							var attr = property.GetCustomAttribute<FromConfigAttribute>();

							if (attr != null)
							{
								var targetType = property.PropertyType;
								string? value = null;
								if (value == null && attr.EnvironmentValueName != null)
								{
									value = Environment.GetEnvironmentVariable(attr.EnvironmentValueName);
								}

								if (value == null && attr.ConfigValueName != null)
								{
									value = "234";
								}

								if (value != null)
								{
									TypeConverter typeConverter = TypeDescriptor.GetConverter(targetType);
									property.SetValue(null, typeConverter.ConvertFrom(value));
								}
							}
						}
						catch (Exception ex)
						{
							int a = 0;
						}
					}
				}
			}
		}
	}
}
