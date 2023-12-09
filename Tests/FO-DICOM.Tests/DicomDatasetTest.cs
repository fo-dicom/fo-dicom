// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.IO.Buffer;
using FellowOakDicom.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace FellowOakDicom.Tests
{

    [Collection(TestCollections.General)]
    public class DicomDatasetTest
    {
        #region Unit tests

        [Fact]
        public void Add_OtherDoubleElement_Succeeds()
        {
            var tag = DicomTag.DoubleFloatPixelData;
            var dataset = new DicomDataset
            {
                { tag, 3.45 }
            };
            Assert.IsType<DicomOtherDouble>(dataset.First(item => item.Tag.Equals(tag)));
        }

        [Fact]
        public void Add_OtherDoubleElementWithMultipleDoubles_Succeeds()
        {
            var tag = DicomTag.DoubleFloatPixelData;
            var dataset = new DicomDataset
            {
                { tag, 3.45, 6.78, 9.01 }
            };
            Assert.IsType<DicomOtherDouble>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal(3, dataset.GetValues<double>(tag).Length);
        }

        [Fact]
        public void Add_UnlimitedCharactersElement_Succeeds()
        {
            var tag = DicomTag.LongCodeValue;
            var dataset = new DicomDataset
            {
                { tag, "abc" }
            };
            Assert.IsType<DicomUnlimitedCharacters>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal("abc", dataset.GetSingleValue<string>(tag));
        }

        [Fact]
        public void Add_UnlimitedCharactersElementWithMultipleStrings_Succeeds()
        {
            var tag = DicomTag.LongCodeValue;
            var dataset = new DicomDataset
            {
                { tag, "a", "b", "c" }
            };
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
            Assert.Single(data, string.IsNullOrEmpty);
        }

        [Fact]
        public void Add_ShortTextElementWithEmptyValues_Succeeds()
        {
            var tag = DicomTag.SelectorSTValue;
            var dataset = new DicomDataset();
            dataset.Add<string>(tag);
            Assert.IsType<DicomShortText>(dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.GetValues<string>(tag);
            Assert.Single(data, string.IsNullOrEmpty);
        }

        [Fact]
        public void Add_LongTextElementWithEmptyValues_Succeeds()
        {
            var tag = DicomTag.SelectorLTValue;
            var dataset = new DicomDataset();
            dataset.Add<string>(tag);
            Assert.IsType<DicomLongText>(dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.GetValues<string>(tag);
            Assert.Single(data, string.IsNullOrEmpty);
        }

        [Fact]
        public void Add_UniversalResourceElementWithEmptyValues_Succeeds()
        {
            var tag = DicomTag.URNCodeValue;
            var dataset = new DicomDataset();
            dataset.Add<string>(tag);
            Assert.IsType<DicomUniversalResource>(dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.GetValues<string>(tag);
            Assert.Single(data, string.IsNullOrEmpty);
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
            var dataset = new DicomDataset
            {
                { tag, "abc" }
            };
            Assert.IsType<DicomUniversalResource>(dataset.First(item => item.Tag.Equals(tag)));
            Assert.Equal("abc", dataset.GetSingleValue<string>(tag));
        }

        [Fact]
        public void Add_UniversalResourceElementWithMultipleStrings_OnlyFirstValueIsUsed()
        {
            var tag = DicomTag.URNCodeValue;
            var dataset = new DicomDataset
            {
                { tag, "a", "b", "c" }
            };
            Assert.IsType<DicomUniversalResource>(dataset.First(item => item.Tag.Equals(tag)));

            var data = dataset.GetValues<string>(tag);
            Assert.Single(data);
            Assert.Equal("a", data.First());
        }

        [Fact]
        public void Add_PersonName_MultipleNames_YieldsMultipleValues()
        {
            var tag = DicomTag.PerformingPhysicianName;
            var dataset = new DicomDataset
            {
                {
                    tag,
                    "Gustafsson^Anders^L",
                    "Yates^Ian",
                    "Desouky^Hesham",
                    "Horn^Chris"
                }
            };

            var data = dataset.GetValues<string>(tag);
            Assert.Equal(4, data.Length);
            Assert.Equal("Desouky^Hesham", data[2]);
        }

        [Theory]
        [MemberData(nameof(MultiVMStringTags))]
        public void Add_MultiVMStringTags_YieldsMultipleValues(DicomTag tag, string[] values, Type expectedType)
        {
            var dataset = new DicomDataset
            {
                { tag, values }
            };

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
            var dataset = new DicomDataset
            {
                { tag, 3, 4, 5 }
            };

            var e = Record.Exception(() => dataset.GetValue<int>(tag, 10));
            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Get_NonGenericIntArgumentEmptyElement_ShouldNotThrow()
        {
            var tag = DicomTag.SelectorISValue;
            var dataset = new DicomDataset
            {
                { tag, Array.Empty<int>() }
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
            var element = new DicomSignedShort(DicomTag.TagAngleSecondAxis, values);

            TestAddElementToDatasetAsString(element, values);

            values = new short[] { 5, 8 }; //multi-value element
            element = new DicomSignedShort(DicomTag.CenterOfCircularExposureControlSensingRegion, values);

            Assert.True(TestAddElementToDatasetAsString(element, values));
        }

        [Fact]
        public void DicomAttributeTagTest()
        {
            var expected = new DicomTag[] { DicomTag.ALinePixelSpacing }; //single value
            DicomElement element = new DicomAttributeTag(DicomTag.DimensionIndexPointer, expected);

            TestAddElementToDatasetAsString(element, expected.Select(n => n.ToString("J", null)).ToArray());

            expected = new DicomTag[] { DicomTag.ALinePixelSpacing, DicomTag.AccessionNumber }; //multi-value
            element = new DicomAttributeTag(DicomTag.FrameIncrementPointer, expected);

            Assert.True(TestAddElementToDatasetAsString(element, expected.Select(n => n.ToString("J", null)).ToArray()));
        }

        [Fact]
        public void DicomUnsignedShortTest()
        {
            ushort[] testValues = new ushort[] { 1, 2, 3, 4, 5 };

            var element = new DicomUnsignedShort(DicomTag.ReferencedFrameNumber, testValues);

            Assert.True(TestAddElementToDatasetAsString(element, testValues));
        }

        [Fact]
        public void DicomSignedLongTest()
        {
            var testValues = new int[] { 1, 2, 3 };
            var element = new DicomSignedLong(DicomTag.RationalNumeratorValue, testValues);

            Assert.True(TestAddElementToDatasetAsString(element, testValues));
        }

        [Fact]
        public void DicomOtherDoubleTest()
        {
            var testValues = new double[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80 };

            var element = new DicomOtherDouble(DicomTag.DoubleFloatPixelData, testValues);

            Assert.True(TestAddElementToDatasetAsByteBuffer(element, testValues));
        }

        [Fact]
        public void DicomOtherByteTest()
        {
            var testValues = new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80 };

            var element = new DicomOtherByte(DicomTag.PixelData, testValues);

            Assert.True(TestAddElementToDatasetAsByteBuffer(element, testValues));
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
            var ds = new DicomDataset();
            var data = new MemoryByteBuffer(new byte[] { 255 }); //dummy data

            ds.AddOrUpdate(DicomTag.BitsAllocated, (ushort)8);
            var pixelData = DicomPixelData.Create(ds, true);
            pixelData.AddFrame(data);

            Assert.Equal(DicomTransferSyntax.ExplicitVRLittleEndian, ds.InternalTransferSyntax);
        }

        [Fact]
        public void Get_ArrayWhenTagExistsEmpty_ShouldReturnEmptyArray()
        {
            var tag = DicomTag.GridFrameOffsetVector;
            var ds = new DicomDataset
            {
                { tag, (string[])null }
            };

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

            var ds = new DicomDataset
            {
                {DicomVR.CS, dictEntry.Tag, "VAL1" }
            };
            Assert.Equal(DicomVR.CS, ds.GetDicomItem<DicomItem>(dictEntry.Tag).ValueRepresentation);
        }

        /// <summary>
        /// Associated with Github issue #1454
        /// </summary>
        [Theory]
        [InlineData(0.142916000000001)]
        [InlineData(0.142916)]
        [InlineData(12345678901234.1)]
        [InlineData(123456789112345.6)]
        [InlineData(1234567891123456)]
        [InlineData(1234567891123456789)]
        [InlineData(123456789112345)]
        [InlineData(0.123456789112345)]
        [InlineData(0.12345678911234)]
        [InlineData(0.1234567891123)]
        [InlineData(5.23e-16)]
        public void Add_AnyDecimalValue_IsStoredWithExpectedPrecision(decimal value)
        {
            var tag = DicomTag.MaterialThickness;
            var negativeTag = DicomTag.TableTopLateralPosition;
            DicomDataset dataSet = null;
            var exception = Record.Exception(() =>
            {
                dataSet = new DicomDataset
                {
                    { tag, value },
                    { negativeTag, -value }
                };
            });

            // Assert
            Assert.Null(exception);

            var actualValue = dataSet.GetSingleValue<decimal>(tag);
            var actualNegativeValue = dataSet.GetSingleValue<decimal>(negativeTag);
            var expectedDelta = 1E-10m * Math.Abs(value);
            var comparer = new DecimalDeltaComparer(expectedDelta);
            Assert.Equal(value, actualValue, comparer);
            Assert.Equal(-value, actualNegativeValue, comparer);
        }

        /// <summary>
        /// Associated with Github issue #535.
        /// </summary>
        [Theory]
        [InlineData(0x0016, 0x0018, 0x000F)]
        [InlineData(0x0016, 0x000F, 0x000E)]
        public void Add_RegularTags_ShouldBeSortedInGroupElementOrder(ushort group, ushort hiElem, ushort loElem)
        {
            var dataset = new DicomDataset
            {
                { new DicomTag(group, hiElem), 2 },
                { new DicomTag(group, loElem), 1 }
            };

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
            var dataset = new DicomDataset
            {
                { DicomVR.IS, new DicomTag(group, hiElem, hiCreator), 2 },
                { DicomVR.IS, new DicomTag(group, loElem, loCreator), 1 }
            };

            var firstElem = dataset.First().Tag.Element;
            var firstCreator = dataset.First().Tag.PrivateCreator.Creator;
            Assert.Equal(loElem, firstElem);
            Assert.Equal(loCreator, firstCreator);
        }

        /// <summary>
        /// Associated with Github issue #1059
        /// </summary>
        [Theory]
        [InlineData(0x0019, 0x1000, "PRIVATE", 0x1000)] // lowest possible element (not used for group length encoding)
        [InlineData(0x0019, 0xff, "PRIVATE", 0x10ff)]
        [InlineData(0x0019, 0xffff, "PRIVATE", 0xffff)] // highest possible element
        public void Add_PrivateTags_LowestAndHighestPossibleElementsShouldBeAdded(ushort group, ushort element, string creator, ushort expectedElement)
        {
            var tag1Private = new DicomTag(group, element, creator);

            var dataset = new DicomDataset
            {
                { DicomVR.IS, tag1Private, 1 }
            };

            var item1 = dataset.SingleOrDefault(item => item.Tag.Group == tag1Private.Group &&
                                            item.Tag.Element == expectedElement);
            Assert.NotNull(item1);
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
            dataset.Add(DicomVR.IS, tag1, 1);
            dataset.Add(DicomVR.FD, tag2, 3.14);

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
            dataset.Add(DicomVR.IS, tag1, 1);
            dataset.Add(DicomVR.FD, tag2, 3.14);
            dataset.Add(DicomVR.LO, tag3, "COOL");

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
        public void Add_PrivateTagWithoutExplicitVR_ShouldThrow()
        {
            var dataset = new DicomDataset();

            var privateTag = new DicomTag(0x3001, 0x08, "PRIVATE");

            var e = Record.Exception(() => dataset.Add<string>(privateTag, "FO-DICOM"));
            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void Add_UnknownPrivateTagWithExplicitVR_ShouldBeAdded()
        {
            var dataset = new DicomDataset();

            var privateTag = new DicomTag(0x3001, 0x08);

            dataset.Add<string>(DicomVR.LO, privateTag, "FO-DICOM");

            Assert.Equal(privateTag, dataset.GetDicomItem<DicomItem>(privateTag).Tag);
        }

        [Fact]
        public void Add_KnownPrivateTagWithoutExplicitVR_ShouldBeAdded()
        {
            var dataset = new DicomDataset();

            // <tag group="0019" element="100d" vr="DS" vm="1">AP Offcenter</tag> is known private tag
            var privateTag = new DicomTag(0x0019, 0x100a,"PHILIPS MR/PART");

            dataset.Add<int>(privateTag,1);

            Assert.Equal(privateTag, dataset.GetDicomItem<DicomItem>(privateTag).Tag);
        }

        [Fact]
        public void AddOrUpdate_PrivateTagWithoutExplicitVR_ShouldThrow()
        {
            var dataset = new DicomDataset();

            var privateTag = new DicomTag(0x3001, 0x08, "PRIVATE");

            var e = Record.Exception(() => dataset.AddOrUpdate<string>(privateTag, "FO-DICOM"));
            Assert.IsType<DicomDataException>(e);
        }

        [Fact]
        public void AddOrUpdate_UnknownPrivateTagWithExplicitVR_ShouldBeAdded()
        {
            var dataset = new DicomDataset();

            var privateTag = new DicomTag(0x3001, 0x08);

            dataset.AddOrUpdate<string>(DicomVR.LO, privateTag, "FO-DICOM");

            Assert.Equal(privateTag, dataset.GetDicomItem<DicomItem>(privateTag).Tag);
        }

        [Fact]
        public void AddOrUpdate_KnownPrivateTagWithoutExplicitVR_ShouldBeAdded()
        {
            var dataset = new DicomDataset();

            // <tag group="0019" element="100d" vr="DS" vm="1">AP Offcenter</tag> is known private tag
            var privateTag = new DicomTag(0x0019, 0x100a,"PHILIPS MR/PART");

            dataset.AddOrUpdate<int>(privateTag,1);

            Assert.Equal(privateTag, dataset.GetDicomItem<DicomItem>(privateTag).Tag);
        }


        [Fact]
        public void Get_ByteArrayFromStringElement_ReturnsValidArray()
        {
            // now the actual unit-test
            var encoding = Encoding.GetEncoding("SHIFT_JIS");
            var tag = DicomTag.AdditionalPatientHistory;
            const string expected = "YamadaTarou山田太郎ﾔﾏﾀﾞﾀﾛｳ";

            var dataset = new DicomDataset
            {
                new DicomLongText(tag, expected)
            };
            dataset.AddOrUpdate(DicomTag.SpecificCharacterSet, DicomEncoding.GetCharset(encoding));

            // simulate some rendering into stream (file, network,..)
            dataset.OnBeforeSerializing();

            var actual = encoding.GetString(dataset.GetDicomItem<DicomElement>(tag).Buffer.Data);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DicomEncoding_AppliedToNestedDatasetsOnWriting()
        {
            // now the actual unit-test
            var encoding = Encoding.GetEncoding("SHIFT_JIS");
            var tag = DicomTag.AdditionalPatientHistory;
            const string expected = "YamadaTarou山田太郎ﾔﾏﾀﾞﾀﾛｳ";

            var dataset = new DicomDataset
            {
                { DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage },
                { DicomTag.SOPInstanceUID, DicomUIDGenerator.GenerateDerivedFromUUID() },
                { tag, "SuperDataset" },
                { DicomTag.SpecificCharacterSet, DicomEncoding.GetCharset(encoding) },
                { DicomTag.ReferencedInstanceSequence,
                    new DicomDataset
                    {
                        { tag, expected }
                    }
                }
            };

            var memStream = new MemoryStream();
            new DicomFile(dataset).Save(memStream);
            memStream.Position = 0;

            var bytesOfStream = memStream.ToArray();
            var expectedBytes = encoding.GetBytes(expected);
            Assert.True(bytesOfStream.ContainsSequence(expectedBytes));

            var notexpectedBytes = DicomEncoding.Default.GetBytes(expected);
            Assert.False(bytesOfStream.ContainsSequence(notexpectedBytes));

            memStream.Position = 0;
            var readDataset = DicomFile.Open(memStream);
            var readValue = readDataset.Dataset.GetSequence(DicomTag.ReferencedInstanceSequence).First().GetSingleValue<string>(tag);
            Assert.Equal(expected, readValue);
        }

        [Fact]
        public void AccessingAttributes_InEmptyPersonNameTag_IsPossible()
        {
            var dataset = new DicomDataset();
            dataset.AddOrUpdate<DicomDataset>(DicomTag.ReferringPhysicianName, null);
            var patientItem = dataset.GetDicomItem<DicomPersonName>(DicomTag.ReferringPhysicianName);

            Assert.Equal("", patientItem.First);
            Assert.Equal("", patientItem.Last);
            Assert.Equal("", patientItem.Middle);
            Assert.Equal("", patientItem.Prefix);
            Assert.Equal("", patientItem.Suffix);
        }

        [Fact]
        public void DicomEncoding_AppliedToMultipleNestedDatasetsWithDifferentEncodingsOnWriting()
        {
            // now the actual unit-test
            var encoding1 = Encoding.GetEncoding("SHIFT_JIS");
            var encoding2 = Encoding.UTF8;
            var tag = DicomTag.AdditionalPatientHistory;
            const string expected = "YamadaTarou山田太郎ﾔﾏﾀﾞﾀﾛｳ";

            var dataset = new DicomDataset
            {
                { DicomTag.SOPClassUID, DicomUID.SecondaryCaptureImageStorage },
                { DicomTag.SOPInstanceUID, DicomUIDGenerator.GenerateDerivedFromUUID() },
                { tag, "SuperDataset" },
                { DicomTag.SpecificCharacterSet, DicomEncoding.GetCharset(encoding1) },
                { DicomTag.ReferencedInstanceSequence,
                    new DicomDataset
                    {
                        { DicomTag.SpecificCharacterSet, DicomEncoding.GetCharset(encoding2) },
                        { tag, expected } // this value is supposed to be encoded in UTF8
                    },
                    new DicomDataset
                    {
                        { tag, expected } // this value is supposed to be encoded like the parent dataset in SHIFT_JIS
                    }
                }
            };

            var memStream = new MemoryStream();
            new DicomFile(dataset).Save(memStream);
            memStream.Position = 0;

            var bytesOfStream = memStream.ToArray();

            // there should be the name encoded in SHIFT_JIS
            var expectedBytes1 = encoding1.GetBytes(expected);
            Assert.True(bytesOfStream.ContainsSequence(expectedBytes1));

            // there should also be the name encoded in UTF8
            var expectedBytes2 = encoding2.GetBytes(expected);
            Assert.True(bytesOfStream.ContainsSequence(expectedBytes2));

            // but there should never be a fallback to default-encoding
            var notexpectedBytes = DicomEncoding.Default.GetBytes(expected);
            Assert.False(bytesOfStream.ContainsSequence(notexpectedBytes));

            memStream.Position = 0;
            var readDataset = DicomFile.Open(memStream);
            var readValue1 = readDataset.Dataset.GetSequence(DicomTag.ReferencedInstanceSequence).First().GetSingleValue<string>(tag);
            Assert.Equal(expected, readValue1);
            var readValue2 = readDataset.Dataset.GetSequence(DicomTag.ReferencedInstanceSequence).Last().GetSingleValue<string>(tag);
            Assert.Equal(expected, readValue2);
        }

        [Fact]
        public void AddOrUpdate_NonDefaultEncodedStringElement_StringIsPreserved()
        {
            var encoding = Encoding.GetEncoding("SHIFT_JIS");
            var tag = DicomTag.AdditionalPatientHistory;
            const string expected = "YamadaTarou山田太郎ﾔﾏﾀﾞﾀﾛｳ";

            var dataset = new DicomDataset();
            dataset.AddOrUpdate(DicomTag.SpecificCharacterSet, DicomEncoding.GetCharset(encoding));
            dataset.AddOrUpdate(tag, expected);

            // simulate some kind of serialization, so that the buffer data is created correctly
            dataset.OnBeforeSerializing();

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
                        Encoding.GetEncoding(0).GetBytes("1.1")
#else
                        Encoding.Default.GetBytes("1.1")
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
                        Encoding.GetEncoding(0).GetBytes("1.1")
#else
                        Encoding.Default.GetBytes("1.1")
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
                        Encoding.GetEncoding(0).GetBytes("1.1")
#else
                        Encoding.Default.GetBytes("1.1")
#endif
                    )
                ) },
                false // do not validate, since the VR violation is intended.
            );
            Assert.False(dataset.TryGetSingleValue(DicomTag.SeriesNumber, out int _));
        }

        #endregion

        #region Support methods

        private bool TestAddElementToDatasetAsString<T>(DicomElement element, T[] testValues)
        {
            var ds = new DicomDataset();
            string[] stringValues = typeof(T) == typeof(string)
                ? testValues.Cast<string>().ToArray()
                : testValues.Select(x => x.ToString()).ToArray();

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
            return true;
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

        private static bool TestAddElementToDatasetAsByteBuffer<T>(DicomElement element, T[] testValues)
        {
            var ds = new DicomDataset
            {
                { element.Tag, element.Buffer }
            };

            for (int index = 0; index < testValues.Count(); index++)
            {
                Assert.Equal(testValues[index], ds.GetValue<T>(element.Tag, index));
            }
            return true;
        }

        private class DecimalDeltaComparer : IEqualityComparer<decimal>
        {
            private readonly decimal _delta;
            public DecimalDeltaComparer(decimal delta) { _delta = delta; }
            public bool Equals(decimal x, decimal y) => Math.Abs(x - y) < _delta;
            public int GetHashCode(decimal obj) => throw new NotImplementedException();
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
