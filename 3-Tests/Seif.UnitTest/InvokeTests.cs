using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seif.Rpc.Default;
using Seif.Rpc.Invoke;

namespace Seif.UnitTest
{
    [TestClass]
    public class InvokeTests
    {
        [TestMethod]
        public void CreateProxy()
        {
            var proxyFactory = new DynamicProxyFactory();
            var service = proxyFactory.CreateProxy<IEchoService>(new ProxyOptions
            {
                EndpointUri = "localhost:3310",
                ServiceKind = ServiceKind.Remote
            }, new IInvokeFilter[0]);

            var result = service.GetServerTime();
            Assert.IsNull(result);
        }
    }
}
