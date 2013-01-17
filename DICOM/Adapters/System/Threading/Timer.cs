// ReSharper disable CheckNamespace
namespace System.Threading
// ReSharper restore CheckNamespace
{
    internal delegate void TimerCallback(object state);

    internal class Timer
    {
        #region CONSTRUCTORS

        internal Timer(TimerCallback callback)
        {
            
        }

	    internal Timer(TimerCallback callback, object state, int dueTime, int period)
	    {
		    
	    }

		#endregion

        #region METHODS

        internal bool Change(int dueTime, int period)
        {
            return true;
        }

        #endregion
    }
}