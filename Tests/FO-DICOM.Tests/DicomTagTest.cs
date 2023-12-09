// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests
{

    /// <summary>
    ///     This is a test class for DicomTagTest and is intended
    ///     to contain all DicomTagTest Unit Tests
    /// </summary>
    [Collection(TestCollections.General)]
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

        [Fact]
        public void GetHashCode_ReturnsDifferentValuesForSameGroupButDifferentElement()
        {
            var hashCode1 = DicomTag.SpecificCharacterSet.GetHashCode(); // 0x0008, 0x0005
            var hashCode2 = DicomTag.LanguageCodeSequence.GetHashCode(); // 0x0008, 0x0006

            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_ReturnsDifferentValuesForDifferentGroupButSameElement()
        {
            var hashCode1 = DicomTag.ImplementationClassUID.GetHashCode(); // 0x0002, 0x00012
            var hashCode2 = DicomTag.InstanceCreationDate.GetHashCode(); // 0x0008, 0x0012

            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_ReturnsDifferentValuesForSameGroupAndElementButOneHasPrivateCreator()
        {
            var privateCreator = DicomDictionary.Default.GetPrivateCreator("HashCodeTesting");

            var tagWithPrivateCreator = new DicomTag(4017, 0x008, privateCreator);
            var tagWithoutPrivateCreator = new DicomTag(4017, 0x008);

            var hashCode1 = tagWithPrivateCreator.GetHashCode();
            var hashCode2 = tagWithoutPrivateCreator.GetHashCode();

            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_ReturnsDifferentValuesForSameGroupAndElementButDifferentPrivateCreator()
        {
            var privateCreator = DicomDictionary.Default.GetPrivateCreator("HashCodeTesting");
            var privateCreator2 = DicomDictionary.Default.GetPrivateCreator("HashCodeTesting2");

            var tagWithPrivateCreator1 = new DicomTag(4017, 0x008, privateCreator);
            var tagWithPrivateCreator2 = new DicomTag(4017, 0x008, privateCreator2);

            var hashCode1 = tagWithPrivateCreator1.GetHashCode();
            var hashCode2 = tagWithPrivateCreator2.GetHashCode();

            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_ReturnsDifferentValuesForSameGroupAndPrivateCreatorButDifferentElement()
        {
            var privateCreator = DicomDictionary.Default.GetPrivateCreator("HashCodeTesting");

            var tagWithPrivateCreator1 = new DicomTag(4017, 0x008, privateCreator);
            var tagWithPrivateCreator2 = new DicomTag(4017, 0x009, privateCreator);

            var hashCode1 = tagWithPrivateCreator1.GetHashCode();
            var hashCode2 = tagWithPrivateCreator2.GetHashCode();

            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_ReturnsSameValuesForSameGroupAndElementAndPrivateCreator()
        {
            var privateCreator = DicomDictionary.Default.GetPrivateCreator("HashCodeTesting");

            var tagWithPrivateCreator1 = new DicomTag(4017, 0x008, privateCreator);
            var tagWithPrivateCreator2 = new DicomTag(4017, 0x008, privateCreator);

            var hashCode1 = tagWithPrivateCreator1.GetHashCode();
            var hashCode2 = tagWithPrivateCreator2.GetHashCode();

            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_ReturnsUniqueValuesForKnownDicomTags()
        {
            var dicomTagsByHashCode = DicomDictionary.Default
                .GroupBy(e => new { e.Tag.Group, e.Tag.Element, e.Tag.PrivateCreator })
                .Select(group => group.First())
                .Select(entry => new { Entry = entry, HashCode = entry.Tag.GetHashCode() })
                .GroupBy(twh => twh.HashCode);

            foreach (var group in dicomTagsByHashCode)
            {
                var hashCode = group.Key;
                var entries = group.Select(g => g.Entry).ToList();

                if (entries.Count != 1)
                {
                    var entriesAsString = entries.Select(e => $"Tag = {e.Tag}, Keyword = {e.Keyword}, Name = {e.Name}");
                    var message = $"The following entries all have the same hash code '{hashCode}': {string.Join(", ", entriesAsString)}";
                    Assert.True(entries.Count == 1, message);
                }
            }
        }

        [Fact]
        public void GetHashCode_AdjustedPrivateTagsShouldBeRetrievable()
        {
            var dicomDictionary = DicomDictionary.Default;

            var privateCreator = dicomDictionary.GetPrivateCreator("HashCodeTesting");
            var privateCreatorDictionary = dicomDictionary[privateCreator];

            var tag = new DicomTag(4017, 0x008, privateCreator);
            var adjustedTag = new DicomDataset().GetPrivateTag(tag);

            privateCreatorDictionary.Add(new DicomDictionaryEntry(tag, "AdjustableHashCodeTestTag1", "AdjustableHashCodeTestKey1", DicomVM.VM_1, false, DicomVR.CS));

            var entry = dicomDictionary[adjustedTag];

            Assert.NotEqual(DicomDictionary.UnknownTag, entry);
            Assert.Equal(tag, entry.Tag);
            Assert.Collection(entry.ValueRepresentations, vr => Assert.Equal(DicomVR.CS, vr));
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
