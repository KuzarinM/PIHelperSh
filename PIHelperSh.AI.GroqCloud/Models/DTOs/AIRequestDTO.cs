namespace PIHelperSh.AI.GroqCloud.Models.DTOs
{
    internal class AIRequestDTO
    {
        public List<AIMessageDTO> messages = new();

        public string model { get; set; } = string.Empty;
    }
}
