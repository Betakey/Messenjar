using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDLL.Data
{
    public class MessageData
    {
        public string Message { get; private set; }

        public string FriendName { get; private set; }

        public DateTime Time { get; private set; }

        public MessageData(PacketSendText packet)
        {
            FriendName = packet.Receiver;
            Message = packet.Text;
            Time = packet.Time;
        }

        public MessageData(string friendName, string message, DateTime time)
        {
            FriendName = friendName;
            Message = message;
            Time = time;
        }
    }
}
