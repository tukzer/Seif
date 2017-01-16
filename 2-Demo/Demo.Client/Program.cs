using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using Demo.Service;
using Seif.Rpc;
using Seif.Rpc.Configuration;
using Seif.Rpc.Default;
using Seif.Rpc.Invoke;
using Seif.Rpc.Registry;

namespace Demo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ////var servConfig = new ProviderConfiguration();
            ////servConfig.ApiDomain = "api.aaa.com";
            ////servConfig.ApiIpAddress = "172.16.1.121";
            ////servConfig.SerializeMode = "application/json";
            ////servConfig.Protocol = "Http";
            //var clientConfig = new ConsumerConfiguration();
            //clientConfig.NodeCode = "CC";
            //clientConfig.Url = "127.0.0.1";
            ////clientConfig.SetInvokeDispatcher(new DefaultInvokeDispatcher());
            ////clientConfig.SetInvokerFactory(new DefaultInvokerFactory());

            //var registryProvider = new RedisRegistryProvider("127.0.0.1:6379");
            //var registry = new GenericRegistry(registryProvider, registryProvider);
            //var typeBuilder = new AutofacTypeBuilder();

            //GlobalConfiguration.InvokerDispatcherDefinition = typeof(DefaultInvokeDispatcher).AssemblyQualifiedName;
            //GlobalConfiguration.InvokerFactoryDefinition = typeof(DefaultInvokerFactory).AssemblyQualifiedName;

            //SeifApplication.Initialize(registry, null, clientConfig, typeBuilder, 
            //    new ISerializer[] {new ServiceStackJsonSerializer()});

            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);  
            var config = new SeifConfiguration();
            config.ProxyFactoryDefinition = typeof (DynamicProxyFactory).AssemblyQualifiedName;
            config.TypeBuilderDefinition = typeof (AutofacTypeBuilder).AssemblyQualifiedName;
            
            config.SerializerDefinition = new KeyValueConfigurationCollection();
            config.SerializerDefinition.Add("ServiceStackJsonSerializer", typeof(ServiceStackJsonSerializer).AssemblyQualifiedName);

            //config.InvokersDefinition = new KeyValueConfigurationCollection();
            //config.InvokersDefinition.Add("HttpInvoker", typeof(HttpInvoker).AssemblyQualifiedName);
            config.InvokeFilterDefinition = new KeyValueConfigurationCollection();
            config.InvokerFactoryDefinition = typeof (DefaultInvokerFactory).AssemblyQualifiedName;
            config.InvokerDispatcherDefinition = typeof (DefaultInvokeDispatcher).AssemblyQualifiedName;

            config.ProviderConfiguration = new ProviderConfiguration
            {
                ApiDomain = "api.seif.com",
                ApiIpAddress = "localhost:3333",
                Protocol = "HttpInvoker",
                SerializeMode = "ServiceStackJsonSerializer",
                NodeCode = "PV",
                AddtionalFields = new KeyValueConfigurationCollection()
            };
            config.ProviderConfiguration.AddtionalFields.Add(AttrKeys.ApiGetEntrance, "api/common/get");
            config.ProviderConfiguration.AddtionalFields.Add(AttrKeys.ApiPostEntrance, "api/common/post");

            config.ConsumerConfiguration = new ConsumerConfiguration
            {
                NodeCode = "CC",
                Url = "127.0.0.1"
            };

            config.RegistryConfiguration = new RegistryConfiguration
            {
                Url = "127.0.0.1:6379",
                NotifyDefinition = typeof(RedisRegistryProvider).AssemblyQualifiedName,
                RegistryFactoryDefinition = typeof(GenericRegistryFactory).AssemblyQualifiedName,
                StoreDefinition = typeof(RedisRegistryProvider).AssemblyQualifiedName
            };

            cfg.Sections.Add("SeifConfiguration", config);
            cfg.Save();

            SeifApplication.Initialize(config);
            SeifApplication.ReferenceService<IDemoService>(new ProxyOptions(), new IInvokeFilter[0]);

            //SeifApplication.ReferenceService<IEchoService>(new ProxyOptions(), new IInvokeFilter[0]);
            SeifApplication.AppEnv.TypeBuilder.Build();


            //ConfigurationManager.RefreshSection("SeifConfiguration");  

            //SeifApplication.ReferenceService<IDemoService>(new ProxyOptions(), new IInvokeFilter[0]);
            //typeBuilder.Build();

            //SeifApplication.ReferenceService<IEchoService>(new ProxyOptions(), new IInvokeFilter[0]);


            //var config = new HttpSelfHostConfiguration("http://localhost:3333");
            //config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            //var server = new HttpSelfHostServer(config);
            //server.OpenAsync().Wait();

            var demoServer = SeifApplication.Resolve<IDemoService>();
            demoServer.PrintServer();

            var model = new ComplexModel
            {
                Name = "AAAA",
                CreateDate = DateTime.Now,
                Values = new Dictionary<string, string> ()
            };

            model.Values.Add("AAA","BBB");

            var serializer = new ServiceStackJsonSerializer();
            var modelJson = serializer.Serialize(model);
            var modelFromJson = serializer.Deserialize(typeof (ComplexModel), modelJson);

            var intVal = demoServer.AddModel(model);
            Console.WriteLine("Create Model: {0}", intVal == 1);

            var dbModel = demoServer.GetModel(model.Name);
            Console.WriteLine("Get Model: {0}", dbModel != null);

            dbModel = demoServer.GetModel(model.Name, "AAA");
            Console.WriteLine("Get Model By Value: {0}", dbModel != null);

            //var result = demoServer.GetCounterAsync("aaa");
            //Console.WriteLine("GetCounterAsync: {0}", result);

            Console.WriteLine("Client is connected");
            Console.Read();

        }
    }
}
