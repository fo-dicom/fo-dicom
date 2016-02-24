// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    using Dicom.IO.Buffer;

    using Xunit;

    [Collection("General")]
    public class DicomElementTest
    {
        #region Unit tests

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

        [Fact]
        public void DicomOtherDouble_ByteBuffer_ReturnsValidNumber()
        {
            var element = new DicomOtherDouble(DicomTag.DoubleFloatPixelData, new MemoryByteBuffer(new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80 }));
            var actual = element.Get<double>();
            Assert.InRange(actual, double.MinValue, double.MaxValue);
        }

        [Fact]
        public void DicomUnlimitedCharacters_MultipleStrings_ReturnsDelimitedString()
        {
            var element = new DicomUnlimitedCharacters(DicomTag.DoubleFloatPixelData, "a", "b", "c");
            var actual = element.Get<string>();
            Assert.Equal(@"a\b\c", actual);
        }

        [Fact]
        public void DicomPersonName_FamilyAndSurname_YieldsCompositeName()
        {
            var element = new DicomPersonName(DicomTag.ConsultingPhysicianName, "Doe", "John");
            var actual = element.Get<string>(0);
            Assert.Equal("Doe^John", actual);
        }

        [Fact]
        public void DicomPersonName_TwoNames_YieldsTwoValues()
        {
            var element = new DicomPersonName(DicomTag.ConsultingPhysicianName, new [] { "Doe^John", "Bar^Foo"});
            var actual = element.Get<string[]>();
            Assert.Equal(2, actual.Length);
            Assert.Equal("Bar^Foo", actual[1]);
        }

        [Fact]
        public void DicomDecimalString_WorksInCommaCulture()
        {
          var currentCulture = Thread.CurrentThread.CurrentCulture;
          try
          {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de-DE");

            var decimals = new[] { 0.1m, 1e10m, -1m, 0, 3.141592656m };
            var element = new DicomDecimalString(DicomTag.CumulativeMetersetWeight, decimals);
            Assert.Equal(decimals, element.Get<decimal[]>());
            Assert.Equal(decimals.Select(x => (double)x), element.Get<double[]>());
          }
          finally
          {
            Thread.CurrentThread.CurrentCulture = currentCulture;
          }
        }

        [Fact]
        public void DicomValueElement_HasData_GetNullableReturnsDefinedNullable()
        {
            const ushort expected = 1;
            var element = new DicomUnsignedShort(DicomTag.ALinesPerFrame, expected);
            var actual = element.Get<ushort?>().Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomValueElement_HasData_GetNullableEnumReturnsDefinedNullableEnum()
        {
            const MockEnum expected = MockEnum.Two;
            var element = new DicomSignedLong(DicomTag.ReferencePixelX0, (int)expected);
            var actual = element.Get<MockEnum?>().Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomIntegerString_HasData_GetNullableReturnsDefinedNullable()
        {
            const double expected = -30.0;
            var element = new DicomIntegerString(DicomTag.EchoPeakPosition, (int)expected);
            var actual = element.Get<double?>().Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomIntegerString_HasData_GetNullableEnumReturnsDefinedNullableEnum()
        {
            const MockEnum expected = MockEnum.One;
            var element = new DicomIntegerString(DicomTag.NumberOfBeams, (int)expected);
            var actual = element.Get<MockEnum?>().Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomDecimalString_HasData_GetNullableReturnsDefinedNullable()
        {
            const int expected = -30;
            var element = new DicomDecimalString(DicomTag.ChannelOffset, expected);
            var actual = element.Get<int?>().Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomCodeString_HasData_GetNullableEnumReturnsDefinedNullableEnum()
        {
            const MockEnum expected = MockEnum.Zero;
            var element = new DicomCodeString(DicomTag.AcquisitionStatus, expected.ToString());
            var actual = element.Get<MockEnum?>().Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomCodeString_HasData_GetEnumReturnsDefinedEnum()
        {
            const MockEnum expected = MockEnum.Zero;
            var element = new DicomCodeString(DicomTag.AcquisitionStatus, expected.ToString());
            var actual = element.Get<MockEnum>();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Support types

        private enum MockEnum
        {
            Zero,
            One,
            Two
        }

        #endregion
    }
}