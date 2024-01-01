// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using System.Collections.Generic;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network)]
    public class DicomPresentationContextTest
    {
        #region Unit Tests

        [Theory]
        [MemberData(nameof(ResultsAcceptedTransferSyntaxes))]
        public void SetResult_TwoArguments_AcceptedTransferSyntaxOnlyRequiredOnAcceptNullOtherwise(
            DicomPresentationContextResult result,
            DicomTransferSyntax acceptedSyntax)
        {
            var context = new DicomPresentationContext(0x01, DicomUID.Verification);
            context.SetResult(result, acceptedSyntax);

            Assert.Equal(result == DicomPresentationContextResult.Accept ? acceptedSyntax : null,
                context.AcceptedTransferSyntax);
        }

        [Theory]
        [MemberData(nameof(ResultsAcceptedTransferSyntaxes))]
        public void SetResult_OneArgument_AcceptedTransferSyntaxOnlyRequiredOnAcceptNullOtherwise(
            DicomPresentationContextResult result,
            DicomTransferSyntax acceptedSyntax)
        {
            var context = new DicomPresentationContext(0x01, DicomUID.Verification);
            context.AddTransferSyntax(acceptedSyntax);
            context.SetResult(result);

            Assert.Equal(result == DicomPresentationContextResult.Accept ? acceptedSyntax : null,
                context.AcceptedTransferSyntax);
        }

        [Fact]
        public void SetResult_OneArgumentAcceptNoTransferSyntax_Throws()
        {
            var context = new DicomPresentationContext(0x03, DicomUID.RTPlanStorage);
            var exception = Record.Exception(() => context.SetResult(DicomPresentationContextResult.Accept));
            Assert.IsType<DicomNetworkException>(exception);
        }

        [Fact]
        public void SetResult_TwoArgumentAcceptTransferSyntaxNull_Throws()
        {
            var context = new DicomPresentationContext(0x03, DicomUID.RTPlanStorage);
            var exception = Record.Exception(() => context.SetResult(DicomPresentationContextResult.Accept, null));
            Assert.IsType<DicomNetworkException>(exception);
        }

        [Fact]
        public void SetResult_OneArgumentNotAcceptNoTransferSyntax_DoesNotThrow()
        {
            var context = new DicomPresentationContext(0x03, DicomUID.RTPlanStorage);
            var exception = Record.Exception(() => context.SetResult(DicomPresentationContextResult.RejectNoReason));
            Assert.Null(exception);
        }

        [Fact]
        public void SetResult_TwoArgumentNotAcceptTransferSyntaxNull_DoesNotThrow()
        {
            var context = new DicomPresentationContext(0x03, DicomUID.RTPlanStorage);
            var exception = Record.Exception(() => context.SetResult(DicomPresentationContextResult.RejectAbstractSyntaxNotSupported, null));
            Assert.Null(exception);
        }

        [Fact]
        public void AcceptedTransferSyntax_ResultNotSet_IsNull()
        {
            var context = new DicomPresentationContext(0x05, DicomUID.EnhancedMRImageStorage);
            context.AddTransferSyntax(DicomTransferSyntax.JPEGLSLossless);
            Assert.Null(context.AcceptedTransferSyntax);
        }

        [Fact]
        public void SetResult_FirstAcceptThenReject_ClearsAcceptedTransferSyntax()
        {
            var context = new DicomPresentationContext(0x07, DicomUID.CTImageStorage);
            context.SetResult(DicomPresentationContextResult.Accept, DicomTransferSyntax.JPEGProcess1);
            context.SetResult(DicomPresentationContextResult.RejectTransferSyntaxesNotSupported);

            Assert.Null(context.AcceptedTransferSyntax);
        }

        #endregion

        #region Support Data

        public static readonly IEnumerable<object[]> ResultsAcceptedTransferSyntaxes = new object[][]
        {
            new object[] { DicomPresentationContextResult.Accept, DicomTransferSyntax.DeflatedExplicitVRLittleEndian },
           new object[] { DicomPresentationContextResult.Accept, DicomTransferSyntax.JPEG2000Lossless },
           new object[] { DicomPresentationContextResult.Accept, DicomTransferSyntax.HTJ2KLossless },
           new object[] { DicomPresentationContextResult.RejectAbstractSyntaxNotSupported, DicomTransferSyntax.ExplicitVRLittleEndian },
           new object[] { DicomPresentationContextResult.RejectNoReason, null },
           new object[] { DicomPresentationContextResult.RejectTransferSyntaxesNotSupported, null },
           new object[] { DicomPresentationContextResult.RejectUser, DicomTransferSyntax.ExplicitVRLittleEndian }
        };

        #endregion
    }
}

