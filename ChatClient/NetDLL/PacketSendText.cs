using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDLL
{
    [Serializable]
   public class PacketSendText : Packet
    {
        public string Text { get; private set; }

        public string Sender { get; private set; }

        public PacketSendText(string text, string sender)
        {
            Text = text;
            Sender = sender;
        }
    }
}
