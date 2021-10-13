// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO.Buffer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection("General")]
    public class DicomElementTest
    {
        #region Unit tests

        [Fact]
        public void DicomSignedShort_Array_GetDefaultValue()
        {
            var element = new DicomSignedShort(DicomTag.SynchronizationChannel, 5, 8);
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
            var element = new DicomUnsignedShort(DicomTag.ReferencedFrameNumber, 1, 2, 3, 4, 5);
            Assert.Equal(element.Get<ushort>(-1), element.Get<ushort>(0));
        }

        [Fact]
        public void DicomUnsignedShort_Array_ExplicitMinus2Throws()
        {
            var element = new DicomUnsignedShort(DicomTag.ReferencedFrameNumber, 1, 2, 3, 4, 5);
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
            var element = new DicomPersonName(DicomTag.ConsultingPhysicianName, new[] { "Doe^John", "Bar^Foo" });
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
            const Mock expected = Mock.One;
            var element = new DicomSignedShort(DicomTag.ALinesPerFrame, (short)expected);
            var actual = element.Get<Mock>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomValueElement_HasData_GetNullableReturnsDefinedNullable()
        {
            const ushort expected = 1;
            var element = new DicomUnsignedShort(DicomTag.ALinesPerFrame, expected);
            var actual = element.Get<ushort?>();
            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void DicomValueElement_HasData_GetNullableEnumReturnsDefinedNullableEnum()
        {
            const Mock expected = Mock.Two;
            var element = new DicomSignedLong(DicomTag.ReferencePixelX0, (int)expected);
            var actual = element.Get<Mock?>();
            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void DicomIntegerString_HasData_GetNullableReturnsDefinedNullable()
        {
            const double expected = -30.0;
            var element = new DicomIntegerString(DicomTag.EchoPeakPosition, (int)expected);
            var actual = element.Get<double?>();
            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void DicomIntegerString_HasData_GetNullableEnumReturnsDefinedNullableEnum()
        {
            const Mock expected = Mock.One;
            var element = new DicomIntegerString(DicomTag.NumberOfBeams, (int)expected);
            var actual = element.Get<Mock?>();
            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void DicomDecimalString_HasData_GetNullableReturnsDefinedNullable()
        {
            const int expected = -30;
            var element = new DicomDecimalString(DicomTag.ChannelOffset, expected);
            var actual = element.Get<int?>();
            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void DicomCodeString_HasData_GetNullableEnumReturnsDefinedNullableEnum()
        {
            const Mock expected = Mock.Zero;
            var element = new DicomCodeString(DicomTag.AcquisitionStatus, expected.ToString());
            var actual = element.Get<Mock?>();
            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void DicomCodeString_HasData_GetEnumReturnsDefinedEnum()
        {
            const Mock expected = Mock.Zero;
            var element = new DicomCodeString(DicomTag.AcquisitionStatus, expected.ToString());
            var actual = element.Get<Mock>();
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
            const Mock expected = Mock.Two;
            var element = new DicomIntegerString(DicomTag.AcquisitionTerminationConditionData, (int)expected, 4, 3);
            var actual = element.Get<Mock>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomCodeString_StringContainingNull_NullIsIgnored()
        {
            const Mock expected = Mock.Two;
            var element = new DicomCodeString(DicomTag.AITDeviceType, "Two\0");
            var actual = element.Get<Mock>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomCodeString_StringEndsWithSpace_SpaceIsIgnored()
        {
            const Mock expected = Mock.One;
            var element = new DicomCodeString(DicomTag.AITDeviceType, "One ");
            var actual = element.Get<Mock>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomDecimalString_GetDecimal_ReturnsValue()
            => TestDicomDecimalStringGetItem<decimal>();

        [Fact]
        public void DicomDecimalString_GetDouble_ReturnsValue()
            => TestDicomDecimalStringGetItem<double>();

        [Fact]
        public void DicomDecimalString_GetSingle_ReturnsValue()
            => TestDicomDecimalStringGetItem<float>();

        [Fact]
        public void DicomDecimalString_GetLong_ReturnsValue()
            => TestDicomDecimalStringGetItem<long>();

        [Fact]
        public void DicomDecimalString_GetString_ReturnsValue()
            => TestDicomDecimalStringGetItem<string>();

        [Fact]
        public void DicomDecimalString_GetObject_ReturnsValue()
            => TestDicomDecimalStringGetItem<object>();

        [Fact]
        public void DicomDecimalString_GetDecimalArray_ReturnsArray()
            => TestDicomDecimalStringGetArray<decimal>();

        [Fact]
        public void DicomDecimalString_GetDoubleArray_ReturnsArray()
            => TestDicomDecimalStringGetArray<double>();

        [Fact]
        public void DicomDecimalString_GetSingleArray_ReturnsArray()
            => TestDicomDecimalStringGetArray<float>();

        [Fact]
        public void DicomDecimalString_GetLongArray_ReturnsArray()
            => TestDicomDecimalStringGetArray<long>();

        [Fact]
        public void DicomDecimalString_GetNullableLongArray_ReturnsArray()
            => TestDicomDecimalStringGetArray<long?>();

        [Fact]
        public void DicomDecimalString_GetStringArray_ReturnsArray()
            => TestDicomDecimalStringGetArray<string>();

        [Fact]
        public void DicomDecimalString_GetObjectArray_ReturnsArray()
            => TestDicomDecimalStringGetArray<object>();

        [Fact]
        public void DicomIntegerString_GetDecimal_ReturnsValue()
            => TestDicomIntegerStringGetItem<decimal>();

        [Fact]
        public void DicomIntegerString_GetString_ReturnsValue()
            => TestDicomIntegerStringGetItem<string>();

        [Fact]
        public void DicomIntegerString_GetNullableUnsignedLong_ReturnsValue()
            => TestDicomIntegerStringGetItem<ulong?>();

        [Fact]
        public void DicomIntegerString_GetSingle_ReturnsValue()
            => TestDicomIntegerStringGetItem<float>();

        [Fact]
        public void DicomIntegerString_GetObject_ReturnsValue()
            => TestDicomIntegerStringGetItem<object>();

        [Fact]
        public void DicomIntegerString_GetIntArray_ReturnsArray()
            => TestDicomIntegerStringGetArray<int>();

        [Fact]
        public void DicomIntegerString_GetIntArrayFromString_ReturnsArray()
            => TestDicomIntegerStringGetArrayFromString<int>();

        [Fact]
        public void DicomIntegerString_GetLongArray_ReturnsArray()
            => TestDicomIntegerStringGetArray<long>();

        [Fact]
        public void DicomIntegerString_GetUnsignedShortArray_ReturnsArray()
            => TestDicomIntegerStringGetArray<ushort>();

        [Fact]
        public void DicomIntegerString_GetNullableDoubleArray_ReturnsArray()
            => TestDicomIntegerStringGetArray<double?>();

        [Fact]
        public void DicomIntegerString_GetSingleArray_ReturnsArray()
            => TestDicomIntegerStringGetArray<float>();

        [Fact]
        public void DicomIntegerString_GetDecimalArray_ReturnsArray()
            => TestDicomIntegerStringGetArray<decimal>();

        [Fact]
        public void DicomIntegerString_GetObjectArray_ReturnsArray()
            => TestDicomIntegerStringGetArray<object>();

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

        [Fact]
        public void AddValue_ToOtherVR_Throws()
        {
            var otherByte = new DicomOtherByte(DicomTag.SelectorOBValue);
            var exception = Record.Exception( () => otherByte.Add(new byte[] {0x33} ));
            Assert.IsType<NotSupportedException>(exception);
            var otherWord = new DicomOtherWord(DicomTag.SelectorOWValue);
            exception = Record.Exception( () => otherWord.Add(new byte[] {0x33} ));
            Assert.IsType<NotSupportedException>(exception);
            var otherFloat = new DicomOtherFloat(DicomTag.SelectorOFValue);
            exception = Record.Exception( () => otherFloat.Add(1.23f));
            Assert.IsType<NotSupportedException>(exception);
            var otherDouble = new DicomOtherDouble(DicomTag.SelectorODValue);
            exception = Record.Exception( () => otherDouble.Add(1.23));
            Assert.IsType<NotSupportedException>(exception);
            var otherLong = new DicomOtherLong(DicomTag.SelectorOLValue);
            exception = Record.Exception( () => otherLong.Add(1234567));
            Assert.IsType<NotSupportedException>(exception);
            var otherVeryLong = new DicomOtherVeryLong(DicomTag.SelectorOVValue);
            exception = Record.Exception( () => otherVeryLong.Add(12345678901234));
            Assert.IsType<NotSupportedException>(exception);
        }

        [Fact]
        public void AddString_ToEmptyMultiStringElement()
        {
            var tag = DicomTag.AccessionNumber;
            var element = new DicomShortString(tag);
            element.Add("AN345");
            Assert.Equal(1, element.Count);
            Assert.Equal("AN345", element.Get<string>(0));
        }

        [Fact]
        public void AddAdditionalString_ToMultiStringElement()
        {
            var tag = DicomTag.ImageType;
            var element = new DicomCodeString(tag, "Derived", "Secondary");
            element.Add("MIP");
            Assert.Equal(3, element.Count);
            Assert.Equal("Derived", element.Get<string>(0));
            Assert.Equal("MIP", element.Get<string>(2));
        }

        [Fact]
        public void AddTooManyValues_ToMultiStringElement_Throws()
        {
            var element = new DicomCodeString(DicomTag.Modality, "US");
            var exception = Record.Exception(() => element.Add("MR"));
            Assert.IsType<DicomValidationException>(exception);
            Assert.Equal(1, element.Count);
        }

        [Fact]
        public void AddingNumber_ToMultiStringElement_Throws()
        {
            var tag = DicomTag.ImageType;
            var element = new DicomCodeString(tag, "Derived", "Secondary");
            var exception = Record.Exception(() => element.Add(42));
            Assert.IsType<InvalidCastException>(exception);
        }

        [Fact]
        public void AddAdditionalValue_ToIntegerString()
        {
            var element = new DicomIntegerString(DicomTag.SelectorISValue, String.Empty);
            element.Add("1234");
            element.Add(5678);
            element.Add("-9101112");
            Assert.Equal(3, element.Count);
            Assert.Equal(1234, element.Get<int>(0));
            Assert.Equal(5678, element.Get<int>(1));
            Assert.Equal(-9101112, element.Get<int>(2));
        }

        [Fact]
        public void AddAdditionalValue_ToDecimalString()
        {
            var element = new DicomDecimalString(DicomTag.SelectorDSValue, String.Empty);
            element.Add("-1234");
            element.Add(5678);
            element.Add(-10f);
            element.Add("0.1234e3");
            Assert.Equal(4, element.Count);
            Assert.Equal(-1234, element.Get<double>(0));
            Assert.Equal(5678, element.Get<double>(1));
            Assert.Equal(-10, element.Get<double>(2));
            Assert.Equal(123.4, element.Get<double>(3));
        }

        [Fact]
        public void AddingInvalidString_ToMultiStringElement_Throws()
        {
            var element = new DicomShortString(DicomTag.AccessionNumber);
            var exception = Record.Exception(() => element.Add("TooLongAccessionNumber"));
            Assert.IsType<DicomValidationException>(exception);
        }

        [Fact]
        public void AddString_ToEmpty_SingleStringElement()
        {
            var element = new DicomShortText(DicomTag.InstitutionAddress, null);
            element.Add("Some address");
            Assert.Equal(1, element.Count);
            Assert.Equal("Some address", element.Get<string>());
        }

        [Fact]
        public void AddingAdditionalString_ToSingleStringElement_Throws()
        {
            var element = new DicomShortText(DicomTag.InstitutionAddress, "Some address");
            var exception = Record.Exception(() => element.Add("Another address"));
            Assert.IsType<DicomValidationException>(exception);
        }

        [Fact]
        public void AddingInvalidValue_ToSingleStringElement_Throws()
        {
            var element = new DicomShortText(DicomTag.InstitutionAddress, null);
            var exception = Record.Exception(() => element.Add(new DateTime()));
            Assert.IsType<InvalidCastException>(exception);
        }

        [Fact]
        public void AddValue_ToFD()
        {
            var element = new DicomFloatingPointDouble(DicomTag.SpectralWidth);
            element.Add(123.4567);
            element.Add(89.2f);
            Assert.Equal(2, element.Count);
            Assert.Equal(123.4567, element.Get<double>(0), 8);
            Assert.Equal(89.2, element.Get<double>(1), 5);
        }

        [Fact]
        public void AddIllegalValues_ToFD()
        {
            var element = new DicomFloatingPointDouble(DicomTag.SpectralWidth);
            var exception = Record.Exception(() => element.Add("12.56"));
            Assert.IsType<InvalidCastException>(exception);
            exception = Record.Exception(() => element.Add(new DateTime()));
            Assert.IsType<InvalidCastException>(exception);
        }

        [Fact]
        public void AddValue_ToFL()
        {
            var element = new DicomFloatingPointSingle(DicomTag.FilterThicknessMaximum);
            element.Add(123.4567);
            element.Add(89.2f);
            element.Add(42);
            Assert.Equal(3, element.Count);
            Assert.Equal(123.4567, element.Get<float>(0), 5);
            Assert.Equal(89.2, element.Get<float>(1), 5);
            Assert.Equal(42, element.Get<float>(2), 5);
        }

        [Fact]
        public void AddIllegalValues_ToFL()
        {
            var element = new DicomFloatingPointSingle(DicomTag.LocalizingCursorPosition);
            var exception = Record.Exception(() => element.Add("not a number"));
            Assert.IsType<InvalidCastException>(exception);
            exception = Record.Exception(() => element.Add(new TimeSpan()));
            Assert.IsType<InvalidCastException>(exception);
        }

        [Fact]
        public void AddValue_ToSS()
        {
            var element = new DicomSignedShort(DicomTag.VerticesOfTheRegion);
            element.Add(short.MaxValue);
            element.Add(short.MinValue);
            element.Add(0);
            Assert.Equal(3, element.Count);
            Assert.Equal(32767, element.Get<short>(0));
            Assert.Equal(-32768, element.Get<short>(1));
            Assert.Equal(0, element.Get<short>(2));
        }

        [Fact]
        public void AddValue_ToUS()
        {
            var element = new DicomUnsignedShort(DicomTag.LUTFrameRange);
            element.Add(ushort.MaxValue);
            element.Add(0);
            Assert.Equal(2, element.Count);
            Assert.Equal(65535, element.Get<ushort>(0));
            Assert.Equal(0, element.Get<ushort>(1));
        }

        [Fact]
        public void AddValue_ToUL()
        {
            var element = new DicomUnsignedLong(DicomTag.PrivateDataElementValueMultiplicity);
            element.Add(uint.MaxValue);
            element.Add(0);
            Assert.Equal(2, element.Count);
            Assert.Equal(4294967295, element.Get<uint>(0));
            Assert.Equal(0u, element.Get<uint>(1));
        }

        [Fact]
        public void AddValue_ToSV()
        {
            var element = new DicomSignedVeryLong(DicomTag.SelectorSVValue);
            element.Add(long.MaxValue);
            element.Add(long.MinValue);
            element.Add(0);
            Assert.Equal(3, element.Count);
            Assert.Equal(9223372036854775807, element.Get<long>(0));
            Assert.Equal(-9223372036854775808, element.Get<long>(1));
            Assert.Equal(0, element.Get<long>(2));
        }

        [Fact]
        public void AddValue_ToUV()
        {
            var element = new DicomUnsignedVeryLong(DicomTag.SelectorUVValue);
            element.Add(ulong.MaxValue);
            element.Add(0);
            Assert.Equal(2, element.Count);
            Assert.Equal(ulong.MaxValue, element.Get<ulong>(0));
            Assert.Equal(0ul, element.Get<ulong>(1));
        }

        [Theory]
        [MemberData(nameof(IllegalValuesToAdd))]
        public void AddIllegalValues(DicomElement element, object illegalValue1, object illegalValue2)
        {
            var exception = Record.Exception(() => element.Add("1234"));
            Assert.IsType<InvalidCastException>(exception);
            exception = Record.Exception(() => element.Add(12.34));
            Assert.IsType<InvalidCastException>(exception);
            exception = Record.Exception(() => element.Add(new DateTime()));
            Assert.IsType<InvalidCastException>(exception);
            exception = Record.Exception(() => element.Add(illegalValue1));
            Assert.IsType<InvalidCastException>(exception);
            exception = Record.Exception(() => element.Add(illegalValue2));
            Assert.IsType<InvalidCastException>(exception);
        }

        public static readonly IEnumerable<object[]> IllegalValuesToAdd = new[]
        {
            new object[] { new DicomSignedShort(DicomTag.LUTFrameRange), 32768, -32769 },
            new object[] { new DicomUnsignedShort(DicomTag.VerticesOfTheRegion), 65536, -1 },
            new object[] { new DicomSignedLong(DicomTag.DisplayedAreaTopLeftHandCorner), 2147483648, -2147483649 },
            new object[] { new DicomUnsignedLong(DicomTag.PrivateDataElementValueMultiplicity), 4294967296, -3 },
            new object[] { new DicomSignedVeryLong(DicomTag.SelectorSVValue), 9223372036854775808, 10223372036854775808 },
            new object[] { new DicomUnsignedVeryLong(DicomTag.SelectorUVValue), -1, -300 }
        };

        #endregion

        #region Support methods

        private void TestDicomDecimalStringGetItem<T>()
        {
            var expected = 45.0m;
            var element = new DicomDecimalString(DicomTag.MaterialThickness, 35.0m, expected, 55.0m);
            var actual = element.Get<T>(1);
            Assert.Equal((T)Convert.ChangeType(expected, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T), CultureInfo.InvariantCulture), actual);
        }

        private void TestDicomDecimalStringGetArray<T>()
        {
            var expected = new[] { 35.0m, 45.0m, 55.0m };
            var element = new DicomDecimalString(DicomTag.MaterialThickness, expected);
            var actual = element.Get<T[]>();
            Assert.Equal(expected.Select(i => (T)Convert.ChangeType(i, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T), CultureInfo.InvariantCulture)), actual);
        }

        private void TestDicomIntegerStringGetItem<T>()
        {
            var expected = 45;
            var element = new DicomIntegerString(DicomTag.AttachedContoursRETIRED, 35, expected, 55);
            var actual = element.Get<T>(1);
            Assert.Equal((T)Convert.ChangeType(expected, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T)), actual);
        }

        private void TestDicomIntegerStringGetArray<T>()
        {
            var expected = new[] { 35, 45, 55 };
            var element = new DicomIntegerString(DicomTag.AttachedContoursRETIRED, expected);
            var actual = element.Get<T[]>();
            Assert.Equal(expected.Select(i => (T)Convert.ChangeType(i, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T))), actual);
        }

        private void TestDicomIntegerStringGetArrayFromString<T>()
        {
            var expected = new[] { 35, 45, 55 };
            var element = new DicomIntegerString(DicomTag.AttachedContoursRETIRED, new[] { "35.0", "45.0000", "55" });
            var actual = element.Get<T[]>();
            Assert.Equal(expected.Select(i => (T)Convert.ChangeType(i, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T))), actual);
        }

        #endregion

        #region Support types

        private enum Mock
        {
            Zero,
            One,
            Two
        }

        #endregion

        #region Support data

        public static IEnumerable<object[]> KnownTransferSyntaxes = new[]
        {
            new object[] { DicomUID.JPEG2000Lossless, DicomTransferSyntax.JPEG2000Lossless },
            new object[] { DicomUID.ImplicitVRLittleEndian, DicomTransferSyntax.ImplicitVRLittleEndian },
            new object[] { DicomUID.JPEGExtended12Bit, DicomTransferSyntax.JPEGProcess2_4 },
            new object[] { DicomUID.JPEG2000Lossless, DicomTransferSyntax.JPEG2000Lossless },
            new object[] { DicomUID.ExplicitVRBigEndianRETIRED, DicomTransferSyntax.ExplicitVRBigEndian },
            new object[] { DicomUID.GEPrivateImplicitVRBigEndian, DicomTransferSyntax.GEPrivateImplicitVRBigEndian },
            new object[] { DicomUID.MPEG2MPML, DicomTransferSyntax.MPEG2 }
        };

        public static IEnumerable<object[]> TransferSyntaxUids = new[]
        {
            new object[] { DicomUID.XMLEncodingRETIRED },
            new object[] { DicomUID.MPEG4HP422D },
            new object[] { DicomUID.JPEG2000MCLossless },
            new object[] { DicomUID.JPIPReferencedDeflate },
            new object[] { DicomUID.RFC2557MIMEEncapsulationRETIRED }
        };

        public static IEnumerable<object[]> NonTransferSyntaxUids = new[]
        {
            new object[] { DicomUID.AbdominopelvicArteriesPaired12111 },
            new object[] { DicomUID.CTImageStorage },
            new object[] { DicomUID.StorageCommitmentPushModel },
            new object[] { DicomUID.dicomTransferSyntax },
            new object[] { DicomUID.PETPalette }
        };

        #endregion
    }
}
