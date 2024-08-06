using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.AI.Core.Models
{
    public class AIChatHistory
    {
        public AIPromt SystemPromt { get; set; }

        public IEnumerable<AIChatRecord> History { get; set; }
    }
}
