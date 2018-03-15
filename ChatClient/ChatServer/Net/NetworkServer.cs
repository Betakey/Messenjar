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
            /*if (packet is PacketSendText)
            {
                Dictionary<DateTime,List<MessageData>> dict = new Dictionary<DateTime,List<MessageData>>();
                List<MessageData> datas = new List<MessageData>();
                datas.Add(new MessageData((PacketSendText)packet));
                dict.Add(DateTime.Today, datas);
                client.SendPacket(new PacketSendHistory(dict));
                ServerHandledClient receiver = GetClient(((PacketSendText)packet).Receiver);
                if (receiver != null) receiver.SendPacket(new PacketSendHistory(dict));
            }
            else */
            if (packet is PacketSendName)
            {
                client.Name = ((PacketSendName) packet).Name;
            }
            else if (packet is PacketRequestHistory)
            {
                UserData userData = Program.Instance.UserDataManager.GetData(client.Name);
                if (userData == null)
                {
                    client.SendPacket(new PacketSendHistory(new Dictionary<DateTime, List<MessageData>>()));
                }
                else
                {
                    client.SendPacket(new PacketSendHistory(userData.SortMessgaeByDate(((PacketRequestHistory)packet).FriendName)));
                }
            }
        }
    }
}
