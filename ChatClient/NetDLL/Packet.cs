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
        /// Converts an ASCII String to a Packet Object via Deserialization
        /// </summary>
        public static Packet ToPacket(string str)
        {
            return JsonConvert.DeserializeObject(str) as Packet;
        }
        /// <summary>
        /// Converts a Packet Object to an ASCII String via Serialization
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
