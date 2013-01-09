// ReSharper disable CheckNamespace
namespace System.Threading
// ReSharper restore CheckNamespace
{
    public delegate void WaitCallback(object state);

    public static class ThreadPool
    {
         public static bool QueueUserWorkItem(WaitCallback callBack)
         {
             return true;
         }

         public static bool QueueUserWorkItem(WaitCallback callBack, object state)
         {
             return true;
         }
    }
}