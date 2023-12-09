// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging.Mathematics
{

    /// <summary>
    /// Representation of a histogram.
    /// 
    /// The <see cref="Histogram"/> can be seen as an integer array of value counts divided into bins from a specified
    /// minimum to maximum value. Bins are accessed via their absolute position, regardless of specified minimum bin.
    /// </summary>
    public class Histogram
    {
        #region FIELDS

        private readonly int[] _values;

        private int _total;

        private readonly int _offset;

        private int _window;

        private int _wstart;

        private int _wend;

        private int _wtotal;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an intstance of the <see cref="Histogram"/> class.
        /// </summary>
        /// <param name="min">Minimum histogram bin.</param>
        /// <param name="max">Maximum histogram bin.</param>
        public Histogram(int min, int max)
        {
            var range = max - min + 1;
            _values = new int[range];
            _offset = -min;
            _wstart = 0;
            _wend = _values.Length - 1;
            _window = -1;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the start bin of the histogram window.
        /// </summary>
        public int WindowStart => _wstart - _offset;

        /// <summary>
        /// Gets the end bin of the histogram window.
        /// </summary>
        public int WindowEnd => _wend - _offset;

        /// <summary>
        /// Gets the total sum inside the window given by <see cref="WindowStart"/> and <see cref="WindowEnd"/>.
        /// </summary>
        public int WindowTotal
        {
            get
            {
                if (_window == -1) ApplyWindow(100);
                return _wtotal;
            }
        }

        /// <summary>
        /// Gets the value count at histogram bin <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Bin at which value count is requested.</param>
        /// <returns>Value count at <paramref name="value">bin</paramref>.</returns>
        public int this[int value]
        {
            get
            {
                var pos = value + _offset;
                if (pos < 0 || pos >= _values.Length) return 0;
                return _values[pos];
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Increment histogram at bin position <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Bin position at which histogram should be incremented.</param>
        public void Add(int value)
        {
            var pos = value + _offset;
            if (pos < 0 || pos >= _values.Length) return;

            _values[pos]++;
            _total++;

            if (pos >= _wstart && pos <= _wend) _wtotal++;
        }

        /// <summary>
        /// Reset histogram at bin position <paramref name="value"/> to 0.
        /// </summary>
        /// <param name="value">Bin position at which histogram should be reset.</param>
        public void Clear(int value)
        {
            var pos = value + _offset - 1;
            if (pos < 0 || pos >= _values.Length) return;

            _total -= _values[pos];
            if (pos >= _wstart && pos <= _wend) _wtotal -= _values[pos];

            _values[pos] = 0;
        }

        /// <summary>
        /// Define <see cref="WindowStart"/> and <see cref="WindowEnd"/> properties by gradually shrinking the 
        /// window until the value count inside the window is  less than or equal to <paramref name="percent"/> % 
        /// of the total value count.
        /// </summary>
        /// <param name="percent">Target percentage for the active window.</param>
        public void ApplyWindow(int percent)
        {
            _wstart = 0;
            _wend = _values.Length - 1;
            _window = percent;
            _wtotal = _total;

            if (percent == 100 || _total == 0) return;

            var target = (int)(_total * (percent / 100.0));

            while (_wtotal > target)
            {
                var wtotal = _wtotal;
                if (_values[_wstart] >= _values[_wend])
                {
                    wtotal -= _values[_wstart];
                    if (wtotal < target) break;
                    _wstart++;
                }
                else
                {
                    wtotal -= _values[_wend];
                    if (wtotal < target) break;
                    _wend--;
                }

                _wtotal = wtotal;

                if (_wstart == _wend) break;
            }
        }

        /// <summary>
        /// Apply an active histogram window at the specified <paramref name="start"/> and <paramref name="end"/> bins.
        /// </summary>
        /// <param name="start">Position of the window's start bin.</param>
        /// <param name="end">Position of the window's end bin.</param>
        public void ApplyWindow(int start, int end)
        {
            _wstart = start + _offset;
            _wend = end + _offset;

            if (_wstart == 0 && _wend == _values.Length - 1)
            {
                _window = 100;
                _wtotal = _total;
                return;
            }

            for (var i = _wstart; i <= _wend; i++) _wtotal += _values[i];

            _window = (int)(_wtotal / (double)_total);
        }

        #endregion
    }
}
