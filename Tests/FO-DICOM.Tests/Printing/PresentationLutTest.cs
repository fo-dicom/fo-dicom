// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Printing;
using System.Collections.Generic;
using Xunit;

namespace FellowOakDicom.Tests.Printing
{

    [Collection(TestCollections.General)]
    public class PresentationLutTest
    {
        #region Unit tests

        [Theory]
        [MemberData(nameof(PresentationLuts))]
        public void LutSequence_InitialObject_IsAlwaysDefined(PresentationLut presentationLut)
        {
            Assert.NotNull(presentationLut.LutSequence);
        }

        [Theory]
        [MemberData(nameof(PresentationLuts))]
        public void LutDescriptor_InitialObject_IsEmpty(PresentationLut presentationLut)
        {
            Assert.Empty(presentationLut.LutDescriptor);
        }

        [Theory]
        [MemberData(nameof(PresentationLuts))]
        public void LutData_InitialObject_IsEmpty(PresentationLut presentationLut)
        {
            Assert.Empty(presentationLut.LutData);
        }

        [Theory]
        [MemberData(nameof(PresentationLuts))]
        public void LutDescriptor_InitialObject_SetterDoesNotThrow(PresentationLut presentationLut)
        {
            var exception = Record.Exception(() => presentationLut.LutDescriptor = new ushort[] { 10, 0, 12 });
            Assert.Null(exception);
        }

        [Theory]
        [MemberData(nameof(PresentationLuts))]
        public void LutExplanation_InitialObject_SetterDoesNotThrow(PresentationLut presentationLut)
        {
            var exception = Record.Exception(() => presentationLut.LutExplanation = "Some dummy explanation");
            Assert.Null(exception);
        }

        [Theory]
        [MemberData(nameof(PresentationLuts))]
        public void LutData_InitialObject_SetterDoesNotThrow(PresentationLut presentationLut)
        {
            var exception = Record.Exception(() => presentationLut.LutData = new ushort[] { 1, 2, 3, 4 });
            Assert.Null(exception);
        }

        [Theory]
        [MemberData(nameof(PresentationLuts))]
        public void LutExplanation_Getter_ReturnsSetValue(PresentationLut presentationLut)
        {
            const string expected = "Explanation";
            presentationLut.LutExplanation = expected;
            var actual = presentationLut.LutExplanation;

            Assert.Equal(expected, actual);
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

        public static IEnumerable<object[]> PresentationLuts
        {
            get
            {
                yield return new object[] { new PresentationLut() };
                yield return new object[] { new PresentationLut(null, new DicomDataset()) };
                yield return new object[]
                {
                    new PresentationLut(null,
                        new DicomDataset { new DicomSequence(DicomTag.PresentationLUTSequence, new DicomDataset()) })
                };
            }
        }

        #endregion
    }
}

