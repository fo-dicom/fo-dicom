using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging {
	public static class DicomImagingExtensions {
		public static DicomImage ToImage(this DicomFile file) {
			return new DicomImage(file.Dataset);
		}

		public static DicomImage ToImage(this DicomDataset dataset) {
			return new DicomImage(dataset);
		}
	}
}
