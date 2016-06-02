using System;
using System.Linq;
using Seif.Rpc.Dispatch;

namespace Seif.Rpc.Invoke.Default
{
    public class DefaultInvokeDispatcher :  IInvokeDispatcher
    {
        public IInvoker Dispatch<T>(DispatchOptions options)
        {
            if (options.ServiceKind == ServiceKind.Local)
            {
                return new LocalInvoker<T>();
            }

            var metta = SeifApplication.AppEnv.GlobalConfiguration.Registry.GetServiceRegistryMetta<T>();

            if (metta == null || !metta.Any())
            {
                throw new Exception("Invoker Metta not exists");
            }

            var selectedInvokeMetta = metta.Length == 1 ? metta[0] : options.Stragedy.Select(typeof(T), metta);

            var invokerFactory = SeifApplication.AppEnv.GlobalConfiguration.InvokerFactory;//SeifApplication.Resolve<IInvokerFactory>();
            return invokerFactory.CreateInvoker(selectedInvokeMetta);
        }
    }
}