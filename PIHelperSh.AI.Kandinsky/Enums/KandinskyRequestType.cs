using PIHelperSh.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.AI.Kandinsky.Enums
{
    public enum KandinskyRequestType
    {
        [TypeValue<string>("GENERATE")]
        Generate
    }
}
