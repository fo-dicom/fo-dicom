using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Log {
	public abstract class DicomLogger {
		public abstract void Log(DicomLogLevel level, string message);

		public virtual void Log(DicomLogLevel level, string format, params object[] args) {
			Log(level, String.Format(format, args));
		}
	}
}
