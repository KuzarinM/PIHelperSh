using PIHelperSh.AI.Core.Models;
using PIHelperSh.AI.GroqCloud.Models.DTOs;
using PIHelperSh.AI.GroqCloud.Models.Enums;
using PIHelperSh.Core.Extentions;

namespace PIHelperSh.AI.GroqCloud.Models.InternelModels
{
    internal class AIMessageInternal : AIPromt
    {
        public AIMessageRole Role { get; set; } = AIMessageRole.User;

        public AIMessageDTO GetRequestModel => new()
        {
            role = Role.GetValue<string>()!,
            content = Text
        };
    }
}
