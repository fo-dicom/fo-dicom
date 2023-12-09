// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Codec;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Codec
{

    [Collection(TestCollections.WithTranscoder)]
    public class DicomTranscoderTest : IClassFixture<GlobalFixture>
    {

        #region Unit tests

        [TheoryForNetCore]
        [MemberData(nameof(TransferSyntaxesNames))]
        public void GetCodec_KnownTransferSyntax_ShouldReturnCodecObject(DicomTransferSyntax transferSyntax, string expected)
        {
            var transcoderManager = Setup.ServiceProvider.GetRequiredService<ITranscoderManager>();
            var codec = transcoderManager.GetCodec(transferSyntax);
            var actual = codec.Name;
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Support data

        public static IEnumerable<object[]> TransferSyntaxesNames
        {
            get
            {
                yield return new object[] { DicomTransferSyntax.JPEGProcess1, DicomUID.JPEGBaseline8Bit.Name };
                yield return new object[] { DicomTransferSyntax.RLELossless, DicomUID.RLELossless.Name };
                yield return new object[] { DicomTransferSyntax.JPEGLSNearLossless, DicomUID.JPEGLSNearLossless.Name };
                yield return new object[] { DicomTransferSyntax.JPEG2000Lossless, DicomUID.JPEG2000Lossless.Name };
            }
        }

        #endregion
    }
}
