namespace PIHelperSh.AI.Kandinsky.Models.Internal
{
	internal class KandinskyGenerateRequestDTO
    {
        public string type { get; set; }

        public string style { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public int num_images { get; set; }

        public string negativePromptUnclip { get; set; }

        public Dictionary<string,string> generateParams { get; set; }
    }
}
