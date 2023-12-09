// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Globalization;
using FellowOakDicom.Imaging.Render;
using FellowOakDicom.IO.Buffer;
using FellowOakDicom.IO.Writer;

namespace FellowOakDicom.Imaging.Codec
{

    /// <summary>
    /// Generic DICOM transcoder.
    /// </summary>
    public class DicomTranscoder : IDicomTranscoder
    {
        #region FIELDS

        private readonly Lazy<IDicomCodec> _inputCodec;

        private readonly Lazy<IDicomCodec> _outputCodec;

        #endregion

        #region CONSTRUCTORS

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

            _inputCodec = new Lazy<IDicomCodec>(() => InitializeCodec(InputSyntax));
            _outputCodec = new Lazy<IDicomCodec>(() => InitializeCodec(OutputSyntax));
        }

        #endregion

        #region PROPERTIES

        /// <inheritdoc />
        public DicomTransferSyntax InputSyntax { get; }

        /// <inheritdoc />
        public DicomCodecParams InputCodecParams { get; }

        private IDicomCodec InputCodec => _inputCodec.Value;

        /// <inheritdoc />
        public DicomTransferSyntax OutputSyntax { get; }

        /// <inheritdoc />
        public DicomCodecParams OutputCodecParams { get; }

        private IDicomCodec OutputCodec => _outputCodec.Value;

        #endregion

        #region METHODS

        public static DicomDataset ExtractOverlays(DicomDataset dataset)
        {
            if (!DicomOverlayData.HasEmbeddedOverlays(dataset))
            {
                return dataset;
            }

            dataset = dataset.Clone();

            var input = dataset;
            if (input.InternalTransferSyntax.IsEncapsulated)
            {
                input = input.Clone(DicomTransferSyntax.ExplicitVRLittleEndian);
            }

            ProcessOverlays(input, dataset);

            return dataset;
        }

        /// <inheritdoc />
        public DicomFile Transcode(DicomFile file)
        {
            var f = new DicomFile();
            using var scope = new UnvalidatedScope(f.Dataset);
            f.FileMetaInfo.Add(file.FileMetaInfo);
            f.FileMetaInfo.TransferSyntax = OutputSyntax;
            f.Dataset.InternalTransferSyntax = OutputSyntax;
            f.Dataset.Add(Transcode(file.Dataset));
            return f;
        }

        /// <inheritdoc />
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

            throw new DicomCodecException($"Unable to find transcoding solution for {InputSyntax.UID.Name} to {OutputSyntax.UID.Name}");
        }

        /// <inheritdoc />
        public IByteBuffer DecodeFrame(DicomDataset dataset, int frame)
        {
            var pixelData = DicomPixelData.Create(dataset);
            var buffer = pixelData.GetFrame(frame);

            // is pixel data already uncompressed?
            if (!dataset.InternalTransferSyntax.IsEncapsulated)
            {
                return buffer;
            }

            // clone dataset to prevent changes to source
            var cloneDataset = dataset.Clone();

            var oldPixelData = DicomPixelData.Create(cloneDataset, true);
            oldPixelData.AddFrame(buffer);

            var newDataset = Decode(cloneDataset, OutputSyntax, InputCodec, InputCodecParams);
            var newPixelData = DicomPixelData.Create(newDataset);

            return newPixelData.GetFrame(0);
        }

        /// <inheritdoc />
        public IPixelData DecodePixelData(DicomDataset dataset, int frame)
        {
            var pixelData = DicomPixelData.Create(dataset);

            // is pixel data already uncompressed?
            if (!dataset.InternalTransferSyntax.IsEncapsulated)
            {
                return PixelDataFactory.Create(pixelData, frame);
            }

            var buffer = pixelData.GetFrame(frame);

            // clone dataset to prevent changes to source
            var cloneDataset = dataset.Clone();

            var oldPixelData = DicomPixelData.Create(cloneDataset, true);
            oldPixelData.AddFrame(buffer);

            var newDataset = Decode(cloneDataset, OutputSyntax, InputCodec, InputCodecParams);
            var newPixelData = DicomPixelData.Create(newDataset);

            return PixelDataFactory.Create(newPixelData, 0);
        }

        private static IDicomCodec InitializeCodec(DicomTransferSyntax syntax)
        {
            var transcoderManager = Setup.ServiceProvider.GetService(typeof(ITranscoderManager)) as ITranscoderManager;
            return syntax.IsEncapsulated && transcoderManager.HasCodec(syntax)
                ? transcoderManager.GetCodec(syntax)
                : null;
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
            if (codec == null)
            {
                throw new DicomCodecException($"Decoding dataset with transfer syntax: {oldDataset.InternalTransferSyntax} is not supported.");
            }

            var oldPixelData = DicomPixelData.Create(oldDataset);

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
            DicomTransferSyntax outSyntax,
            IDicomCodec codec,
            DicomCodecParams parameters)
        {
            if (codec == null)
            {
                throw new DicomCodecException($"Encoding dataset to transfer syntax {outSyntax} is not supported.");
            }

            var oldPixelData = DicomPixelData.Create(oldDataset);

            var newDataset = oldDataset.Clone();
            newDataset.InternalTransferSyntax = outSyntax;
            var newPixelData = DicomPixelData.Create(newDataset, true);

            codec.Encode(oldPixelData, newPixelData, parameters);

            if (outSyntax.IsLossy && newPixelData.NumberOfFrames > 0)
            {
                newDataset.AddOrUpdate(new DicomCodeString(DicomTag.LossyImageCompression, "01"));

                var methods = new List<string>();
                if (newDataset.Contains(DicomTag.LossyImageCompressionMethod))
                {
                    methods.AddRange(newDataset.GetValues<string>(DicomTag.LossyImageCompressionMethod));
                }

                methods.Add(outSyntax.LossyCompressionMethod);
                newDataset.AddOrUpdate(new DicomCodeString(DicomTag.LossyImageCompressionMethod, methods.ToArray()));

                double oldSize = oldPixelData.GetFrame(0).Size;
                double newSize = newPixelData.GetFrame(0).Size;

                List<string> ratios = new List<string>();
                if (newDataset.Contains(DicomTag.LossyImageCompressionRatio))
                {
                    ratios.AddRange(newDataset.GetValues<string>(DicomTag.LossyImageCompressionRatio));
                }
                
                ratios.Add(string.Format(CultureInfo.InvariantCulture, "{0:0.000}", oldSize / newSize));
                newDataset.AddOrUpdate(new DicomDecimalString(DicomTag.LossyImageCompressionRatio, ratios.ToArray()));
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
                if (output.Contains(dataTag))
                {
                    continue;
                }

                // If embedded overlay, Overlay Bits Allocated should equal Bits Allocated (#110).
                var bitsAlloc = output.GetSingleValueOrDefault(DicomTag.BitsAllocated, (ushort)0);
                output.AddOrUpdate(new DicomTag(overlay.Group, DicomTag.OverlayBitsAllocated.Element), bitsAlloc);

                var data = overlay.Data;
                if (output.InternalTransferSyntax.IsExplicitVR)
                {
                    output.AddOrUpdate(new DicomOtherByte(dataTag, data));
                }
                else
                {
                    output.AddOrUpdate(new DicomOtherWord(dataTag, data));
                }
            }
        }

        #endregion
    }
}
