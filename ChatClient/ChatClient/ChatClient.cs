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
                Tclient.Connect("91.37.182.37", 2016);
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
            closeButton.TabStop = false;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.FlatAppearance.BorderSize = 0;
        }

        private void ChatClient_Load(object sender, EventArgs e)
        {

        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void ChatClient_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void ChatClient_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            {
                client.Write(new PacketSendText(sendTextRichTextBox.Text, userNameTextBox.Text));
                sendTextRichTextBox.Clear();
            }
        }
        /// <summary>
        /// Checks the type of a Packet.
        /// </summary>
        /// <param name="packet"></param>
        public void PacketHandler(object packet)
        {
            if (packet is PacketSendID)
            {
                client.ID = (packet as PacketSendID).ID;
            }
            else if (packet is PacketSendText)
            {
                if (!string.IsNullOrEmpty(chatRichTextBox.Text))
                    chatRichTextBox.Text += "\n" + (packet as PacketSendText).Receiver +
                        ": " + (packet as PacketSendText).Text;
                else
                    chatRichTextBox.Text += (packet as PacketSendText).Receiver +
                        ": " + (packet as PacketSendText).Text;
            }
        }
    }
}
