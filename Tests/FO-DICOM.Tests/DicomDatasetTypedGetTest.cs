// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection(TestCollections.General)]
    public class DicomDatasetTypedGetTest
    {
        #region Unit tests
        [Fact]
        public void Get_ValueCount_Succeeds()
        {
            Assert.Equal(_stringTestData.Values.Length, _stringTestData.Dataset.GetElem(_stringTestData.Tag).Count);
        }

        [Fact]
        public void GetElem_TagMissing_ShouldThrow()
        {
            var tag = DicomTag.SOPClassesSupported;
            var dataset = new DicomDataset();

            var e = Record.Exception(() => dataset.GetElem(tag));
            Assert.IsType<DicomDataException>(e);
        }


        [Fact]
        public void Get_Value_Succeeds()
        {
            Assert.Equal(_uLTestData.Values[1], _uLTestData.Dataset.GetElem(_uLTestData.Tag).Values[1]);
        }

        [Fact]
        public void Get_Value_TagMissing_ShouldThrow()
        {
            var tag = DicomTag.SimpleFrameList;
            var dataset = new DicomDataset();

            var e = Record.Exception(() => dataset.GetElem(tag).Values[0]);
            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Get_Value_OutOfRange_ShouldThrow()
        {
            var e = Record.Exception(() => _uLTestData.Dataset.GetElem(_uLTestData.Tag).Values[_uLTestData.Values.Length]);

            Assert.IsType<IndexOutOfRangeException>(e);
        }

        [Fact]
        public void Get_Value_NegativeIndex_OutOfRange_ShouldThrow()
        {
            var e = Record.Exception(() => _uLTestData.Dataset.GetElem(_uLTestData.Tag).Values[-1]);

            Assert.IsType<IndexOutOfRangeException>(e);
        }

        [Fact]
        public void TryGet_Value_Success()
        {
            bool success = _uLTestData.Dataset.TryGetElem(_uLTestData.Tag, out var elem);

            Assert.True(success);
            Assert.Equal(_uLTestData.Values[0], elem.Values[0]);
        }

        [Fact]
        public void Get_Values_Succeeds()
        {
            Assert.Equal(_uLTestData.Values,
                           _uLTestData.Dataset.GetElem(_uLTestData.Tag).Values);
        }

        [Fact]
        public void Get_Values_Throw()
        {
            var ds = new DicomDataset();

            var e = Record.Exception(() => ds.GetElem(_uLTestData.Tag).Values);

            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Get_Values_EmptyArray_Success()
        {
            Assert.Equal(_emptyStringTestData.Dataset.GetElem(_emptyStringTestData.Tag).StringValues, Array.Empty<string>());
        }

        [Fact]
        public void Try_Get_Values_TagMissing_Fails()
        {
            var dataset = new DicomDataset();
            bool success = dataset.TryGetElem(DicomTag.SOPClassesSupported, out var _);

            Assert.False(success);
        }

        [Fact]
        public void Try_Get_Values_TagEmpty_Success()
        {
            bool success = _emptyStringTestData.Dataset.TryGetElem(_emptyStringTestData.Tag, out var testElem);

            Assert.True(success);
            Assert.Equal(Array.Empty<string>(), testElem.StringValues);
        }

        [Fact]
        public void Get_SingleValue_Success()
        {
            Assert.Equal(_singleValueTestData.Values[0], _singleValueTestData.Dataset.GetElem(_singleValueTestData.Tag).StringValue);
        }

        [Fact]
        public void TryGet_SingleValue_Success()
        {
            bool success = _singleValueTestData.Dataset.TryGetElem(_singleValueTestData.Tag, out var testElem);

            Assert.True(success);
            Assert.Equal(_singleValueTestData.Values[0], testElem.StringValue);
        }

        [Fact]
        public void TryGet_SingleValue_Fail()
        {
            var ds = new DicomDataset();
            bool success = ds.TryGetElem(DicomTag.Modality, out var _);

            Assert.False(success);
        }

        [Fact]
        public void Get_String_Success()
        {
            string testValue = _stringTestData.Dataset.GetString(_stringTestData.Tag);

            Assert.Equal(string.Join("\\", _stringTestData.Values), testValue);
        }

        [Fact]
        public void Get_String_NonStringTag_ShouldThrow()
        {
            var e = Record.Exception(() => _uLTestData.Dataset.GetString(_stringTestData.Tag));

            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void TryGet_String_EmptyTag_Success()
        {
            bool success = _emptyStringTestData.Dataset.TryGetString(_emptyStringTestData.Tag, out string testValue);

            Assert.True(success);
            Assert.Equal(string.Empty, testValue);
        }

        [Fact]
        public void TryGet_String_TagMissing_Fail()
        {
            var ds = new DicomDataset();
            bool success = ds.TryGetString(DicomTag.Modality, out _);

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
            var ds = new DicomDataset(new[] { element }, false); // skip validation, since the intention of this test is retrieving various numbers of objects, even if they are violating VR constraints

            object[] objects = ds.GetValues<object>(element.Tag);

            Assert.Equal(expected, objects);
        }

        [Fact]
        public void TryGetStringMayNeverThrow()
        {
            var ds = new DicomDataset
            {
                // add some empty values
                { DicomTag.PregnancyStatus, "" }
            };

            foreach (var item in ds)
            {
                Assert.Null(Record.Exception(() => Assert.True(ds.TryGetString(item.Tag, out var _))));
            }
        }


        #endregion

        #region Support data
        private readonly DatasetTestData<DicomTagULs, uint> _uLTestData = new(DicomTag.SimpleFrameList, [1, 2, 3]);
        private readonly DatasetTestData<DicomTagUIs, string> _stringTestData = new(DicomTag.SOPClassesSupported, ["1.2.3", "4.5.6", "7.8.8.9"]);
        private readonly DatasetTestData<DicomTagUIs, string> _emptyStringTestData = new(DicomTag.SOPClassesSupported, []);
        private readonly DatasetTestData<DicomTagCS, string> _singleValueTestData = new(DicomTag.Modality, ["CT"]);

        private class DatasetTestData<U, T> where U : DicomTag
        {
            public DatasetTestData(U tag, T[] values)
            {
                Dataset = [];
                Tag = tag;
                Values = values;

                Dataset.Add(tag, values);
            }

            public U Tag { get; private set; }
            public DicomDataset Dataset { get; private set; }
            public T[] Values { get; private set; }
        }

        /// <summary>
        /// instances of DicomValueElement-derived classes with no values
        /// </summary>
        public static readonly object[][] ValueElementsWithNoValues = [
            [new DicomFloatingPointDouble(DicomTag.SelectorFDValue), Array.Empty<object>()],
            [new DicomFloatingPointSingle(DicomTag.SelectorFLValue), Array.Empty<object>()],
            [new DicomOtherByte(DicomTag.SelectorOBValue), Array.Empty<object>()],
            [new DicomOtherDouble(DicomTag.SelectorODValue), Array.Empty<object>()],
            [new DicomOtherFloat(DicomTag.SelectorOFValue), Array.Empty<object>()],
            [new DicomOtherLong(DicomTag.SelectorOLValue), Array.Empty<object>()],
            [new DicomOtherWord(DicomTag.SelectorOWValue), Array.Empty<object>()],
            [new DicomSignedLong(DicomTag.SelectorSLValue), Array.Empty<object>()],
            [new DicomSignedShort(DicomTag.SelectorSSValue), Array.Empty<object>()],
            [new DicomUnsignedLong(DicomTag.SelectorULValue), Array.Empty<object>()],
            [new DicomUnsignedShort(DicomTag.SelectorUSValue), Array.Empty<object>()],
        ];

        /// <summary>
        /// instances of DicomValueElement-derived classes with single value
        /// </summary>
        public static readonly object[][] ValueElementsWithSingleValue = [
            [new DicomFloatingPointDouble(DicomTag.SelectorFDValue, 1), new object[] { 1D }],
            [new DicomFloatingPointSingle(DicomTag.SelectorFLValue, 1), new object[] { 1F }],
            [new DicomOtherByte(DicomTag.SelectorOBValue, 1), new object[] { (byte)1, }],
            [new DicomOtherDouble(DicomTag.SelectorODValue, 1), new object[] { 1D }],
            [new DicomOtherFloat(DicomTag.SelectorOFValue, 1), new object[] { 1F }],
            [new DicomOtherLong(DicomTag.SelectorOLValue, 1), new object[] { 1U }],
            [new DicomOtherWord(DicomTag.SelectorOWValue, 1), new object[] { (ushort)1 }],
            [new DicomSignedLong(DicomTag.SelectorSLValue, 1), new object[] { 1 }],
            [new DicomSignedShort(DicomTag.SelectorSSValue, 1), new object[] { (short)1 }],
            [new DicomUnsignedLong(DicomTag.SelectorULValue, 1), new object[] { 1U }],
            [new DicomUnsignedShort(DicomTag.SelectorUSValue, 1), new object[] { (ushort)1 }],
        ];

        /// <summary>
        /// instances of DicomValueElement-derived classes with 2 values
        /// </summary>
        public static readonly object[][] ValueElementsWithTwoValues = [
            [new DicomFloatingPointDouble(DicomTag.SelectorFDValue, 1, 2), new object[] { 1D, 2D }],
            [new DicomFloatingPointSingle(DicomTag.SelectorFLValue, 1, 2), new object[] { 1F, 2F }],
            [new DicomOtherByte(DicomTag.SelectorOBValue, 1, 2), new object[] { (byte)1, (byte)2 }],
            [new DicomOtherDouble(DicomTag.SelectorODValue, 1, 2), new object[] { 1D, 2D }],
            [new DicomOtherFloat(DicomTag.SelectorOFValue, 1, 2), new object[] { 1F, 2F }],
            [new DicomOtherLong(DicomTag.SelectorOLValue, 1, 2), new object[] { 1U, 2U }],
            [new DicomOtherWord(DicomTag.SelectorOWValue, 1, 2), new object[] { (ushort)1, (ushort)2 }],
            [new DicomSignedLong(DicomTag.SelectorSLValue, 1, 2), new object[] { 1, 2 }],
            [new DicomSignedShort(DicomTag.SelectorSSValue, 1, 2), new object[] { (short)1, (short)2 }],
            [new DicomUnsignedLong(DicomTag.SelectorULValue, 1, 2), new object[] { 1U, 2U }],
            [new DicomUnsignedShort(DicomTag.SelectorUSValue, 1, 2), new object[] { (ushort)1, (ushort)2 }],
        ];

        #endregion
    }
}
