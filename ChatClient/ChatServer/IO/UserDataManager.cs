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
        /// <summary>
        /// The Path to the Dir where the UserData is storing
        /// </summary>
        private string dirPath;

        /// <summary>
        /// Stores all loaded UserData
        /// </summary>
        public List<UserData> Datas { get; private set; }

        public UserDataManager()
        {
            dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\users";
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            Datas = new List<UserData>();
            foreach (string filePath in Directory.GetFiles(dirPath))
            {
                if (Path.GetExtension(filePath).ToLower().Contains("json"))
                {
                    Datas.Add(UserData.ToUserData(Path.GetFileNameWithoutExtension(filePath), File.ReadAllText(filePath)));
                }
            }
        }

        /// <summary>
        /// Gets the Data of the Client to the given Name
        /// </summary>
        /// <param name="name">The Name of the Client</param>
        /// <returns>A UserData Object</returns>
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

        /// <summary>
        /// Saves all UserData
        /// </summary>
        public void Save()
        {
            foreach (UserData data in Datas)
            {
                File.WriteAllText(dirPath + "\\" + data.Name + ".json", data.ToString());
            }
        }
    }
}
