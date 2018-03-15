using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiDLL
{
    public class ChatBoxEntry
    {
        public string Sender { get; private set; }

        public string Message { get; private set; }

        public DateTime Time { get; private set; }

        public ChatBoxEntry(string sender, string message, DateTime time)
        {
            Sender = sender;
            Message = message;
            Time = time;
        }
    }
}
