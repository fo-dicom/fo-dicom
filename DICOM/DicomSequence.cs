using System;
using System.Collections.Generic;

namespace Dicom {
	public class DicomSequence : DicomItem, IEnumerable<DicomDataset> {
		private IList<DicomDataset> _items;

		public DicomSequence(DicomTag tag, params DicomDataset[] items) : base(tag) {
			_items = new List<DicomDataset>(items);
		}

		public override DicomVR ValueRepresentation {
			get { return DicomVR.SQ; }
		}

		public IList<DicomDataset> Items {
			get { return _items; }
		}

		public IEnumerator<DicomDataset> GetEnumerator() {
			return _items.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return _items.GetEnumerator();
		}
	}
}
