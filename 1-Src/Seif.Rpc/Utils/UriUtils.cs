using System;
using System.Linq;
using System.Net;

namespace Seif.Rpc.Utils
{
    public class UriUtils
    {
        public static string GetIpAddressByDomainName(string domain)
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(domain);
                return entry.AddressList[0].ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}