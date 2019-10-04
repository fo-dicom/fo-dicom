// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection("General")]
    public class DicomDatasetGetValuesTest
    {
        #region Unit tests
        [Fact]
        public void Get_ValueCount_Succeeds()
        {
            Assert.Equal(StringTestData.Values.Length, StringTestData.Dataset.GetValueCount(StringTestData.Tag));
        }

        [Fact]
        public void Get_ValueCount_TagMissing_ShouldThrow()
        {
            var tag = DicomTag.SOPClassesSupported;
            var dataset = new DicomDataset();

            var e = Record.Exception(() => dataset.GetValueCount(tag));
            Assert.IsType<DicomDataException>(e);
        }


        [Fact]
        public void Get_Value_Succeeds()
        {
            Assert.Equal(ULTestData.Values[1], ULTestData.Dataset.GetValue<long>(ULTestData.Tag, 1));
        }

        [Fact]
        public void Get_Value_TagMissing_ShouldThrow()
        {
            var tag = DicomTag.SimpleFrameList;
            var dataset = new DicomDataset();

            var e = Record.Exception(() => dataset.GetValue<long>(tag, 0));
            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Get_Value_OutOfRange_ShouldThrow()
        {
            var e = Record.Exception(() => ULTestData.Dataset.GetValue<long>(ULTestData.Tag, ULTestData.Values.Length));

            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Get_Value_NegativeIndex_OutOfRange_ShouldThrow()
        {
            var e = Record.Exception(() => ULTestData.Dataset.GetValue<long>(ULTestData.Tag, -1));

            Assert.IsType<ArgumentOutOfRangeException>(e);
        }

        [Fact]
        public void Get_Value_ArrayType_InvalidOperation_ShouldThrow()
        {
            var e = Record.Exception(() => ULTestData.Dataset.GetValue<long[]>(ULTestData.Tag, 0));

            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void TryGet_Value_Success()
        {
            long testValue;

            bool success = ULTestData.Dataset.TryGetValue(ULTestData.Tag, 0, out testValue);

            Assert.True(success);
            Assert.Equal(ULTestData.Values[0], testValue);
        }

        [Fact]
        public void TryGet_Value_Empty_Fail()
        {
            string testValue;

            bool success = EmptyStringTestData.Dataset.TryGetValue(EmptyStringTestData.Tag, 0, out testValue);

            Assert.False(success);
        }

        [Fact]
        public void Get_Values_Succeeds()
        {
            Assert.Equal(ULTestData.Values,
                           ULTestData.Dataset.GetValues<uint>(ULTestData.Tag));
        }

        [Fact]
        public void Get_Values_Throw()
        {
            DicomDataset ds = new DicomDataset();

            var e = Record.Exception(() => ds.GetValues<uint>(ULTestData.Tag));

            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Get_Values_EmptyArray_Success()
        {
            Assert.Equal(EmptyStringTestData.Dataset.GetValues<string>(EmptyStringTestData.Tag), new string[0]);
        }

        [Fact]
        public void Try_Get_Values_TagMissing_Fails()
        {
            var dataset = new DicomDataset();
            string[] testValues;
            bool success = dataset.TryGetValues<string>(DicomTag.SOPClassesSupported, out testValues);

            Assert.False(success);
        }

        [Fact]
        public void Try_Get_Values_TagEmpty_Success()
        {
            string[] testValues;
            bool success = EmptyStringTestData.Dataset.TryGetValues(EmptyStringTestData.Tag, out testValues);

            Assert.True(success);
            Assert.Equal(new string[0], testValues);
        }

        [Fact]
        public void Get_SingleValue_Success()
        {
            Assert.Equal(SingleValueTestData.Values[0], SingleValueTestData.Dataset.GetSingleValue<string>(SingleValueTestData.Tag));
        }

        [Fact]
        public void Get_SingleValue_MultiValue_Throws()
        {
            var e = Record.Exception(() => StringTestData.Dataset.GetSingleValue<string>(StringTestData.Tag));
            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void TryGet_SingleValue_Success()
        {
            string testValue;

            bool success = SingleValueTestData.Dataset.TryGetSingleValue(SingleValueTestData.Tag, out testValue);

            Assert.True(success);
            Assert.Equal(SingleValueTestData.Values[0], testValue);
        }

        [Fact]
        public void TryGet_SingleValue_Fail()
        {
            DicomDataset ds = new DicomDataset();
            string testValue;

            bool success = ds.TryGetSingleValue(DicomTag.Modality, out testValue);

            Assert.False(success);
        }

        [Fact]
        public void Get_String_Success()
        {
            string testValue = StringTestData.Dataset.GetString(StringTestData.Tag);

            Assert.Equal(string.Join("\\", StringTestData.Values), testValue);
        }

        [Fact]
        public void Get_String_NonStringTag_ShouldThrow()
        {
            var e = Record.Exception(() => ULTestData.Dataset.GetString(StringTestData.Tag));

            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void TryGet_String_EmptyTag_Success()
        {
            string testValue;
            bool success = EmptyStringTestData.Dataset.TryGetString(EmptyStringTestData.Tag, out testValue);

            Assert.True(success);
            Assert.Equal(string.Empty, testValue);
        }

        [Fact]
        public void TryGet_String_TagMissing_Fail()
        {
            DicomDataset ds = new DicomDataset();
            string testValue;
            bool success = ds.TryGetString(DicomTag.Modality, out testValue);

            Assert.False(success);
        }

        /// <summary>
        /// issue #720
        /// </summary>
        /// <param name="element"></param>
        /// <param name="expected"></param>
        [Theory]
        [MemberData(nameof(ValueElementsWithNoValues))]
        [MemberData(nameof(ValueElementsWithSingleValue))]
        [MemberData(nameof(ValueElementsWithTwoValues))]
        public void Get_Values_ObjectArray_Success(DicomElement element, object[] expected)
        {
            DicomDataset ds = new DicomDataset( new[] { element }, false); // skip validation, since the intention of this test is retrieving various numbers of objects, even if they are violating VR constraints

            object[] objects = ds.GetValues<object>(element.Tag);

            Assert.Equal(expected, objects);
        }

        #endregion

        #region Support data
        private DatasetTestData<uint> ULTestData = new DatasetTestData<uint>(DicomTag.SimpleFrameList, new uint[] { 1, 2, 3 });
        private DatasetTestData<string> StringTestData = new DatasetTestData<string>(DicomTag.SOPClassesSupported, new string[] { "1.2.3", "4.5.6", "7.8.8.9" });
        private DatasetTestData<string> EmptyStringTestData = new DatasetTestData<string>(DicomTag.SOPClassesSupported, new string[] { });
        private DatasetTestData<string> SingleValueTestData = new DatasetTestData<string>(DicomTag.Modality, new string[] { "CT" });

        private class DatasetTestData<T>
        {
            public DatasetTestData(DicomTag tag, T[] values)
            {
                Dataset = new DicomDataset();
                Tag = tag;
                Values = values;

                Dataset.Add(tag, values);
            }

            public DicomTag Tag { get; private set; }
            public DicomDataset Dataset { get; private set; }
            public T[] Values { get; private set; }
        }

        /// <summary>
        /// instances of DicomValueElement-derived classes with no values
        /// </summary>
        public static readonly object[][] ValueElementsWithNoValues = new[]{
            new object[] { new DicomFloatingPointDouble(DicomTag.SelectorFDValue), new object[0] },
            new object[] { new DicomFloatingPointSingle(DicomTag.SelectorFLValue), new object[0] },
            new object[] { new DicomOtherByte(DicomTag.SelectorOBValue), new object[0] },
            new object[] { new DicomOtherDouble(DicomTag.SelectorODValue), new object[0] },
            new object[] { new DicomOtherFloat(DicomTag.SelectorOFValue), new object[0] },
            new object[] { new DicomOtherLong(DicomTag.SelectorOLValue), new object[0] },
            new object[] { new DicomOtherWord(DicomTag.SelectorOWValue), new object[0] },
            new object[] { new DicomSignedLong(DicomTag.SelectorSLValue), new object[0] },
            new object[] { new DicomSignedShort(DicomTag.SelectorSSValue), new object[0] },
            new object[] { new DicomUnsignedLong(DicomTag.SelectorULValue), new object[0] },
            new object[] { new DicomUnsignedShort(DicomTag.SelectorUSValue), new object[0] },
        };

        /// <summary>
        /// instances of DicomValueElement-derived classes with single value
        /// </summary>
        public static readonly object[][] ValueElementsWithSingleValue = new[]{
            new object[] { new DicomFloatingPointDouble(DicomTag.SelectorFDValue, 1), new object[] { 1D } },
            new object[] { new DicomFloatingPointSingle(DicomTag.SelectorFLValue, 1), new object[] { 1F } },
            new object[] { new DicomOtherByte(DicomTag.SelectorOBValue, 1), new object[] { (byte)1, }  },
            new object[] { new DicomOtherDouble(DicomTag.SelectorODValue, 1), new object[] { 1D } },
            new object[] { new DicomOtherFloat(DicomTag.SelectorOFValue, 1), new object[] { 1F } },
            new object[] { new DicomOtherLong(DicomTag.SelectorOLValue, 1), new object[] { 1U } },
            new object[] { new DicomOtherWord(DicomTag.SelectorOWValue, 1), new object[] { (ushort)1 } },
            new object[] { new DicomSignedLong(DicomTag.SelectorSLValue, 1), new object[] { 1 } },
            new object[] { new DicomSignedShort(DicomTag.SelectorSSValue, 1), new object[] { (short)1 } },
            new object[] { new DicomUnsignedLong(DicomTag.SelectorULValue, 1), new object[] { 1U } },
            new object[] { new DicomUnsignedShort(DicomTag.SelectorUSValue, 1), new object[] { (ushort)1 } },
        };

        /// <summary>
        /// instances of DicomValueElement-derived classes with 2 values
        /// </summary>
        public static readonly object[][] ValueElementsWithTwoValues = new[]{
            new object[] { new DicomFloatingPointDouble(DicomTag.SelectorFDValue, 1, 2), new object[] { 1D, 2D } },
            new object[] { new DicomFloatingPointSingle(DicomTag.SelectorFLValue, 1, 2), new object[] { 1F, 2F } },
            new object[] { new DicomOtherByte(DicomTag.SelectorOBValue, 1, 2), new object[] { (byte)1, (byte)2 }  },
            new object[] { new DicomOtherDouble(DicomTag.SelectorODValue, 1, 2), new object[] { 1D, 2D } },
            new object[] { new DicomOtherFloat(DicomTag.SelectorOFValue, 1, 2), new object[] { 1F, 2F } },
            new object[] { new DicomOtherLong(DicomTag.SelectorOLValue, 1, 2), new object[] { 1U, 2U } },
            new object[] { new DicomOtherWord(DicomTag.SelectorOWValue, 1, 2), new object[] { (ushort)1, (ushort)2 } },
            new object[] { new DicomSignedLong(DicomTag.SelectorSLValue, 1, 2), new object[] { 1, 2 } },
            new object[] { new DicomSignedShort(DicomTag.SelectorSSValue, 1, 2), new object[] { (short)1, (short)2 } },
            new object[] { new DicomUnsignedLong(DicomTag.SelectorULValue, 1, 2), new object[] { 1U, 2U } },
            new object[] { new DicomUnsignedShort(DicomTag.SelectorUSValue, 1, 2), new object[] { (ushort)1, (ushort)2 } },
        };

        #endregion
    }
}
