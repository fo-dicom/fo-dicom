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
				if (item is DicomOtherByteFragment)
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
				else if (Syntax.Endian == Endian.Big)
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
					Element = dataset.Get<DicomOtherByteFragment>(DicomTag.PixelData);
			}

			public DicomOtherByteFragment Element {
				get;
				private set;
			}

			public override IByteBuffer GetFrame(int frame) {
				if (NumberOfFrames == 1) {
					if (Element.Fragments.Count == 1)
						return EndianByteBuffer.Create(Element.Fragments[0], Syntax.Endian, BytesAllocated);
					else
						return EndianByteBuffer.Create(
							new CompositeByteBuffer(Element.Fragments.ToArray()),
							Syntax.Endian, BytesAllocated);
				}

				if (Element.Fragments.Count == NumberOfFrames)
					return EndianByteBuffer.Create(Element.Fragments[frame], Syntax.Endian, BytesAllocated);

				if (Element.OffsetTable.Count == NumberOfFrames) {
					uint start = Element.OffsetTable[frame];
					uint stop = (Element.OffsetTable.Count == (frame + 1)) ? uint.MaxValue : Element.OffsetTable[frame + 1];

					CompositeByteBuffer buffer = new CompositeByteBuffer();

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
						buffer.Buffers.Add(Element.Fragments[frag]);

						pos += 8;
						pos += Element.Fragments[frag].Size;
						frag++;
					}

					if (pos < stop && stop != uint.MaxValue)
						throw new DicomImagingException("Image frame truncated while reading fragments from offset table.");

					return EndianByteBuffer.Create(buffer, Syntax.Endian, BytesAllocated);
				}

				throw new DicomImagingException("Support for multi-frame images with varying fragment sizes and no offset table has not been implemented.");
			}

			public override void AddFrame(IByteBuffer data) {
				NumberOfFrames++;

				if (NumberOfFrames > 0) {
					if (NumberOfFrames == 1)
						Element.OffsetTable.Add(0u); // first frame

					uint pos = (uint)Element.Fragments.Sum(x => x.Size + 8);
					Element.OffsetTable.Add(pos);
				}

				data = EndianByteBuffer.Create(data, Syntax.Endian, BytesAllocated);
				Element.Fragments.Add(data);
			}
		}
	}
}
