// ReSharper disable CheckNamespace
namespace System.Threading
// ReSharper restore CheckNamespace
{
    public delegate void TimerCallback(object state);

    public class Timer
    {
        #region CONSTRUCTORS

        public Timer(TimerCallback callback)
        {
            
        }

        #endregion

        #region METHODS

        public bool Change(int dueTime, int period)
        {
            return true;
        }

        #endregion
    }
}