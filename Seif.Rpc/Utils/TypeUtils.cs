using System;
using System.Collections.Generic;
using System.Reflection;

namespace Seif.Rpc.Utils
{
    public static class TypeUtils
    {
        public static T LoadInstance<T>(string typeDef)
        {
            var typeDefArr = typeDef.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            if(typeDefArr.Length != 2) throw new Exception("Error Type Definition");

            var assembly = Assembly.Load(typeDefArr[1]);
            return (T) assembly.CreateInstance(typeDefArr[0]);
        }
    }
}