using System;

using NLog;

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

		private static Logger _default = LogManager.GetLogger("Dicom");
		public static Logger DefaultLogger {
			get { return _default; }
			set { _default = value; }
		}
	}
}
