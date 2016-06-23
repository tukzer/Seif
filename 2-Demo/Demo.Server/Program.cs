using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Demo.Server.ServiceImpl;
using Demo.Service;
using Seif.Rpc;
using Seif.Rpc.Configuration;

namespace Demo.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var selfConfig = cfg.GetSection("SeifConfiguration") as SeifConfiguration;

            //var servConfig = new ProviderConfiguration();
            //servConfig.ApiDomain = "api.aaa.com";
            //servConfig.ApiIpAddress = "172.16.1.121";
            //servConfig.SerializeMode = "ssjson";
            //servConfig.Protocol = "Http";
            //servConfig.AddtionalFields.Add(AttrKeys.ApiGetEntrance, "api/get");
            //servConfig.AddtionalFields.Add(AttrKeys.ApiPostEntrance, "api/post");

            //var clientConfig = new ConsumerConfiguration();

            //var registryProvider = new RedisRegistryProvider();
            //var registry = new GenericRegistry(registryProvider, registryProvider);
            //var typeBuilder = new AutofacTypeBuilder();
            ////SeifApplication.Initialize(registry, servConfig, clientConfig, typeBuilder, new ISerializer[]{new ServiceStackJsonSerializer()});

            SeifApplication.Initialize(selfConfig);
            SeifApplication.ExposeService<IDemoService, DemoService>();
            //SeifApplication.ReferenceService<IEchoService>(new ProxyOptions(), new IInvokeFilter[0]);

            SeifApplication.AppEnv.TypeBuilder.Build();

            var config = new HttpSelfHostConfiguration("http://localhost:3333");
            config.Filters.Add(new ExceptionFilter());
            config.Routes.MapHttpRoute("default", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            Console.WriteLine("Server is opened");
            Console.Read();
        }
    }
}
