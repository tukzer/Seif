using System;

namespace Seif.Rpc.Invoke
{
    public interface IEchoService
    {
        string GetServerTime();
    }

    public class EchoService : IEchoService
    {
        public string GetServerTime()
        {
            return DateTime.Now.ToLongTimeString();
        }
    }
}