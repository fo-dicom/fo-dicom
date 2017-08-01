// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

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
            Assert.Equal(3, dataset.Get<double[]>(tag).Length);
        }

        [Fact]
        public void Add_UnlimitedCharactersElement_Succeeds()
        {
            var tag = DicomTag.LongCodeValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, "abc");
            Assert.IsType<DicomUnlimitedCharacters>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal("abc", dataset.Get<string>(tag));
        }

        [Fact]
        public void Add_UnlimitedCharactersElementWithMultipleStrings_Succeeds()
        {
            var tag = DicomTag.LongCodeValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, "a", "b", "c");
            Assert.IsType<DicomUnlimitedCharacters>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal("c", dataset.Get<string>(tag, 2));
        }

        [Fact]
        public void Add_UniversalResourceElement_Succeeds()
        {
            var tag = DicomTag.URNCodeValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, "abc");
            Assert.IsType<DicomUniversalResource>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal("abc", dataset.Get<string>(tag));
        }

        [Fact]
        public void Add_UniversalResourceElementWithMultipleStrings_OnlyFirstValueIsUsed()
        {
            var tag = DicomTag.URNCodeValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, "a", "b", "c");
            Assert.IsType<DicomUniversalResource>(dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.Get<string[]>(tag);
            Assert.Equal(1, data.Length);
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

            var data = dataset.Get<string[]>(tag);
            Assert.Equal(4, data.Length);
            Assert.Equal("Desouky^Hesham", data[2]);
        }

        [Theory]
        [MemberData("MultiVMStringTags")]
        public void Add_MultiVMStringTags_YieldsMultipleValues(DicomTag tag, string[] values, Type expectedType)
        {
            var dataset = new DicomDataset();
            dataset.Add(tag, values);

            Assert.IsType(expectedType, dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.Get<string[]>(tag);
            Assert.Equal(values.Length, data.Length);
            Assert.Equal(values.Last(), data.Last());
        }

        [Fact]
        public void Get_IntWithoutArgumentTagNonExisting_ShouldThrow()
        {
            var dataset = new DicomDataset();
            var e = Record.Exception(() => dataset.Get<int>(DicomTag.MetersetRate));
            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Get_IntWithIntArgumentTagNonExisting_ShouldThrow()
        {
            var dataset = new DicomDataset();
            var e = Record.Exception(() => dataset.Get<int>(DicomTag.MetersetRate, 20));
            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Get_NonGenericWithIntArgumentTagNonExisting_ShouldNotThrow()
        {
            var dataset = new DicomDataset();
            var e = Record.Exception(() => Assert.Equal(20, dataset.Get(DicomTag.MetersetRate, 20)));
            Assert.Null(e);
        }

        [Fact]
        public void Get_IntOutsideRange_ShouldThrow()
        {
            var tag = DicomTag.SelectorISValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, 3, 4, 5);

            var e = Record.Exception(() => dataset.Get<int>(tag, 10));
            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Get_NonGenericIntArgumentEmptyElement_ShouldNotThrow()
        {
            var tag = DicomTag.SelectorISValue;
            var dataset = new DicomDataset();
            dataset.Add(tag, new int[0]);

            var e = Record.Exception(() => Assert.Equal(10, dataset.Get(tag, 10)));
            Assert.Null(e);
        }

        [Fact]
        public void Get_NullableReturnType_ReturnsDefinedValue()
        {
            var tag = DicomTag.SelectorULValue;
            const uint expected = 100u;
            var dataset = new DicomDataset { { tag, expected } };

            var actual = dataset.Get<uint?>(tag).Value;
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

            var element = new DicomUnsignedShort(DicomTag.ReferencedFrameNumbers, testValues);

            TestAddElementToDatasetAsString<ushort>(element, testValues);
        }

        [Fact]
        public void DicomSignedLongTest()
        {
            var testValues = new int[] { 0, 1, 2 };
            var element = new DicomSignedLong(DicomTag.ReferencePixelX0, testValues);

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

            Assert.Equal("1", ds.Get<string>(DicomTag.PatientID));
            Assert.Equal(
                "1",
                ds.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).Items[0].Get<string>(
                    DicomTag.ScheduledStationName));
            Assert.Equal(
                "2",
                ds.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).Items[1].Get<string>(
                    DicomTag.ScheduledStationName));
            Assert.Equal(
                "1",
                ds.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).Items[0].Get<DicomSequence>(
                    DicomTag.ScheduledProtocolCodeSequence).Items[0].Get<string>(DicomTag.ContextIdentifier));
            Assert.Equal(
                "2",
                ds.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).Items[0].Get<DicomSequence>(
                    DicomTag.ScheduledProtocolCodeSequence).Items[1].Get<string>(DicomTag.ContextIdentifier));
            Assert.Equal(
                "3",
                ds.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).Items[1].Get<DicomSequence>(
                    DicomTag.ScheduledProtocolCodeSequence).Items[0].Get<string>(DicomTag.ContextIdentifier));
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
            ds2.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).Items[0].AddOrUpdate(DicomTag.ScheduledStationName, "2");
            ds2.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).Items[0].Get<DicomSequence>(
                DicomTag.ScheduledProtocolCodeSequence).Items[0].AddOrUpdate(DicomTag.ContextIdentifier, "2");

            Assert.Equal("1", ds.Get<string>(DicomTag.PatientID));
            Assert.Equal(
                "1",
                ds.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).Items[0].Get<string>(
                    DicomTag.ScheduledStationName));
            Assert.Equal(
                "1",
                ds.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).Items[0].Get<DicomSequence>(
                    DicomTag.ScheduledProtocolCodeSequence).Items[0].Get<string>(DicomTag.ContextIdentifier));
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
                ds.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).Items[0].InternalTransferSyntax);
            Assert.Equal(
                newSyntax,
                ds.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).Items[0].Get<DicomSequence>(
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

            var array = ds.Get<string[]>(tag);
            Assert.Equal(0, array.Length);
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
            Assert.Equal(DicomVR.CS, ds.Get<DicomVR>(dictEntry.Tag));
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
                val = ds.Get<string>(element.Tag, index);
            }

            return val;
        }

        private static string GetATElementValue(DicomElement element, DicomDataset ds, int index)
        {
            var atElement = ds.Get<DicomElement>(element.Tag, null);

            var testValue = atElement.Get<DicomTag>(index);

            return testValue.ToString("J", null);
        }

        private static void TestAddElementToDatasetAsByteBuffer<T>(DicomElement element, T[] testValues)
        {
            DicomDataset ds = new DicomDataset();


            ds.Add(element.Tag, element.Buffer);

            for (int index = 0; index < testValues.Count(); index++)
            {
                Assert.Equal(testValues[index], ds.Get<T>(element.Tag, index));
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
