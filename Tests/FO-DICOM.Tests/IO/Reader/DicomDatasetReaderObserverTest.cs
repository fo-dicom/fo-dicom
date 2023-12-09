// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using FellowOakDicom.IO.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FellowOakDicom.Tests.IO.Reader
{

    [Collection(TestCollections.General)]
    public class DicomDatasetReaderObserverTest
    {
        #region Unit tests

        [Theory]
        [MemberData(nameof(TestData))]
        public void OnElement_ValidData_AddsCorrectTypeToDataset(DicomTag tag, DicomVR vr, string data, Type expected)
        {
            var dataset = new DicomDataset();
            var observer = new DicomDatasetReaderObserver(dataset);
            var buffer = new MemoryByteBuffer(Encoding.ASCII.GetBytes(data));

            observer.OnElement(null, tag, vr, buffer);
            Assert.IsType(expected, dataset.First());
        }

        #endregion

        #region Support data

        public static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[] { DicomTag.MaterialThickness, DicomVR.DS, "45.6", typeof(DicomDecimalString) };
                yield return new object[] { DicomTag.UID, DicomVR.UI, "45.6.34.123", typeof(DicomUniqueIdentifier) };
                yield return new object[] { DicomTag.Originator, DicomVR.AE, "STORESCP", typeof(DicomApplicationEntity) };
                yield return new object[] { DicomTag.DoubleFloatPixelData, DicomVR.OD, Encoding.ASCII.GetString(new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80 }), typeof(DicomOtherDouble) };
                yield return new object[] { DicomTag.LongCodeValue, DicomVR.UC, "Very long text", typeof(DicomUnlimitedCharacters) };
                yield return new object[] { DicomTag.URNCodeValue, DicomVR.UR, "https://en.wikipedia.org/wiki/Uniform_resource_identifier#Examples_of_URI_references", typeof(DicomUniversalResource) };
            }
        }

        #endregion
    }
}
