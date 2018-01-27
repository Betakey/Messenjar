using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatServer.IO;
using NetDLL.Utils;

namespace ChatServer.Net
{
    public class MessageServerHandler
    {
        public List<MessageServer> MessageServers { get; private set; }

        private int port;
        private List<int> exceptionPorts;
        private string ip;

        public MessageServerHandler()
        {
            try
            {
                if (Program.Instance.Config.AsString(ConfigKey.IPType).ToLower() == "localhost")
                {
                    ip = "127.0.0.1";
                }
                else if (Program.Instance.Config.AsString(ConfigKey.IPType).ToLower() == "external")
                {
                    ip = NetUtils.GetExternalIPAddress().ToString();
                }
            }
            catch
            {
                ip = "127.0.0.1";
            }
            MessageServers = new List<MessageServer>();
            port = Program.Instance.Config.AsInt(ConfigKey.PortRangeMin);
            exceptionPorts = new List<int>();
            foreach (string s in Program.Instance.Config.AsString(ConfigKey.ExceptionPorts).Split(','))
            {
                exceptionPorts.Add(Int32.Parse(s));
            }
        }

        public void StartNewServer()
        {
            int port = GeneratePort();
            MessageServer server = new MessageServer(ip, port);
            server.Start();
            MessageServers.Add(server);
        }

        public int GeneratePort()
        {
            port++;
            while (exceptionPorts.Contains(port) || IsPortInUse(port))
            {
                port++;
            }
            return port;
        }

        private bool IsPortInUse(int port)
        {
            try
            {
                MessageServers.Find(x => x.Port == port);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ServerHandledClient GetClient(string name)
        {
            foreach (Server server in MessageServers)
            {
                ServerHandledClient client = server.GetClient(name);
                if (client != null) return client;
            }
            return null;
        }
    }
}
