using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.IO;
using Dicom.IO.Buffer;

namespace Dicom {
	public abstract class DicomFragmentSequence : DicomItem, IEnumerable<IByteBuffer> {
		private IList<uint> _offsetTable;
		private IList<IByteBuffer> _fragments;

		protected DicomFragmentSequence(DicomTag tag) : base(tag) {
			_fragments = new List<IByteBuffer>();
		}

		public IList<uint> OffsetTable {
			get {
				if (_offsetTable == null)
					_offsetTable = new List<uint>();
				return _offsetTable;
			}
		}

		public IList<IByteBuffer> Fragments {
			get { return _fragments; }
		}

		internal void Add(IByteBuffer fragment) {
			if (_offsetTable == null) {
				var en = ByteBufferEnumerator<uint>.Create(fragment);
				_offsetTable = new List<uint>(en);
				return;
			}
			_fragments.Add(fragment);
		}

		public IEnumerator<IByteBuffer> GetEnumerator() {
			return _fragments.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return _fragments.GetEnumerator();
		}
	}

	public abstract class DicomFragmentSequenceT<T> : DicomFragmentSequence {
		protected DicomFragmentSequenceT(DicomTag tag) : base(tag) {
		}
	}

	public class DicomOtherByteFragment : DicomFragmentSequenceT<byte> {
		public DicomOtherByteFragment(DicomTag tag) : base(tag) {
		}

		public override DicomVR ValueRepresentation {
			get { return DicomVR.OB; }
		}
	}

	public class DicomOtherWordFragment : DicomFragmentSequenceT<ushort> {
		public DicomOtherWordFragment(DicomTag tag) : base(tag) {
		}

		public override DicomVR ValueRepresentation {
			get { return DicomVR.OW; }
		}
	}
}
