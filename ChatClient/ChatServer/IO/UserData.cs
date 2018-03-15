using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using NetDLL.Data;
using Newtonsoft.Json;

namespace ChatServer.IO
{
    [Serializable]
    public class UserData
    {
        public string Name { get; private set; }

        public List<MessageData> MessageHistory { get; private set; }

        public List<string> NewMessages { get; private set; }

        public UserData(string name)
        {
            Name = name;
            MessageHistory = new List<MessageData>();
            NewMessages = new List<string>();
        }

        public UserData()
        {
            
        }

        public UserData(List<MessageData> messageHistory, List<string> newMessages)
        {
            MessageHistory = messageHistory;
            NewMessages = newMessages;
        }

        /// <summary>
        /// Gets all Message of the Chat with the given FriendName sorted by the Date
        /// </summary>
        public Dictionary<DateTime, List<MessageData>> SortMessgaeByDate(string friendName)
        {
            Dictionary<DateTime, List<MessageData>> dict = new Dictionary<DateTime, List<MessageData>>();
            foreach (MessageData data in MessageHistory)
            {
                if(data.FriendName != friendName) continue;
                DateTime date = data.Time.Date;
                if (dict.ContainsKey(date))
                {
                    List<MessageData> datas = dict[date];
                    datas.Add(data);
                    datas.Sort((a, b) => b.Time.CompareTo(a.Time));
                    dict[date] = datas;
                }
                else
                {
                    List<MessageData> datas = new List<MessageData>();
                    datas.Add(data);
                    datas.Sort((a, b) => b.Time.CompareTo(a.Time));
                    dict[date] = datas;
                }
            }
            return dict;
        }

        /// <summary>
        /// Gets all Messages sorted by the Date
        /// </summary>
        public Dictionary<DateTime, List<MessageData>> SortMessageByDate()
        {
            Dictionary<DateTime, List<MessageData>> dict = new Dictionary<DateTime, List<MessageData>>();
            foreach (MessageData data in MessageHistory)
            {
                DateTime date = data.Time.Date;
                if (dict.ContainsKey(date))
                {
                    List<MessageData> datas = dict[date];
                    datas.Add(data);
                    datas.Sort((a, b) => b.Time.CompareTo(a.Time));
                    dict[date] = datas;
                }
                else
                {
                    List<MessageData> datas = new List<MessageData>();
                    datas.Add(data);
                    datas.Sort((a, b) => b.Time.CompareTo(a.Time));
                    dict[date] = datas;
                }
            }
            return dict;
        }

        /// <summary>
        /// Converts UserData Object to a Byte Array
        /// </summary>
        public byte[] ToBytes()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            binaryFormatter.Serialize(ms, this);
            byte[] bytes = ms.GetBuffer();
            ms.Close();
            return bytes;
        }

        /// <summary>
        /// Converts a Byte Array to an UserData Object
        /// </summary>
        public static UserData ToUserData(string name, byte[] bytes)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            object obj = binaryFormatter.Deserialize(ms);
            ms.Close();
            UserData data = obj as UserData;
            data.Name = name;
            if(data.NewMessages == null) data.NewMessages = new List<string>();
            return data;
        }
    }
}
