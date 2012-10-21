using System;
using System.Collections.Generic;
using System.Linq;

using Dicom.IO.Writer;

namespace Dicom {
	public static class DicomDatasetExtensions {
		public static DicomDataset Clone(this DicomDataset dataset) {
			return new DicomDataset(dataset);
		}

		public static DateTime GetDateTime(this DicomDataset dataset, DicomTag date, DicomTag time) {
			DicomDate dd = dataset.Get<DicomDate>(date);
			DicomTime dt = dataset.Get<DicomTime>(time);

			DateTime da = DateTime.Today;
			if (dd != null && dd.Count > 0)
				da = dd.Get<DateTime>(0);

			DateTime tm = DateTime.Today;
			if (dt != null && dt.Count > 0)
				tm = dt.Get<DateTime>(0);

			return new DateTime(da.Year, da.Month, da.Day, tm.Hour, tm.Minute, tm.Second);
		}
	}
}
