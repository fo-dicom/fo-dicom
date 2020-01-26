// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using Xunit;

namespace FellowOakDicom.Tests
{

    /// <summary>
    ///     This is a test class for DicomTagTest and is intended
    ///     to contain all DicomTagTest Unit Tests
    /// </summary>
    [Collection("General")]
    public class DicomTagTest
    {
        #region Unit tests

        /// <summary>
        ///     A test for ToString
        /// </summary>
        [Fact]
        public void ToJsonStringTest()
        {
            const ushort @group = 0x7FE0;
            const ushort element = 0x00FF;
            var target = new DicomTag(group, element);
            const string format = "J";
            IFormatProvider formatProvider = null;
            const string expected = "7FE000FF";
            string actual = string.Empty;
            actual = target.ToString(format, formatProvider);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(DoesContain))]
        public void Contains_Existing_ReturnsTrue(DicomTag tag)
        {
            var dataset = new DicomDataset { new DicomUnknown(tag) };
            Assert.True(dataset.Contains(tag));
        }

        [Theory]
        [MemberData(nameof(DoesNotContain))]
        public void Contains_NonExisting_ReturnsFalse(DicomTag datasetTag, DicomTag containsTag)
        {
            var dataset = new DicomDataset { new DicomUnknown(datasetTag) };
            Assert.False(dataset.Contains(containsTag));
        }

        [Fact]
        public void Contains_SamePrivateTagsDifferentPrivateCreator_ReturnsTrue()
        {
            var dataset = new DicomDataset { new DicomUnknown(new DicomTag(0x3005, 0x3025, "PRIVATE")) };
            Assert.True(dataset.Contains(new DicomTag(0x3005, 0x1525, "PRIVATE")));
        }

        #endregion

        #region Test data

        public static readonly IEnumerable<object[]> DoesContain = new[]
                                                                 {
                                                                     new[] { DicomTag.BitsAllocated },
                                                                     new[] { DicomTag.AcquisitionContextSequence },
                                                                     new[] { new DicomTag(0x3005, 0x25, "PRIVATE") }
                                                                 };

        public static readonly IEnumerable<object[]> DoesNotContain = new[]
                                                                 {
                                                                     new[] { DicomTag.BitsAllocated, DicomTag.BitsStored },
                                                                     new[] { new DicomTag(0x3005, 0x25, "PRIVATE"), new DicomTag(0x3005, 0x26, "PRIVATE") }
                                                                 };

        #endregion
    }
}
