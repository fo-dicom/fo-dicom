// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using Windows.System.Threading;

namespace System.Threading
{
    public delegate void TimerCallback(object state);

    public class Timer
	{
		#region FIELDS

	    private readonly TimerCallback _callback;
	    private readonly object _state;
	    private ThreadPoolTimer _timer;

		#endregion

		#region CONSTRUCTORS

		public Timer(TimerCallback callback, object state = null, int dueTime = Timeout.Infinite, int period = Timeout.Infinite)
		{
			_callback = callback;
			_state = state;
			_timer = CreateThreadPoolTimer(callback, state, dueTime, period);
		}

	    #endregion

        #region METHODS

        public bool Change(int dueTime, int period)
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