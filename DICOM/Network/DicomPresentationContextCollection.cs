using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomPresentationContextCollection : ICollection<DicomPresentationContext> {
		private SortedList<byte, DicomPresentationContext> _pc;

		public DicomPresentationContextCollection() {
			_pc = new SortedList<byte, DicomPresentationContext>();
		}

		public DicomPresentationContext this[byte id] {
			get { return _pc[id]; }
		}

		private byte GetNextPresentationContextID() {
			if (_pc.Count == 0)
				return 1;

			return (byte)(_pc.Max(x => x.Key) + 2);
		}

		public void Add(DicomUID abstractSyntax, params DicomTransferSyntax[] transferSyntaxes) {
			var pc = new DicomPresentationContext(GetNextPresentationContextID(), abstractSyntax);

			foreach (var tx in transferSyntaxes)
				pc.AddTransferSyntax(tx);

			Add(pc);
		}

		public void Add(DicomPresentationContext item) {
			_pc.Add(item.ID, item);
		}

		public void AddFromRequest(DicomRequest request) {
			if (request is DicomCStoreRequest) {
				var cstore = request as DicomCStoreRequest;
				var pc = _pc.Values.FirstOrDefault(x => x.AbstractSyntax == request.AffectedSOPClassUID && x.AcceptedTransferSyntax == cstore.TransferSyntax);
				if (pc == null) {
					if (cstore.TransferSyntax != DicomTransferSyntax.ImplicitVRLittleEndian)
						Add(cstore.AffectedSOPClassUID, cstore.TransferSyntax, DicomTransferSyntax.ExplicitVRLittleEndian, DicomTransferSyntax.ImplicitVRLittleEndian);
					else
						Add(cstore.AffectedSOPClassUID, DicomTransferSyntax.ExplicitVRLittleEndian, DicomTransferSyntax.ImplicitVRLittleEndian);
				}
			} else {
				var pc = _pc.Values.FirstOrDefault(x => x.AbstractSyntax == request.AffectedSOPClassUID);
				if (pc == null)
					Add(request.AffectedSOPClassUID, DicomTransferSyntax.ExplicitVRLittleEndian, DicomTransferSyntax.ImplicitVRLittleEndian);
			}
		}

		public void Clear() {
			_pc.Clear();
		}

		public bool Contains(DicomPresentationContext item) {
			return _pc.ContainsKey(item.ID) && _pc[item.ID].AbstractSyntax == item.AbstractSyntax;
		}

		public void CopyTo(DicomPresentationContext[] array, int arrayIndex) {
			throw new NotImplementedException();
		}

		public int Count {
			get { return _pc.Count; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public bool Remove(DicomPresentationContext item) {
			return _pc.Remove(item.ID);
		}

		public IEnumerator<DicomPresentationContext> GetEnumerator() {
			return _pc.Values.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
