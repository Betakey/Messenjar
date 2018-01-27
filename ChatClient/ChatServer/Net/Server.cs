using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatServer.IO;
using NetDLL;
using NetDLL.Data;

namespace ChatServer.Net
{
    public abstract class Server
    {
        public int Port { get; private set; }

        public IPAddress IP { get; private set; }

        public TcpListener Listener { get; private set; }

        public List<ServerHandledClient> Clients { get; private set; }

        public Thread AcceptThread { get; private set; }

        public bool IsAlive { get; private set; }

        public Server(string ip, int port)
        {
            IP = IPAddress.Parse(ip);
            Port = port;
            Clients = new List<ServerHandledClient>();
            Listener = new TcpListener(IP, Port);
            IsAlive = false;
        }

        /// <summary>
        /// Starts the TcpListener - TcpListener listens on the IP and the Port
        /// Creates an Accept Thread in which requesting Clients get accepted
        /// </summary>
        public void Start()
        {
            IsAlive = true;
            Console.WriteLine("[" + Port + "] <> Start Server on " + IP + "...");
            Console.WriteLine("[" + Port + "] <> Start Listening...");
            Listener.Start();
            Console.WriteLine("[" + Port + "] <> Listener started!");
            Console.WriteLine("[" + Port + "] <> Start Accepting Thread...");
            AcceptThread = new Thread(Accept);
            AcceptThread.IsBackground = true;
            AcceptThread.Start();
            Console.WriteLine("[" + Port + "] <> Accepting Thread started!");
        }

        /// <summary>
        /// Stops the Server completely:
        /// - Breaks all Client Connections
        /// - Stops Listening to incoming Requests
        /// - Stops the Accept Thread
        /// </summary>
        public void Stop()
        {
            Console.WriteLine("[" + Port + "] <> Closing all Connections!");
            foreach (ServerHandledClient handledClient in Clients)
            {
                handledClient.Close();
            }
            Console.WriteLine("[" + Port + "] <> Stopping Listener...");
            Listener.Stop();
            Console.WriteLine("[" + Port + "] <> Listener stopped!");
            Console.WriteLine("[" + Port + "] <> Stopping Accepting Thread...");
            AcceptThread.Abort();
            Console.WriteLine("[" + Port + "] <> Accepting Thread stopped!");
            IsAlive = false;
        }

        /// <summary>
        /// The Accept Method which is accepting incoming requests of clients, adding them to the Client List and sending them their Guid
        /// </summary>
        private void Accept()
        {
            try
            {
                while (IsAlive)
                {
                    TcpClient client = Listener.AcceptTcpClient();
                    Console.WriteLine("[" + Port + "] -> Client accepted (IP-Endpoint: " + client.Client.LocalEndPoint + ")");
                    ServerHandledClient handledClient = new ServerHandledClient(client, this);
                    Clients.Add(handledClient);
                    handledClient.SendPacket(new PacketSendID(handledClient.ID));
                }
            }
            catch
            {
                // There would be an exception which is a little bit weird but not important because it gets thrown when the program shuts down
            }
        }

        public ServerHandledClient GetClient(string name)
        {
            try
            {
                return Clients.Find(x => x.Name == name);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// The Event which get called when a Packet comes in from a Client
        /// The Event is then handling the incoming Packets
        /// </summary>
        public abstract void OnPacketReceived(ServerHandledClient client, object packet);

        /// <summary>
        /// The Event which get called when a Connection to a Client breaks
        /// </summary>
        /// <param name="client"></param>
        public void OnClientDisconnect(ServerHandledClient client)
        {
            if (Clients.Contains(client))
            {
                Clients.Remove(client);
            }
        }
    }
}
