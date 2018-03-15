using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatServer.IO;
using NetDLL;
using NetDLL.Data;

namespace ChatServer.Net
{
    public class NetworkServer : Server
    {
        public NetworkServer(string ip) : base(ip, 34563)
        {
        }

        public override void OnPacketReceived(ServerHandledClient client, Packet packet)
        {
            if (packet is PacketSendName)
            {
                client.Name = ((PacketSendName) packet).Name;
                UserData data = Program.Instance.UserDataManager.GetData(client.Name);
                if (data != null)
                {
                    foreach (string friendName in data.NewMessages)
                    {
                        client.SendPacket(new PacketSendNewMessageNotify(friendName));
                    }
                    data.NewMessages.Clear();
                    Program.Instance.UserDataManager.Save();
                }
            }
            else if (packet is PacketRequestHistory)
            {
                UserData userData = Program.Instance.UserDataManager.GetData(client.Name);
                if (userData == null)
                {
                    client.SendPacket(new PacketSendHistory(new Dictionary<DateTime, List<MessageData>>()));
                    client.SendPacket(new PacketSendHistory(new Dictionary<DateTime, List<MessageData>>())); // I have to send this Packet twice because of a Bug:
                                                                                                             // with the ChatBox which is duplicating values
                }
                else
                {
                    client.SendPacket(new PacketSendHistory(userData.SortMessgaeByDate(((PacketRequestHistory)packet).FriendName)));
                    client.SendPacket(new PacketSendHistory(userData.SortMessgaeByDate(((PacketRequestHistory)packet).FriendName))); // I have to send this Packet twice because of a Bug:
                                                                                                                                     // with the ChatBox which is duplicating values
                }
            }
        }
    }
}
