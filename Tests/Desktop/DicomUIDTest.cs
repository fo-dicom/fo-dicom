// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System.Collections.Generic;
    using System.Linq;

    using Xunit;

    [Collection("General")]
    public class DicomUIDTest
    {
        #region Fields

        private static readonly IList<DicomUID> KnownUids = DicomUID.Enumerate().ToList();

        #endregion

        #region Unit tests

        [Theory]
        [MemberData(nameof(Uids))]
        public void Enumerate_TestKnownUids_TypeAndRetirementShouldMatch(
            DicomUID uid,
            DicomUidType type,
            bool isRetired)
        {
            var found = KnownUids.Single(item => item.UID.Equals(uid.UID));
            Assert.Equal(type, found.Type);
            Assert.Equal(isRetired, found.IsRetired);
        }

        #endregion

        #region Support data

        public static IEnumerable<object[]> Uids
        {
            get
            {
                yield return new object[] { DicomUID.AbnormalLinesFindingOrFeature6103, DicomUidType.ContextGroupName, false };
                yield return new object[] { DicomUID.AbstractMultiDimensionalImageModel, DicomUidType.ApplicationHostingModel, false };
                yield return new object[] { DicomUID.NuclearMedicineImageStorageRETIRED, DicomUidType.SOPClass, true };
                yield return new object[] { DicomUID.SPM2GRAYFrameOfReference, DicomUidType.FrameOfReference, false };
                yield return new object[] { DicomUID.GeneralPurposeWorklistInformationModelFINDRETIRED, DicomUidType.SOPClass, true };
                yield return new object[] { DicomUID.TransducerOrientation6, DicomUidType.ContextGroupName, false };
                yield return new object[] { DicomUID.UltrasoundTransducerGeometry12033, DicomUidType.ContextGroupName, false };
                yield return new object[] { DicomUID.ExplicitVRBigEndianRETIRED, DicomUidType.TransferSyntax, true };
                yield return new object[] { DicomUID.JPEGFullProgressionHierarchical2426RETIRED, DicomUidType.TransferSyntax, true };
                yield return new object[] { DicomUID.IEC61217PatientSupportPositionParameters9403, DicomUidType.ContextGroupName, false };   // 2015c
            }
        }
        #endregion
    }
}
