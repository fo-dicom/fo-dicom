using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.IO;
using Dicom.IO.Buffer;

namespace Dicom.Imaging {
	public abstract class DicomPixelData {
		protected DicomPixelData(DicomDataset dataset) {
			Dataset = dataset;
			Syntax = dataset.InternalTransferSyntax;
		}

		public DicomDataset Dataset {
			get;
			private set;
		}

		public DicomTransferSyntax Syntax {
			get;
			private set;
		}

		public ushort Width {
			get { return Dataset.Get<ushort>(DicomTag.Columns); }
			set { Dataset.Add(new DicomUnsignedShort(DicomTag.Columns, value)); }
		}

		public ushort Height {
			get { return Dataset.Get<ushort>(DicomTag.Rows); }
			set { Dataset.Add(new DicomUnsignedShort(DicomTag.Rows, value)); }
		}

		public int NumberOfFrames {
			get { return Dataset.Get<ushort>(DicomTag.NumberOfFrames, (ushort)1); }
			set { Dataset.Add(new DicomIntegerString(DicomTag.NumberOfFrames, value)); }
		}

		public BitDepth BitDepth {
			get {
				return new BitDepth(BitsAllocated, BitsStored, HighBit, PixelRepresentation == PixelRepresentation.Signed);
			}
		}

		public ushort BitsAllocated {
			get { return Dataset.Get<ushort>(DicomTag.BitsAllocated); }
			set { Dataset.Add(new DicomUnsignedShort(DicomTag.BitsAllocated, value)); }
		}

		public ushort BitsStored {
			get { return Dataset.Get<ushort>(DicomTag.BitsStored); }
			set { Dataset.Add(new DicomUnsignedShort(DicomTag.BitsStored, value)); }
		}

		public ushort HighBit {
			get { return Dataset.Get<ushort>(DicomTag.HighBit); }
			set { Dataset.Add(new DicomUnsignedShort(DicomTag.HighBit, value)); }
		}

		public ushort SamplesPerPixel {
			get { return Dataset.Get<ushort>(DicomTag.SamplesPerPixel); }
			set { Dataset.Add(new DicomUnsignedShort(DicomTag.SamplesPerPixel, value)); }
		}

		public PixelRepresentation PixelRepresentation {
			get { return Dataset.Get<PixelRepresentation>(DicomTag.PixelRepresentation); }
			set { Dataset.Add(new DicomUnsignedShort(DicomTag.PixelRepresentation, (ushort)value)); }
		}

		public PlanarConfiguration PlanarConfiguration {
			get { return Dataset.Get<PlanarConfiguration>(DicomTag.PlanarConfiguration); }
			set { Dataset.Add(new DicomUnsignedShort(DicomTag.PlanarConfiguration, (ushort)value)); }
		}

		public PhotometricInterpretation PhotometricInterpretation {
			get { return Dataset.Get<PhotometricInterpretation>(DicomTag.PhotometricInterpretation); }
			set { Dataset.Add(new DicomCodeString(DicomTag.PhotometricInterpretation, value.Value)); }
		}

		public bool IsLossy {
			get { return Dataset.Get<string>(DicomTag.LossyImageCompression, "00") == "01"; }
		}

		public string LossyCompressionMethod {
			get { return Dataset.Get<string>(DicomTag.LossyImageCompressionMethod); }
		}

		public decimal LossyCompressionRatio {
			get { return Dataset.Get<decimal>(DicomTag.LossyImageCompressionRatio); }
		}

		public int BytesAllocated {
			get {
				int bytes = BitsAllocated / 8;
				if ((BitsAllocated % 8) > 0)
					bytes++;
				return bytes;
			}
		}

		public int UncompressedFrameSize {
			get {
				return BytesAllocated * SamplesPerPixel * Width * Height;
			}
		}

		public Color32[] PaletteColorLUT {
			get { return GetPaletteColorLUT(); }
			set { throw new NotImplementedException(); }
		}

