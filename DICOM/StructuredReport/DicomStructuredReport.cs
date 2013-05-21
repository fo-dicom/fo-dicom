using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.StructuredReport {
	public class DicomStructuredReport {
		private DicomDataset _dataset;

		public DicomStructuredReport(DicomDataset dataset) {
			Dataset = dataset;
		}

		public DicomDataset Dataset {
			get {
				return _dataset;
			}
			private set {
				_dataset = value;
			}
		}

		public DicomContentItem Root {
			get {
				return new DicomContentItem(Dataset);
			}
			set {
				Dataset.Add(value.Dataset);
			}
		}
	}
}
