using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ChatClient.Net
{
    public class Client
    {
        public Guid ID { get; private set; }

        public string Name { get; private set; }

        public Thread Thread { get; private set; }

        public StreamWriter Out { get; private set; }

        public StreamReader In { get; private set; }
    }
}
