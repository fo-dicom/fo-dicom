// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    public partial class DicomUID
    {
        private static void LoadPrivateUIDs()
        {
            _uids.Add(DicomUID.GEPrivateImplicitVRBigEndian.UID, DicomUID.GEPrivateImplicitVRBigEndian);
        }

        /// <summary>GE Private Implicit VR Big Endian</summary>
        /// <remarks>Same as Implicit VR Little Endian except for big endian pixel data.</remarks>
        public static readonly DicomUID GEPrivateImplicitVRBigEndian = new DicomUID(
            "1.2.840.113619.5.2",
            "GE Private Implicit VR Big Endian",
            DicomUidType.TransferSyntax);
    }
}
