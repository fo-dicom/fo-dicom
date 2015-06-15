// Copyright (c) 2011-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;

    using Xunit;

    public class DicomElementTest
    {
        [Fact]
        public void DicomSignedShort_Array_GetDefaultValue()
        {
            DicomSignedShort element = new DicomSignedShort(DicomTag.SynchronizationChannel, 5, 8);
            Assert.Equal((short)5, element.Get<short>());
        }

        [Fact]
        public void DicomSignedShortAsDicomElement_Array_GetDefaultValue()
        {
            DicomElement element = new DicomSignedShort(DicomTag.SynchronizationChannel, 5, 8);
            Assert.Equal((short)5, element.Get<short>());
        }

        [Fact]
        public void AttributeTagAsDicomElement_Array_GetDefaultValue()
        {
            var expected = DicomTag.ALinePixelSpacing;
            DicomElement element = new DicomAttributeTag(DicomTag.DimensionIndexPointer, DicomTag.ALinePixelSpacing);
            var actual = element.Get<DicomTag>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomUnsignedShort_Array_ExplicitMinus1InterpretAs0()
        {
            var element = new DicomUnsignedShort(DicomTag.ReferencedFrameNumbers, 1, 2, 3, 4, 5);
            Assert.Equal(element.Get<ushort>(-1), element.Get<ushort>(0));
        }

        [Fact]
        public void DicomUnsignedShort_Array_ExplicitMinus2Throws()
        {
            var element = new DicomUnsignedShort(DicomTag.ReferencedFrameNumbers, 1, 2, 3, 4, 5);
            Assert.Throws<ArgumentOutOfRangeException>(() => element.Get<ushort>(-2));
        }
    }
}