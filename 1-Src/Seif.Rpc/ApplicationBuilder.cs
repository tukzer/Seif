using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using Seif.Rpc.Dispatch;
using Seif.Rpc.Invoke;
using Seif.Rpc.Utils;

namespace Seif.Rpc.Configuration
{
    public class ApplicationBuilder
    {
        #region Configuration

        public static SeifApplication UseTypeBuilder<T>() where T : ITypeBuilder
        {
            SeifApplication.AppEnv.GlobalConfiguration.TypeBuilderDefinition = typeof(T).AssemblyQualifiedName;
            return SeifApplication.AppEnv;
        }

        public static SeifApplication UseProxyFactory<T>() where T : IProxyFactory
        {
            SeifApplication.AppEnv.GlobalConfiguration.ProxyFactoryDefinition = typeof(T).AssemblyQualifiedName;
            return SeifApplication.AppEnv;
        }

        public static SeifApplication UseInvokerFactory<T>() where T : IInvokerFactory
        {
            SeifApplication.AppEnv.GlobalConfiguration.InvokerFactoryDefinition = typeof(T).AssemblyQualifiedName;
            return SeifApplication.AppEnv;
        }

        public static SeifApplication UseDispatcher<T>() where T : IInvokeDispatcher
        {
            SeifApplication.AppEnv.GlobalConfiguration.InvokerDispatcherDefinition = typeof(T).AssemblyQualifiedName;
            return SeifApplication.AppEnv;
        }

        public static SeifApplication UseSerializers(IDictionary<string,Type> dicTypes )
        {
            SeifApplication.AppEnv.GlobalConfiguration.SerializerDefinition = new KeyValueConfigurationCollection();
            foreach (var key in dicTypes.Keys)
            {
                SeifApplication.AppEnv.GlobalConfiguration.SerializerDefinition.Add(key, dicTypes[key].AssemblyQualifiedName);
            }
            return SeifApplication.AppEnv;
        }

        public static SeifApplication UseInvokeFilters(IDictionary<string,Type> dicTypes )
        {
            SeifApplication.AppEnv.GlobalConfiguration.InvokeFilterDefinition = new KeyValueConfigurationCollection();
            foreach (var key in dicTypes.Keys)
            {
                SeifApplication.AppEnv.GlobalConfiguration.InvokeFilterDefinition.Add(key, dicTypes[key].AssemblyQualifiedName);
            }
            return SeifApplication.AppEnv;
        }



        #endregion

        #region  Provider

        public static SeifApplication AsProvider(string hostDomain, string protocol, string serializeMode,IDictionary<string, string> dictionary )
        {
            SeifApplication.AppEnv.GlobalConfiguration.ProviderConfiguration.ApiDomain = hostDomain;
            SeifApplication.AppEnv.GlobalConfiguration.ProviderConfiguration.ApiIpAddress =
                UriUtils.GetIpAddressByDomainName(hostDomain);
            SeifApplication.AppEnv.GlobalConfiguration.ProviderConfiguration.SerializeMode = serializeMode;
            SeifApplication.AppEnv.GlobalConfiguration.ProviderConfiguration.AddtionalFields =
                DictionaryUtils.ToConfig(dictionary);

            return SeifApplication.AppEnv;
        }

        #endregion

        #region Consumer

        public static SeifApplication AsConsumer(string url, Type resultHandlerType)
        {
            SeifApplication.AppEnv.GlobalConfiguration.ConsumerConfiguration.Url = url;
            SeifApplication.AppEnv.GlobalConfiguration.ConsumerConfiguration.ResultHandlerDefinition =
                resultHandlerType.AssemblyQualifiedName;

            return SeifApplication.AppEnv;
        }

        #endregion
    }
}