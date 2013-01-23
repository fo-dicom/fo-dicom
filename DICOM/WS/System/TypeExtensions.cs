using System.Linq;
using System.Reflection;

// ReSharper disable CheckNamespace
namespace System
// ReSharper restore CheckNamespace
{
    public static class TypeExtensions
    {
        public static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

        public static bool IsValueType(this Type type)
        {
            return type.GetTypeInfo().IsValueType;
        }

        public static bool IsAssignableFrom(this Type type, Type c)
        {
            return type.GetTypeInfo().IsAssignableFrom(c.GetTypeInfo());
        }

        public static bool IsSubclassOf(this Type type, Type c)
        {
            return type.GetTypeInfo().IsSubclassOf(c);
        }

        public static MethodInfo GetMethod(this Type type, string name, BindingFlags bindingAttr)
        {
            return
                type.GetRuntimeMethods()
                    .Where(mi => AreBindingFlagsMatching(mi, bindingAttr))
                    .SingleOrDefault(mi => mi.Name.Equals(name));
        }

        private static bool AreBindingFlagsMatching(MethodInfo methodInfo, BindingFlags bindingAttr)
        {
            var publicFlag = bindingAttr.HasFlag(BindingFlags.Public);
            var nonPublicFlag = bindingAttr.HasFlag(BindingFlags.NonPublic);
            if (publicFlag == nonPublicFlag) throw new ArgumentException("Binding must be set to either public or non-public.");

            var staticFlag = bindingAttr.HasFlag(BindingFlags.Static);
            var instanceFlag = bindingAttr.HasFlag(BindingFlags.Instance);
            if (staticFlag == instanceFlag) throw new ArgumentException("Binding must be set to either static or instance.");

            return ((methodInfo.IsPublic && publicFlag) || (!methodInfo.IsPublic && nonPublicFlag)) &&
                   ((methodInfo.IsStatic && staticFlag) || (!methodInfo.IsStatic && instanceFlag));
        }
    }
}