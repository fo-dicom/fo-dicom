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

			var id = _pc.Max(x => x.Key) + 2;

			if (id >= 256)
				throw new DicomNetworkException("Too many presentation contexts configured for this association!");

			return (byte)id;
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

				var pcs = _pc.Values.Where(x => x.AbstractSyntax == request.SOPClassUID);
				if (cstore.TransferSyntax == DicomTransferSyntax.ImplicitVRLittleEndian)
					pcs = pcs.Where(x => x.GetTransferSyntaxes().Contains(DicomTransferSyntax.ImplicitVRLittleEndian));
				else
					pcs = pcs.Where(x => x.AcceptedTransferSyntax == cstore.TransferSyntax);

				var pc = pcs.FirstOrDefault();
				if (pc == null) {
					var tx = new List<DicomTransferSyntax>();
					if (cstore.TransferSyntax != DicomTransferSyntax.ImplicitVRLittleEndian)
						tx.Add(cstore.TransferSyntax);
					if (cstore.AdditionalTransferSyntaxes != null)
						tx.AddRange(cstore.AdditionalTransferSyntaxes);
					tx.Add(DicomTransferSyntax.ExplicitVRLittleEndian);
					tx.Add(DicomTransferSyntax.ImplicitVRLittleEndian);
					
					Add(cstore.SOPClassUID, tx.ToArray());
				}
			} else {
				var pc = _pc.Values.FirstOrDefault(x => x.AbstractSyntax == request.SOPClassUID);
				if (pc == null)
					Add(request.SOPClassUID, DicomTransferSyntax.ExplicitVRLittleEndian, DicomTransferSyntax.ImplicitVRLittleEndian);
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
