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
                    if (!IsPrivateIP(myIPAddress))
                    {
                        return myIPAddress;
                    }
                }
            }
            return null;
        }

        private static bool IsPrivateIP(IPAddress myIPAddress)
        {
            if (myIPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                byte[] ipBytes = myIPAddress.GetAddressBytes();
                if (ipBytes[0] == 10)
                {
                    return true;
                }
                if (ipBytes[0] == 172 && ipBytes[1] == 16)
                {
                    return true;
                }
                if (ipBytes[0] == 192 && ipBytes[1] == 168)
                {
                    return true;
                }
                if (ipBytes[0] == 169 && ipBytes[1] == 254)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
