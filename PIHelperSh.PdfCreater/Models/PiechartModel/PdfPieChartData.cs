using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreator.Models.PiechartModel
{
    /// <summary>
    /// Элемент данных. Одно значение для диаграммы.
    /// </summary>
    public class PdfPieChartData
    {
        /// <summary>
        /// Название варианта
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Значение варианта(по ним строят диаграмму)
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Цвет области на диаграме. При null будет использоватсся выдача цветов по умолчанию)
        /// </summary>
        public Color? Color { get; set; } = null;

        public PdfPieChartData() { }

        public PdfPieChartData(string displayName, double value)
        {
            DisplayName = displayName;
            Value = value;
        }
    }
}
