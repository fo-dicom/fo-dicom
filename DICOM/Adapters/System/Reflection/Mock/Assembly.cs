// ReSharper disable CheckNamespace
namespace System.Reflection.Mock
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