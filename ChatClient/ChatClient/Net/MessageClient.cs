using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using NetDLL;

namespace ChatClient
{
    public class MessageClient
    {
        public Guid ID { get; set; }

        public Thread Thread { get; private set; }

        public StreamWriter Out { get; private set; }

        public StreamReader In { get; private set; }

        public TcpClient TClient { get; private set; }

        public MessageClient(TcpClient client, Action disconnectCallback, Action<Packet> packetHandleCallback)
        {
            TClient = client;
            Out = new StreamWriter(TClient.GetStream());
            In = new StreamReader(TClient.GetStream());
            Thread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        int bytesRead = 0;
                        int bufferSize = 0;
                        byte[] datalength = new byte[4];
                        TClient.GetStream().Read(datalength, 0, datalength.Length);
                        bufferSize = BitConverter.ToInt32(datalength, 0);

                        if (bufferSize != 0)
                        {
                            byte[] bytes = new byte[bufferSize];
                            bytesRead = TClient.GetStream().Read(bytes, 0, bufferSize);
                            if (bytesRead == 0)
                            {
                                continue;
                            }
                            Packet packet = Packet.ToPacket(bytes);
                            if (packet != null)
                                packetHandleCallback(packet);
                        }
                    }
                }
                catch (Exception)
                {
                    disconnectCallback();
                }
            });
            Thread.IsBackground = true;
            Thread.Start();
        }
        /// <summary>
        /// Methode closes every connection and thread.
        /// </summary>
        public void Close()
        {
            TClient.GetStream().Close();
            Thread.Abort();
            In.Close();
            Out.Close();
            TClient.Close();
        }

        /// <summary>
        /// Converts packet to String and sends it.
        /// </summary>
        /// <param name="packet"></param>
        public void Write(Packet packet)
        {
            byte[] bytes = packet.ToBytes();
            TClient.GetStream().Write(BitConverter.GetBytes(bytes.Length), 0, BitConverter.GetBytes(bytes.Length).Length);
            TClient.GetStream().Write(bytes, 0, bytes.Length);
        }
    }
}      


