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
		/// <summary>
		/// Sets the <c>Result</c> of this presentation context.
		/// 
		/// The preferred method of accepting presentation contexts is to call one of the <c>AcceptTransferSyntaxes</c> methods.
		/// </summary>
		/// <param name="result">Result status to return for this proposed presentation context.</param>
		public void SetResult(DicomPresentationContextResult result) {
			SetResult(result, _transferSyntaxes[0]);
		}

		/// <summary>
		/// Sets the <c>Result</c> and <c>AcceptedTransferSyntax</c> of this presentation context.
		/// 
		/// The preferred method of accepting presentation contexts is to call one of the <c>AcceptTransferSyntaxes</c> methods.
		/// </summary>
		/// <param name="result">Result status to return for this proposed presentation context.</param>
		/// <param name="acceptedTransferSyntax">Accepted transfer syntax for this proposed presentation context.</param>
		public void SetResult(DicomPresentationContextResult result, DicomTransferSyntax acceptedTransferSyntax) {
			_transferSyntaxes.Clear();
			_transferSyntaxes.Add(acceptedTransferSyntax);
			_result = result;
		}

		/// <summary>
		/// Compares a list of transfer syntaxes accepted by the SCP against the list of transfer syntaxes proposed by the SCU. Sets the presentation 
		/// context <c>Result</c> to <c>DicomPresentationContextResult.Accept</c> if an accepted transfer syntax is found. If no accepted transfer
		/// syntax is found, the presentation context <c>Result</c> is set to <c>DicomPresentationContextResult.RejectTransferSyntaxesNotSupported</c>.
		/// </summary>
		/// <param name="acceptedTransferSyntaxes">Transfer syntaxes that the SCP accepts for the proposed abstract syntax.</param>
		/// <returns>Returns <c>true</c> if an accepted transfer syntax was found. Returns <c>false</c> if no accepted transfer syntax was found.</returns>
		public bool AcceptTransferSyntaxes(params DicomTransferSyntax[] acceptedTransferSyntaxes) {
			return AcceptTransferSyntaxes(acceptedTransferSyntaxes, false);
		}

		/// <summary>
		/// Compares a list of transfer syntaxes accepted by the SCP against the list of transfer syntaxes proposed by the SCU. Sets the presentation 
		/// context <c>Result</c> to <c>DicomPresentationContextResult.Accept</c> if an accepted transfer syntax is found. If no accepted transfer
		/// syntax is found, the presentation context <c>Result</c> is set to <c>DicomPresentationContextResult.RejectTransferSyntaxesNotSupported</c>.
		/// </summary>
		/// <param name="acceptedTransferSyntaxes">Transfer syntaxes that the SCP accepts for the proposed abstract syntax.</param>
		/// <param name="scpPriority">If set to <c>true</c>, transfer syntaxes will be accepted in the order specified by <paramref name="acceptedTransferSyntaxes"/>. If set to <c>false</c>, transfer syntaxes will be accepted in the order proposed by the SCU.</param>
		/// <returns>Returns <c>true</c> if an accepted transfer syntax was found. Returns <c>false</c> if no accepted transfer syntax was found.</returns>
		public bool AcceptTransferSyntaxes(DicomTransferSyntax[] acceptedTransferSyntaxes, bool scpPriority = false) {
			if (Result == DicomPresentationContextResult.Accept)
				return true;

			if (scpPriority) {
				// let the SCP decide which syntax that it would prefer
				foreach (DicomTransferSyntax ts in acceptedTransferSyntaxes) {
					if (ts != null && HasTransferSyntax(ts)) {
						SetResult(DicomPresentationContextResult.Accept, ts);
						return true;
					}
				}
			} else {
				// accept syntaxes in the order that the SCU proposed them
				foreach (DicomTransferSyntax ts in _transferSyntaxes) {
					if (acceptedTransferSyntaxes.Contains(ts)) {
						SetResult(DicomPresentationContextResult.Accept, ts);
						return true;
					}
				}
			}

			SetResult(DicomPresentationContextResult.RejectTransferSyntaxesNotSupported);

			return false;
		}

		public void AddTransferSyntax(DicomTransferSyntax ts) {
			if (ts != null && !_transferSyntaxes.Contains(ts))
				_transferSyntaxes.Add(ts);
		}

		public void RemoveTransferSyntax(DicomTransferSyntax ts) {
			if (ts != null && _transferSyntaxes.Contains(ts))
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
