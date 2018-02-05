using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using NetDLL;

namespace ChatClient
{
    public partial class ChatClientForm : Form
    {
        private Client client;

        public ChatClientForm()
        {
            TcpClient Tclient = new TcpClient();
            try
            {
                Tclient.Connect(Program.Config.AsString(IO.ConfigKey.ServerIP), 34563);
            }
            catch
            {
               MessageBox.Show("Connection to server failed!");
               Close();
            }
            client = new Client(Tclient, this);
            InitializeComponent();
            sendButton.TabStop = false;
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.FlatAppearance.BorderSize = 0;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            client.Write(new PacketSendText(inputRichTextbox.Text, "Receiver1"));
            inputRichTextbox.Clear();
        }

        /// <summary>
        /// Checks the type of a Packet.
        /// </summary>
        /// <param name="packet"></param>
        public void PacketHandler(Packet packet)
        {
            if (packet is PacketSendID)
            {
                client.ID = (packet as PacketSendID).ID;
            }

        }
    }
}
