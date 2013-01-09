using System.Linq;
using System.Reflection;

// ReSharper disable CheckNamespace
namespace System
// ReSharper restore CheckNamespace
{
    public static class TypeExtensions
    {
        public static bool IsAssignableFrom(this Type obj, Type c)
        {
            return obj.GetTypeInfo().IsAssignableFrom(c.GetTypeInfo());
        }

        public static bool IsSubclassOf(this Type obj, Type c)
        {
            return obj.GetTypeInfo().IsSubclassOf(c);
        }

        public static MethodInfo GetMethod(this Type obj, string name, BindingFlags bindingAttr)
        {
            return obj.GetRuntimeMethods().SingleOrDefault(mi => mi.Name.Equals(name));
        }

    }
}