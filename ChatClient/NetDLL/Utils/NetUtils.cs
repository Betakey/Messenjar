using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetDLL.Utils
{
    public class NetUtils
    {
        public static IPAddress GetExternalIPAddress()
        {
            IPHostEntry myIPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress myIPAddress in myIPHostEntry.AddressList)
            {
                if (myIPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return myIPAddress;
                }
            }
            return null;
        }
    }
}
