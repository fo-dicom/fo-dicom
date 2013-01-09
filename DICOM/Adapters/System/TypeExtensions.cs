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
            return type.GetRuntimeMethods().SingleOrDefault(mi => mi.Name.Equals(name));
        }

    }
}