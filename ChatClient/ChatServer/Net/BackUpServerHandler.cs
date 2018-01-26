using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatServer.IO;
using NetDLL.Utils;

namespace ChatServer.Net
{
    public class BackUpServerHandler
    {
        public List<NetworkServer> BackUpServers { get; private set; }

        private int port;
        private List<int> exceptionPorts;
        private string ip;

        public BackUpServerHandler()
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
            BackUpServers = new List<NetworkServer>();
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
            NetworkServer server = new NetworkServer(ip, port);
            server.Start();
            BackUpServers.Add(server);
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
                BackUpServers.Find(x => x.Port == port);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ServerHandledClient GetClient(string name)
        {
            foreach (NetworkServer server in BackUpServers)
            {
                ServerHandledClient client = server.GetClient(name);
                if (client != null) return client;
            }
            return null;
        }
    }
}
