using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDLL
{
    [Serializable]
    public class PacketSendNewMessageNotify : Packet
    {
        public string FriendName { get; private set; }

        public PacketSendNewMessageNotify(string friendName)
        {
            FriendName = friendName;
        }
    }
}
