﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

using Dicom.IO.Buffer;

using Xunit;


namespace Dicom
{
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
        public void DicomSignedShortAsDicomElement_Array_GetObjectValue()
        {
            DicomElement element = new DicomSignedShort(DicomTag.SynchronizationChannel, 5, 8);
            Assert.Equal((short)5, element.Get<object>());
        }

        [Fact]
        public void DicomSignedShortAsDicomElement_Array_GetObjectArrayValue()
        {
            DicomElement element = new DicomSignedShort(DicomTag.SynchronizationChannel, 5, 8);
            Assert.Equal(new object[] { (short)5, (short)8 }, element.Get<object[]>());
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
        public void DicomUnsignedShortAsDicomElement_Array_GetObjectValue()
        {
            DicomElement element = new DicomUnsignedShort(DicomTag.SynchronizationChannel, 5, 8);
            Assert.Equal((ushort)5, element.Get<object>());
        }

        [Fact]
        public void DicomUnsignedShortAsDicomElement_Array_GetObjectArrayValue()
        {
            DicomElement element = new DicomUnsignedShort(DicomTag.SynchronizationChannel, 5, 8);
            Assert.Equal(new object[] { (ushort)5, (ushort)8 }, element.Get<object[]>());
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
        public void DicomOtherDouble_Value_ReturnsObjectNumber()
        {
            var element = new DicomOtherDouble(DicomTag.DoubleFloatPixelData, 12.345);
            var actual = element.Get<object>();
            Assert.Equal(12.345, actual);
        }

        [Fact]
        public void DicomFloatingPointDouble_Value_ReturnsObjectNumber()
        {
            var element = new DicomFloatingPointDouble(DicomTag.XRayTubeCurrentInmA, 12.345);
            var actual = element.Get<object>();
            Assert.Equal(12.345, actual);
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
        public void DicomValueElement_GetEnum_ReturnsEnum()
        {
            const MockEnum expected = MockEnum.One;
            var element = new DicomSignedShort(DicomTag.ALinesPerFrame, (short)expected);
            var actual = element.Get<MockEnum>();
            Assert.Equal(expected, actual);
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

        [Fact(DisplayName = "GH-231")]
        public void DicomIntegerString_GetIntegerDefaultArgument_ShouldReturnFirstValue()
        {
            const int expected = 5;
            var element = new DicomIntegerString(DicomTag.AcquisitionTerminationConditionData, expected, 4, 3);
            var actual = element.Get<int>();
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "GH-231")]
        public void DicomIntegerString_GetDecimalDefaultArgument_ShouldReturnFirstValue()
        {
            const decimal expected = 5m;
            var element = new DicomIntegerString(DicomTag.AcquisitionTerminationConditionData, (int)expected, 4, 3);
            var actual = element.Get<decimal>();
            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "GH-231")]
        public void DicomIntegerString_GetEnumDefaultArgument_ShouldReturnFirstValue()
        {
            const MockEnum expected = MockEnum.Two;
            var element = new DicomIntegerString(DicomTag.AcquisitionTerminationConditionData, (int)expected, 4, 3);
            var actual = element.Get<MockEnum>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomCodeString_StringContainingNull_NullIsIgnored()
        {
            const MockEnum expected = MockEnum.Two;
            var element = new DicomCodeString(DicomTag.AITDeviceType, "Two\0");
            var actual = element.Get<MockEnum>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomCodeString_StringEndsWithSpace_SpaceIsIgnored()
        {
            const MockEnum expected = MockEnum.One;
            var element = new DicomCodeString(DicomTag.AITDeviceType, "One ");
            var actual = element.Get<MockEnum>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomDecimalString_GetDecimal_ReturnsValue()
        {
            this.TestDicomDecimalStringGetItem<decimal>();
        }

        [Fact]
        public void DicomDecimalString_GetDouble_ReturnsValue()
        {
            this.TestDicomDecimalStringGetItem<double>();
        }

        [Fact]
        public void DicomDecimalString_GetSingle_ReturnsValue()
        {
            this.TestDicomDecimalStringGetItem<float>();
        }

        [Fact]
        public void DicomDecimalString_GetLong_ReturnsValue()
        {
            this.TestDicomDecimalStringGetItem<long>();
        }

        [Fact]
        public void DicomDecimalString_GetString_ReturnsValue()
        {
            this.TestDicomDecimalStringGetItem<string>();
        }

        [Fact]
        public void DicomDecimalString_GetObject_ReturnsValue()
        {
            this.TestDicomDecimalStringGetItem<object>();
        }

        [Fact]
        public void DicomDecimalString_GetDecimalArray_ReturnsArray()
        {
            this.TestDicomDecimalStringGetArray<decimal>();
        }

        [Fact]
        public void DicomDecimalString_GetDoubleArray_ReturnsArray()
        {
            this.TestDicomDecimalStringGetArray<double>();
        }

        [Fact]
        public void DicomDecimalString_GetSingleArray_ReturnsArray()
        {
            this.TestDicomDecimalStringGetArray<float>();
        }

        [Fact]
        public void DicomDecimalString_GetLongArray_ReturnsArray()
        {
            this.TestDicomDecimalStringGetArray<long>();
        }

        [Fact]
        public void DicomDecimalString_GetNullableLongArray_ReturnsArray()
        {
            this.TestDicomDecimalStringGetArray<long?>();
        }

        [Fact]
        public void DicomDecimalString_GetStringArray_ReturnsArray()
        {
            this.TestDicomDecimalStringGetArray<string>();
        }

        [Fact]
        public void DicomDecimalString_GetObjectArray_ReturnsArray()
        {
            this.TestDicomDecimalStringGetArray<object>();
        }

        [Fact]
        public void DicomIntegerString_GetDecimal_ReturnsValue()
        {
            this.TestDicomIntegerStringGetItem<decimal>();
        }

        [Fact]
        public void DicomIntegerString_GetString_ReturnsValue()
        {
            this.TestDicomIntegerStringGetItem<string>();
        }

        [Fact]
        public void DicomIntegerString_GetNullableUnsignedLong_ReturnsValue()
        {
            this.TestDicomIntegerStringGetItem<ulong?>();
        }

        [Fact]
        public void DicomIntegerString_GetSingle_ReturnsValue()
        {
            this.TestDicomIntegerStringGetItem<float>();
        }

        [Fact]
        public void DicomIntegerString_GetObject_ReturnsValue()
        {
            this.TestDicomIntegerStringGetItem<object>();
        }

        [Fact]
        public void DicomIntegerString_GetIntArray_ReturnsArray()
        {
            this.TestDicomIntegerStringGetArray<int>();
        }

        [Fact]
        public void DicomIntegerString_GetLongArray_ReturnsArray()
        {
            this.TestDicomIntegerStringGetArray<long>();
        }

        [Fact]
        public void DicomIntegerString_GetUnsignedShortArray_ReturnsArray()
        {
            this.TestDicomIntegerStringGetArray<ushort>();
        }

        [Fact]
        public void DicomIntegerString_GetNullableDoubleArray_ReturnsArray()
        {
            this.TestDicomIntegerStringGetArray<double?>();
        }

        [Fact]
        public void DicomIntegerString_GetSingleArray_ReturnsArray()
        {
            this.TestDicomIntegerStringGetArray<float>();
        }

        [Fact]
        public void DicomIntegerString_GetDecimalArray_ReturnsArray()
        {
            this.TestDicomIntegerStringGetArray<decimal>();
        }

        [Fact]
        public void DicomIntegerString_GetObjectArray_ReturnsArray()
        {
            this.TestDicomIntegerStringGetArray<object>();
        }

        [Theory]
        [MemberData(nameof(KnownTransferSyntaxes))]
        public void DicomUniqueIdentifier_GetKnownTransferSyntax_ReturnsFullField(DicomUID uid, DicomTransferSyntax expected)
        {
            var tag = DicomTag.ReferencedTransferSyntaxUIDInFile;
            var ui = new DicomUniqueIdentifier(tag, uid);
            var actual = ui.Get<DicomTransferSyntax>(0);

            Assert.Equal(expected.UID, actual.UID);
            Assert.Equal(expected.Endian, actual.Endian);
            Assert.Equal(expected.LossyCompressionMethod, actual.LossyCompressionMethod);
        }

        [Theory]
        [MemberData(nameof(TransferSyntaxUids))]
        public void DicomUniqueIdentifier_UidTransferSyntax_DoesNotThrow(DicomUID expected)
        {
            var tag = DicomTag.EncryptedContentTransferSyntaxUID;
            var ui = new DicomUniqueIdentifier(tag, expected);

            DicomUID actual = null;
            var exception = Record.Exception(() => actual = ui.Get<DicomTransferSyntax>(0).UID);
            Assert.Null(exception);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(NonTransferSyntaxUids))]
        public void DicomUniqueIdentifier_UidNotTransferSyntax_Throws(DicomUID uid)
        {
            var tag = DicomTag.EncryptedContentTransferSyntaxUID;
            var ui = new DicomUniqueIdentifier(tag, uid);

            var exception = Record.Exception(() => ui.Get<DicomTransferSyntax>(0));
            Assert.IsType<DicomDataException>(exception);
        }

        #endregion

        #region Support methods

        internal void TestDicomDecimalStringGetItem<T>()
        {
            var expected = 45.0m;
            var element = new DicomDecimalString(DicomTag.MaterialThickness, 35.0m, expected, 55.0m);
            var actual = element.Get<T>(1);
            Assert.Equal((T)Convert.ChangeType(expected, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T)), actual);
        }

        internal void TestDicomDecimalStringGetArray<T>()
        {
            var expected = new[] { 35.0m, 45.0m, 55.0m };
            var element = new DicomDecimalString(DicomTag.MaterialThickness, expected);
            var actual = element.Get<T[]>();
            Assert.Equal(expected.Select(i => (T)Convert.ChangeType(i, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T))), actual);
        }

        internal void TestDicomIntegerStringGetItem<T>()
        {
            var expected = 45;
            var element = new DicomIntegerString(DicomTag.AttachedContours, 35, expected, 55);
            var actual = element.Get<T>(1);
            Assert.Equal((T)Convert.ChangeType(expected, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T)), actual);
        }

        internal void TestDicomIntegerStringGetArray<T>()
        {
            var expected = new[] { 35, 45, 55 };
            var element = new DicomIntegerString(DicomTag.AttachedContours, expected);
            var actual = element.Get<T[]>();
            Assert.Equal(expected.Select(i => (T)Convert.ChangeType(i, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T))), actual);
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

        #region Support data

        public static IEnumerable<object[]> KnownTransferSyntaxes = new[]
        {
            new object[] { DicomUID.JPEG2000LosslessOnly, DicomTransferSyntax.JPEG2000Lossless },
            new object[] { DicomUID.ImplicitVRLittleEndian, DicomTransferSyntax.ImplicitVRLittleEndian },
            new object[] { DicomUID.JPEGExtended24, DicomTransferSyntax.JPEGProcess2_4 },
            new object[] { DicomUID.JPEG2000LosslessOnly, DicomTransferSyntax.JPEG2000Lossless },
            new object[] { DicomUID.ExplicitVRBigEndianRETIRED, DicomTransferSyntax.ExplicitVRBigEndian },
            new object[] { DicomUID.GEPrivateImplicitVRBigEndian, DicomTransferSyntax.GEPrivateImplicitVRBigEndian },
            new object[] { DicomUID.MPEG2, DicomTransferSyntax.MPEG2 }
        };

        public static IEnumerable<object[]> TransferSyntaxUids = new[]
        {
            new object[] { DicomUID.XMLEncoding },
            new object[] { DicomUID.MPEG4AVCH264HighProfileLevel42For2DVideo },
            new object[] { DicomUID.JPEG2000Part2MultiComponentLosslessOnly },
            new object[] { DicomUID.JPIPReferencedDeflate },
            new object[] { DicomUID.RFC2557MIMEEncapsulation }
        };

        public static IEnumerable<object[]> NonTransferSyntaxUids = new[]
        {
            new object[] { DicomUID.AbdominalArteriesLateral12111 },
            new object[] { DicomUID.CTImageStorage },
            new object[] { DicomUID.StorageCommitmentPushModelSOPClass },
            new object[] { DicomUID.dicomTransferSyntax },
            new object[] { DicomUID.PETColorPaletteSOPInstance }
        };

        #endregion
    }
}
