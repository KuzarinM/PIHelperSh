using PIHelperSh.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.AI.Kandinsky.Enums
{
    public enum KandinskyStyles
    {
        [TypeValue<string>("KANDINSKY")]
        Kandinsky,
        [TypeValue<string>("UHD")]
        Detailed,
        [TypeValue<string>("ANIME")]
        Anime,
        [TypeValue<string>("DEFAULT")]
        No
    }
}
