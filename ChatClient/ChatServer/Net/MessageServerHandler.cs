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
        /// <summary>
        /// Every started Message Server
        /// </summary>
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

        /// <summary>
        /// Shuts down all acitve Message Servers
        /// </summary>
        public void Shutdown()
        {
            foreach (MessageServer server in MessageServers)
            {
                server.Stop();
            }
        }

        /// <summary>
        /// Starts a new Message Server:
        /// - Generates a Port which is free
        /// - Starts the Server
        /// - Adds the Server to the handling List
        /// </summary>
        public void StartNewServer()
        {
            int port = GeneratePort();
            MessageServer server = new MessageServer(ip, port);
            server.Start();
            MessageServers.Add(server);
        }

        /// <summary>
        /// Generates a Port which can be used and is free
        /// </summary>
        public int GeneratePort()
        {
            port++;
            while (exceptionPorts.Contains(port) || IsPortInUse(port))
            {
                port++;
            }
            return port;
        }

        /// <summary>
        /// Checks if there is another Message Server with the given Port
        /// </summary>
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

        /// <summary>
        /// Returns the ServerHandledClient object to the given Name when the Client is on one of the Message Servers.
        /// If not it returns null
        /// </summary>
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
