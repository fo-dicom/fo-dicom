// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging.Codec;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace FellowOakDicom.Tests.Imaging.Codec
{

    [Collection("General")]
    public class DicomTranscoderTest : IClassFixture<GlobalFixture>
    {

        #region Unit tests

        [Theory(Skip = "Codec tests are temporarily disabled")] // TODO re-enable this
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
                // TODO re-enable this
                yield break;
                yield return new object[] { DicomTransferSyntax.JPEGProcess1, DicomUID.JPEGBaseline1.Name };
                yield return new object[] { DicomTransferSyntax.RLELossless, DicomUID.RLELossless.Name };
                yield return new object[] { DicomTransferSyntax.JPEGLSNearLossless, DicomUID.JPEGLSLossyNearLossless.Name };
                yield return
                    new object[]
                        {
                            DicomTransferSyntax.JPEG2000Lossless,
                            DicomUID.JPEG2000LosslessOnly.Name
                        };
            }
        }

        #endregion
    }
}
