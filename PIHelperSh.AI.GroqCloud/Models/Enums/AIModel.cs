using PIHelperSh.Core.Attributes;

namespace PIHelperSh.AI.GroqCloud.Models.Enums
{
    public enum AIModel
    {
        [TypeValue<string>("gemma-7b-it")]
        Gemma_7b,

        [TypeValue<string>("llama3-70b-8192")]
        Llam3_70b,

        [TypeValue<string>("llama3-8b-8192")]
        Llam3_8b,

        [TypeValue<string>("mixtral-8x7b-32768")]
        Mixral8_7b,

        [TypeValue<string>("gemma2-9b-it")]
        Gemma_2_9b,

        [TypeValue<string>("whisper-large-v3")]
        Whisper,

        [TypeValue<string>("llama-3.1-405b-reasoning")]
        Llama_31_405b,

        [TypeValue<string>("llama-3.1-70b-versatile")]
        Llama_31_70b,

        [TypeValue<string>("llama-3.1-8b-instant")]
        Llama_31_8b
    }
}
