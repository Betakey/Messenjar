using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDLL
{
    [Serializable]
    public class PacketRequestMessageServer : Packet
    {
        public string FriendName { get; private set; }

        public PacketRequestMessageServer(string friendName)
        {
            FriendName = friendName;
        }
    }
}
