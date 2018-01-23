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
        public Guid ID { get; private set; }

        public PacketSendID(Guid id)
        {
            ID = id;
        }
    }
}
