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

        [Fact]
        public void IsVolumeStorage()
        {
            Assert.False(DicomUID.EnhancedUSVolumeStorage.IsImageStorage);
            Assert.True(DicomUID.EnhancedUSVolumeStorage.IsVolumeStorage);
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
            var uid = DicomUID.Parse<DicomUID>("1.2.3.4.5.6.7.8.9.0");
            Assert.Equal("Unknown", uid.Name);
            Assert.Equal("1.2.3.4.5.6.7.8.9.0", uid.UID);
            Assert.Equal(DicomUidType.Unknown, uid.Type);
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