		private Color32[] GetPaletteColorLUT() {
			if (PhotometricInterpretation != PhotometricInterpretation.PaletteColor)
				throw new DicomImagingException("Attempted to get Palette Color LUT from image with invalid photometric interpretation.");

			if (!Dataset.Contains(DicomTag.RedPaletteColorLookupTableDescriptor))
				throw new DicomImagingException("Palette Color LUT missing from dataset.");

			int size = Dataset.Get<int>(DicomTag.RedPaletteColorLookupTableDescriptor, 0);
			int first = Dataset.Get<int>(DicomTag.RedPaletteColorLookupTableDescriptor, 1);
			int bits = Dataset.Get<int>(DicomTag.RedPaletteColorLookupTableDescriptor, 2);

			var r = Dataset.Get<byte[]>(DicomTag.RedPaletteColorLookupTableData);
			var g = Dataset.Get<byte[]>(DicomTag.GreenPaletteColorLookupTableData);
			var b = Dataset.Get<byte[]>(DicomTag.BluePaletteColorLookupTableData);

			var lut = new Color32[size];

			if (r.Length == size) {
				// 8-bit LUT entries
				for (int i = 0; i < size; i++)
					lut[i] = new Color32(0xff, r[i], g[i], b[i]);
			} else {
				// 16-bit LUT entries... we only support 8-bit until someone can find a sample image with a 16-bit palette

				// 8-bit entries with 16-bits allocated
				int offset = 0;

				// 16-bit entries with 8-bits stored
				if (bits == 16)
					offset = 1;

				for (int i = 0; i < size; i++, offset += 2)
					lut[i] = new Color32(0xff, r[offset], g[offset], b[offset]);
			}

			return lut;
		}

		public abstract IByteBuffer GetFrame(int frame);

		public abstract void AddFrame(IByteBuffer data);

		public static DicomPixelData Create(DicomDataset dataset, bool newPixelData = false) {
			var syntax = dataset.InternalTransferSyntax;

			if (newPixelData) {
				if (syntax == DicomTransferSyntax.ImplicitVRLittleEndian)
					return new OtherWordPixelData(dataset, true);

				if (syntax.IsEncapsulated)
					return new EncapsulatedPixelData(dataset, true);

				if (dataset.Get<ushort>(DicomTag.BitsAllocated) == 16)
					return new OtherWordPixelData(dataset, true);
				else
					return new OtherBytePixelData(dataset, true);
			} else {
				var item = dataset.Get<DicomItem>(DicomTag.PixelData);
				if (item == null)
					throw new DicomImagingException("DICOM dataset is missing pixel data element.");

				if (item is DicomOtherByte)
					return new OtherBytePixelData(dataset, false);
				if (item is DicomOtherWord)
					return new OtherWordPixelData(dataset, false);
				if (item is DicomOtherByteFragment || item is DicomOtherWordFragment)
					return new EncapsulatedPixelData(dataset, false);

				throw new DicomImagingException("Unexpected or unhandled pixel data element type: {0}", item.GetType());
			}
		}

		private class OtherBytePixelData : DicomPixelData {
			public OtherBytePixelData(DicomDataset dataset, bool newPixelData) : base(dataset) {
				if (newPixelData) {
					NumberOfFrames = 0;
					Element = new DicomOtherByte(DicomTag.PixelData, new CompositeByteBuffer());
					Dataset.Add(Element);
				} else
					Element = dataset.Get<DicomOtherByte>(DicomTag.PixelData);
			}

			public DicomOtherByte Element {
				get;
				private set;
			}

			public override IByteBuffer GetFrame(int frame) {
				int offset = UncompressedFrameSize * frame;
				return new RangeByteBuffer(Element.Buffer, (uint)offset, (uint)UncompressedFrameSize);
			}

			public override void AddFrame(IByteBuffer data) {
				if (!(Element.Buffer is CompositeByteBuffer))
					throw new DicomImagingException("Expected pixel data element to have a CompositeByteBuffer");

				CompositeByteBuffer buffer = Element.Buffer as CompositeByteBuffer;
				buffer.Buffers.Add(data);

				NumberOfFrames++;
			}
		}

