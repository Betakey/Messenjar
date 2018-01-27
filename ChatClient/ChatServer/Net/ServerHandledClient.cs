using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Net;
using NetDLL;

namespace ChatServer.Net
{
    public class ServerHandledClient
    {
        public Guid ID { get; private set; }

        public string Name { get; set; }

        public TcpClient Client { get; private set; }

        public StreamReader In { get; private set; }

        public StreamWriter Out { get; private set; }

        public Thread Thread { get; private set; }

        private Server server;

        public ServerHandledClient(TcpClient client, Server server)
        {
            this.server = server;
            Client = client;
            ID = Guid.NewGuid();
            Thread = new Thread(Receive);
            Thread.Start();
        }

        /// <summary>
        /// Closes the Connection to the Client:
        /// - Closes the Receiving Thread
        /// - Closes the Input Stream (Reader)
        /// - Closes the Output Stream (Writer)
        /// </summary>
        public void Close()
        {
            Thread.Abort();
            In.Close();
            Out.Close();
        }

        /// <summary>
        /// Sends Data / a Packet to the Client
        /// </summary>
        public void SendPacket(Packet packet)
        {
            Console.WriteLine("[" + server.Port + "] <- Sending Packet to " + Client.Client.LocalEndPoint + " (Type: " + packet.GetType() + ")");
            //Out.WriteLine(Cryptor.Encrypt(ID.ToString(), packet.ToString()));
            Out.WriteLine(packet.ToString());
            Out.Flush();
        }

        /// <summary>
        /// Receives all incoming Data from the Client
        /// Converts the incoming Data to a Packet Object
        /// Gives the Packet Object to the Server PacketReceived Event
        /// 
        /// When a Exception gets thrown, the Client will be disconnected and deleted from the Server Clients List to prevent Server Crashes or lost of Data
        /// </summary>
        private void Receive()
        {
            try
            {
                while (true)
                {
                    string s = In.ReadLine();
                    if (s != null)
                    {
                        //Packet packet = Packet.ToPacket(Cryptor.Decrypt(ID.ToString(), s));
                        Packet packet = Packet.ToPacket(s);
                        if (packet != null)
                        {
                            Console.WriteLine("[" + server.Port + "] -> Packet received from " + Client.Client.LocalEndPoint + " (Type: " + packet.GetType() + ")");
                            server.OnPacketReceived(this, packet);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("[" + server.Port + "] <> Client (IP: " + Client.Client.LocalEndPoint + ") disconnected");
                server.OnClientDisconnect(this);
                Close();
            }
        }
    }
}
