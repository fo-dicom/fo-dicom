// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    using System.Collections.Generic;

    using Xunit;

    [Collection("General")]
    public class DicomTranscoderTest
    {
        #region Unit tests

        [Theory]
        [MemberData(nameof(TransferSyntaxesNames))]
        public void GetCodec_KnownTransferSyntax_ShouldReturnCodecObject(DicomTransferSyntax transferSyntax, string expected)
        {
            var codec = TranscoderManager.GetCodec(transferSyntax);
            var actual = codec.Name;
            Assert.Equal(expected, actual);
        }

        #endregion

        #region Support data

        public static IEnumerable<object[]> TransferSyntaxesNames
        {
            get
            {
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
