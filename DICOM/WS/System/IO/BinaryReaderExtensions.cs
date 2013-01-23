// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    internal static class BinaryReaderExtensions
    {
         internal static void Close(this BinaryReader reader)
         {
             reader.Dispose();
         }
    }
}