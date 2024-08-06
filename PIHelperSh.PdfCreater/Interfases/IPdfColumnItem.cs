using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.PdfCreater.Interfases
{
    public interface IPdfColumnItem
    {
        public float? Size { get; }

        public string? Title { get; set; }
    }
}
