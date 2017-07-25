// // Copyright (c) 2012-2017 fo-dicom contributors.
// // Licensed under the Microsoft Public License (MS-PL).
// 

using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Dicom.Printing
{
    public class PresentationLutTest
    {
        #region Unit tests

        [Fact]
        public void LutSequence_NoPresentationLutSequenceInDataset_GetterReturnsNull()
        {
            var presentationLut = new PresentationLut(null, new DicomDataset());
            Assert.Null(presentationLut.LutSequence);
        }

        [Fact]
        public void Constructor_FromDataset_SopInstanceUidMaintained()
        {
            var expected = DicomUID.Generate();
            var presentationLut = new PresentationLut(expected, new DicomDataset());
            var actual = presentationLut.SopInstanceUid;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(PresentationLuts))]
        public void Constructor_WithNullSopInstanceUid_GetterReturnsNonNull(PresentationLut presentationLut)
        {
            var actual = presentationLut.SopInstanceUid;
            Assert.NotNull(actual);
        }

        #endregion

        #region Support data

        public static readonly IEnumerable<object[]> PresentationLuts = new[]
        {
            new object[] { new PresentationLut() },
            new object[] { new PresentationLut(null, new DicomDataset()) }
        };

        #endregion
    }
}

