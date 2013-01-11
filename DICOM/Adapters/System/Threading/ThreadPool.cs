using System.Threading.Tasks;

// ReSharper disable CheckNamespace

namespace System.Threading
// ReSharper restore CheckNamespace
{
    public delegate void WaitCallback(object state);

    public static class ThreadPool
    {
         public static bool QueueUserWorkItem(WaitCallback callBack, object state = null)
         {
             Task.Run(() => callBack(state));
             return true;
         }
    }
}