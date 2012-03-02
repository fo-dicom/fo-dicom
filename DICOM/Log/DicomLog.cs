using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Log {
	public static class DicomLog {
		internal static void DicomExceptionConstructed(DicomException ex) {
			if (OnDicomException != null) {
				try {
					OnDicomException(ex, new DicomExceptionEventArgs(ex));
				} catch {
				}
			}
		}

		public static EventHandler<DicomExceptionEventArgs> OnDicomException;

		private static DicomLogger _default = VoidLogger.Instance;
		public static DicomLogger DefaultLogger {
			get { return _default; }
			set { _default = value; }
		}
	}
}
