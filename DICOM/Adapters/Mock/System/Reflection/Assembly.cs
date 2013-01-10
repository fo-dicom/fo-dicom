using System.Reflection;

// ReSharper disable CheckNamespace
namespace Mock.System.Reflection
// ReSharper restore CheckNamespace
{
    public static class Assembly
    {
        public static global::System.Reflection.Assembly GetExecutingAssembly()
        {
            return typeof(Assembly).GetTypeInfo().Assembly;
        }
    }
}