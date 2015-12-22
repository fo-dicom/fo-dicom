// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Xunit;

    [Collection("General")]
    public class DicomDictionaryTest
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

        [Fact]
        public void Throw_If_Already_Loaded()
        {
            var dict = DicomDictionary.EnsureDefaultDictionariesLoaded(false);
            Assert.Throws<DicomDataException>(() => DicomDictionary.Default = new DicomDictionary());
        }

        [Fact]
        public void Throw_If_Already_Loaded_Implicitly_Via_Getter()
        {
            var dict = DicomDictionary.Default;
            Assert.Throws<DicomDataException>(() => DicomDictionary.Default = new DicomDictionary());
        }

        [Fact]
        public async Task Ensure_MultiThreaded_Init_Runs_Once()
        {
            var release = new ManualResetEvent(initialState: false);

            var multipleSimultaneousCallsToDefaultTask = Task.WhenAll(Enumerable.Range(0, 10).Select(_ => Task.Run(() =>
            {
                release.WaitOne();
                return DicomDictionary.Default;
            })));

            release.Set();

            var multipleSimultaneousCallsToDefault = await multipleSimultaneousCallsToDefaultTask;

            var firstResolvedDict = multipleSimultaneousCallsToDefault.First();
            Assert.All(multipleSimultaneousCallsToDefault, dicomDict=>Assert.Equal(dicomDict, firstResolvedDict));
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
            }
        }

        #endregion
    }
}