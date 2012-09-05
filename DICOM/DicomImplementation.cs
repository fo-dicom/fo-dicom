using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dicom {
	public static class DicomImplementation {
		public static DicomUID ClassUID = new DicomUID("1.3.6.1.4.1.30071.8", "Implementation Class UID", DicomUidType.Unknown);
		public static string Version = GetImplementationVersion();

		private static string GetImplementationVersion() {
			var version = Assembly.GetExecutingAssembly().GetName().Version;
			return String.Format("fo-dicom {0}.{1}.{2}", version.Major, version.Minor, version.Build);
		}
	}
}
