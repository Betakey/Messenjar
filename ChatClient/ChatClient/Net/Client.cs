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
    public class Client
    {
        public Guid ID { get; set; }

        public string Name { get; private set; }

        public Thread Thread { get; private set; }

        public StreamWriter Out { get; private set; }

        public StreamReader In { get; private set; }

        public TcpClient TClient { get; private set; }

        public Client(TcpClient client, ChatClientForm chatClientForm)
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
                        string check = null;
                        while ((check = In.ReadLine()) != null)
                        {
                            chatClientForm.PacketHandler(Packet.ToPacket(Encoding.ASCII.GetBytes(check)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    //MessageBox.Show("Connection lost!", " Failed to connect",
                    //MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //chatClientForm.Close();
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
            Thread.Abort();
            In.Close();
            Out.Close();
        }

        /// <summary>
        /// Converts packet to String and sends it.
        /// </summary>
        /// <param name="packet"></param>
        public void Write(Packet packet)
        {
            Out.WriteLine(packet.ToString());
            Out.Flush();
        }
    }
}      


