using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom {
	public static class DicomFileExtensions {
		public static DicomFile Clone(this DicomFile original) {
			var df = new DicomFile();
			df.FileMetaInfo.Add(original.FileMetaInfo);
			df.Dataset.Add(original.Dataset);
			df.Dataset.InternalTransferSyntax = original.Dataset.InternalTransferSyntax;
			return df;
		}
	}
}
