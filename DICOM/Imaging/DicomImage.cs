using System;

using Dicom.Imaging.Codec;

namespace Dicom.Imaging {
	public class DicomImage {
		#region Private Members
		private DicomPixelData _pixelData;
		#endregion

		public DicomImage(DicomDataset dataset) {
			Dataset = dataset;
		}

		public DicomDataset Dataset {
			get;
			private set;
		}

		public DicomPixelData PixelData {
			get {
				if (_pixelData == null) {
					if (Dataset.InternalTransferSyntax.IsEncapsulated)
						Dataset.ChangeTransferSyntax(DicomTransferSyntax.ExplicitVRLittleEndian);

					_pixelData = DicomPixelData.Create(Dataset);
				}
				return _pixelData;
			}
		}


	}
}
