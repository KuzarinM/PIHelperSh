using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.AI.Core.Models
{
    public class ProxyModel
    {
        /// <summary>
        /// IP
        /// </summary>
        public string ServerIP { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public int ServerPort { get; set; }

        public ProxyModel() { }

        public ProxyModel(string ip, int port)
        {
            ServerIP = ip;
            ServerPort = port;
        }

        public WebProxy CreateProxy() => new WebProxy(ServerIP, ServerPort);

        public override string ToString()
        {
            return $"WebProxyModel[{ServerIP}:{ServerPort}]";
        }
    }
}
