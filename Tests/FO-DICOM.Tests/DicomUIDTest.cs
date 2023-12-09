// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection(TestCollections.General)]
    public class DicomUIDTest
    {
        #region Fields

        private static readonly IList<DicomUID> _knownUids = DicomUID.Enumerate().ToList();

        #endregion

        #region Unit tests

        [Theory]
        [MemberData(nameof(Uids))]
        public void Enumerate_TestKnownUids_TypeAndRetirementShouldMatch(
            DicomUID uid,
            DicomUidType type,
            bool isRetired)
        {
            var found = _knownUids.Single(item => item.UID.Equals(uid.UID));
            Assert.Equal(type, found.Type);
            Assert.Equal(isRetired, found.IsRetired);
        }

        [Fact]
        public void IsVolumeStorage()
        {
            Assert.False(DicomUID.EnhancedUSVolumeStorage.IsImageStorage);
            Assert.True(DicomUID.EnhancedUSVolumeStorage.IsVolumeStorage);
        }

        [Fact]
        public void Generate_ReturnsValidUid()
        {
            var uid = DicomUID.Generate();

            Assert.True(DicomUID.IsValidUid(uid.UID));
            Assert.True(uid.UID.Length <= 64); // Currently not checked by DicomUID.IsValid
        }

        [Fact]
        public void Generate_ReturnsDifferentUidsEachTime()
        {
            // Note: it is statistically not possible to verify that all returned Uids
            // are unique in a unit test. Just verify that 2 calls result in 2 different values.
            var uidA = DicomUID.Generate();
            var uidB = DicomUID.Generate();

            Assert.NotEqual(uidA, uidB);
        }

        /// <summary>
        /// Parse can parse string UID.
        /// </summary>
        [Fact]
        public void CanParse()
        {
            var uid = DicomUID.Parse("1.2.3.4.5.6.7.8.9.0");
            Assert.Equal("Unknown", uid.Name);
            Assert.Equal("1.2.3.4.5.6.7.8.9.0", uid.UID);
            Assert.Equal(DicomUidType.Unknown, uid.Type);
        }

        /// <summary>
        /// Parse can parse string with trailing space into UID
        /// </summary>
        [Fact]
        public void CanParseStringWithTrailingBlank()
        {
            var uid = DicomUID.Parse("1.2.3.4.5.6.7.8.9.0 ");
            Assert.Equal("Unknown", uid.Name);
            Assert.Equal("1.2.3.4.5.6.7.8.9.0", uid.UID);
            Assert.Equal(DicomUidType.Unknown, uid.Type);
        }

        /// <summary>
        /// Parse can parse string with trailing null into UID
        /// </summary>
        [Fact]
        public void CanParseStringWithTrailingNull()
        {
            var uid = DicomUID.Parse("1.2.3.4.5.6.7.8.9.0\0");
            Assert.Equal("Unknown", uid.Name);
            Assert.Equal("1.2.3.4.5.6.7.8.9.0", uid.UID);
            Assert.Equal(DicomUidType.Unknown, uid.Type);
        }

        /// <summary>
        /// Parse can parse UID with name.
        /// </summary>
        [Fact]
        public void CanParseWithName()
        {
            var uid = DicomUID.Parse(s: "1.2.3.4.5.6.7.8.9.0", name: "UidName");
            Assert.Equal("UidName", uid.Name);
            Assert.Equal("1.2.3.4.5.6.7.8.9.0", uid.UID);
            Assert.Equal(DicomUidType.Unknown, uid.Type);
        }

        /// <summary>
        /// Parse can parse UID with type.
        /// </summary>
        [Fact]
        public void CanParseWithType()
        {
            var uid = DicomUID.Parse(s: "1.2.3.4.5.6.7.8.9.0", type: DicomUidType.TransferSyntax);
            Assert.Equal("Unknown", uid.Name);
            Assert.Equal("1.2.3.4.5.6.7.8.9.0", uid.UID);
            Assert.Equal(DicomUidType.TransferSyntax, uid.Type);
        }

        /// <summary>
        /// Parse can parse UID with type.
        /// </summary>
        [Fact]
        public void CanParseWithNameAndType()
        {
            var uid = DicomUID.Parse(s: "1.2.3.4.5.6.7.8.9.0", name: "UidName", type: DicomUidType.TransferSyntax);
            Assert.Equal("UidName", uid.Name);
            Assert.Equal("1.2.3.4.5.6.7.8.9.0", uid.UID);
            Assert.Equal(DicomUidType.TransferSyntax, uid.Type);
        }

        /// <summary>
        /// Parse can parse UID with type.
        /// </summary>
        [Fact]
        public void CanParseGeneric()
        {
            var uid = DicomParseable.Parse<DicomUID>("1.2.3.4.5.6.7.8.9.0");
            Assert.Equal("Unknown", uid.Name);
            Assert.Equal("1.2.3.4.5.6.7.8.9.0", uid.UID);
            Assert.Equal(DicomUidType.Unknown, uid.Type);
        }

        [Fact]
        public void DicomStorageCategoryTest()
        {
            // just check some DicomUIDs randomly
            Assert.Equal(DicomStorageCategory.Image, DicomUID.SecondaryCaptureImageStorage.StorageCategory);
            Assert.Equal(DicomStorageCategory.None, DicomUID.StorageCommitmentPushModel.StorageCategory);
            Assert.Equal(DicomStorageCategory.None, DicomUID.Verification.StorageCategory);
            Assert.Equal(DicomStorageCategory.StructuredReport, DicomUID.MammographyCADSRStorage.StorageCategory);
            Assert.Equal(DicomStorageCategory.Raw, DicomUID.RawDataStorage.StorageCategory);
            Assert.Equal(DicomStorageCategory.Image, DicomUID.DigitalXRayImageStorageForProcessing.StorageCategory);
            Assert.Equal(DicomStorageCategory.Volume, DicomUID.EnhancedUSVolumeStorage.StorageCategory);
            Assert.Equal(DicomStorageCategory.None, DicomUID.VolumeMeasurement7472.StorageCategory);
        }

        #endregion

        #region Support data

        public static IEnumerable<object[]> Uids
        {
            get
            {
                yield return new object[] { DicomUID.AbnormalLinesFindingOrFeature6103, DicomUidType.ContextGroupName, false };
                yield return new object[] { DicomUID.AbstractMultiDimensionalImageModel, DicomUidType.ApplicationHostingModel, false };
                yield return new object[] { DicomUID.NuclearMedicineImageStorageRetiredRETIRED, DicomUidType.SOPClass, true };
                yield return new object[] { DicomUID.UTC, DicomUidType.FrameOfReference, false };
                yield return new object[] { DicomUID.GeneralPurposeWorklistInformationModelFindRETIRED, DicomUidType.SOPClass, true };
                yield return new object[] { DicomUID.TransducerOrientation6, DicomUidType.ContextGroupName, false };
                yield return new object[] { DicomUID.UltrasoundTransducerGeometry12033, DicomUidType.ContextGroupName, false };
                yield return new object[] { DicomUID.ExplicitVRBigEndianRETIRED, DicomUidType.TransferSyntax, true };
                yield return new object[] { DicomUID.JPEGFullProgressionHierarchical2426RETIRED, DicomUidType.TransferSyntax, true };
                yield return new object[] { DicomUID.IEC61217PatientSupportPositionParameter9403, DicomUidType.ContextGroupName, false };   // 2015c
            }
        }
        #endregion
    }
}
