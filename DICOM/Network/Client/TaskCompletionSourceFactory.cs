using System.Threading.Tasks;

namespace Dicom.Network.Client
{
    internal static class TaskCompletionSourceFactory
    {
        public static TaskCompletionSource<T> Create<T>()
        {
#if NETSTANDARD
            return new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
#else
            return new TaskCompletionSource<T>();
#endif
        }
    }
}
