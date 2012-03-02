using System;
using System.Linq;

namespace Dicom {
	public static class DicomDatasetExtensions {
		public static DicomDataset Clone(this DicomDataset dataset) {
			return new DicomDataset(dataset);
		}

		public static DateTime GetDateTime(this DicomDataset dataset, DicomTag date, DicomTag time) {
			DateTime da = dataset.Get<DateTime>(date);
			DateTime tm = dataset.Get<DateTime>(time);
			return new DateTime(da.Year, da.Month, da.Day, tm.Hour, tm.Minute, tm.Second);
		}
	}
}
