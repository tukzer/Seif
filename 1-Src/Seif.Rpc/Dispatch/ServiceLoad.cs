using System;

namespace Seif.Rpc.Dispatch
{
    public class ServiceLoad
    {
         
    }

    public struct ServiceLoadData
    {
        public Type InterfaceType { get; set; }
        public int ServerPriority { get; set; }
        public int ServerLoad { get; set; }
        public int Server { get; set; }
    }
}