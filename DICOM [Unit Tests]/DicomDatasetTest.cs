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
        }

        #endregion
    }
}