using DocumentFormat.OpenXml.Spreadsheet;

namespace PIHelperSh.ExcelCreator.Models
{
    public class ExcelCellValue
    {
        private object _value;

        public ExcelCellValue(object value)
        {
            _value = value;   
        }

        public CellValue Value
        {
            get
            {
                var constructor = typeof(CellValue).GetConstructor([_value.GetType()]);
                if (constructor is not null)
                {
                    return (CellValue)constructor.Invoke([_value]);
                }
                return new CellValue(_value.ToString()!);
            }
        }

        public CellValues Type {
            get
            {
                if (_value.GetType() == typeof(int) || _value.GetType() == typeof(decimal) || _value.GetType() == typeof(double))
                    return CellValues.Number;

                if (_value.GetType() == typeof(bool))
                    return CellValues.Boolean;

                if (_value.GetType() == typeof(DateTime) || _value.GetType() == typeof(DateTimeOffset))
                    return CellValues.Date;

                return CellValues.SharedString;
            }
        }
    }
}
