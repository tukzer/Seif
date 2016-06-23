using System;
using Seif.Rpc.Utils;

namespace Seif.Rpc.Common
{
    public class ParameterData
    {
        public string TypeName { get; set; }
        public string Data { get; set; }
    }
    public class ParameterExt
    {
        private Type _type;
        private object _value;

        public static ParameterExt From(ParameterData data, ISerializer serializer)
        {
            var para = new ParameterExt();
            para.Type = TypeUtils.GetType(data.TypeName);
            para.Value = serializer.Deserialize(para.Type, data.Data);
            return para;
        }

        public Type Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

}