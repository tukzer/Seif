using System;
using System.Collections.Generic;

namespace Seif.Rpc
{
    public class RpcResult<T>
    {
        private readonly Exception _ex;
        private readonly T _value;
        private IDictionary<string, string> _dicAttrs;

        public RpcResult(T value, IDictionary<string, string> dicAttrs = null)
        {
            _value = value;
            _dicAttrs = dicAttrs ?? new Dictionary<string, string>();
        }

        public RpcResult(Exception ex, IDictionary<string, string> dicAttrs = null)
        {
            _ex = ex;
            _dicAttrs = dicAttrs ?? new Dictionary<string, string>();
        }

        public T Value
        {
            get { return _value; }
        }

        public Exception Exception
        {
            get { return _ex; }
        }

        public IDictionary<string, string> Attributes
        {
            get { return _dicAttrs; }
            set { _dicAttrs = value; }
        }

        public bool HasException()
        {
            return _ex != null;
        }

        public void SetAttributes(string key, string val)
        {
            _dicAttrs.Add(key, val);
        }

        public void RemoveAttributes(string key)
        {
            _dicAttrs.Remove(key);
        }

    }
}