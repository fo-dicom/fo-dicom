﻿// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Xunit;

    [Collection("General")]
    public class DicomDictionaryTest : IDisposable
    {
        #region Unit tests

        [Theory]
        [MemberData("Tags")]
        public void Default_Item_ExistingTag_EntryFound(DicomTag tag)
        {
            var entry = DicomDictionary.Default[tag];
            Assert.NotEqual(DicomDictionary.UnknownTag, entry);
        }

        [Theory]
        [MemberData("Tags")]
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

#if !NETSTANDARD

        [Fact]
        public void Throws_If_Already_Loaded()
        {
            this.testDomain.DoCallBack(() =>
            {
                var dict = DicomDictionary.EnsureDefaultDictionariesLoaded(false);
                Assert.Throws<DicomDataException>(() => DicomDictionary.Default = new DicomDictionary());
            });
        }

        [Fact]
        public void Throw_If_Already_Loaded_Implicitly_Via_Getter()
        {
            this.testDomain.DoCallBack(() =>
            {
                var dict = DicomDictionary.Default;
                Assert.Throws<DicomDataException>(() => DicomDictionary.Default = new DicomDictionary());
            });
        }

        [Fact]
        public void Ensure_MultiThreaded_Init_Runs_Once()
        {
            this.testDomain.DoCallBack(() =>
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
            this.testDomain.DoCallBack(() =>
            {
                DicomDictionary.EnsureDefaultDictionariesLoaded(true);
                DicomDictionary.EnsureDefaultDictionariesLoaded(true);
                DicomDictionary.EnsureDefaultDictionariesLoaded();
            });
        }


        [Fact]
        public void Can_Call_EnsureLoaded_Multiple_Times_Excluding_Private()
        {
            this.testDomain.DoCallBack(() =>
            {
                DicomDictionary.EnsureDefaultDictionariesLoaded(false);
                DicomDictionary.EnsureDefaultDictionariesLoaded(false);
                DicomDictionary.EnsureDefaultDictionariesLoaded();
            });
        }


        [Fact]
        public void Throws_If_EnsureLoaded_Called_With_And_Without_Private()
        {
            this.testDomain.DoCallBack(() =>
            {
                DicomDictionary.EnsureDefaultDictionariesLoaded(true);
                Assert.Throws<DicomDataException>(() => DicomDictionary.EnsureDefaultDictionariesLoaded(false));
            });
        }


        [Fact]
        public void Throws_If_EnsureLoaded_Called_Without_And_With_Private()
        {
            this.testDomain.DoCallBack(() =>
            {
                DicomDictionary.EnsureDefaultDictionariesLoaded(false);
                Assert.Throws<DicomDataException>(() => DicomDictionary.EnsureDefaultDictionariesLoaded(true));
            });
        }

        [Fact]
        public void EnsureLoaded_Assumes_Loading_Private_Dictionary_Data_By_Default()
        {
            this.testDomain.DoCallBack(() =>
            {
                var dict = DicomDictionary.EnsureDefaultDictionariesLoaded();
                var secondEnsurCall = DicomDictionary.EnsureDefaultDictionariesLoaded(loadPrivateDictionary: true);
                Assert.Equal(dict, secondEnsurCall);
                Assert.Throws<DicomDataException>(
                    () => DicomDictionary.EnsureDefaultDictionariesLoaded(loadPrivateDictionary: false));
            });
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

#if NETSTANDARD
        public void Dispose() { }
#else
        #region appDomain Stuff

        private static int index = 0;
        private readonly AppDomain testDomain;

        public void Dispose()
        {

            if (testDomain != null)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("[{0}] Unloading.", testDomain.FriendlyName));
                AppDomain.Unload(testDomain);
            }
        }

        public DicomDictionaryTest()
        {
            var name = string.Concat("DicomDictionary test appdomain #", ++index);
            testDomain = AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.SetupInformation);
            System.Diagnostics.Trace.WriteLine(string.Format("[{0}] Created.", testDomain.FriendlyName));

        }

        #endregion

#endif
    }
}