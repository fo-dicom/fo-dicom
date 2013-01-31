// ReSharper disable CheckNamespace
namespace System.Reflection
// ReSharper restore CheckNamespace
{
    [Flags]
    public enum BindingFlags
    {
        Instance = 0x0001,
        Static = 0x0010,
        NonPublic = 0x0100,
        Public = 0x1000
    }
}