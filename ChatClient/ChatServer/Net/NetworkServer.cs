using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetDLL;

namespace ChatServer.Net
{
    public class NetworkServer : Server
    {
        public NetworkServer(string ip) : base(ip, 2016)
        {
        }

        public override void OnPacketReceived(ServerHandledClient client, Packet packet)
        {
            
        }
    }
}
