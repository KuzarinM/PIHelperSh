namespace PIHelperSh.AI.GroqCloud.Models.DTOs
{
    internal class AIResponceBaodyDTO
    {
        public int index { get; set; }
        public AIMessageDTO message { get; set; }
        public string logprobs { get; set; }
        public string finish_reason { get; set; }
    }
}
