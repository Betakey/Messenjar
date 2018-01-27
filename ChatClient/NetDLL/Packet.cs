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
        /// <summary>
        /// Converts an ASCII String to a Packet Object via Deserialization
        /// </summary>
        public static Packet ToPacket(byte[] bytes)
        { 
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            object obj = bf.Deserialize(ms);
            ms.Close();
            return obj as Packet;
        }
        /// <summary>
        /// Converts a Packet Object to an ASCII String via Serialization
        /// </summary>
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
