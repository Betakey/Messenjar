using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace NetDLL
{
    [Serializable]
    public abstract class Packet
    {
        /// <summary>
        /// Converts a Byte Array to a Packet Object via Deserialization
        /// </summary>
        public static Packet ToPacket(byte[] bytes)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(bytes);
            stream.Position = 0;
            object rval = formatter.Deserialize(stream);
            stream.Close();
            return rval as Packet;
        }

        /// <summary>
        /// Converts a Packet Object to a Byte Array via Serialization
        /// </summary>
        public byte[] ToBytes()
        {
            MemoryStream fs = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, this);
            byte[] rval = fs.ToArray();
            fs.Close();
            return rval;
        }
    }
}
