using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.StructuredReport {
	public class DicomStructuredReport : DicomContentItem {
		public DicomStructuredReport(DicomDataset dataset) : base(dataset) {
		}

		public DicomStructuredReport(DicomCodeItem code, params DicomContentItem[] items) : base(code, DicomRelationship.Contains, DicomContinuity.Separate, items) {
			// relationship type is not needed for root element
			Dataset.Remove(DicomTag.RelationshipType);
		}
	}
}
