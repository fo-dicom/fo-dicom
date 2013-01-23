using Windows.Foundation;

// ReSharper disable CheckNamespace
namespace System.Threading
// ReSharper restore CheckNamespace
{
    public delegate void WaitCallback(object state);

    internal static class ThreadPool
    {
         internal static bool QueueUserWorkItem(WaitCallback callBack, object state = null)
         {
	         var workItem = global::Windows.System.Threading.ThreadPool.RunAsync(source => callBack(state));
             return workItem.Status != AsyncStatus.Error;
         }
    }
}