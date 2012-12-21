using System;

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
			DicomExceptionConstructed(this);
		}

		protected DicomException(string format, params object[] args) : this(String.Format(format, args)) {
		}

		protected DicomException(string message, Exception innerException) : base(message, innerException) {
			DicomExceptionConstructed(this);
		}

		internal static void DicomExceptionConstructed(DicomException ex) {
			if (OnException != null) {
				try {
					OnException(ex, new DicomExceptionEventArgs(ex));
				} catch {
				}
			}
		}

		public static EventHandler<DicomExceptionEventArgs> OnException;
	}
}
