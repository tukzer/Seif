using System;
using System.Collections.Generic;
using Seif.Soa.Client;

namespace Seif.Soa.Proxy
{
    public class RpcInvocation : IInvocation
    {
        private readonly string _serviceName;
        private readonly string _methodName;
        private readonly IDictionary<Type, object> _parameters;
        private readonly string _url;

        public string Url
        {
            get { return _url; }
        }

        public RpcInvocation(string url, string serviceName, string methodName, IDictionary<Type,object> parameters)
        {
            _url = url;
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

        public IDictionary<string, string> Attributes
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}