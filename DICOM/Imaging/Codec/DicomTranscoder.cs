// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    using System;
    using System.Collections.Generic;

    using Dicom.Imaging.Render;
    using Dicom.IO.Buffer;
    using Dicom.IO.Writer;

    /// <summary>
    /// Generic DICOM transcoder.
    /// </summary>
    public class DicomTranscoder : IDicomTranscoder
    {
        /// <summary>
        /// Initializes an instance of <see cref="DicomTranscoder"/>.
        /// </summary>
        /// <param name="inputSyntax">Input transfer syntax.</param>
        /// <param name="outputSyntax">Output transfer syntax.</param>
        /// <param name="inputCodecParams">Input codec parameters.</param>
        /// <param name="outputCodecParams">Output codec parameters.</param>
        public DicomTranscoder(
            DicomTransferSyntax inputSyntax,
            DicomTransferSyntax outputSyntax,
            DicomCodecParams inputCodecParams = null,
            DicomCodecParams outputCodecParams = null)
        {
            InputSyntax = inputSyntax;
            OutputSyntax = outputSyntax;
            InputCodecParams = inputCodecParams ?? DefaultInputCodecParams(inputSyntax);
            OutputCodecParams = outputCodecParams;
        }

        /// <summary>
        /// Gets the transfer syntax of the input codec.
        /// </summary>
        public DicomTransferSyntax InputSyntax { get; private set; }

        /// <summary>
        /// Gets the parameters associated with the input codec.
        /// </summary>
        public DicomCodecParams InputCodecParams { get; private set; }

        private IDicomCodec _inputCodec;

        private IDicomCodec InputCodec
        {
            get
            {
                if (InputSyntax.IsEncapsulated && _inputCodec == null) _inputCodec = TranscoderManager.GetCodec(InputSyntax);
                return _inputCodec;
            }
        }

        /// <summary>
        /// Gets the transfer syntax of the output codec.
        /// </summary>
        public DicomTransferSyntax OutputSyntax { get; private set; }

        /// <summary>
        /// Gets the parameters associated with the output codec.
        /// </summary>
        public DicomCodecParams OutputCodecParams { get; private set; }

        private IDicomCodec _outputCodec;

        private IDicomCodec OutputCodec
        {
            get
            {
                if (OutputSyntax.IsEncapsulated && _outputCodec == null) _outputCodec = TranscoderManager.GetCodec(OutputSyntax);
                return _outputCodec;
            }
        }

        /// <summary>
        /// Transcode a <see cref="DicomFile"/> from <see cref="IDicomTranscoder.InputSyntax"/> to <see cref="IDicomTranscoder.OutputSyntax"/>.
        /// </summary>
        /// <param name="file">DICOM file.</param>
        /// <returns>New, transcoded, DICOM file.</returns>
        public DicomFile Transcode(DicomFile file)
        {
            var f = new DicomFile();
            f.FileMetaInfo.Add(file.FileMetaInfo);
            f.FileMetaInfo.TransferSyntax = OutputSyntax;
            f.Dataset.InternalTransferSyntax = OutputSyntax;
            f.Dataset.Add(Transcode(file.Dataset));
            return f;
        }

        /// <summary>
        /// Transcode a <see cref="DicomDataset"/> from <see cref="IDicomTranscoder.InputSyntax"/> to <see cref="IDicomTranscoder.OutputSyntax"/>.
        /// </summary>
        /// <param name="dataset">DICOM dataset.</param>
        /// <returns>New, transcoded, DICOM dataset.</returns>
        public DicomDataset Transcode(DicomDataset dataset)
        {
            if (!dataset.Contains(DicomTag.PixelData))
            {
                var newDataset = dataset.Clone();
                newDataset.InternalTransferSyntax = OutputSyntax;
                newDataset.RecalculateGroupLengths(false);
                return newDataset;
            }

            if (!InputSyntax.IsEncapsulated && !OutputSyntax.IsEncapsulated)
            {
                // transcode from uncompressed to uncompressed
                var newDataset = dataset.Clone();
                newDataset.InternalTransferSyntax = OutputSyntax;

                var oldPixelData = DicomPixelData.Create(dataset, false);
                var newPixelData = DicomPixelData.Create(newDataset, true);

                for (int i = 0; i < oldPixelData.NumberOfFrames; i++)
                {
                    var frame = oldPixelData.GetFrame(i);
                    newPixelData.AddFrame(frame);
                }

                ProcessOverlays(dataset, newDataset);

                newDataset.RecalculateGroupLengths(false);

                return newDataset;
            }

            if (InputSyntax.IsEncapsulated && OutputSyntax.IsEncapsulated)
            {
                // transcode from compressed to compressed
                var temp = Decode(dataset, DicomTransferSyntax.ExplicitVRLittleEndian, InputCodec, InputCodecParams);
                return Encode(temp, OutputSyntax, OutputCodec, OutputCodecParams);
            }

            if (InputSyntax.IsEncapsulated)
            {
                // transcode from compressed to uncompressed
                return Decode(dataset, OutputSyntax, InputCodec, InputCodecParams);
            }

            if (OutputSyntax.IsEncapsulated)
            {
                // transcode from uncompressed to compressed
                return Encode(dataset, OutputSyntax, OutputCodec, OutputCodecParams);
            }

            throw new DicomCodecException(
                "Unable to find transcoding solution for {0} to {1}",
                InputSyntax.UID.Name,
                OutputSyntax.UID.Name);
        }

        /// <summary>
        /// Decompress single frame from DICOM dataset and return uncompressed frame buffer.
        /// </summary>
        /// <param name="dataset">DICOM dataset</param>
        /// <param name="frame">Frame number</param>
        /// <returns>Uncompressed frame buffer</returns>
        public IByteBuffer DecodeFrame(DicomDataset dataset, int frame)
        {
            var pixelData = DicomPixelData.Create(dataset);
            var buffer = pixelData.GetFrame(frame);

            // is pixel data already uncompressed?
            if (!dataset.InternalTransferSyntax.IsEncapsulated) return buffer;

            // clone dataset to prevent changes to source
            var cloneDataset = dataset.Clone();

            var oldPixelData = DicomPixelData.Create(cloneDataset, true);
            oldPixelData.AddFrame(buffer);

            var newDataset = Decode(cloneDataset, OutputSyntax, InputCodec, InputCodecParams);
            var newPixelData = DicomPixelData.Create(newDataset);

            return newPixelData.GetFrame(0);
        }

        /// <summary>
        /// Decompress pixel data from DICOM dataset and return uncompressed pixel data.
        /// </summary>
        /// <param name="dataset">DICOM dataset.</param>
        /// <param name="frame">Frame number.</param>
        /// <returns>Uncompressed pixel data.</returns>
        public IPixelData DecodePixelData(DicomDataset dataset, int frame)
        {
            var pixelData = DicomPixelData.Create(dataset);

            // is pixel data already uncompressed?
            if (!dataset.InternalTransferSyntax.IsEncapsulated) return PixelDataFactory.Create(pixelData, frame);

            var buffer = pixelData.GetFrame(frame);

            // clone dataset to prevent changes to source
            var cloneDataset = dataset.Clone();

            var oldPixelData = DicomPixelData.Create(cloneDataset, true);
            oldPixelData.AddFrame(buffer);

            var newDataset = Decode(cloneDataset, OutputSyntax, InputCodec, InputCodecParams);
            var newPixelData = DicomPixelData.Create(newDataset);

            return PixelDataFactory.Create(newPixelData, 0);
        }

        private static DicomCodecParams DefaultInputCodecParams(DicomTransferSyntax inputSyntax)
        {
            return inputSyntax == DicomTransferSyntax.JPEGProcess1 || inputSyntax == DicomTransferSyntax.JPEGProcess2_4
                       ? new DicomJpegParams { ConvertColorspaceToRGB = true }
                       : null;
        }

        private static DicomDataset Decode(
            DicomDataset oldDataset,
            DicomTransferSyntax outSyntax,
            IDicomCodec codec,
            DicomCodecParams parameters)
        {
            var oldPixelData = DicomPixelData.Create(oldDataset, false);

            var newDataset = oldDataset.Clone();
            newDataset.InternalTransferSyntax = outSyntax;
            var newPixelData = DicomPixelData.Create(newDataset, true);

            codec.Decode(oldPixelData, newPixelData, parameters);

            ProcessOverlays(oldDataset, newDataset);

            newDataset.RecalculateGroupLengths(false);

            return newDataset;
        }

        private static DicomDataset Encode(
            DicomDataset oldDataset,
            DicomTransferSyntax inSyntax,
            IDicomCodec codec,
            DicomCodecParams parameters)
        {
            var oldPixelData = DicomPixelData.Create(oldDataset, false);

            var newDataset = oldDataset.Clone();
            newDataset.InternalTransferSyntax = codec.TransferSyntax;
            var newPixelData = DicomPixelData.Create(newDataset, true);

            codec.Encode(oldPixelData, newPixelData, parameters);

            if (codec.TransferSyntax.IsLossy && newPixelData.NumberOfFrames > 0)
            {
                newDataset.AddOrUpdate(new DicomCodeString(DicomTag.LossyImageCompression, "01"));

                var methods = new List<string>();
                if (newDataset.Contains(DicomTag.LossyImageCompressionMethod)) methods.AddRange(newDataset.Get<string[]>(DicomTag.LossyImageCompressionMethod));
                methods.Add(codec.TransferSyntax.LossyCompressionMethod);
                newDataset.AddOrUpdate(new DicomCodeString(DicomTag.LossyImageCompressionMethod, methods.ToArray()));

                double oldSize = oldPixelData.GetFrame(0).Size;
                double newSize = newPixelData.GetFrame(0).Size;
                var ratio = String.Format("{0:0.000}", oldSize / newSize);
                newDataset.AddOrUpdate(new DicomDecimalString(DicomTag.LossyImageCompressionRatio, ratio));
            }

            ProcessOverlays(oldDataset, newDataset);

            newDataset.RecalculateGroupLengths(false);

            return newDataset;
        }

        private static void ProcessOverlays(DicomDataset input, DicomDataset output)
        {
            var overlays = DicomOverlayData.FromDataset(input.InternalTransferSyntax.IsEncapsulated ? output : input);

            foreach (var overlay in overlays)
            {
                var dataTag = new DicomTag(overlay.Group, DicomTag.OverlayData.Element);

                // Don't run conversion on non-embedded overlays.
                if (output.Contains(dataTag)) continue;

                // If embedded overlay, Overlay Bits Allocated should equal Bits Allocated (#110).
                var bitsAlloc = output.Get(DicomTag.BitsAllocated, (ushort)0);
                output.AddOrUpdate(new DicomTag(overlay.Group, DicomTag.OverlayBitsAllocated.Element), bitsAlloc);

                var data = overlay.Data;
                if (output.InternalTransferSyntax.IsExplicitVR) output.AddOrUpdate(new DicomOtherByte(dataTag, data));
                else output.AddOrUpdate(new DicomOtherWord(dataTag, data));
            }
        }

        public static DicomDataset ExtractOverlays(DicomDataset dataset)
        {
            if (!DicomOverlayData.HasEmbeddedOverlays(dataset)) return dataset;

            dataset = dataset.Clone();

            var input = dataset;
            if (input.InternalTransferSyntax.IsEncapsulated) input = input.Clone(DicomTransferSyntax.ExplicitVRLittleEndian);

            ProcessOverlays(input, dataset);

            return dataset;
        }
    }
}
