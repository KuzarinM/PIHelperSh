using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.Configuration.Attributes
{
    /// <summary>
    /// Атрибут статического поля подтягивающегося из Environment Variables и appsettings.json 
    /// </summary>
    public class ConstantAttribute : Attribute
    {
        /// <summary>
        /// Название константы по умолчанию имя переменной в Паскаль кейсе
        /// </summary>
        public string ConstantName { get; set; } = string.Empty;

        /// <summary>
        /// Имя блока в appsettings, укажите string.Empty если не хотите получать поле из appsettings.json
        /// по умолчанию: "AppConstants"
        /// </summary>
        public string BlockName { get; set; } = "AppConstants";

        /// <summary>
        /// Название в переменных окружения
        /// </summary>
        public string VariableName { get; set; } = string.Empty;
    }
}
