using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FromConfigAttribute: Attribute
    {
        public string? EnvirmentValueName = null;

        public string? ConfigValueName = null;
    }
}
