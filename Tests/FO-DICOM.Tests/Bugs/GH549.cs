// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Codec;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.WithTranscoder)]
    public class GH549
    {
        #region Unit Tests

        [TheoryForNetCore(Skip = "This test causes test host process crashes. See Github issue #1072 at https://github.com/fo-dicom/fo-dicom/issues/1072")]
        [MemberData(nameof(CodecsNumbers))]
        public void DicomTranscoderTranscode_ToCompressedCodecInParallel_NoMultithreadIssues(DicomTransferSyntax syntax, int filesToTranscode)
        {
            var original = DicomFile.Open(TestData.Resolve("CT-MONO2-16-ankle")).Dataset;

            var datasets = Enumerable.Range(0, filesToTranscode).Select(_ => original.Clone()).ToList();
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

            var refPixelData = originalTranscoded.GetDicomItem<DicomFragmentSequence>(DicomTag.PixelData);
            foreach (var dataset in bag)
            {
                var pixelData = dataset.GetDicomItem<DicomFragmentSequence>(DicomTag.PixelData);
                Assert.Equal(refPixelData, pixelData);
            }
        }

        [TheoryForNetCore(Skip = "This test causes test host process crashes. See Github issue #1072 at https://github.com/fo-dicom/fo-dicom/issues/1072")]
        [MemberData(nameof(CodecsNumbers))]
        public void DicomDatasetClone_ToCompressedCodecInParallel_NoMultithreadIssues(DicomTransferSyntax syntax,
            int filesToTranscode)
        {
            var original = DicomFile.Open(TestData.Resolve("D_CLUNIE_CT1_RLE_FRAGS.dcm")).Dataset;

            var datasets = Enumerable.Range(0, filesToTranscode).Select(_ => original.Clone()).ToList();

            var originalTranscoded = original.Clone(syntax);

            var bag = new ConcurrentBag<DicomDataset>();

            var exception =
                Record.Exception(() => Parallel.ForEach(datasets, dataset =>
                {
                    var transcoded = dataset.Clone(syntax);
                    bag.Add(transcoded);
                }));
            Assert.Null(exception);

            var refPixelData = originalTranscoded.GetDicomItem<DicomFragmentSequence>(DicomTag.PixelData);
            foreach (var dataset in bag)
            {
                var pixelData = dataset.GetDicomItem<DicomFragmentSequence>(DicomTag.PixelData);
                Assert.Equal(refPixelData, pixelData);
            }
        }

        #endregion

        #region Support Data

        public static readonly IEnumerable<object[]> CodecsNumbers = new []
        {

            new object[] {DicomTransferSyntax.JPEGLSLossless, 100},
            new object[] {DicomTransferSyntax.JPEGLSNearLossless, 200},
            new object[] {DicomTransferSyntax.JPEG2000Lossless, 100},
            new object[] {DicomTransferSyntax.JPEG2000Lossy, 200},
            new object[] {DicomTransferSyntax.JPEGProcess14SV1, 100},
            new object[] {DicomTransferSyntax.RLELossless, 100}
        };

    #endregion
}
}
