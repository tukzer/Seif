using System;
using System.Linq;
using Seif.Rpc.Dispatch;

namespace Seif.Rpc.Invoke.Default
{
    public class DefaultInvokeDispatcher :  IInvokeDispatcher
    {
        public IDispathStragedy Stragedy { get; set; }


        public IInvoker Select<T>(DispatchOptions options)
        {
            if (options.ServiceKind == ServiceKind.Local)
            {
                return new LocalInvoker<T>();
            }

            var metta = SeifApplication.AppEnv.ServiceRegistry.GetServiceRegistryMetta<T>();

            if (metta == null || !metta.Any())
            {
                throw new Exception("Invoker Metta not exists");
            }

            var selectedInvokeMetta = metta.Length == 1 ? metta[0] : Stragedy.Select(typeof(T), metta);

            var invokerFactory = SeifApplication.Resolve<IInvokerFactory>();
            return invokerFactory.CreateInvoker(selectedInvokeMetta);
        }
    }
}