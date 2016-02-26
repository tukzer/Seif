using System;
using Seif.Core;

namespace Seif.Soa.Default
{
    public class DefaultClient : IRpcClient
    {
        public Uri Url
        {
            get { throw new NotImplementedException(); }
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public ILogger Logger
        {
            get { throw new NotImplementedException(); }
        }

        public void Refer<T>() where T : class
        {
            var proxyFacotry = new DynamicProxyFactory(null);

            var instance = proxyFacotry.CreateProxy<T>(Url.ToString());
            ApplicationContext.SetDefault(instance);
        }
    }
}