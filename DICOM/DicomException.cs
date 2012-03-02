using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.Log;

namespace Dicom {
	public class DicomExceptionEventArgs : EventArgs {
		public readonly DicomException Exception;

		public DicomExceptionEventArgs(DicomException ex) {
			Exception = ex;
		}
	}

	/// <summary>Base type for all DICOM library exceptions.</summary>
	public abstract class DicomException : Exception {
		protected DicomException(string message) : base(message) {
			DicomLog.DicomExceptionConstructed(this);
		}

		protected DicomException(string format, params object[] args) : this(String.Format(format, args)) {
		}

		protected DicomException(string message, Exception innerException) : base(message, innerException) {
			DicomLog.DicomExceptionConstructed(this);
		}
	}
}
