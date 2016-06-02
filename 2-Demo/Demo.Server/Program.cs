using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Demo.Server.ServiceImpl;
using Demo.Service;
using Seif.Rpc;
using Seif.Rpc.Configuration;
using Seif.Rpc.Default;
using Seif.Rpc.Registry;

namespace Demo.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var servConfig = new ProviderConfiguration();
            servConfig.ApiDomain = "api.aaa.com";
            servConfig.ApiIpAddress = "172.16.1.121";
            servConfig.SerializeMode = "ssjson";
            servConfig.Protocol = "Http";
            servConfig.AddtionalFields.Add(AttrKeys.ApiGetEntrance, "api/get");
            servConfig.AddtionalFields.Add(AttrKeys.ApiPostEntrance, "api/post");

            var clientConfig = new ConsumerConfiguration();

            var registryProvider = new RedisRegistryProvider("127.0.0.1:6379");
            var registry = new GenericRegistry(registryProvider, registryProvider);
            var typeBuilder = new AutofacTypeBuilder();
            SeifApplication.Initialize(registry, servConfig, clientConfig, typeBuilder, new ISerializer[]{new ServiceStackJsonSerializer()});
            
            SeifApplication.ExposeService<IDemoService, DemoService>();
            //SeifApplication.ReferenceService<IEchoService>(new ProxyOptions(), new IInvokeFilter[0]);

            typeBuilder.Build();

            var config = new HttpSelfHostConfiguration("http://localhost:3333");
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            Console.WriteLine("Server is opened");
            Console.Read();
        }
    }
}
