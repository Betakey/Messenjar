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
            In = new StreamReader(client.GetStream());
            Out = new StreamWriter(client.GetStream());
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
            Console.WriteLine("[" + server.Port + "] <- Sending Packet to " + Client.Client.LocalEndPoint + " (Type: " + packet.GetType().ToString().Replace("NetDLL.", "").Replace("Packet", "") + ")");
            //Out.WriteLine(Cryptor.Encrypt(ID.ToString(), packet.ToString()));
            byte[] bytes = packet.ToBytes();
            Client.GetStream().Write(BitConverter.GetBytes(bytes.Length), 0, BitConverter.GetBytes(bytes.Length).Length);
            Client.GetStream().Write(bytes, 0, bytes.Length);
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
                    int bytesRead = 0;
                    int bufferSize = 0;
                    byte[] datalength = new byte[4];
                    Client.GetStream().Read(datalength, 0, datalength.Length);
                    bufferSize = BitConverter.ToInt32(datalength, 0);

                    if (bufferSize != 0)
                    {
                        byte[] bytes = new byte[bufferSize];
                        bytesRead = Client.GetStream().Read(bytes, 0, bufferSize);
                        if (bytesRead == 0)
                        {
                            continue;
                        }
                        Packet packet = Packet.ToPacket(bytes);
                        if (packet != null)
                        {
                            Console.WriteLine("[" + server.Port + "] -> Packet received from " + Client.Client.LocalEndPoint + " (Type: " + packet.GetType().ToString().Replace("NetDLL.", "").Replace("Packet", "") + ")");
                            server.OnPacketReceived(this, packet);
                        }
                    }
                }
            }
            catch(Exception)
            {
                server.OnClientDisconnect(this);
                Console.WriteLine("[" + server.Port + "] <> Client (IP: " + Client.Client.LocalEndPoint + ") disconnected");
                Close();
            }
        }
    }
}
