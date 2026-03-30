// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Xunit;

namespace FellowOakDicom.Tests
{
    [Collection(TestCollections.General)]
    public class DicomMatchRulesTest
    {
        public DicomDataset TestDataset { get; } = new DicomDataset()
        {
            { DicomTag.PatientName, "Doe^John" },
            { DicomTag.PatientID, "123456" },
            { DicomTag.StudyDescription, "Test study 1" },
            { DicomTag.SeriesDescription, "Test series 1" },
        };

        [Fact]
        public void EqualsDicomMatchRule_MatchingString_ReturnsTrue()
        {
            var matchRule = new EqualsDicomMatchRule(DicomTag.PatientName, "Doe^John");
            Assert.True(matchRule.Match(TestDataset));
        }

        [Fact]
        public void EqualsDicomMatchRule_NonMatchingString_ReturnsFalse()
        {
            var matchRule = new EqualsDicomMatchRule(DicomTag.PatientID, "Doe^John");
            Assert.False(matchRule.Match(TestDataset));
        }

        [Fact]
        public void StartsWithDicomMatchRule_MatchingString_ReturnsTrue()
        {
            var matchRule = new StartsWithDicomMatchRule(DicomTag.PatientName, "Do");
            Assert.True(matchRule.Match(TestDataset));
        }

        [Fact]
        public void StartsWithDicomMatchRule_NonMatchingString_ReturnsFalse()
        {
            var matchRule = new StartsWithDicomMatchRule(DicomTag.PatientID, "Do");
            Assert.False(matchRule.Match(TestDataset));
        }

        [Fact]
        public void EndsWithDicomMatchRule_MatchingString_ReturnsTrue()
        {
            var matchRule = new EndsWithDicomMatchRule(DicomTag.PatientName, "ohn");
            Assert.True(matchRule.Match(TestDataset));
        }

        [Fact]
        public void EndsWithDicomMatchRule_NonMatchingString_ReturnsFalse()
        {
            var matchRule = new EndsWithDicomMatchRule(DicomTag.PatientID, "ohn");
            Assert.False(matchRule.Match(TestDataset));
        }

        [Fact]
        public void ContainsDicomMatchRule_MatchingString_ReturnsTrue()
        {
            var matchRule = new ContainsDicomMatchRule(DicomTag.StudyDescription, "study");
            Assert.True(matchRule.Match(TestDataset));
        }

        [Fact]
        public void ContainsDicomMatchRule_NonMatchingString_ReturnsFalse()
        {
            var matchRule = new ContainsDicomMatchRule(DicomTag.SeriesDescription, "study");
            Assert.False(matchRule.Match(TestDataset));
        }

        [Fact]
        public void WildcardDicomMatchRule_MatchingString_ReturnsTrue()
        {
            var matchRule = new WildcardDicomMatchRule(DicomTag.SeriesDescription, "*series?1");
            Assert.True(matchRule.Match(TestDataset));
        }

        [Fact]
        public void WildcardDicomMatchRule_NonMatchingString_ReturnsFalse()
        {
            var matchRule = new WildcardDicomMatchRule(DicomTag.SeriesDescription, "?series 1");
            Assert.False(matchRule.Match(TestDataset));
        }

        [Fact]
        public void RegexDicomMatchRule_MatchingString_ReturnsTrue()
        {
            var matchRule = new RegexDicomMatchRule(DicomTag.StudyDescription, @"^(?i)test StuDy \d{1}$");
            Assert.True(matchRule.Match(TestDataset));
        }

        [Fact]
        public void RegexDicomMatchRule_NonMatchingString_ReturnsFalse()
        {
            var matchRule = new RegexDicomMatchRule(DicomTag.SeriesDescription, @"^(?i)test series \d{2}$");
            Assert.False(matchRule.Match(TestDataset));
        }

        [Fact]
        public void OneOfDicomMatchRule_MatchingString_ReturnsTrue()
        {
            var matchRule1 = new OneOfDicomMatchRule(DicomTag.PatientID, "123456", "Doe^John");
            var matchRule2 = new OneOfDicomMatchRule(DicomTag.PatientName, "123456", "Doe^John");
            Assert.True(matchRule1.Match(TestDataset));
            Assert.True(matchRule2.Match(TestDataset));
        }

        [Fact]
        public void OneOfDicomMatchRule_NonMatchingString_ReturnsFalse()
        {
            var matchRule1 = new OneOfDicomMatchRule(DicomTag.SeriesDescription, "123456", "Doe^John", "series");
            var matchRule2 = new OneOfDicomMatchRule(DicomTag.StudyDescription, "123456", "Doe^John", "study");
            Assert.False(matchRule1.Match(TestDataset));
            Assert.False(matchRule2.Match(TestDataset));
        }

        [Fact]
        public void BoolDicomMatchRule_ReturnsTrue()
        {
            var matchRule = new BoolDicomMatchRule(true);
            Assert.True(matchRule.Match(TestDataset));
        }

        [Fact]
        public void BoolDicomMatchRule_ReturnsFalse()
        {
            var matchRule = new BoolDicomMatchRule(false);
            Assert.False(matchRule.Match(TestDataset));
        }
    }
}
