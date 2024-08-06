using PIHelperSh.AI.Core.Interfaces;
using PIHelperSh.AI.Core.Models;
using PIHelperSh.AI.Kandinsky.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.AI.Kandinsky
{
    public class KandinskyAIContext : ICloudAIContext
    {
        public string ApiUrl { get; set; } = "https://api-key.fusionbrain.ai/key/api/v1";
        public string? ApiKey { get; set; }
        public string? SecretKey { get; set; }
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
            throw new NotImplementedException("Kandinsky не поддерживает текстовые запросы");
        }

        public ICloudAIImageGenerator GetNewImageGenerator()
        {
            return new KandinskyAIImageGenerator(this, KandinskyStyles.No);
        }

        public ICloudAIImageGenerator GetNewImageGenerator(KandinskyStyles style)
        {
            return new KandinskyAIImageGenerator(this, style);
        }
    }
}
