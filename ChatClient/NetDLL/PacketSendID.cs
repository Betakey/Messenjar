using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDLL
{
    [Serializable]
    public class PacketSendID : Packet
    {
        public string ID { get; private set; }

        public PacketSendID(string id)
        {
            ID = id;
        }
    }
}
