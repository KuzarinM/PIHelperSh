using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.AI.Core.Models.ImageGenerator
{
    public class AIPromtForImage : AIPromt
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public string WithoutText { get; set; }
    }
}
