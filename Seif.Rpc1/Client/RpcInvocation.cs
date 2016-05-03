using System;
using System.Collections.Generic;

namespace Seif.Rpc.Client
{
    public class RpcInvocation : IInvocation
    {
        private readonly string _serviceName;
        private readonly string _methodName;
        private readonly IDictionary<Type, object> _parameters;


        public RpcInvocation(string serviceName, string methodName, IDictionary<Type,object> parameters)
        {
            _serviceName = serviceName;
            _methodName = methodName;
            _parameters = parameters;
        }

        public string ServiceName
        {
            get { return _serviceName; }
        }

        public string MethodName
        {
            get { return _methodName; }
        }

        public IDictionary<Type, object> Parameters
        {
            get { return _parameters; }
        }
    }
}