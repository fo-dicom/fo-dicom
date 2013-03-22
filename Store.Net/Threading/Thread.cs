// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using System.Threading.Tasks;

namespace System.Threading
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