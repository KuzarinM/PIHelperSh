using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PIHelperSh.Configuration.Attributes;
using System.ComponentModel;
using System.Reflection;


namespace PIHelperSh.Configuration
{
    /// <summary>
    /// Расширения извлекающее параметры из переменных окружения
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Получение параметров из переменных окружения и файла конфигурации
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureWithENV<T>(this IServiceCollection services, IConfiguration configuration) where T : class
        {
            IConfigurationSection section = configuration.GetSection(typeof(T).Name);

            if (configuration.GetSection(typeof(T).Name.Replace("Configuration", "")).Exists())
                section = configuration.GetSection(typeof(T).Name.Replace("Configuration", ""));

            Type type = typeof(T);
            var defaultValue = type.GetConstructor(Type.EmptyTypes)!.Invoke(Type.EmptyTypes);

            foreach (var field in type.GetProperties())
            {
                bool flag = false;

                string? value = section![field.Name] ?? null;


                foreach (var attrubute in field!.GetCustomAttributes<FromEnvironmentAttribute>())
                {
                    if (attrubute == null)
                        continue;
                    
                    flag = true;

                    var variableName = attrubute.VariableName;

                    if (string.IsNullOrEmpty(variableName))
                        variableName = $"{type.Name.ToConstantCase()}_{field.Name.ToConstantCase()}";
                    
                    value = Environment.GetEnvironmentVariable(variableName) ?? value;
                }

                if (!flag) //На случай, если никто не повесил анотацию для конфигурации с ENV. 
                    value = Environment.GetEnvironmentVariable($"{type.Name.ToConstantCase()}_{field.Name.ToConstantCase()}") ?? value;
                
                if (string.IsNullOrEmpty(value))
                    value = field.GetValue(defaultValue)?.ToString();
                
                if (section is not null)
                    section[field.Name] = value;
            }

            services.Configure<T>(section!);

            return services;
        }

        /// <summary>
        /// Добавить классы конфигурации помеченные атрибутом AutoConfiguration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x=>x.GetTypes()))
            {
                foreach (var attribute in type.GetCustomAttributes<AutoConfigurationAttribute>())
                {
                    if (attribute == null)
                        continue;
                    
                    services.ConfigureWithENV(type, configuration);
                }
            }

            return services;
        }

        /// <summary>
        /// Инициализировать поля отмеченные атрибутом Constant
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IConfiguration AddConstants(this IConfiguration configuration)
        {
            IterateAllTypes(type =>
            {
                foreach (var field in type.GetRuntimeFields())
                {
                    var attribute = field.GetCustomAttribute<ConstantAttribute>();

                    if (attribute == null)
                        continue;
                    
                    var convert = TypeDescriptor.GetConverter(field.FieldType);
                    field.SetValue(null, convert.ConvertFromString(GetConstant(configuration, attribute, field)));
                }

                foreach (var field in type.GetRuntimeProperties())
                {
                    var attribute = field.GetCustomAttribute<ConstantAttribute>();

                    if (attribute == null)
                        continue;

                    var convert = TypeDescriptor.GetConverter(field.PropertyType);
                    field.SetValue(null, convert.ConvertFromString(GetConstant(configuration, attribute, field)));
                }
            },
            typeof(TrackedTypeAttribute));

            return configuration;
        }

        /// <summary>
        /// Получение параметров из переменных окружения и файла конфигурации используя объект типа
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static IServiceCollection ConfigureWithENV(this IServiceCollection services, Type type, IConfiguration configuration)
        {
             typeof(Configuration) // this
             .GetMethods(BindingFlags.Public | BindingFlags.Static)
             .FirstOrDefault(x => x.Name == nameof(ConfigureWithENV) && x.IsGenericMethod)!
             .MakeGenericMethod(type).Invoke(null, [services, configuration]);
            return services;
        }

        private static string GetConstant(IConfiguration configuration, ConstantAttribute attribute, MemberInfo field)
        {
            string value = string.Empty;

            if (!string.IsNullOrEmpty(attribute.VariableName))
                value = Environment.GetEnvironmentVariable(attribute.VariableName) ?? value;


            if (string.IsNullOrEmpty(value))
            {
                string name = attribute.ConstantName;
                if (string.IsNullOrEmpty(name))
                    name = field.Name.ToPascalCase();

                value = Environment.GetEnvironmentVariable($"{attribute.BlockName.ToConstantCase()}_{name.ToConstantCase()}") 
                    ?? configuration.GetSection(attribute.BlockName)[name] 
                    ?? string.Empty;
            }

            return value;
        }

        private static void IterateAllTypes(Action<Type> func, Type? filterAttribute = null)
        {
            foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x=>x.GetTypes()))
            {
                if (filterAttribute is null)
                {
                    func(type);
                    continue;
                }

                foreach (var attribute in type.GetCustomAttributes(filterAttribute))
                {
                    if (attribute == null || attribute.GetType() != filterAttribute)
                        continue;
                        
                    func(type);
                }
            }
        }
    }
}
