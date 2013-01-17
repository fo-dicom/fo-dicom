// ReSharper disable CheckNamespace

using System.Threading.Tasks;
using Dicom;

namespace System.IO
// ReSharper restore CheckNamespace
{
    internal static class StreamExtensions
    {
        internal static void Close(this Stream stream)
        {
            stream.Dispose();
        }

        internal static IAsyncResult BeginWrite(this Stream stream, byte[] buffer, int offset, int count,
                                              AsyncCallback callback, object state)
        {
            return Task.Run(() => stream.Write(buffer, offset, count)).ContinueWith(task => callback(new EventAsyncResult(null, state)));
        }

        internal static void EndWrite(this Stream stream, IAsyncResult asyncResult)
        {
        }

		internal static IAsyncResult BeginRead(this Stream stream, byte[] buffer, int offset, int count,
											  AsyncCallback callback, object state)
		{
			return Task.Run(() => stream.Read(buffer, offset, count)).ContinueWith(task => callback(new EventAsyncResult(null, state)));
		}

		internal static int EndRead(this Stream stream, IAsyncResult asyncResult)
		{
			return ((byte[])asyncResult.AsyncState).Length;
		}
	}
}