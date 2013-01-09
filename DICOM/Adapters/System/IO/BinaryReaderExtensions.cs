// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    public static class BinaryReaderExtensions
    {
         public static void Close(this BinaryReader reader)
         {
             reader.Dispose();
         }
    }
}