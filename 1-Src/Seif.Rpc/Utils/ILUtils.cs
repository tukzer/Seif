using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Web.UI;

namespace Seif.Rpc.Utils
{
    /// <summary>
    /// IL相关的辅助类
    /// </summary>
    public class ILUtils
    {
        private const string ASSEMBLY_NAME = "EmitWraper";
        private const string MODULE_NAME = "EmitModule_";
        private const string TYPE_NAME = "Emit_";
        private const TypeAttributes TYPE_ATTRIBUTES = TypeAttributes.Public | TypeAttributes.Class;
        private const MethodAttributes METHOD_ATTRIBUTES = MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual;


        private static Type BuildType(Type type)
        {
            AssemblyName an = new AssemblyName(ASSEMBLY_NAME);
            an.SetPublicKey(Assembly.GetExecutingAssembly().GetName().GetPublicKey());

            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an,
                AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(MODULE_NAME + type.Name);
            var typeBuilder = moduleBuilder.DefineType(TYPE_NAME + type.Name, TYPE_ATTRIBUTES, type);

            BuildConstructor(typeBuilder, type);

            BuildMethod(typeBuilder, type);

            return typeBuilder.CreateType();
        }

        private static void BuildMethod(TypeBuilder typeBuilder, Type type)
        {
            throw new NotImplementedException();
        }

        private static void BuildConstructor(TypeBuilder typeBuilder, Type type)
        {
            throw new NotImplementedException();
        }
    }
}