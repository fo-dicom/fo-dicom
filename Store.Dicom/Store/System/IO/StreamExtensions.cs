using System.Threading.Tasks;

// ReSharper disable CheckNamespace
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
	        return
		        new TaskFactory().StartNew(asyncState => stream.Write(buffer, offset, count), state)
		                         .ContinueWith(task => callback(task));
        }

        internal static void EndWrite(this Stream stream, IAsyncResult asyncResult)
        {
        }

		internal static IAsyncResult BeginRead(this Stream stream, byte[] buffer, int offset, int count,
											  AsyncCallback callback, object state)
		{
			return new TaskFactory<int>().StartNew(asyncState => stream.Read(buffer, offset, count), state)
			                             .ContinueWith(task =>
				                                           {
					                                           callback(task);
					                                           return task.Result;
				                                           });
		}

		internal static int EndRead(this Stream stream, IAsyncResult asyncResult)
		{
			var task = asyncResult as Task<int>;
			return task != null ? task.Result : 0;
		}
	}
}