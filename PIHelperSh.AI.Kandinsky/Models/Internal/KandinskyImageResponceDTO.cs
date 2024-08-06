using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.AI.Kandinsky.Models.Internal
{
    internal class KandinskyImageResponceDTO
    {
        public Guid uuid {  get; set; }

        public string status { get; set; }

        public List<string>? images { get; set; }

        public bool? censored { get; set; }

        public int? generationTime { get; set; }
    }
}
