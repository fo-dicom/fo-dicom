using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.StructuredReport {
	public class DicomMeasuredValue : DicomDataset {
		public DicomMeasuredValue(DicomDataset dataset) : base(dataset) {
		}

		public DicomMeasuredValue(DicomSequence sequence) {
			if (sequence.Items.Count == 0)
				throw new DicomDataException("No measurement item found in sequence.");
			Add(sequence.Items[0]);
		}

		public DicomMeasuredValue(decimal value, DicomCodeItem units) {
			Add(DicomTag.NumericValue, value);
			Add(new DicomSequence(DicomTag.MeasurementUnitsCodeSequence, units));
		}

		public DicomCodeItem Code {
			get { return Get<DicomCodeItem>(DicomTag.MeasurementUnitsCodeSequence); }
		}

		public decimal Value {
			get { return Get<decimal>(DicomTag.NumericValue); }
		}

		public override string ToString() {
			return String.Format("{0} {1}", Value, Code.Value);
		}
	}
}
