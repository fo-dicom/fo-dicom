// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Linq;

namespace FellowOakDicom.Imaging.Mathematics
{

    public class MovingAverage
    {
        private readonly int _window;

        private readonly int[] _values;

        private int _count;

        public MovingAverage(int window)
        {
            _window = window;
            _values = new int[_window];
            _count = 0;
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public int Next(int value)
        {
            _values[_count % _window] = value;
            _count++;
            if (_count < _window) return _values.Sum() / _count;
            return _values.Sum() / _window;
        }
    }

    public class MovingAverageF
    {
        private readonly int _window;

        private readonly float[] _values;

        private int _count;

        public MovingAverageF(int window)
        {
            _window = window;
            _values = new float[_window];
            _count = 0;
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public float Next(float value)
        {
            _values[_count % _window] = value;
            _count++;
            if (_count < _window) return _values.Sum() / _count;
            return _values.Sum() / _window;
        }
    }

    public class MovingAverageD
    {
        private readonly int _window;

        private readonly double[] _values;

        private int _count;

        public MovingAverageD(int window)
        {
            _window = window;
            _values = new double[_window];
            _count = 0;
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public double Next(double value)
        {
            _values[_count % _window] = value;
            _count++;
            if (_count < _window) return _values.Sum() / _count;
            return _values.Sum() / _window;
        }
    }
}
