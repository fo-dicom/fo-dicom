// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Xunit;
    using Xunit.Abstractions;

    [Collection("General")]
    public class DicomDictionaryTest : IDisposable
    {
        #region Fields

#if !NETSTANDARD
        private static int _index = 0;
        private readonly AppDomain _testDomain;
#endif
        private readonly ITestOutputHelper _output;

        #endregion

        #region Constructors

        public DicomDictionaryTest(ITestOutputHelper output)
        {
#if !NETSTANDARD
            var name = string.Concat("DicomDictionary test appdomain #", ++_index);
            _testDomain = AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.SetupInformation);
            Trace.WriteLine($"[{_testDomain.FriendlyName}] Created.");
#endif
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
            dict.Load(@".\Test Data\minimumdict.xml", DicomDictionaryFormat.XML);

            var expected = DicomVR.CS;
            var actual = dict[DicomTag.FileSetID].ValueRepresentations.Single();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Load_CompressedFile_LoadedTagFound()
        {
            var dict = new DicomDictionary();
            dict.Load(@".\Test Data\minimumdict.xml.gz", DicomDictionaryFormat.XML);

            var expected = DicomVR.CS;
            var actual = dict[DicomTag.FileSetID].ValueRepresentations.Single();
            Assert.Equal(expected, actual);
        }

        private double TimeCall(int numCalls, Action call)
        {
            var start = Process.GetCurrentProcess().TotalProcessorTime;

            for (int i = 0; i < numCalls; i++) call();

            var end = Process.GetCurrentProcess().TotalProcessorTime;

            var millisecondsPerCall = (end - start).TotalMilliseconds / numCalls;

            return millisecondsPerCall;
        }

        [Fact]
        public void GetEnumerator_ExecutionTime_IsNotSlow()
        {
            DicomDictionary.EnsureDefaultDictionariesLoaded();

            var millisecondsPerCall = TimeCall(100, () => Assert.NotNull(DicomDictionary.Default.Last()));

            var referenceTime = TimeCall(100, () => Assert.NotNull(Enumerable.Range(0, 1000).ToDictionary(i => 2 * i).Values.Last()));

            _output.WriteLine($"GetEnumerator: {millisecondsPerCall} ms per call, reference time: {referenceTime} ms per call");

            Assert.InRange(millisecondsPerCall, 0, (1 + referenceTime) * 5);
        }


#if !NETSTANDARD

        [Fact]
        public void Throws_If_Already_Loaded()
        {
            _testDomain.DoCallBack(() =>
            {
                var dict = DicomDictionary.EnsureDefaultDictionariesLoaded(false);
                Assert.Throws<DicomDataException>(() => DicomDictionary.Default = new DicomDictionary());
            });
        }

        [Fact]
        public void Throw_If_Already_Loaded_Implicitly_Via_Getter()
        {
            _testDomain.DoCallBack(() =>
            {
                var dict = DicomDictionary.Default;
                Assert.Throws<DicomDataException>(() => DicomDictionary.Default = new DicomDictionary());
            });
        }

        [Fact]
        public void Ensure_MultiThreaded_Init_Runs_Once()
        {
            _testDomain.DoCallBack(() =>
            {
                var release = new ManualResetEvent(initialState: false);

                var multipleSimultaneousCallsToDefaultTask =
                    Task.WhenAll(Enumerable.Range(0, 10).Select(_ => Task.Run(() =>
                    {
                        release.WaitOne();
                        return DicomDictionary.Default;
                    })));

                release.Set();

                var multipleSimultaneousCallsToDefault = multipleSimultaneousCallsToDefaultTask.Result;

                var firstResolvedDict = multipleSimultaneousCallsToDefault.First();
                Assert.All(multipleSimultaneousCallsToDefault, dicomDict => Assert.Equal(dicomDict, firstResolvedDict));
            });
        }


        [Fact]
        public void Can_Call_EnsureLoaded_Multiple_Times_Including_Private()
        {
            _testDomain.DoCallBack(() =>
            {
                DicomDictionary.EnsureDefaultDictionariesLoaded(true);
                DicomDictionary.EnsureDefaultDictionariesLoaded(true);
                DicomDictionary.EnsureDefaultDictionariesLoaded();
            });
        }


        [Fact]
        public void Can_Call_EnsureLoaded_Multiple_Times_Excluding_Private()
        {
            _testDomain.DoCallBack(() =>
            {
                DicomDictionary.EnsureDefaultDictionariesLoaded(false);
                DicomDictionary.EnsureDefaultDictionariesLoaded(false);
                DicomDictionary.EnsureDefaultDictionariesLoaded();
            });
        }


        [Fact]
        public void Throws_If_EnsureLoaded_Called_With_And_Without_Private()
        {
            _testDomain.DoCallBack(() =>
            {
                DicomDictionary.EnsureDefaultDictionariesLoaded(true);
                Assert.Throws<DicomDataException>(() => DicomDictionary.EnsureDefaultDictionariesLoaded(false));
            });
        }


        [Fact]
        public void Throws_If_EnsureLoaded_Called_Without_And_With_Private()
        {
            _testDomain.DoCallBack(() =>
            {
                DicomDictionary.EnsureDefaultDictionariesLoaded(false);
                Assert.Throws<DicomDataException>(() => DicomDictionary.EnsureDefaultDictionariesLoaded(true));
            });
        }

        [Fact]
        public void EnsureLoaded_Assumes_Loading_Private_Dictionary_Data_By_Default()
        {
            _testDomain.DoCallBack(() =>
            {
                var dict = DicomDictionary.EnsureDefaultDictionariesLoaded();
                var secondEnsurCall = DicomDictionary.EnsureDefaultDictionariesLoaded(loadPrivateDictionary: true);
                Assert.Equal(dict, secondEnsurCall);
                Assert.Throws<DicomDataException>(
                    () => DicomDictionary.EnsureDefaultDictionariesLoaded(loadPrivateDictionary: false));
            });
        }

        [Fact]
        public void Add_PrivateTag_GetsCorrectVR()
        {
            var privCreatorDictEntry = new DicomDictionaryEntry(
                                           new DicomTag(0x0011, 0x0010),
                                           "Private Creator",
                                           "PrivateCreator",
                                           DicomVM.VM_1,
                                           false,
                                           DicomVR.LO);
            DicomDictionary.Default.Add(privCreatorDictEntry);

            DicomPrivateCreator privateCreator1 = DicomDictionary.Default.GetPrivateCreator("TESTCREATOR1");
            DicomDictionary privDict1 = DicomDictionary.Default[privateCreator1];

            var dictEntry = new DicomDictionaryEntry(
                                DicomMaskedTag.Parse("0011", "xx10"),
                                "TestPrivTagName",
                                "TestPrivTagKeyword",
                                DicomVM.VM_1,
                                false,
                                DicomVR.CS);
            privDict1.Add(dictEntry);

            var ds = new DicomDataset();
            ds.Add(dictEntry.Tag, "VAL1");

            Assert.Equal(DicomVR.CS, ds.Get<DicomVR>(ds.GetPrivateTag(dictEntry.Tag)));
        }

        [Fact]
        public void Enumerate_DictionaryEntriesWithPrivateTags_ContainsAllExpectedEntries()
        {
            var dict = new DicomDictionary();

            var tag1 = new DicomTag(0x0010, 0x0020);
            var dictEntry1 = new DicomDictionaryEntry(
                                 tag1,
                                 "TestPublicTagName",
                                 "TestPublicTagKeyword",
                                 DicomVM.VM_1,
                                 false,
                                 DicomVR.DT);
            var privCreatorDictEntry = new DicomDictionaryEntry(
                                           new DicomTag(0x0011, 0x0010),
                                           "Private Creator",
                                           "PrivateCreator",
                                           DicomVM.VM_1,
                                           false,
                                           DicomVR.LO);
            dict.Add(privCreatorDictEntry);

            DicomPrivateCreator privateCreator = dict.GetPrivateCreator("TESTCREATOR");
            DicomDictionary privDict = dict[privateCreator];

            var dictEntry2 = new DicomDictionaryEntry(
                                 DicomMaskedTag.Parse("0011", "xx10"),
                                 "TestPrivTagName",
                                 "TestPrivTagKeyword",
                                 DicomVM.VM_1,
                                 false,
                                 DicomVR.DT);

            privDict.Add(dictEntry2);
            dict.Add(dictEntry1);

            Assert.True(dict.Contains(dictEntry1));
            Assert.True(dict.Contains(privCreatorDictEntry));
            Assert.True(dict[dictEntry2.Tag.PrivateCreator].Contains(dictEntry2));
        }
#endif

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

        #region IDisposable implementation

        public void Dispose()
        {
#if !NETSTANDARD
            if (_testDomain != null)
            {
                Trace.WriteLine($"[{_testDomain.FriendlyName}] unloading.");
                AppDomain.Unload(_testDomain);
            }
#endif
        }

        #endregion
    }
    }
