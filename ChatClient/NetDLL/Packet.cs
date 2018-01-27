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
        
        public static Packet ToPacket(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes)
            {
                Position = 0
            };
            BinaryFormatter bf = new BinaryFormatter();
            object obj = bf.Deserialize(ms);
            ms.Close();
            return obj as Packet;
        }
       
        public override string ToString()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            byte[] bytes = ms.GetBuffer();
            ms.Close();
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
