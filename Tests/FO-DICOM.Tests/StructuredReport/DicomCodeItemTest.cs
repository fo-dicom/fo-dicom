// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.StructuredReport;
using Xunit;

namespace FellowOakDicom.Tests.StructuredReport
{
    [Collection(TestCollections.General)]
    public class DicomCodeItemTest
    {
        [Fact]
        public void GetHashCode_IdenticalInstances_ReturnsEqualHashes()
        {
            var a = new DicomCodeItem("113820", "DCM", "CT Acquisition Type");
            var b = new DicomCodeItem("113820", "DCM", "CT Acquisition Type");

            Assert.True(a.Equals((object)b));
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        [Fact]
        public void GetHashCode_EqualInstancesWithDifferentMeaning_ReturnsEqualHashes()
        {
            var a = new DicomCodeItem("113820", "DCM", "Original meaning");
            var b = new DicomCodeItem("113820", "DCM", "Reworded meaning");

            Assert.True(a.Equals((object)b));
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        [Fact]
        public void GetHashCode_DifferentVersion_ReturnsDifferentHashes()
        {
            var a = new DicomCodeItem("113820", "DCM", "CT Acquisition Type");
            var b = new DicomCodeItem("113820", "DCM", "CT Acquisition Type", "20240101");

            Assert.False(a.Equals((object)b));
            Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Fact]
        public void GetHashCode_DifferentValue_ReturnsDifferentHashes()
        {
            var a = new DicomCodeItem("113820", "DCM", "CT Acquisition Type");
            var b = new DicomCodeItem("113821", "DCM", "CT Acquisition Type");

            Assert.False(a.Equals((object)b));
            Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        }
    }
}
