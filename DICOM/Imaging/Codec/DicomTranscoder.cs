using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.ComponentModel.Composition.Hosting;

using Dicom.Imaging.Render;
using Dicom.IO;
using Dicom.IO.Buffer;
using Dicom.IO.Writer;
using Dicom.Log;

namespace Dicom.Imaging.Codec {
	public class DicomTranscoder {
		#region Static
		private static Dictionary<DicomTransferSyntax,IDicomCodec> _codecs = new Dictionary<DicomTransferSyntax,IDicomCodec>();

		static DicomTranscoder() {
			LoadCodecs(null, "Dicom.Native*.dll");
		}

		public static IDicomCodec GetCodec(DicomTransferSyntax syntax) {
			IDicomCodec codec = null;
			if (!_codecs.TryGetValue(syntax, out codec))
				throw new DicomCodecException("No codec registered for tranfer syntax: {0}", syntax);
			return codec;
		}

		public static void LoadCodecs(string path = null, string search = null) {
			if (path == null)
				path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

			var log = LogManager.Default.GetLogger("Dicom.Imaging.Codec");

			var catalog = (search == null) ?
				new DirectoryCatalog(path) :
				new DirectoryCatalog(path, search);
			var container = new CompositionContainer(catalog);
			foreach (var lazy in container.GetExports<IDicomCodec>()) {
				var codec = lazy.Value;
				log.Debug("Codec: {0}", codec.TransferSyntax.UID.Name);
				_codecs[codec.TransferSyntax] = codec;
			}
		}
		#endregion

		public DicomTranscoder(DicomTransferSyntax input, DicomTransferSyntax output) {
			InputSyntax = input;
			OutputSyntax = output;
		}

		public DicomTransferSyntax InputSyntax {
			get;
			private set;
		}

		public DicomCodecParams InputCodecParams {
			get;
			set;
		}

		private IDicomCodec _inputCodec;
		private IDicomCodec InputCodec {
			get {
				if (InputSyntax.IsEncapsulated && _inputCodec == null)
					_inputCodec = GetCodec(InputSyntax);
				return _inputCodec;
			}
		}

		public DicomTransferSyntax OutputSyntax {
			get;
			private set;
		}

		public DicomCodecParams OutputCodecParams {
			get;
			set;
		}

		private IDicomCodec _outputCodec;
		private IDicomCodec OutputCodec {
			get {
				if (OutputSyntax.IsEncapsulated && _outputCodec == null)
					_outputCodec = GetCodec(OutputSyntax);
				return _outputCodec;
			}
		}

		public DicomFile Transcode(DicomFile file) {
			DicomFile f = new DicomFile();
			f.FileMetaInfo.Add(file.FileMetaInfo);
			f.FileMetaInfo.TransferSyntax = OutputSyntax;
			f.Dataset.InternalTransferSyntax = OutputSyntax;
			f.Dataset.Add(Transcode(file.Dataset));
			return f;
		}

		public DicomDataset Transcode(DicomDataset dataset) {
			if (!dataset.Contains(DicomTag.PixelData)) {
				var newDataset = dataset.Clone();
				newDataset.InternalTransferSyntax = OutputSyntax;
				newDataset.RecalculateGroupLengths(false);
				return newDataset;
			}

			if (!InputSyntax.IsEncapsulated && !OutputSyntax.IsEncapsulated) {
				// transcode from uncompressed to uncompressed
				var newDataset = dataset.Clone();
				newDataset.InternalTransferSyntax = OutputSyntax;

				var oldPixelData = DicomPixelData.Create(dataset, false);
				var newPixelData = DicomPixelData.Create(newDataset, true);

				for (int i = 0; i < oldPixelData.NumberOfFrames; i++) {
					var frame = oldPixelData.GetFrame(i);
					newPixelData.AddFrame(frame);
				}

				ProcessOverlays(dataset, newDataset);

				newDataset.RecalculateGroupLengths(false);

				return newDataset;
			}

			if (InputSyntax.IsEncapsulated && OutputSyntax.IsEncapsulated) {
				// transcode from compressed to compressed
				var temp = Decode(dataset, DicomTransferSyntax.ExplicitVRLittleEndian, InputCodec, InputCodecParams);
				return Encode(temp, OutputSyntax, OutputCodec, OutputCodecParams);
			}

			if (InputSyntax.IsEncapsulated) {
				// transcode from compressed to uncompressed
				return Decode(dataset, OutputSyntax, InputCodec, InputCodecParams);
			}

			if (OutputSyntax.IsEncapsulated) {
				// transcode from uncompressed to compressed
				return Encode(dataset, OutputSyntax, OutputCodec, OutputCodecParams);
			}

			throw new DicomCodecException("Unable to find transcoding solution for {0} to {1}", InputSyntax.UID.Name, OutputSyntax.UID.Name);
		}

