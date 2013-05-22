using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.StructuredReport {
	public class DicomStructuredReport : DicomContentItem {
		public DicomStructuredReport(DicomDataset dataset) : base(dataset) {
		}

		public DicomStructuredReport(DicomCodeItem code) : base(code, DicomRelationshipType.Contains, DicomContinuity.Separate) {
			// relationship type is not needed for root element
			Dataset.Remove(DicomTag.RelationshipType);
		}
	}
}
