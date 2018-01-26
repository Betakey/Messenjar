using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetDLL.Data;

namespace NetDLL
{
    [Serializable]
    public class PacketSendHistory : Packet
    {
        public Dictionary<DateTime, List<MessageData>> History { get; private set; }

        public PacketSendHistory(Dictionary<DateTime, List<MessageData>> history)
        {
            History = history;
        }
    }
}
