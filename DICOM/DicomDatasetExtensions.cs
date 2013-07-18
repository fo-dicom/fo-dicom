using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicom {
	public static class DicomDatasetExtensions {
		public static DicomDataset Clone(this DicomDataset dataset) {
			var ds = new DicomDataset(dataset);
			ds.InternalTransferSyntax = dataset.InternalTransferSyntax;
			return ds;
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

		/// <summary>
		/// Enumerates DICOM items matching mask.
		/// </summary>
		/// <param name="mask">Mask</param>
		/// <returns>Enumeration of DICOM items</returns>
		public static IEnumerable<DicomItem> EnumerateMasked(this DicomDataset dataset, DicomMaskedTag mask) {
			return dataset.Where(x => mask.IsMatch(x.Tag));
		}

		/// <summary>
		/// Enumerates DICOM items for specified group.
		/// </summary>
		/// <param name="group">Group</param>
		/// <returns>Enumeration of DICOM items</returns>
		public static IEnumerable<DicomItem> EnumerateGroup(this DicomDataset dataset, ushort group) {
			return dataset.Where(x => x.Tag.Group == group && x.Tag.Element != 0x0000);
		}
	}
}
