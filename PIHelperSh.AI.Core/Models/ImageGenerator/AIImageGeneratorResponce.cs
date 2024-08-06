using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.AI.Core.Models.ImageGenerator
{
    public class AIImageGeneratorResponce
    {
        public string Id { get; set; }

        public Stream? Image { get; set; }
    }
}
