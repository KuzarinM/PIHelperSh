using PIHelperSh.AI.GroqCloud.Models.DTOs;
using PIHelperSh.AI.GroqCloud.Models.Enums;
using PIHelperSh.Core.Extensions;

namespace PIHelperSh.AI.GroqCloud.Models.InternelModels
{
	internal class AIRequestInternal
    {
        public List<AIMessageInternal> Messages = new();

        public AIModel Model { get; set; }

        public AIRequestDTO GetRequestModel => new()
        {
            messages = Messages.Select(x => x.GetRequestModel).ToList(),
            model = Model.GetValue<string>()!
        };
    }
}
