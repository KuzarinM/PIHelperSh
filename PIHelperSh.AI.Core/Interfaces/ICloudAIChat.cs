using PIHelperSh.AI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.AI.Core.Interfaces
{
    public interface ICloudAIChat : IDisposable
    {
        public AIPromt? SystemPromt { get; set; }

        public AIPromt SendToChat(AIPromt message);

        public Task<AIPromt> SendToChatAsync(AIPromt message);

        public void ClearChatHistory(bool removeSystemPromt = false);

        public void ClearChatHistoryAsync(bool removeSystemPromt = false);

        public AIChatHistory SaveChatHistory();

        public Task<AIChatHistory> SaveChatHistoryAsync();

        public void LoadChatHistory(AIChatHistory chatHistory);

        public Task LoadChatHistoryAsync(AIChatHistory chatHistory);
    }
}
