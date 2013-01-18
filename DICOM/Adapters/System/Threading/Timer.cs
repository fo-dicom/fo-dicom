using Windows.System.Threading;

// ReSharper disable CheckNamespace
namespace System.Threading
// ReSharper restore CheckNamespace
{
    internal delegate void TimerCallback(object state);

    internal class Timer
	{
		#region FIELDS

	    private readonly TimerCallback _callback;
	    private readonly object _state;
	    private ThreadPoolTimer _timer;

		#endregion

		#region CONSTRUCTORS

		internal Timer(TimerCallback callback, object state = null, int dueTime = Timeout.Infinite, int period = Timeout.Infinite)
		{
			_callback = callback;
			_state = state;
			_timer = CreateThreadPoolTimer(callback, state, dueTime, period);
		}

	    #endregion

        #region METHODS

        internal bool Change(int dueTime, int period)
        {
			if (_timer != null) _timer.Cancel();
	        _timer = CreateThreadPoolTimer(_callback, _state, dueTime, period);
            return _timer != null;
        }

	    private static ThreadPoolTimer CreateThreadPoolTimer(TimerCallback callback, object state, int dueTime, int period)
	    {
		    if (dueTime == Timeout.Infinite) return null;
		    return period == Timeout.Infinite
			             ? ThreadPoolTimer.CreateTimer(timer => callback(state), TimeSpan.FromMilliseconds(dueTime))
			             : ThreadPoolTimer.CreatePeriodicTimer(timer => callback(state), TimeSpan.FromMilliseconds(period));
	    }

        #endregion
    }
}