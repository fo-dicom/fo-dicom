using System.Threading.Tasks;

// ReSharper disable CheckNamespace
namespace System.Threading
// ReSharper restore CheckNamespace
{
    public class Thread
    {
        #region FIELDS

        private readonly Action _start;
        private readonly Action<object> _parameterizedStart;
        private Task _task;

        #endregion

        #region CONSTRUCTORS

        public Thread(Action start)
        {
            _start = start;
            _parameterizedStart = null;
            _task = null;
        }

        public Thread(Action<object> parameterizedStart)
        {
            _start = null;
            _parameterizedStart = parameterizedStart;
            _task = null;
        }

        #endregion

        #region PROPERTIES

        public bool IsBackground { get; set; }

        #endregion

        #region METHODS

        public void Start()
        {
            if (_start == null) throw new InvalidOperationException("Parameter-less action not defined for this thread instance.");
            _task = Task.Run(_start);
        }

        public void Start(object parameter)
        {
            if (_parameterizedStart == null) throw new InvalidOperationException("Parameterized action not defined for this thread instance.");
            _task = Task.Run(() => _parameterizedStart(parameter));
        }

        public static void Sleep(int millisecondsTimeout)
        {
            new ManualResetEvent(false).WaitOne(millisecondsTimeout);
        }

        #endregion
    }
}