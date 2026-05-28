// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Linq;
using System.Numerics;

namespace FellowOakDicom.Imaging.Mathematics
{

    public class MovingAverage
    {
        private readonly int _window;

        private readonly int[] _values;

        public MovingAverage(int window)
        {
            _window = window;
            _values = new int[_window];
            Count = 0;
        }

        public int Count { get; private set; }

        public int Next(int value)
        {
            _values[Count % _window] = value;
            Count++;
            if (Count < _window)
            {
                return _values.Sum() / Count;
            }

            return _values.Sum() / _window;
        }
    }


    public class MovingAverage<T> where T : INumber<T>
    {
        private readonly int _window;

        private readonly T[] _values;

        public MovingAverage(int window)
        {
            _window = window;
            _values = new T[_window];
            Count = 0;
        }

        public int Count { get; private set; }

        public T Next(T value)
        {
            _values[Count % _window] = value;
            Count++;
            if (Count < _window)
            {
                return Sum() / T.CreateChecked(Count);
            }

            return Sum() / T.CreateChecked(_window);
        }

        private T Sum()
        {
            T sum = T.Zero;
            for (var i = 0; i < _values.Length; i++)
            {
                sum += _values[i];
            }
            return sum;
        }
    }
}
