using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetDLL.Data;
using Newtonsoft.Json;

namespace ChatServer.IO
{
    public class UserData
    {
        [JsonIgnore]
        public string Name { get; private set; }

        public List<MessageData> MessageHistory { get; private set; }

        public List<string> NewMessages { get; private set; }

        public UserData(string name)
        {
            Name = name;
            MessageHistory = new List<MessageData>();
            NewMessages = new List<string>();
        }

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

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static UserData ToUserData(string name, string s)
        {
            UserData data = JsonConvert.DeserializeObject<UserData>(s);
            data.Name = name;
            return data;
        }
    }
}
