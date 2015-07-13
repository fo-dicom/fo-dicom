// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System.Collections.Generic;

    using Xunit;

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

        #endregion

        #region Support data

        public static IEnumerable<object[]> Tags
        {
            get
            {
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