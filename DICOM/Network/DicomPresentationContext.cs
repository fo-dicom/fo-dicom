using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public enum DicomPresentationContextResult : byte {
		Proposed = 255,
		Accept = 0,
		RejectUser = 1,
		RejectNoReason = 2,
		RejectAbstractSyntaxNotSupported = 3,
		RejectTransferSyntaxesNotSupported = 4
	}

	public class DicomPresentationContext {
		#region Private Members
		private byte _pcid;
		private DicomPresentationContextResult _result;
		private DicomUID _abstract;
		private List<DicomTransferSyntax> _transferSyntaxes;
		#endregion

		#region Public Constructor
		public DicomPresentationContext(byte pcid, DicomUID abstractSyntax) {
			_pcid = pcid;
			_result = DicomPresentationContextResult.Proposed;
			_abstract = abstractSyntax;
			_transferSyntaxes = new List<DicomTransferSyntax>();
		}

		internal DicomPresentationContext(byte pcid, DicomUID abstractSyntax, DicomTransferSyntax transferSyntax, DicomPresentationContextResult result) {
			_pcid = pcid;
			_result = result;
			_abstract = abstractSyntax;
			_transferSyntaxes = new List<DicomTransferSyntax>();
			_transferSyntaxes.Add(transferSyntax);
		}
		#endregion

		#region Public Properties
		public byte ID {
			get { return _pcid; }
		}

		public DicomPresentationContextResult Result {
			get { return _result; }
		}

		public DicomUID AbstractSyntax {
			get { return _abstract; }
		}

		public DicomTransferSyntax AcceptedTransferSyntax {
			get {
				if (_transferSyntaxes.Count > 0)
					return _transferSyntaxes[0];
				return null;
			}
		}
		#endregion

		#region Public Members
		public void SetResult(DicomPresentationContextResult result) {
			SetResult(result, _transferSyntaxes[0]);
		}

		public void SetResult(DicomPresentationContextResult result, DicomTransferSyntax acceptedTransferSyntax) {
			_transferSyntaxes.Clear();
			_transferSyntaxes.Add(acceptedTransferSyntax);
			_result = result;
		}

		public bool AcceptTransferSyntaxes(params DicomTransferSyntax[] acceptedTs) {
			if (Result == DicomPresentationContextResult.Accept)
				return true;
			foreach (DicomTransferSyntax ts in acceptedTs) {
				if (HasTransferSyntax(ts)) {
					SetResult(DicomPresentationContextResult.Accept, ts);
					return true;
				}
			}
			return false;
		}

		public void AddTransferSyntax(DicomTransferSyntax ts) {
			if (!_transferSyntaxes.Contains(ts))
				_transferSyntaxes.Add(ts);
		}

		public void RemoveTransferSyntax(DicomTransferSyntax ts) {
			if (_transferSyntaxes.Contains(ts))
				_transferSyntaxes.Remove(ts);
		}

		public void ClearTransferSyntaxes() {
			_transferSyntaxes.Clear();
		}

		public IList<DicomTransferSyntax> GetTransferSyntaxes() {
			return _transferSyntaxes.AsReadOnly();
		}

		public bool HasTransferSyntax(DicomTransferSyntax ts) {
			return _transferSyntaxes.Contains(ts);
		}

		public string GetResultDescription() {
			switch (_result) {
			case DicomPresentationContextResult.Accept:
				return "Accept";
			case DicomPresentationContextResult.Proposed:
				return "Proposed";
			case DicomPresentationContextResult.RejectAbstractSyntaxNotSupported:
				return "Reject - Abstract Syntax Not Supported";
			case DicomPresentationContextResult.RejectNoReason:
				return "Reject - No Reason";
			case DicomPresentationContextResult.RejectTransferSyntaxesNotSupported:
				return "Reject - Transfer Syntaxes Not Supported";
			case DicomPresentationContextResult.RejectUser:
				return "Reject - User";
			default:
				return "Unknown";
			}
		}
		#endregion
	}
}
