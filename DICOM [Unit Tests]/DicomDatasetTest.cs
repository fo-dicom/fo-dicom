// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;
    using System.Linq;

    using Xunit;

    public class DicomDatasetTest
    {
        #region Unit tests

        [Fact]
        public void Add_OtherDoubleElement_Succeeds()
        {
            var dataset = new DicomDataset();
            dataset.Add(DicomTag.DoubleFloatPixelData, 3.45);
            Assert.IsType(typeof(DicomOtherDouble), dataset.First());
        }

        [Fact]
        public void Add_OtherDoubleElementWithMultipleDoubles_Succeeds()
        {
            var dataset = new DicomDataset();
            dataset.Add(DicomTag.DoubleFloatPixelData, 3.45, 6.78, 9.01);
            Assert.IsType(typeof(DicomOtherDouble), dataset.First());
            Assert.Equal(3, dataset.Get<double[]>(DicomTag.DoubleFloatPixelData).Length);
        }

        [Fact]
        public void Add_UnlimitedCharactersElement_Succeeds()
        {
            var dataset = new DicomDataset();
            dataset.Add(DicomTag.LongCodeValue, "abc");
            Assert.IsType(typeof(DicomUnlimitedCharacters), dataset.First());
            Assert.Equal("abc", dataset.Get<string>(DicomTag.LongCodeValue));
        }

        [Fact]
        public void Add_UnlimitedCharactersElementWithMultipleStrings_Succeeds()
        {
            var dataset = new DicomDataset();
            dataset.Add(DicomTag.LongCodeValue, "a", "b", "c");
            Assert.IsType(typeof(DicomUnlimitedCharacters), dataset.First());
            Assert.Equal("c", dataset.Get<string>(DicomTag.LongCodeValue, 2));
        }

        [Fact]
        public void Add_UniversalResourceElement_Succeeds()
        {
            var dataset = new DicomDataset();
            dataset.Add(DicomTag.URNCodeValue, "abc");
            Assert.IsType(typeof(DicomUniversalResource), dataset.First());
            Assert.Equal("abc", dataset.Get<string>(DicomTag.URNCodeValue));
        }

        [Fact]
        public void Add_UniversalResourceElementWithMultipleStrings_OnlyFirstValueIsUsed()
        {
            var dataset = new DicomDataset();
            dataset.Add(DicomTag.URNCodeValue, "a", "b", "c");
            Assert.IsType(typeof(DicomUniversalResource), dataset.First());

            var data = dataset.Get<string[]>(DicomTag.URNCodeValue);
            Assert.Equal(1, data.Length);
            Assert.Equal("a", data.First());
        }

        #endregion
    }
}