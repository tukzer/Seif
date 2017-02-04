using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Seif.Rpc.Common;

namespace Seif.Rpc.Utils
{
    public static class TypeUtils
    {
        public static T LoadInstance<T>(string typeDef)
        {
            var typeDefArr = typeDef.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            if(typeDefArr.Length <= 2) throw new Exception("Error Type Definition");

            var assemblyName = string.Join(",", typeDefArr.Skip(1));
            //byte[] buffer = System.IO.File.ReadAllBytes(assemblyName);
            var assembly = Assembly.Load(assemblyName);
            return (T) assembly.CreateInstance(typeDefArr[0]);
        }

        /// <summary>
        /// 根据类型名称获取类型信息。如果当前程序集中找不到，从当前的AppDomain中查找。
        /// </summary>
        /// <param name="fullTypeName"></param>
        /// <returns></returns>
        public static Type GetType(string fullTypeName)
        {
            if (SeifApplication.AppEnv.ExportedTypes.ContainsKey(fullTypeName))
            {
                return  SeifApplication.AppEnv.ExportedTypes[fullTypeName];
            }

            var type = Type.GetType(fullTypeName);
            if (type != null) return type;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(fullTypeName);
                if (type != null) return type;
            }

            return null;
        }

        public static string GetTypeDefinition<TImpl>()
        {
            return typeof (TImpl).AssemblyQualifiedName;
        }

        public static IDictionary<Type, object> ConvertTypeMap(ISerializer serializer, IDictionary<string, string> paraObjects)
        {
            IDictionary<Type, object> dic = new Dictionary<Type, object>();

            if (paraObjects == null || paraObjects.Count < 1) return dic;

            foreach (var paraObject in paraObjects)
            {
                Type type = TypeUtils.GetType(paraObject.Key);
                dic.Add(type, serializer.Deserialize(type, paraObject.Value));
            }
            return dic;
        }        
        
    }
}