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
    public class MessageServer : Server
    {
        public MessageServer(string ip, int port) : base(ip, port)
        {
        }

        protected override bool IsFull(int count)
        {
            return count >= 10;
        }


        public override void OnPacketReceived(ServerHandledClient client, Packet packet)
        {
            if (packet is PacketSendText)
            {
                PacketSendText _packet = (PacketSendText)packet;
                // Sender
                UserData data = Program.Instance.UserDataManager.GetData(client.Name);
                if (data == null)
                {
                    data = new UserData(client.Name);
                    Program.Instance.UserDataManager.Datas.Add(data);
                }
                data.MessageHistory.Add(new MessageData(_packet, client.Name));
                client.SendPacket(new PacketSendHistory(data.SortMessageByDate()));
                client.SendPacket(new PacketSendHistory(data.SortMessageByDate()));// I have to send this Packet twice because of a Bug:
                                                                                   // with the ChatBox which is duplicating values
                // Receiver
                UserData receiverData = Program.Instance.UserDataManager.GetData(_packet.Receiver);
                if (receiverData == null)
                {
                    receiverData = new UserData(_packet.Receiver);
                    Program.Instance.UserDataManager.Datas.Add(receiverData);
                }
                receiverData.MessageHistory.Add(new MessageData(client.Name, _packet.Text, _packet.Time, client.Name));
                ServerHandledClient receiver = Program.Instance.MessageServerHandler.GetClient(_packet.Receiver);
                if (receiver != null)
                {
                    receiver.SendPacket(new PacketSendHistory(receiverData.SortMessageByDate()));
                    receiver.SendPacket(new PacketSendHistory(receiverData.SortMessageByDate())); // I have to send this Packet twice because of a Bug:
                                                                                                  // with the ChatBox which is duplicating values
                    receiver.SendPacket(new PacketSendNewMessageNotify(client.Name));
                }
                else
                {
                    ServerHandledClient networkReceiver = Program.Instance.NetworkServer.GetClient(_packet.Receiver);
                    if (networkReceiver != null)
                    {
                        networkReceiver.SendPacket(new PacketSendNewMessageNotify(client.Name));
                    }
                    else
                    {

                        receiverData.NewMessages.Add(client.Name);
                    }
                }
                Program.Instance.UserDataManager.Save();
            }
            else if (packet is PacketSendName)
            {
                client.Name = ((PacketSendName)packet).Name;
            }
        }
    }
}
