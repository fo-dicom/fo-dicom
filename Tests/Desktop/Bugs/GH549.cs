// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dicom.Imaging.Codec;

using Xunit;

namespace Dicom.Bugs
{
    public class GH549
    {
        #region Unit Tests

        [Theory]
        [MemberData(nameof(CodecsNumbers))]
        public void DicomTranscoderTranscode_ToCompressedCodecInParallel_NoMultithreadIssues(DicomTransferSyntax syntax,
            int filesToTranscode)
        {
            var original = DicomFile.Open(@"Test Data/CT-MONO2-16-ankle").Dataset;

            var datasets = Enumerable.Repeat(original.Clone(), filesToTranscode).ToList();
            var transcoder = new DicomTranscoder(original.InternalTransferSyntax, syntax);

            var originalTranscoded = transcoder.Transcode(original);

            var bag = new ConcurrentBag<DicomDataset>();

            var exception =
                Record.Exception(() => Parallel.ForEach(datasets, dataset =>
                {
                    var transcoded = transcoder.Transcode(dataset);
                    bag.Add(transcoded);
                }));
            Assert.Null(exception);

            var refPixelData = originalTranscoded.Get<DicomFragmentSequence>(DicomTag.PixelData);
            foreach (var dataset in bag)
            {
                var pixelData = dataset.Get<DicomFragmentSequence>(DicomTag.PixelData);
                Assert.Equal(refPixelData, pixelData);
            }
        }

        [Theory]
        [MemberData(nameof(CodecsNumbers))]
        public void DicomDatasetClone_ToCompressedCodecInParallel_NoMultithreadIssues(DicomTransferSyntax syntax,
            int filesToTranscode)
        {
            var original = DicomFile.Open(@"Test Data/D_CLUNIE_CT1_RLE_FRAGS.dcm").Dataset;

            var datasets = Enumerable.Repeat(original.Clone(), filesToTranscode).ToList();
            //var transcoder = new DicomTranscoder(original.InternalTransferSyntax, syntax);

            var originalTranscoded = original.Clone(syntax);

            var bag = new ConcurrentBag<DicomDataset>();

            var exception =
                Record.Exception(() => Parallel.ForEach(datasets, dataset =>
                {
                    var transcoded = dataset.Clone(syntax);
                    bag.Add(transcoded);
                }));
            Assert.Null(exception);

            var refPixelData = originalTranscoded.Get<DicomFragmentSequence>(DicomTag.PixelData);
            foreach (var dataset in bag)
            {
                var pixelData = dataset.Get<DicomFragmentSequence>(DicomTag.PixelData);
                Assert.Equal(refPixelData, pixelData);
            }
        }

        #endregion

        #region Support Data

        public static readonly IEnumerable<object[]> CodecsNumbers = new[]
        {
            new object[] { DicomTransferSyntax.JPEGLSLossless, 100 },
            new object[] { DicomTransferSyntax.JPEGLSNearLossless, 200 },
            new object[] { DicomTransferSyntax.JPEG2000Lossless, 100 },
            new object[] { DicomTransferSyntax.JPEG2000Lossy, 200 },
            new object[] { DicomTransferSyntax.JPEGProcess14SV1, 100 },
            new object[] { DicomTransferSyntax.RLELossless, 100 }
        };

        #endregion
    }
}
