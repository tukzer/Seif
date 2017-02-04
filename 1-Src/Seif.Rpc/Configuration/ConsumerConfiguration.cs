using System.Configuration;
using Seif.Rpc.Invoke;
using Seif.Rpc.Utils;

namespace Seif.Rpc.Configuration
{
    public class ConsumerConfiguration : ConfigurationElement
    {
        private ResultHandler _handler;

        [ConfigurationProperty("NodeCode", IsRequired = true)]
        public string NodeCode
        {
            get { return this["NodeCode"].ToString(); }
            set { this["NodeCode"] = value; }
        }

        [ConfigurationProperty("Url", IsRequired = true)]
        public string Url
        {
            get { return this["Url"].ToString(); }
            set { this["Url"] = value; }
        }

        [ConfigurationProperty("ResultHandler")]
        public string ResultHandlerDefinition
        {
            get { return this["ResultHandler"].ToString(); }
            set { this["ResultHandler"] = value; }
        }

        public ResultHandler GetResultHandler()
        {
            if (string.IsNullOrEmpty(ResultHandlerDefinition))
            {
                _handler = new ResultHandler();
                return _handler;
            }

            _handler = TypeUtils.LoadInstance<ResultHandler>(ResultHandlerDefinition) ?? new ResultHandler();

            return _handler;
        }


        //public void SetInvokerFactory(IInvokerFactory invokerFactory)
        //{
        //    InvokerFactory = invokerFactory;
        //    SeifApplication.AppEnv.TypeBuilder.RegisterType(invokerFactory);
        //}
        //public void SetInvokeDispatcher(IInvokeDispatcher dispatcher)
        //{
        //    InvokeDispatcher = dispatcher;
        //    SeifApplication.AppEnv.TypeBuilder.RegisterType(dispatcher);
        //}
    }
}