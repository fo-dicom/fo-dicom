using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging.Mathematics {
	public class BitList {
		private List<byte> _bytes;

		public BitList() {
			_bytes = new List<byte>();
		}

		public int Capacity {
			get { return _bytes.Count * 8; }
			set {
				int count = value / 8;
				if (value % 8 > 0)
					count++;
				while (_bytes.Count < count)
					_bytes.Add(0);
			}
		}

		public List<byte> List {
			get { return _bytes; }
		}

		public byte[] Array {
			get { return _bytes.ToArray(); }
		}

		public bool this[int pos] {
			get {
				int p = pos / 8;
				int m = pos % 8;

				if (p >= _bytes.Count)
					return false;

				var b = _bytes[p];

				return (b & (1 << m)) != 0;
			}
			set {
				int p = pos / 8;
				int m = pos % 8;

				if (p >= _bytes.Count)
					Capacity = pos + 1;

				if (value)
					_bytes[p] |= (byte)(1 << m);
				else
					_bytes[p] -= (byte)(1 << m);
			}
		}
	}
}
