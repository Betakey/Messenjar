using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.IO
{
    public class UserDataManager
    {
        private string dirPath;

        public List<UserData> Datas { get; private set; }

        public UserDataManager()
        {
            dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\users";
            if (!Directory.Exists(dirPath)) Directory.Exists(dirPath);
            Datas = new List<UserData>();
            foreach (string filePath in Directory.GetFiles(dirPath))
            {
                if (Path.GetExtension(filePath).ToLower().Contains("json"))
                {
                    Datas.Add(UserData.ToUserData(Path.GetFileNameWithoutExtension(filePath), File.ReadAllText(filePath)));
                }
            }
        }

        public UserData GetData(string name)
        {
            try
            {
                return Datas.Find(x => x.Name == name);
            }
            catch
            {
                return null;
            }
        }

        public void Save()
        {
            foreach (UserData data in Datas)
            {
                File.WriteAllText(dirPath + "\\" + data.Name + ".json", data.ToString());
            }
        }
    }
}
