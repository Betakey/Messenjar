using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDLL
{
    [Serializable]
    public class PacketSendUniText : Packet
    {
        public string Receiver { get; private set; }

        public string Text { get; private set; }

        public PacketSendUniText(string receiver, string text)
        {
            Text = text;
            Receiver = receiver;
        }
    }
}