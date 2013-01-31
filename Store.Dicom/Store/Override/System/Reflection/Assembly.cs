using System.Reflection;

// ReSharper disable CheckNamespace
namespace Override.System.Reflection
// ReSharper restore CheckNamespace
{
    internal static class Assembly
    {
        internal static global::System.Reflection.Assembly GetExecutingAssembly()
        {
            return typeof(Assembly).GetTypeInfo().Assembly;
        }
    }
}