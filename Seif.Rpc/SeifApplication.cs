using System.Collections.Generic;
using Seif.Rpc.Common;
using Seif.Rpc.Runtime;

namespace Seif.Rpc
{
    public class SeifApplication
    {
        private static readonly SeifApplication Instance = new SeifApplication();


        public ITypeContainer TypeContainer { get; set; }

        public IDictionary<string, IChannel> Protocols { get; set; }


        public static SeifApplication Current
        {
            get { return Instance; }
        }

        public T Resolve<T>()
        {
            return Instance.Resolve<T>();
        }

        public void Register<T>(T instance)
        {
            Instance.Register<T>(instance);
        }

    }
}