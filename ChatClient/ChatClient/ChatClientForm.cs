using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using NetDLL;
using System.IO;
using GuiDLL;
using NetDLL.Data;
using System.Speech.Synthesis;

namespace ChatClient
{
    public partial class ChatClientForm : Form
    {
        private Client client;
        private MessageClient messageClient;
        private readonly int FirstPort = Program.Config.AsInt(IO.ConfigKey.PortRange);
        private string name;
        private SpeechSynthesizer synthesizer;
        private string statusHoverMessage;
        private string statusHoverTitle;
        private ToolTip hoverTooltip;
        private Database database;
        private string friends;

        /// <summary>
        /// Tries to connect to the server, checks if a ProfilImage exists and checks if you have friends.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <param name="friends"></param>
        /// <param name="imageBytes"></param>
        
        public ChatClientForm(Database db, string name, string friends, byte[] imageBytes)
        {
            this.friends = friends;
            database = db;
            synthesizer = new SpeechSynthesizer();
            this.name = name;
            TcpClient Tclient = new TcpClient();
            try
            {
                Tclient.Connect(Program.Config.AsString(IO.ConfigKey.ServerIP), 34563);
            }
            catch
            {
                MessageBox.Show("Verbindung fehlgeschlagen!", "Verbindungsfehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
               return;
            }
            client = new Client(Tclient, this, name);
            InitializeComponent();
            if(imageBytes == null || imageBytes.Length <= 0)
            {
                imagePictureBox.Image = Properties.Resources.profile;
            }
            else
            {
                using(MemoryStream stream = new MemoryStream(imageBytes))
                {
                    imagePictureBox.Image = Bitmap.FromStream(stream);
                }
            }
            sendButton.TabStop = false;
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.FlatAppearance.BorderSize = 0;
            List<FriendEntry> entries = new List<FriendEntry>();
            foreach (string friend in friends.Split(';'))
            {
                if (string.IsNullOrEmpty(friend)) continue;
                entries.Add(new FriendEntry(friend, database.GetProfileImage(friend)));
            }
            friendsList.Update(entries);
            yourFriendsList.Update(entries);
        }

        /// <summary>
        /// Sends the message and checks if you have a friends selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendButton_Click(object sender, EventArgs e)
        {
            if (friendsList.Selected == null) return;
            SendMessage();
        }

        /// <summary>
        /// Methode for sending a message and updates the statusPictureBox.
        /// </summary>
        private void SendMessage()
        {
            try
            {
                statusPictureBox.Image = Properties.Resources.loading;
                statusHoverMessage = "Nachricht wird gesendet...";
                statusHoverTitle = "Sende Nachricht...";
                messageClient.Write(new PacketSendText(inputRichTextbox.Text, friendsList.Selected.Name));
                inputRichTextbox.Clear();
                statusPictureBox.Image = null;
                statusHoverMessage = "";
                statusHoverTitle = "";
            }
            catch(Exception)
            {
                statusPictureBox.Image = Properties.Resources.error;
                statusHoverMessage = "Fehler beim Senden der Nachricht aufgetreten! Versuche erneut...";
                statusHoverTitle = "Fehler beim Senden!";
                SendMessage();
            }
        }

        /// <summary>
        /// Checks the type of a Packet.
        /// </summary>
        /// <param name="packet">The Receive Packet</param>
        public void PacketHandler(Packet packet)
        {
            if (packet is PacketSendID)
            {
                client.ID = (packet as PacketSendID).ID;
                client.Write(new PacketSendName(client.Name));
            }
            else if (packet is PacketSendHistory)
            {
                Dictionary<DateTime, List<MessageData>> dict = ((PacketSendHistory)packet).History;
                Dictionary<DateTime, List<ChatBoxEntry>> convertedDict = new Dictionary<DateTime, List<ChatBoxEntry>>();
                foreach (DateTime time in dict.Keys)
                {
                    List<ChatBoxEntry> entries = new List<ChatBoxEntry>();
                    foreach (MessageData data in dict[time])
                    {
                        entries.Add(new ChatBoxEntry(data.FriendName, data.Message, data.Time));
                    }
                    convertedDict.Add(time, entries);
                }
                if (chatBox.InvokeRequired)
                {
                    MethodInvoker invoker = delegate
                    {
                        chatBox.AddChatMessage(convertedDict, name);
                    };
                    chatBox.Invoke(invoker);
                } 
            }
            else if (packet is PacketSendNewMessageNotify)
            {
                string friendName = ((PacketSendNewMessageNotify)packet).FriendName;
                if(friendsList.Selected != null)
                {
                    if(friendsList.Selected.Name == friendName)
                    {
                        return;
                    }
                }
                friendsList.ShowNotify(friendName);
                synthesizer.Speak("Sie haben Post");
            }
        }

        /// <summary>
        /// Stops the Program completely
        /// </summary>
        private void ChatClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// When the Client isn't connected to the Message Server, it searches for an open Message Server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="entry"></param>
        private void FriendsList_BubbleClick(object sender, MouseEventArgs e, FriendEntry entry)
        {
            chatBox.Clear();
            if (messageClient == null)
            {
                statusPictureBox.Image = Properties.Resources.loading;
                statusHoverMessage = "Es wird versucht eine Verbindung zu unseren Server aufzubauen. Bitte haben Sie Geduld...";
                statusHoverTitle = "Verbinde...";
                TcpClient client = SearchForPort(FirstPort);
                if(client == null)
                {
                    statusHoverMessage = "Es konnte keine Verbindung aufgebaut werden! Es wird in 30 Sekunden nochmal versucht!";
                    statusHoverTitle = "Fehler beim Verbinden";
                    statusPictureBox.Image = Properties.Resources.error;
                    Thread thread = new Thread(() =>
                    {
                        Thread.Sleep(1000 * 30);
                        FriendsList_BubbleClick(sender, e, entry);
                    });
                    thread.IsBackground = true;
                    thread.Start();
                    return;
                }
                messageClient = new MessageClient(client, () =>
                {
                    statusPictureBox.Image = Properties.Resources.error;
                    statusHoverMessage = "Die Verbindung zum Message Server wurde geschlossen!";
                    statusHoverTitle = "Verbindung geschlossen";
                }, PacketHandler);
                statusHoverTitle = "";
                statusHoverMessage = "";
                statusPictureBox.Image = null;
                sendButton.Show();
                inputRichTextbox.Show();
                chatBox.Show();
                homePanel.Hide();
            }
            this.client.Write(new PacketRequestHistory(friendsList.Selected.Name));
            friendsList.HideNotify(entry.Name);
        }

        /// <summary>
        /// Searchs for an online Message Server in the Network recursively
        /// </summary>
        /// <param name="port">First Port to start off</param>
        /// <returns>Connected Message Server TcpClient</returns>
        private TcpClient SearchForPort(int port)
        {
            try
            {
                port++;
                if (port == 34563) port++;
                if(port - FirstPort >= 1000)
                {
                    return null;
                }
                TcpClient client = new TcpClient();
                client.Connect(Program.Config.AsString(IO.ConfigKey.ServerIP), port);
                Thread.Sleep(10);
                return client.Connected ? client : SearchForPort(port);
            }
            catch
            {
                return SearchForPort(port);
            }
        }

        /// <summary>
        /// Shows the homePanel and hides the messageClient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void iconPictureBox_Click(object sender, EventArgs e)
        {
            if(messageClient != null)
            {
                messageClient.Close();
                messageClient = null;
            }
            homePanel.Show();
            friendsList.ResetSelection();
            sendButton.Hide();
            inputRichTextbox.Hide();
            chatBox.Hide();
        }

        /// <summary>
        /// Shows the status by hovering over it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      
        private void statusPictureBox_MouseHover(object sender, EventArgs e)
        {
            if(statusHoverTitle != "" && statusHoverMessage != "")
            {
                hoverTooltip = new ToolTip();
                hoverTooltip.ToolTipTitle = statusHoverTitle;
                hoverTooltip.Show(statusHoverMessage, statusPictureBox, 10 * 1000);
            }
        }

        /// <summary>
        /// Editing your ProfilImage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void editImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Bilder | *.jpg; *.jpeg; *.png";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = (Bitmap)Bitmap.FromFile(dialog.FileName);
                imagePictureBox.Image = bitmap;
                MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, bitmap.RawFormat);
                database.ChangePicture(stream.GetBuffer(), name);
                stream.Close();
            }
        }

        /// <summary>
        /// Adds a friend to your friendsList and checks if hes already in your list or if he even exists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void addButton_Click(object sender, EventArgs e)
        {
            if (!database.IsUserExisting(searchNameTextBox.Text))
            {
                MessageBox.Show("Benutzer existiert nicht!", " Fehler!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(friends))
            {
                friends += searchNameTextBox.Text;
            }
            else
            {
                if (friends.Contains(searchNameTextBox.Text))
                {
                    MessageBox.Show("Du hast bereits " + searchNameTextBox.Text + " als Freund!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                friends += ";" + searchNameTextBox.Text;
            }
            database.UpdateFriends(name, friends);
            searchNameTextBox.Clear();
            List<FriendEntry> entries = new List<FriendEntry>();
            foreach (string friend in friends.Split(';'))
            {
                if (string.IsNullOrEmpty(friend)) continue;
                entries.Add(new FriendEntry(friend, database.GetProfileImage(friend)));
            }
            friendsList.Update(entries);
            yourFriendsList.Update(entries);
            MessageBox.Show("Freund erfolgreich hinzugefügt!","Hinzugefügt", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Removes a friend in your friendsList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void removeFriendTextBox_Click(object sender, EventArgs e)
        {
            if(yourFriendsList.Selected != null)
            {
                string name = yourFriendsList.Selected.Name;
                if(friends.Contains(";" + name))
                {
                    friends = friends.Replace(";" + name, "");
                }
                else
                {
                    friends = friends.Replace(name, "");
                }
                database.UpdateFriends(this.name, friends);
                List<FriendEntry> entries = new List<FriendEntry>();
                foreach (string friend in friends.Split(';'))
                {
                    if (string.IsNullOrEmpty(friend)) continue;
                    entries.Add(new FriendEntry(friend, database.GetProfileImage(friend)));
                }
                friendsList.Update(entries);
                yourFriendsList.Update(entries);
                MessageBox.Show("Freund erfolgreich entfernt!", "Entfernt!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