		/// <summary>
		/// Decompress single frame from DICOM dataset and return uncompressed frame buffer.
		/// </summary>
		/// <param name="dataset">DICOM dataset</param>
		/// <param name="frame">Frame number</param>
		/// <returns>Uncompressed frame buffer</returns>
		public IByteBuffer DecodeFrame(DicomDataset dataset, int frame) {
			var pixelData = DicomPixelData.Create(dataset, false);
			var buffer = pixelData.GetFrame(frame);

			// is pixel data already uncompressed?
			if (!dataset.InternalTransferSyntax.IsEncapsulated)
				return buffer;

			// clone dataset to prevent changes to source
			var cloneDataset = dataset.Clone();

			var oldPixelData = DicomPixelData.Create(cloneDataset, true);
			oldPixelData.AddFrame(buffer);

			var newDataset = Decode(cloneDataset, OutputSyntax, InputCodec, InputCodecParams);
			var newPixelData = DicomPixelData.Create(newDataset, false);

			return newPixelData.GetFrame(0);
		}

		public IPixelData DecodePixelData(DicomDataset dataset, int frame) {
			var pixelData = DicomPixelData.Create(dataset, false);
			
			// is pixel data already uncompressed?
			if (!dataset.InternalTransferSyntax.IsEncapsulated)
				return PixelDataFactory.Create(pixelData, frame);

			var buffer = pixelData.GetFrame(frame);

			// clone dataset to prevent changes to source
			var cloneDataset = dataset.Clone();

			var oldPixelData = DicomPixelData.Create(cloneDataset, true);
			oldPixelData.AddFrame(buffer);

			var newDataset = Decode(cloneDataset, OutputSyntax, InputCodec, InputCodecParams);
			var newPixelData = DicomPixelData.Create(newDataset, false);

			return PixelDataFactory.Create(newPixelData, 0);
		}

		private DicomDataset Decode(DicomDataset oldDataset, DicomTransferSyntax outSyntax, IDicomCodec codec, DicomCodecParams parameters) {
			DicomPixelData oldPixelData = DicomPixelData.Create(oldDataset, false);

			DicomDataset newDataset = oldDataset.Clone();
			newDataset.InternalTransferSyntax = outSyntax;
			DicomPixelData newPixelData = DicomPixelData.Create(newDataset, true);

			codec.Decode(oldPixelData, newPixelData, parameters);

			ProcessOverlays(oldDataset, newDataset);

			newDataset.RecalculateGroupLengths(false);

			return newDataset;
		}

		private DicomDataset Encode(DicomDataset oldDataset, DicomTransferSyntax inSyntax, IDicomCodec codec, DicomCodecParams parameters) {
			DicomPixelData oldPixelData = DicomPixelData.Create(oldDataset, false);

			DicomDataset newDataset = oldDataset.Clone();
			newDataset.InternalTransferSyntax = codec.TransferSyntax;
			DicomPixelData newPixelData = DicomPixelData.Create(newDataset, true);

			codec.Encode(oldPixelData, newPixelData, parameters);

			if (codec.TransferSyntax.IsLossy && newPixelData.NumberOfFrames > 0) {
				newDataset.Add(new DicomCodeString(DicomTag.LossyImageCompression, "01"));

				List<string> methods = new List<string>();
				if (newDataset.Contains(DicomTag.LossyImageCompressionMethod))
					methods.AddRange(newDataset.Get<string[]>(DicomTag.LossyImageCompressionMethod));
				methods.Add(codec.TransferSyntax.LossyCompressionMethod);
				newDataset.Add(new DicomCodeString(DicomTag.LossyImageCompressionMethod, methods.ToArray()));

				double oldSize = oldPixelData.GetFrame(0).Size;
				double newSize = newPixelData.GetFrame(0).Size;
				string ratio = String.Format("{0:0.000}", oldSize / newSize);
				newDataset.Add(new DicomDecimalString(DicomTag.LossyImageCompressionRatio, ratio));
			}

			ProcessOverlays(oldDataset, newDataset);

			newDataset.RecalculateGroupLengths(false);

			return newDataset;
		}

		private static void ProcessOverlays(DicomDataset input, DicomDataset output) {
			DicomOverlayData[] overlays = null;
			if (input.InternalTransferSyntax.IsEncapsulated)
				overlays = DicomOverlayData.FromDataset(output);
			else
				overlays = DicomOverlayData.FromDataset(input);

			foreach (var overlay in overlays) {
				var dataTag = new DicomTag(overlay.Group, DicomTag.OverlayData.Element);

				// don't run conversion on non-embedded overlays
				if (output.Contains(dataTag))
					continue;

				output.Add(new DicomTag(overlay.Group, DicomTag.OverlayBitsAllocated.Element), (ushort)1);
				output.Add(new DicomTag(overlay.Group, DicomTag.OverlayBitPosition.Element), (ushort)0);

				var data = overlay.Data;
				if (output.InternalTransferSyntax.IsExplicitVR)
					output.Add(new DicomOtherByte(dataTag, data));
				else
					output.Add(new DicomOtherWord(dataTag, data));
			}
		}

		public static DicomDataset ExtractOverlays(DicomDataset dataset) {
			if (!DicomOverlayData.HasEmbeddedOverlays(dataset))
				return dataset;

			dataset = dataset.Clone();

			var input = dataset;
			if (input.InternalTransferSyntax.IsEncapsulated)
				input = input.ChangeTransferSyntax(DicomTransferSyntax.ExplicitVRLittleEndian);

			ProcessOverlays(input, dataset);

			return dataset;
		}
	}
}
