// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests
{

    [Collection(TestCollections.General)]
    public class DicomDictionaryTest
    {
        #region Fields

        private readonly ITestOutputHelper _output;

        #endregion

        #region Constructors

        public DicomDictionaryTest(ITestOutputHelper output)
        {
            _output = output;
        }

        #endregion

        #region Unit tests

        [Theory]
        [MemberData(nameof(Tags))]
        public void Default_Item_ExistingTag_EntryFound(DicomTag tag)
        {
            var entry = DicomDictionary.Default[tag];
            Assert.NotEqual(DicomDictionary.UnknownTag, entry);
        }

        [Theory]
        [MemberData(nameof(Tags))]
        public void Constructor_NoExplicitLoading_TagsNotFound(DicomTag tag)
        {
            var dict = new DicomDictionary();
            var actual = dict[tag];
            Assert.Equal(DicomDictionary.UnknownTag, actual);
        }

        [Fact]
        public void Load_UncompressedFile_LoadedTagFound()
        {
            var dict = new DicomDictionary();
            dict.Load(TestData.Resolve("minimumdict.xml"), DicomDictionaryFormat.XML);

            var expected = DicomVR.CS;
            var actual = dict[DicomTag.FileSetID].ValueRepresentations.Single();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Load_CompressedFile_LoadedTagFound()
        {
            var dict = new DicomDictionary();
            dict.Load(TestData.Resolve("minimumdict.xml.gz"), DicomDictionaryFormat.XML);

            var expected = DicomVR.CS;
            var actual = dict[DicomTag.FileSetID].ValueRepresentations.Single();
            Assert.Equal(expected, actual);
        }

        private double TimeCall(int numCalls, Action call)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < numCalls; i++) { call(); }

            stopWatch.Stop();

            var totalElapsedMilliseconds = stopWatch.ElapsedMilliseconds;
            var millisecondsPerCall = totalElapsedMilliseconds / (double) numCalls;

            return millisecondsPerCall;
        }

        [Fact]
        public void GetEnumerator_ExecutionTime_IsNotSlow()
        {
            DicomDictionary.EnsureDefaultDictionariesLoaded();

            var millisecondsPerCall = TimeCall(1000, () => Assert.NotNull(DicomDictionary.Default.Last()));

            var referenceDictionarySize = DicomDictionary.Default.Count();
            var referenceTime = TimeCall(1000, () => Assert.NotEqual(0, Enumerable.Range(0, referenceDictionarySize).ToDictionary(i => 2 * i).Values.Last()));

            _output.WriteLine($"GetEnumerator: {millisecondsPerCall} ms per call, reference time: {referenceTime} ms per call");

            Assert.InRange(millisecondsPerCall, 0, (1 + referenceTime) * 5);
        }

        [Fact]
        public void AccessNonExistingKeyword()
        {
            DicomTag tag = null;
            var ex = Record.Exception(() => tag = DicomDictionary.Default["invalid-keyword"]);
            Assert.Null(ex); // assert that no exception was thrown
            Assert.Null(tag); // assert that the dictionary returns a null tag for invalid keyword
        }

        [Fact]
        public void VerifyDicomStandardTags()
        {
            Assert.Equal("Current Frame Functional Groups Sequence", new DicomTag(0x0006, 0x0001).DictionaryEntry.Name);
        }

        #endregion

        #region Support data

        public static IEnumerable<object[]> Tags
        {
            get
            {
                yield return new object[] { DicomTag.FileSetID };
                yield return new object[] { DicomTag.ContourData };
                yield return new object[] { DicomTag.ReferencePixelX0 };
                yield return new object[] { DicomTag.dBdt };
                yield return new object[] { DicomTag.TherapyDescriptionRETIRED };
                yield return new object[] { DicomTag.DICOMMediaRetrievalSequence };
                yield return new object[] { DicomTag.ConsultingPhysicianIdentificationSequence }; // 2015c
                yield return new object[] { DicomTag.TrackPointIndexList }; // 2016a
            }
        }

        #endregion

    }
}
