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
        /// Wandelt ACSCII in Bytes um
        /// </summary>
        public static Packet ToPacket(string str)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(str));
            ms.Position = 0;
            object obj = bf.Deserialize(ms);
            ms.Close();
            return obj as Packet;
        }
        /// <summary>
        /// ToString Methode wandelt den String in ASCII um 
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
