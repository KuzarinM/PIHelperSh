using PIHelperSh.AI.Core.Interfaces;
using PIHelperSh.AI.Core.Models;
using PIHelperSh.AI.GroqCloud.Models.Enums;
using System.Reflection;

namespace PIHelperSh.AI.GroqCloud
{
    /// <summary>
    /// Configuration for all chats
    /// </summary>
    public class GroqAIContext : ICloudAIContext
    {

        public string ApiUrl { get; set; } = "https://api.groq.com/openai/v1/chat/completions";
        public string? ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Proxyes servers list
        /// </summary>
        public List<ProxyModel> Proxys { get; set; } = new();
        private int position = 0;

        public bool AddProxy(ProxyModel proxy)
        {
            Proxys.Add(proxy);
            return true;
        }

        public Task<bool> AddProxyAsync(ProxyModel proxy)
        {
            return Task.FromResult(AddProxy(proxy));
        }

        public ProxyModel? GetProxy(bool next = true)
        {
            if (Proxys.Count == 0)
            {
                return null;
            }
            if (next)
            {
                position++;
                if (position >= Proxys.Count)
                    position = 0;
            }
            return Proxys[position];
        }

        public Task<ProxyModel?> GetProxyAsync(bool next = true)
        {
            return Task.FromResult(GetProxy(next));
        }

        public ICloudAIChat GetNewChat()
        {
            return new GroqAIChat(this, AIModel.Llama_31_70b);
        }

        public ICloudAIChat GetNewChat(AIModel models)
        {
            return new GroqAIChat(this, models);
        }

        public ICloudAIImageGenerator GetNewImageGenerator()
        {
            throw new NotImplementedException("Groq не поддерживает генерацию изображений");
        }
    }
}
