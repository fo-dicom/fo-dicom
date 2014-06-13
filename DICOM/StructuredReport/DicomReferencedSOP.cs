using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.StructuredReport {
	public class DicomReferencedSOP : DicomDataset {
		public DicomReferencedSOP(DicomDataset dataset) : base(dataset) {
		}

		public DicomReferencedSOP(DicomSequence sequence) {
			if (sequence.Items.Count == 0)
				throw new DicomDataException("No referenced SOP pair item found in sequence.");
			Add(sequence.Items[0]);
		}

		public DicomReferencedSOP(DicomUID inst, DicomUID clazz) {
			Add(DicomTag.ReferencedSOPInstanceUID, inst);
			Add(DicomTag.ReferencedSOPClassUID, clazz);
		}

		public DicomUID Instance {
			get { return Get<DicomUID>(DicomTag.ReferencedSOPInstanceUID); }
		}

		public DicomUID Class {
			get { return Get<DicomUID>(DicomTag.ReferencedSOPClassUID); }
		}
	}
}
