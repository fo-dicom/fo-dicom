// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    public static class StreamExtensions
    {
        public static void Close(this Stream stream)
        {
            stream.Dispose();
        }

        public static IAsyncResult BeginWrite(this Stream stream, byte[] buffer, int offset, int count,
                                              AsyncCallback callback, object state)
        {
            return null;
        }

        public static void EndWrite(this Stream stream, IAsyncResult asyncResult)
        {
        }
    }
}