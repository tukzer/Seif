using System.Linq;
using System.Net;

namespace Seif.Rpc.Utils
{
    public static class NetUtils
    {
        private static readonly LruCache DnsCache = new LruCache(100);

        public static string GetIp(string hostName)
        {
            IPHostEntry host = Dns.GetHostEntry(hostName);

            if (DnsCache.Exists(hostName))
            {
                return DnsCache.Get(hostName);
            }

            if (host != null && host.AddressList.Any())
            {
                string ipAddress = host.AddressList[0].ToString();
                DnsCache.Add(hostName, ipAddress);
                return ipAddress;
            }

            return string.Empty;
        }

    }
}