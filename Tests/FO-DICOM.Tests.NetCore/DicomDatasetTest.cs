// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Text;

using Dicom.IO.Buffer;

namespace Dicom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Xunit;

    [Collection("General")]
    public class DicomDatasetTest
    {
        #region Unit tests

        [Fact]
        public void Add_OtherDoubleElement_Succeeds()
        {
            var tag = DicomTag.DoubleFloatPixelData;
            var dataset = new DicomDataset();
            dataset.Add(tag, 3.45);
            Assert.IsType<DicomOtherDouble>(dataset.First(item => item.Tag.Equals(tag)));
        }

        [Fact]
        public void Add_OtherDoubleElementWithMultipleDoubles_Succeeds()
        {
            var tag = DicomTag.DoubleFloatPixelData;
            var dataset = new DicomDataset();
            dataset.Add(tag, 3.45, 6.78, 9.01);
            Assert.IsType<DicomOtherDouble>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal(3, dataset.GetValues<double>(tag).Length);
        }

        [Fact]
        public void Add_UnlimitedCharactersElement_Succeeds()
        {
            var tag = DicomTag.LongCodeValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, "abc");
            Assert.IsType<DicomUnlimitedCharacters>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal("abc", dataset.GetSingleValue<string>(tag));
        }

        [Fact]
        public void Add_UnlimitedCharactersElementWithMultipleStrings_Succeeds()
        {
            var tag = DicomTag.LongCodeValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, "a", "b", "c");
            Assert.IsType<DicomUnlimitedCharacters>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal("c", dataset.GetValue<string>(tag, 2));
        }

        [Fact]
        public void Add_UnlimitedTextElementWithEmptyValues_Succeeds()
        {
            var tag = DicomTag.SelectorUTValue;
            var dataset = new DicomDataset();
            dataset.Add<string>(tag);
            Assert.IsType<DicomUnlimitedText>(dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.GetValues<string>(tag);
            Assert.Single(data, (item) => item == string.Empty);
        }

        [Fact]
        public void Add_ShortTextElementWithEmptyValues_Succeeds()
        {
            var tag = DicomTag.SelectorSTValue;
            var dataset = new DicomDataset();
            dataset.Add<string>(tag);
            Assert.IsType<DicomShortText>(dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.GetValues<string>(tag);
            Assert.Single(data, (item) => item == string.Empty);
        }

        [Fact]
        public void Add_LongTextElementWithEmptyValues_Succeeds()
        {
            var tag = DicomTag.SelectorLTValue;
            var dataset = new DicomDataset();
            dataset.Add<string>(tag);
            Assert.IsType<DicomLongText>(dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.GetValues<string>(tag);
            Assert.Single(data, (item) => item == string.Empty);
        }

        [Fact]
        public void Add_UniversalResourceElementWithEmptyValues_Succeeds()
        {
            var tag = DicomTag.URNCodeValue;
            var dataset = new DicomDataset();
            dataset.Add<string>(tag);
            Assert.IsType<DicomUniversalResource>(dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.GetValues<string>(tag);
            Assert.Single(data, (item) => item == string.Empty);
        }

        [Fact]
        public void Add_UnsignedShortElementWithEmptyValues_Succeeds()
        {
            var tag = DicomTag.SamplesPerPixel;
            var dataset = new DicomDataset();
            dataset.Add<ushort>(tag);
            Assert.IsType<DicomUnsignedShort>(dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.GetValues<ushort>(tag);

            Assert.Empty(data);
        }

        [Fact]
        public void Add_UniversalResourceElement_Succeeds()
        {
            var tag = DicomTag.URNCodeValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, "abc");
            Assert.IsType<DicomUniversalResource>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal("abc", dataset.GetSingleValue<string>(tag));
        }

        [Fact]
        public void Add_UniversalResourceElementWithMultipleStrings_OnlyFirstValueIsUsed()
        {
            var tag = DicomTag.URNCodeValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, "a", "b", "c");
            Assert.IsType<DicomUniversalResource>(dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.GetValues<string>(tag);
            Assert.Single(data);
            Assert.Equal("a", data.First());
        }

        [Fact]
        public void Add_PersonName_MultipleNames_YieldsMultipleValues()
        {
            var tag = DicomTag.PerformingPhysicianName;
            var dataset = new DicomDataset();
            dataset.Add(
                tag,
                "Gustafsson^Anders^L",
                "Yates^Ian",
                "Desouky^Hesham",
                "Horn^Chris");

            var data = dataset.GetValues<string>(tag);
            Assert.Equal(4, data.Length);
            Assert.Equal("Desouky^Hesham", data[2]);
        }

        [Theory]
        [MemberData(nameof(MultiVMStringTags))]
        public void Add_MultiVMStringTags_YieldsMultipleValues(DicomTag tag, string[] values, Type expectedType)
        {
            var dataset = new DicomDataset();
            dataset.Add(tag, values);

            Assert.IsType(expectedType, dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.GetValues<string>(tag);
            Assert.Equal(values.Length, data.Length);
            Assert.Equal(values.Last(), data.Last());
        }

        [Fact]
        public void Get_IntWithoutArgumentTagNonExisting_ShouldThrow()
        {
            var dataset = new DicomDataset();
            var e = Record.Exception(() => dataset.GetSingleValue<int>(DicomTag.MetersetRate));
            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Get_IntWithIntArgumentTagNonExisting_ShouldThrow()
        {
            var dataset = new DicomDataset();
            var e = Record.Exception(() => dataset.GetValue<int>(DicomTag.MetersetRate, 20));
            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Get_NonGenericWithIntArgumentTagNonExisting_ShouldNotThrow()
        {
            var dataset = new DicomDataset();
            var e = Record.Exception(() => Assert.Equal(20, dataset.GetSingleValueOrDefault(DicomTag.MetersetRate, 20)));
            Assert.Null(e);
        }

        [Fact]
        public void Get_IntOutsideRange_ShouldThrow()
        {
            var tag = DicomTag.SelectorISValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, 3, 4, 5);

            var e = Record.Exception(() => dataset.GetValue<int>(tag, 10));
            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Get_NonGenericIntArgumentEmptyElement_ShouldNotThrow()
        {
            var tag = DicomTag.SelectorISValue;
            var dataset = new DicomDataset
            {
                { tag, new int[0] }
            };

            var e = Record.Exception(() => Assert.Equal(10, dataset.GetSingleValueOrDefault(tag, 10)));
            Assert.Null(e);
        }

        [Fact]
        public void Get_NullableReturnType_ReturnsDefinedValue()
        {
            var tag = DicomTag.SelectorULValue;
            const uint expected = 100u;
            var dataset = new DicomDataset { { tag, expected } };

            var actual = dataset.GetSingleValue<uint>(tag);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomSignedShortTest()
        {
            short[] values = new short[] { 5 }; //single Value element
            DicomSignedShort element = new DicomSignedShort(DicomTag.TagAngleSecondAxis, values);

            TestAddElementToDatasetAsString<short>(element, values);

            values = new short[] { 5, 8 }; //multi-value element
            element = new DicomSignedShort(DicomTag.CenterOfCircularExposureControlSensingRegion, values);

            TestAddElementToDatasetAsString<short>(element, values);
        }

        [Fact]
        public void DicomAttributeTagTest()
        {
            var expected = new DicomTag[] { DicomTag.ALinePixelSpacing }; //single value
            DicomElement element = new DicomAttributeTag(DicomTag.DimensionIndexPointer, expected);


            TestAddElementToDatasetAsString<string>(element, expected.Select(n => n.ToString("J", null)).ToArray());

            expected = new DicomTag[] { DicomTag.ALinePixelSpacing, DicomTag.AccessionNumber }; //multi-value
            element = new DicomAttributeTag(DicomTag.FrameIncrementPointer, expected);

            TestAddElementToDatasetAsString(element, expected.Select(n => n.ToString("J", null)).ToArray());
        }

        [Fact]
        public void DicomUnsignedShortTest()
        {
            ushort[] testValues = new ushort[] { 1, 2, 3, 4, 5 };

            var element = new DicomUnsignedShort(DicomTag.ReferencedFrameNumber, testValues);

            TestAddElementToDatasetAsString<ushort>(element, testValues);
        }

        [Fact]
        public void DicomSignedLongTest()
        {
            var testValues = new int[] { 1, 2, 3 };
            var element = new DicomSignedLong(DicomTag.RationalNumeratorValue, testValues);

            TestAddElementToDatasetAsString(element, testValues);
        }

        [Fact]
        public void DicomOtherDoubleTest()
        {
            var testValues = new double[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80 };

            var element = new DicomOtherDouble(DicomTag.DoubleFloatPixelData, testValues);

            TestAddElementToDatasetAsByteBuffer(element, testValues);
        }

        [Fact]
        public void DicomOtherByteTest()
        {
            var testValues = new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80 };

            var element = new DicomOtherByte(DicomTag.PixelData, testValues);

            TestAddElementToDatasetAsByteBuffer(element, testValues);
        }

        [Fact]
        public void Constructor_FromDataset_DataReproduced()
        {
            var ds = new DicomDataset { { DicomTag.PatientID, "1" } };
            var sps1 = new DicomDataset { { DicomTag.ScheduledStationName, "1" } };
            var sps2 = new DicomDataset { { DicomTag.ScheduledStationName, "2" } };
            var spcs1 = new DicomDataset { { DicomTag.ContextIdentifier, "1" } };
            var spcs2 = new DicomDataset { { DicomTag.ContextIdentifier, "2" } };
            var spcs3 = new DicomDataset { { DicomTag.ContextIdentifier, "3" } };
            sps1.Add(new DicomSequence(DicomTag.ScheduledProtocolCodeSequence, spcs1, spcs2));
            sps2.Add(new DicomSequence(DicomTag.ScheduledProtocolCodeSequence, spcs3));
            ds.Add(new DicomSequence(DicomTag.ScheduledProcedureStepSequence, sps1, sps2));

            Assert.Equal("1", ds.GetString(DicomTag.PatientID));
            Assert.Equal(
                "1",
                ds.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetString(DicomTag.ScheduledStationName));
            Assert.Equal(
                "2",
                ds.GetSequence(DicomTag.ScheduledProcedureStepSequence).Items[1].GetString(DicomTag.ScheduledStationName));
            Assert.Equal(
                "1",
                ds.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSequence(
                    DicomTag.ScheduledProtocolCodeSequence).First().GetString(DicomTag.ContextIdentifier));
            Assert.Equal(
                "2",
                ds.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSequence(
                    DicomTag.ScheduledProtocolCodeSequence).Items[1].GetString(DicomTag.ContextIdentifier));
            Assert.Equal(
                "3",
                ds.GetSequence(DicomTag.ScheduledProcedureStepSequence).Items[1].GetSequence(
                    DicomTag.ScheduledProtocolCodeSequence).First().GetString(DicomTag.ContextIdentifier));
        }

        [Fact]
        public void Constructor_FromDataset_SequenceItemsNotLinked()
        {
            var ds = new DicomDataset { { DicomTag.PatientID, "1" } };
            var sps = new DicomDataset { { DicomTag.ScheduledStationName, "1" } };
            var spcs = new DicomDataset { { DicomTag.ContextIdentifier, "1" } };
            sps.Add(new DicomSequence(DicomTag.ScheduledProtocolCodeSequence, spcs));
            ds.Add(new DicomSequence(DicomTag.ScheduledProcedureStepSequence, sps));

            var ds2 = new DicomDataset(ds);
            ds2.AddOrUpdate(DicomTag.PatientID, "2");
            ds2.GetSequence(DicomTag.ScheduledProcedureStepSequence).Items[0].AddOrUpdate(DicomTag.ScheduledStationName, "2");
            ds2.GetSequence(DicomTag.ScheduledProcedureStepSequence).Items[0].GetSequence(
                DicomTag.ScheduledProtocolCodeSequence).Items[0].AddOrUpdate(DicomTag.ContextIdentifier, "2");

            Assert.Equal("1", ds.GetSingleValue<string>(DicomTag.PatientID));
            Assert.Equal(
                "1",
                ds.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetString(DicomTag.ScheduledStationName));
            Assert.Equal(
                "1",
                ds.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSequence(
                    DicomTag.ScheduledProtocolCodeSequence).First().GetString(DicomTag.ContextIdentifier));
        }

        [Fact]
        public void InternalTransferSyntax_Setter_AppliesToAllSequenceDepths()
        {
            var ds = new DicomDataset { { DicomTag.PatientID, "1" } };
            var sps = new DicomDataset { { DicomTag.ScheduledStationName, "1" } };
            var spcs = new DicomDataset { { DicomTag.ContextIdentifier, "1" } };
            sps.Add(new DicomSequence(DicomTag.ScheduledProtocolCodeSequence, spcs));
            ds.Add(new DicomSequence(DicomTag.ScheduledProcedureStepSequence, sps));

            var newSyntax = DicomTransferSyntax.DeflatedExplicitVRLittleEndian;
            ds.InternalTransferSyntax = newSyntax;
            Assert.Equal(newSyntax, ds.InternalTransferSyntax);
            Assert.Equal(
                newSyntax,
                ds.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().InternalTransferSyntax);
            Assert.Equal(
                newSyntax,
                ds.GetSequence(DicomTag.ScheduledProcedureStepSequence).First().GetSequence(
                    DicomTag.ScheduledProtocolCodeSequence).Items[0].InternalTransferSyntax);
        }

        [Fact]
        public void AddOrUpdatePixelData_InternalTransferSyntax_Succeeds()
        {
            var ds        = new DicomDataset ( );
            var data      = new IO.Buffer.MemoryByteBuffer ( new byte[] { 255 } ); //dummy data
            var newSyntax = DicomTransferSyntax.DeflatedExplicitVRLittleEndian;

            ds.AddOrUpdatePixelData ( DicomVR.OB, data );

            Assert.Equal(DicomTransferSyntax.ExplicitVRLittleEndian, ds.InternalTransferSyntax);

            ds.AddOrUpdatePixelData ( DicomVR.OB, data, newSyntax );

            Assert.Equal(newSyntax, ds.InternalTransferSyntax);

        }

        [Fact]
        public void Get_ArrayWhenTagExistsEmpty_ShouldReturnEmptyArray()
        {
            var tag = DicomTag.GridFrameOffsetVector;
            var ds = new DicomDataset();
            ds.Add(tag, (string[])null);

            var array = ds.GetValues<string>(tag);
            Assert.Empty(array);
        }

        [Fact]
        public void Add_PrivateTag_ShouldBeAddedWithCorrectVR()
        {
            var privCreatorDictEntry = new DicomDictionaryEntry(new DicomTag(0x0011, 0x0010), "Private Creator", "PrivateCreator", DicomVM.VM_1, false, DicomVR.LO);
            DicomDictionary.Default.Add(privCreatorDictEntry);

            DicomPrivateCreator privateCreator1 = DicomDictionary.Default.GetPrivateCreator("TESTCREATOR1");
            DicomDictionary privDict1 = DicomDictionary.Default[privateCreator1];

            var dictEntry = new DicomDictionaryEntry(DicomMaskedTag.Parse("0011", "xx10"), "TestPrivTagName", "TestPrivTagKeyword", DicomVM.VM_1, false, DicomVR.CS);
            privDict1.Add(dictEntry);

            var ds = new DicomDataset();
            ds.Add(dictEntry.Tag, "VAL1");
            Assert.Equal(DicomVR.CS, ds.GetDicomItem<DicomItem>(dictEntry.Tag).ValueRepresentation);
        }

        /// <summary>
        /// Associated with Github issue #535.
        /// </summary>
        [Theory]
        [InlineData(0x0016, 0x1106, 0x1053)]
        [InlineData(0x0016, 0x1053, 0x1006)]
        public void Add_RegularTags_ShouldBeSortedInGroupElementOrder(ushort group, ushort hiElem, ushort loElem)
        {
            var dataset = new DicomDataset();
            dataset.Add(new DicomTag(group, hiElem), 2);
            dataset.Add(new DicomTag(group, loElem), 1);

            var firstElem = dataset.First().Tag.Element;
            Assert.Equal(loElem, firstElem);
        }

        /// <summary>
        /// Associated with Github issue #535.
        /// </summary>
        [Theory]
        [InlineData(0x0019, 0x1153, 0x1006, "", "PRIVATE")]
        [InlineData(0x0019, 0x1053, 0x1006, "PRIVATE", "PRIVATE")]
        [InlineData(0x0019, 0x1106, 0x1053, "PRIVATE", "PRIVATE")]
        [InlineData(0x0019, 0x1106, 0x1053, "PRIVATE", "")]
        [InlineData(0x0019, 0x1053, 0x1006, "ALSOPRIVATE", "PRIVATE")]
        [InlineData(0x0019, 0x1006, 0x1006, "PRIVATE", "ALSOPRIVATE")]
        public void Add_PrivateTags_ShouldBeSortedInGroupByteElementCreatorOrder(ushort group, ushort hiElem,
            ushort loElem, string hiCreator, string loCreator)
        {
            var dataset = new DicomDataset();
            dataset.Add(new DicomTag(group, hiElem, hiCreator), 2);
            dataset.Add(new DicomTag(group, loElem, loCreator), 1);

            var firstElem = dataset.First().Tag.Element;
            var firstCreator = dataset.First().Tag.PrivateCreator.Creator;
            Assert.Equal(loElem, firstElem);
            Assert.Equal(loCreator, firstCreator);
        }

        [Fact]
        public void Add_DicomItemOnNonExistingPrivateTag_PrivateGroupShouldCorrespondToPrivateCreator()
        {
            var dataset = new DicomDataset();

            var tag1 = new DicomTag(0x3001, 0x08, "PRIVATE");
            var tag2 = new DicomTag(0x3001, 0x12, "PRIVATE");
            var tag3 = new DicomTag(0x3001, 0x08, "ALSOPRIVATE");

            // By using the .Add(DicomTag, ...) method, private tags get automatically updated so that a private
            // creator group number is generated (if private creator is new) and inserted into the tag element.
            dataset.Add(tag1, 1);
            dataset.Add(tag2, 3.14);

            // Should confirm that element of the tag is not updated to include the private creator group number.
            dataset.Add(new DicomIntegerString(tag3, 50));

            var tag3Private = dataset.GetPrivateTag(tag3);
            var contained = dataset.SingleOrDefault(item => item.Tag.Group == tag3Private.Group &&
                                                        item.Tag.Element == tag3Private.Element);
            Assert.NotNull(contained);

            var fifthItem = dataset.ElementAt(4);
            Assert.Equal(fifthItem, contained);
        }

        [Fact]
        public void AddOrUpdate_DicomItemOnExistingPrivateTag_PrivateGroupShouldCorrespondToPrivateCreator()
        {
            var dataset = new DicomDataset();

            var tag1 = new DicomTag(0x3001, 0x08, "PRIVATE");
            var tag2 = new DicomTag(0x3001, 0x12, "PRIVATE");
            var tag3 = new DicomTag(0x3001, 0x08, "ALSOPRIVATE");

            // By using the .Add(DicomTag, ...) method, private tags get automatically updated so that a private
            // creator group number is generated (if private creator is new) and inserted into the tag element.
            dataset.Add(tag1, 1);
            dataset.Add(tag2, 3.14);
            dataset.Add(tag3, "COOL");

            var tag1Private = dataset.GetPrivateTag(tag1);
            var contained = dataset.SingleOrDefault(item => item.Tag.Group == tag1Private.Group &&
                                                            item.Tag.Element == tag1Private.Element);
            Assert.NotNull(contained);

            // Should confirm that element of the tag is not updated to include the private creator group number.
            dataset.AddOrUpdate(new DicomIntegerString(tag1, 50));

            contained = dataset.SingleOrDefault(item => item.Tag.Group == tag1Private.Group &&
                                                        item.Tag.Element == tag1Private.Element);
            Assert.NotNull(contained);

            var thirdItem = dataset.ElementAt(2);
            Assert.Equal(thirdItem, contained);
        }

        [Fact]
        public void Get_ByteArrayFromStringElement_ReturnsValidArray()
        {
            var encoding = Encoding.GetEncoding("SHIFT_JIS");
            var tag = DicomTag.AdditionalPatientHistory;
            const string expected = "YamadaTarou山田太郎ﾔﾏﾀﾞﾀﾛｳ";

            var dataset = new DicomDataset
            {
                new DicomLongText(tag, encoding, expected)
            };

            var actual = encoding.GetString(dataset.GetDicomItem<DicomElement>(tag).Buffer.Data);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddOrUpdate_NonDefaultEncodedStringElement_StringIsPreserved()
        {
            var encoding = Encoding.GetEncoding("SHIFT_JIS");
            var tag = DicomTag.AdditionalPatientHistory;
            const string expected = "YamadaTarou山田太郎ﾔﾏﾀﾞﾀﾛｳ";

            var dataset = new DicomDataset();
            dataset.AddOrUpdate(tag, encoding, expected);

            var actual = encoding.GetString(dataset.GetDicomItem<DicomElement>(tag).Buffer.Data);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryGetValue_MustNotThrowOnVRViolation()
        {
            //  related #746
            var dataset = new DicomDataset(
                new DicomItem[] {
                new DicomIntegerString(
                    DicomTag.SeriesNumber,
                    new MemoryByteBuffer(
#if NETSTANDARD
                        Encoding.GetEncoding(0).GetBytes("1.0")
#else
                        Encoding.Default.GetBytes("1.0")
#endif
                    )
                ) },
                false // do not validate, since the VR violation is intended.
            );
            Assert.False(dataset.TryGetValue(DicomTag.SeriesNumber, 0, out int _));
        }

        [Fact]
        public void TryGetValues_MustNotThrowOnVRViolation()
        {
            //  related #746
            var dataset = new DicomDataset(
                new DicomItem[] {
                new DicomIntegerString(
                    DicomTag.SeriesNumber,
                    new MemoryByteBuffer(
#if NETSTANDARD
                        Encoding.GetEncoding(0).GetBytes("1.0")
#else
                        Encoding.Default.GetBytes("1.0")
#endif
                    )
                ) },
                false // do not validate, since the VR violation is intended.
            );
            Assert.False(dataset.TryGetValues(DicomTag.SeriesNumber, out int[] _));
        }

        [Fact]
        public void TryGetSingleValue_MustNotThrowOnVRViolation()
        {
            //  #746
            var dataset = new DicomDataset(
                new DicomItem[] {
                new DicomIntegerString(
                    DicomTag.SeriesNumber,
                    new MemoryByteBuffer(
#if NETSTANDARD
                        Encoding.GetEncoding(0).GetBytes("1.0")
#else
                        Encoding.Default.GetBytes("1.0")
#endif
                    )
                ) },
                false // do not validate, since the VR violation is intended.
            );
            Assert.False(dataset.TryGetSingleValue(DicomTag.SeriesNumber, out int _));
        }

        #endregion

        #region Support methods

        private void TestAddElementToDatasetAsString<T>(DicomElement element, T[] testValues)
        {
            DicomDataset ds = new DicomDataset();
            string[] stringValues;


            if (typeof(T) == typeof(string))
            {
                stringValues = testValues.Cast<string>().ToArray();
            }
            else
            {
                stringValues = testValues.Select(x => x.ToString()).ToArray();
            }


            ds.AddOrUpdate(element.Tag, stringValues);


            for (int index = 0; index < element.Count; index++)
            {
                string val;

                val = GetStringValue(element, ds, index);

                Assert.Equal(stringValues[index], val);
            }

            if (element.Tag.DictionaryEntry.ValueMultiplicity.Maximum > 1)
            {
                var stringValue = string.Join("\\", testValues);

                ds.AddOrUpdate(element.Tag, stringValue);

                for (int index = 0; index < element.Count; index++)
                {
                    string val;

                    val = GetStringValue(element, ds, index);

                    Assert.Equal(stringValues[index], val);
                }
            }
        }

        private string GetStringValue(DicomElement element, DicomDataset ds, int index)
        {
            string val;


            if (element.ValueRepresentation == DicomVR.AT)
            {
                //Should this be a updated in the AT DicomTag?
                val = GetATElementValue(element, ds, index);
            }
            else
            {
                val = ds.GetValue<string>(element.Tag, index);
            }

            return val;
        }

        private static string GetATElementValue(DicomElement element, DicomDataset ds, int index)
        {
            var atElement = ds.GetDicomItem<DicomElement>(element.Tag);

            var testValue = atElement.Get<DicomTag>(index);

            return testValue.ToString("J", null);
        }

        private static void TestAddElementToDatasetAsByteBuffer<T>(DicomElement element, T[] testValues)
        {
            DicomDataset ds = new DicomDataset();


            ds.Add(element.Tag, element.Buffer);

            for (int index = 0; index < testValues.Count(); index++)
            {
                Assert.Equal(testValues[index], ds.GetValue<T>(element.Tag, index));
            }
        }

        #endregion

        #region Support data

        public static IEnumerable<object[]> MultiVMStringTags
        {
            get
            {
                yield return
                    new object[]
                        {
                            DicomTag.ReferencedFrameNumber, new[] { "3", "5", "8" },
                            typeof(DicomIntegerString)
                        };
                yield return
                    new object[]
                        {
                            DicomTag.EventElapsedTimes, new[] { "3.2", "5.8", "8.7" },
                            typeof(DicomDecimalString)
                        };
                yield return
                new object[]
                        {
                            DicomTag.PatientTelephoneNumbers, new[] { "0271-22117", "070-669 5073", "0270-11204" },
                            typeof(DicomShortString)
                        };
                yield return
                new object[]
                        {
                            DicomTag.EventTimerNames, new[] { "a", "b", "c", "e", "f" },
                            typeof(DicomLongString)
                        };
                yield return
                new object[]
                        {
                            DicomTag.ConsultingPhysicianName, new[] { "a", "b", "c", "e", "f" },
                            typeof(DicomPersonName)
                        };
                yield return
                new object[]
                        {
                            DicomTag.SOPClassesSupported, new[] { "1.2.3", "4.5.6", "7.8.8.9" },
                            typeof(DicomUniqueIdentifier)
                        };
            }
        }

        #endregion
    }
}
