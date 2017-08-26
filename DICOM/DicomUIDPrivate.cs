// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    public partial class DicomUID
    {
        private static void LoadPrivateUIDs()
        {
            _uids.Add(DicomUID.GEPrivateImplicitVRBigEndian.UID, DicomUID.GEPrivateImplicitVRBigEndian);
			_uids.Add (DicomUID.PracticeBuilderReportText.UID, DicomUID.PracticeBuilderReportText);
			_uids.Add (DicomUID.PracticeBuilderReportDictation.UID, DicomUID.PracticeBuilderReportDictation);
        }

        /// <summary>GE Private Implicit VR Big Endian</summary>
        /// <remarks>Same as Implicit VR Little Endian except for big endian pixel data.</remarks>
        public static readonly DicomUID GEPrivateImplicitVRBigEndian = new DicomUID(
            "1.2.840.113619.5.2",
            "GE Private Implicit VR Big Endian",
            DicomUidType.TransferSyntax);

		/// <summary>Practice Builder Report Text</summary>
		/// <remarks>Practice Builder SOP Class for report text.</remarks>
		public static readonly DicomUID PracticeBuilderReportText = new DicomUID (
			"1.2.826.0.1.3680043.2.93.1.0.1",
			"Practice Builder Report Text",
			DicomUidType.SOPClass);

		/// <summary>Practice Builder Report Dictation</summary>
		/// <remarks>Practice Builder SOP Class for report dictation.</remarks>
		public static readonly DicomUID PracticeBuilderReportDictation = new DicomUID (
			"1.2.826.0.1.3680043.2.93.1.0.2",
			"Practice Builder Report Dictation",
			DicomUidType.SOPClass);
    }
}
