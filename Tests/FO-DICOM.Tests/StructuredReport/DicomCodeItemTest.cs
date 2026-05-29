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

            Assert.True(a.Equals(b));
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        [Fact]
        public void GetHashCode_EqualInstancesWithDifferentMeaning_ReturnsEqualHashes()
        {
            var a = new DicomCodeItem("113820", "DCM", "Original meaning");
            var b = new DicomCodeItem("113820", "DCM", "Reworded meaning");

            Assert.True(a.Equals(b));
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        [Fact]
        public void GetHashCode_DifferentVersion_ReturnsDifferentHashes()
        {
            var a = new DicomCodeItem("113820", "DCM", "CT Acquisition Type");
            var b = new DicomCodeItem("113820", "DCM", "CT Acquisition Type", "20240101");

            Assert.False(a.Equals(b));
            Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Fact]
        public void GetHashCode_DifferentValue_ReturnsDifferentHashes()
        {
            var a = new DicomCodeItem("113820", "DCM", "CT Acquisition Type");
            var b = new DicomCodeItem("113821", "DCM", "CT Acquisition Type");

            Assert.False(a.Equals(b));
            Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Fact]
        public void DicomCodeItem_ParseShortCodeValue()
        {
            var ds = new DicomDataset
            {
                {DicomTag.CodeValue, "113820" },
                {DicomTag.CodingSchemeDesignator, "DCM" },
                {DicomTag.CodeMeaning, "CT Acquisition Type" }
            };
            var codeItem = new DicomCodeItem(ds);

            Assert.Equal("113820", codeItem.Value);
        }

        [Fact]
        public void DicomCodeItem_WriteShortCodeValue()
        {
            var codeItem = new DicomCodeItem("113820", "DCM", "CT Acquisition Type");

            Assert.True(codeItem.Contains(DicomTag.CodeValue));
            Assert.False(codeItem.Contains(DicomTag.LongCodeValue));
            Assert.False(codeItem.Contains(DicomTag.URNCodeValue));
            Assert.Equal("113820", codeItem.GetString(DicomTag.CodeValue));
        }

        [Fact]
        public void DicomCodeItem_ParseLongCodeValue()
        {
            var ds = new DicomDataset
            {
                {DicomTag.LongCodeValue, "113820113820113820" },
                {DicomTag.CodingSchemeDesignator, "DCM" },
                {DicomTag.CodeMeaning, "CT Acquisition Type" }
            };
            var codeItem = new DicomCodeItem(ds);

            Assert.Equal("113820113820113820", codeItem.Value);
        }

        [Fact]
        public void DicomCodeItem_WriteLongCodeValue()
        {
            var codeItem = new DicomCodeItem("113820113820113820", "DCM", "CT Acquisition Type");

            Assert.False(codeItem.Contains(DicomTag.CodeValue));
            Assert.True(codeItem.Contains(DicomTag.LongCodeValue));
            Assert.False(codeItem.Contains(DicomTag.URNCodeValue));
            Assert.Equal("113820113820113820", codeItem.GetString(DicomTag.LongCodeValue));
        }

        [Fact]
        public void DicomCodeItem_ParseURNCodeValue()
        {
            var ds = new DicomDataset
            {
                {DicomTag.URNCodeValue, "urn:lex:us:federal:codified.regulation:2013-04-25;45CFR164" },
                {DicomTag.CodingSchemeDesignator, "DCM" },
                {DicomTag.CodeMeaning, "HIPAA Privacy Rule" }
            };
            var codeItem = new DicomCodeItem(ds);

            Assert.Equal("urn:lex:us:federal:codified.regulation:2013-04-25;45CFR164", codeItem.Value);
        }

        [Fact]
        public void DicomCodeItem_WriteURNCodeValue()
        {
            var codeItem = new DicomCodeItem("urn:lex:us:federal:codified.regulation:2013-04-25;45CFR164", "DCM", "HIPAA Privacy Rule");

            Assert.False(codeItem.Contains(DicomTag.CodeValue));
            Assert.False(codeItem.Contains(DicomTag.LongCodeValue));
            Assert.True(codeItem.Contains(DicomTag.URNCodeValue));
            Assert.Equal("urn:lex:us:federal:codified.regulation:2013-04-25;45CFR164", codeItem.GetString(DicomTag.URNCodeValue));
        }

    }
}
