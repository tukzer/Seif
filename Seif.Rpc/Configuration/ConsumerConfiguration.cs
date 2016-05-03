using System.Configuration;
using Seif.Rpc.Dispatch;
using Seif.Rpc.Invoke;
using Seif.Rpc.Utils;

namespace Seif.Rpc.Configuration
{
    public class ConsumerConfiguration : ConfigurationElement
    {
        private IInvokerFactory _invokerFactory;
        private IInvokeDispatcher _invokeDispatcher;

        [ConfigurationProperty("NodeCode", IsRequired = true)]
        public string NodeCode { get; set; }
        [ConfigurationProperty("Url", IsRequired = true)]
        public string Url { get; set; }

        [ConfigurationProperty("InvokerFactory", IsRequired = true)]
        public string InvokerFactoryDefinition { get; set; }
        [ConfigurationProperty("InvokerDispatcher", IsRequired = true)]
        public string InvokerDispatcherDefinition { get; set; }

        public IInvokerFactory InvokerFactory
        {
            get
            {
                if (_invokerFactory == null)
                {
                    _invokerFactory = TypeUtils.LoadInstance<IInvokerFactory>(this.InvokerFactoryDefinition);
                }
                return _invokerFactory;
            }
            private set { _invokerFactory = value; }
        }

        public IInvokeDispatcher InvokeDispatcher
        {
            get
            {
                if (_invokeDispatcher == null)
                {
                    _invokeDispatcher = TypeUtils.LoadInstance<IInvokeDispatcher>(this.InvokerDispatcherDefinition);
                }

                return _invokeDispatcher;
            }
            private set { _invokeDispatcher = value; }
        }

        public void SetInvokerFactory(IInvokerFactory invokerFactory)
        {
            InvokerFactory = invokerFactory;
        }
        public void SetInvokeDispatcher(IInvokeDispatcher dispatcher)
        {
            InvokeDispatcher = dispatcher;
        }
    }
}