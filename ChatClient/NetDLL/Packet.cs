using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetDLL
{
    [Serializable]
    public abstract class Packet
    {
        public static Packet ToPacket(string str)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(str));
            ms.Position = 0;
            object obj = bf.Deserialize(ms);
            ms.Close();
            return obj as Packet;
        }

        public override string ToString()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, this);
            string str = Encoding.ASCII.GetString(ms.GetBuffer());
            ms.Close();
            return str;
        }
    }
}
