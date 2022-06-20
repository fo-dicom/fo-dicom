// Copyright (c) 2012-2022 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using FellowOakDicom.Imaging.Codec;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection("General")]
    public class GH1403
    {
        // The test file is an RGB Dataset with 3 Rows and 3 Columns, encoded in Implicit Little Endian,
        // so the pixel data size without padding is 3 * 3 * 3 = 27
        private const int PixelDataSize = 27;

        [Theory]
        [MemberData(nameof(TransferSyntaxes))]
        public void Transforming_FromToUncompressed_AddsPadding_IfDataSizeIsOdd(
            DicomTransferSyntax sourceSyntax, DicomTransferSyntax destinationSyntax)
        {
            var file = DicomFile.Open(TestData.Resolve("GH1403.dcm"));
            var dataset = file.Dataset;

            // if the source syntax is not the same as in the file, first transform the dataset to it
            if (sourceSyntax != dataset.InternalTransferSyntax)
            {
                dataset = TransformedDataset(dataset, sourceSyntax);
            }

            // for Implicit Little Endian, pixel data is of type OW, otherwise OB
            if (destinationSyntax == DicomTransferSyntax.ImplicitVRLittleEndian)
            {
                CheckPaddingByteAfterTranscoding<DicomOtherWord>(dataset, destinationSyntax);
            }
            else
            {
                CheckPaddingByteAfterTranscoding<DicomOtherByte>(dataset, destinationSyntax);
            }
        }


        // defines source and destination transfer syntaxes
        public static readonly IEnumerable<object[]> TransferSyntaxes = new[]
        {
            new object[] { DicomTransferSyntax.ImplicitVRLittleEndian, DicomTransferSyntax.ExplicitVRLittleEndian },
            new object[] { DicomTransferSyntax.ExplicitVRLittleEndian, DicomTransferSyntax.ImplicitVRLittleEndian },
            new object[]
                { DicomTransferSyntax.ImplicitVRLittleEndian, DicomTransferSyntax.GEPrivateImplicitVRBigEndian },
            new object[] { DicomTransferSyntax.ImplicitVRLittleEndian, DicomTransferSyntax.ExplicitVRBigEndian },
            new object[] { DicomTransferSyntax.ExplicitVRBigEndian, DicomTransferSyntax.ImplicitVRLittleEndian }
        };

        private static void CheckPaddingByteAfterTranscoding<T>(DicomDataset dataset,
            DicomTransferSyntax newTransferSyntax) where T : DicomElement
        {
            dataset = TransformedDataset(dataset, newTransferSyntax);
            var pixelData = dataset.GetDicomItem<T>(DicomTag.PixelData);

            // padding byte shall still be present after the transcoding
            Assert.Equal(PixelDataSize + 1u, pixelData.Length);
            Assert.Equal(0, pixelData.Buffer.Data[PixelDataSize]);
        }

        private static DicomDataset TransformedDataset(DicomDataset dataset, DicomTransferSyntax newTransferSyntax)
        {
            var transcoder = new DicomTranscoder(dataset.InternalTransferSyntax,
                newTransferSyntax);
            return transcoder.Transcode(dataset);
        }
    }
}
