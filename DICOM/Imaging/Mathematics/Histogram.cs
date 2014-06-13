using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging.Mathematics {
	public class Histogram {
		private int[] _values;
		private int _total;
		private int _min;
		private int _max;
		private int _offset;

		private int _window;
		private int _wstart;
		private int _wend;
		private int _wtotal;

		public Histogram(int min, int max) {
			int range = max - min + 1;
			_values = new int[range];
			_min = min;
			_max = max;
			_offset = -_min;
			_wstart = 0;
			_wend = _values.Length - 1;
			_window = -1;
		}

		public int WindowStart {
			get { return _wstart - _offset; }
		}

		public int WindowEnd {
			get { return _wend - _offset; }
		}

		public int WindowTotal {
			get {
				if (_window == -1)
					ApplyWindow(100);
				return _wtotal;
			}
		}

		public int this[int value] {
			get {
				int pos = value + _offset;
				if (pos < 0 || pos >= _values.Length)
					return 0;
				return _values[pos];
			}
		}

		public void Add(int value) {
			int pos = value + _offset;
			if (pos < 0 || pos >= _values.Length)
				return;

			_values[pos]++;
			_total++;

			if (pos >= _wstart && pos <= _wend)
				_wtotal++;
		}

		public void Clear(int value) {
			int pos = value + _offset - 1;
			if (pos < 0 || pos >= _values.Length)
				return;

			_total -= _values[pos];
			if (pos >= _wstart && pos <= _wend)
				_wtotal -= _values[pos];

			_values[pos] = 0;
		}

		public void ApplyWindow(int percent) {
			_wstart = 0;
			_wend = _values.Length - 1;
			_window = percent;
			_wtotal = _total;

			if (percent == 100 || _total == 0)
				return;

			var target = (int)(_total * (percent / 100.0));

			while (_wtotal > target) {
				var wtotal = _wtotal;
				if (_values[_wstart] >= _values[_wend]) {
					wtotal -= _values[_wstart];
					if (wtotal < target)
						break;
					_wstart++;
				} else {
					wtotal -= _values[_wend];
					if (wtotal < target)
						break;
					_wend--;
				}

				_wtotal = wtotal;

				if (_wstart == _wend)
					break;
			}
		}

		public void ApplyWindow(int start, int end) {
			_wstart = start + _offset;
			_wend = end + _offset;

			if (_wstart == 0 && _wend == _values.Length - 1) {
				_window = 100;
				_wtotal = _total;
				return;
			}

			for (int i = _wstart; i <= _wend; i++)
				_wtotal += _values[i];

			_window = (int)((double)_wtotal / (double)_total);
		}
	}
}
