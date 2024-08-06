
using PIHelperSh.Core.Attributes;

namespace PIHelperSh.AI.GroqCloud.Models.Enums
{
    internal enum AIMessageRole
    {
        [TypeValue<string>("system")]
        System,

        [TypeValue<string>("user")]
        User,

        [TypeValue<string>("assistant")]
        Assistant
    }
}