		private class OtherWordPixelData : DicomPixelData {
			public OtherWordPixelData(DicomDataset dataset, bool newPixelData) : base(dataset) {
				if (newPixelData) {
					NumberOfFrames = 0;
					Element = new DicomOtherWord(DicomTag.PixelData, new CompositeByteBuffer());
					Dataset.Add(Element);
				} else
					Element = dataset.Get<DicomOtherWord>(DicomTag.PixelData);
			}

			public DicomOtherWord Element {
				get;
				private set;
			}

			public override IByteBuffer GetFrame(int frame) {
				int offset = UncompressedFrameSize * frame;
				IByteBuffer buffer = new RangeByteBuffer(Element.Buffer, (uint)offset, (uint)UncompressedFrameSize);

				if (BytesAllocated == 1 && Syntax.Endian == Endian.Little)
					buffer = new SwapByteBuffer(buffer, 2);

				return buffer;
			}

			public override void AddFrame(IByteBuffer data) {
				if (!(Element.Buffer is CompositeByteBuffer))
					throw new DicomImagingException("Expected pixel data element to have a CompositeByteBuffer.");

				CompositeByteBuffer buffer = Element.Buffer as CompositeByteBuffer;
				if (BytesAllocated == 1)
					data = new SwapByteBuffer(buffer, 2);
				buffer.Buffers.Add(data);

				NumberOfFrames++;
			}
		}

		private class EncapsulatedPixelData : DicomPixelData {
			public EncapsulatedPixelData(DicomDataset dataset, bool newPixelData) : base(dataset) {
				if (newPixelData) {
					NumberOfFrames = 0;
					Element = new DicomOtherByteFragment(DicomTag.PixelData);
					Dataset.Add(Element);
				} else
					Element = dataset.Get<DicomFragmentSequence>(DicomTag.PixelData);
			}

			public DicomFragmentSequence Element {
				get;
				private set;
			}

			public override IByteBuffer GetFrame(int frame) {
				IByteBuffer buffer = null;

				if (NumberOfFrames == 1) {
					if (Element.Fragments.Count == 1)
						buffer = Element.Fragments[0];
					else
						buffer = new CompositeByteBuffer(Element.Fragments.ToArray());
				}
				else if (Element.Fragments.Count == NumberOfFrames)
					buffer = Element.Fragments[frame];
				else if (Element.OffsetTable.Count == NumberOfFrames) {
					uint start = Element.OffsetTable[frame];
					uint stop = (Element.OffsetTable.Count == (frame + 1)) ? uint.MaxValue : Element.OffsetTable[frame + 1];

					var composite = new CompositeByteBuffer();

					uint pos = 0;
					int frag = 0;

					while (pos < start && frag < Element.Fragments.Count) {
						pos += 8;
						pos += Element.Fragments[frag].Size;
						frag++;
					}

					if (pos != start)
						throw new DicomImagingException("Fragment start position does not match offset table.");

					while (pos < stop && frag < Element.Fragments.Count) {
						composite.Buffers.Add(Element.Fragments[frag]);

						pos += 8;
						pos += Element.Fragments[frag].Size;
						frag++;
					}

					if (pos < stop && stop != uint.MaxValue)
						throw new DicomImagingException("Image frame truncated while reading fragments from offset table.");

					buffer = composite;
				}
				else
					throw new DicomImagingException("Support for multi-frame images with varying fragment sizes and no offset table has not been implemented.");

				return EndianByteBuffer.Create(buffer, Syntax.Endian, BytesAllocated);
			}

			public override void AddFrame(IByteBuffer data) {
				NumberOfFrames++;

				long pos = Element.Fragments.Sum(x => (long)x.Size + 8);
				if (pos < uint.MaxValue) {
					Element.OffsetTable.Add((uint)pos);
				} else {
					// do not create an offset table for very large datasets
					Element.OffsetTable.Clear();
				}

				data = EndianByteBuffer.Create(data, Syntax.Endian, BytesAllocated);
				Element.Fragments.Add(data);
			}
		}
	}
}
