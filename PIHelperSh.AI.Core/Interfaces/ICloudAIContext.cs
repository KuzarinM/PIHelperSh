using PIHelperSh.AI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.AI.Core.Interfaces
{
    public interface ICloudAIContext
    {
        public string ApiUrl { get; set; }

        public string? ApiKey { get; set; }

        public ProxyModel? GetProxy(bool next = true);

        public Task<ProxyModel?> GetProxyAsync(bool next = true);

        public bool AddProxy(ProxyModel proxy);

        public Task<bool> AddProxyAsync(ProxyModel proxy);

        public ICloudAIChat GetNewChat();

        public ICloudAIImageGenerator GetNewImageGenerator();
    }
}
