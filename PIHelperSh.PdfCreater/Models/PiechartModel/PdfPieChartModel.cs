using PIHelperSh.PdfCreator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreator.Models.PiechartModel
{
    /// <summary>
    /// Модель круговой диаграммы
    /// </summary>
    public class PdfPieChartModel
    {
        /// <summary>
        /// Ширина диаграммы в см по умолванию на всю ширину
        /// </summary>
        public double Width { get; set; } = 16;

        /// <summary>
        /// Высота диаграммы в см 
        /// </summary>
        public double Height { get; set; } = 12;

        /// <summary>
        /// Заголовок диаграммы. Будет отбражён над ней
        /// </summary>
        public string ChartName { get; set; }

        /// <summary>
        /// Стиль заголовка
        /// </summary>
        public PdfStyleType HeaderStyle { get; set; } = PdfStyleType.Bold;

        /// <summary>
        /// Выравнивание заголовка по горизонтали
        /// </summary>
        public PdfAlignmentType HeaderAlignment { get; set; } = PdfAlignmentType.Center;

        /// <summary>
        /// Вариант расположения легенды
        /// </summary>
        public PdfLegendPosition LegendPosition { get; set; } = PdfLegendPosition.Bottom;

        /// <summary>
        /// Набор данных для создания диаграммы
        /// </summary>
        public List<PdfPieChartData> DataSet { get; set; }
    }
}
