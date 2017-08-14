// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Linq;

using Dicom.IO.Buffer;

namespace Dicom.Imaging
{
    /// <summary>
    /// DICOM Pixel Data abstract class for reading and writing DICOM images pixel data according to the specified transfer syntax
    /// </summary>
    public abstract class DicomPixelData
    {
        /// <summary>
        /// Initializes new instance of <see cref="DicomPixelData"/> using passed <paramref name="dataset"/>.
        /// </summary>
        /// <param name="dataset"></param>
        protected DicomPixelData(DicomDataset dataset)
        {
            Dataset = dataset;
            Syntax = dataset.InternalTransferSyntax;
        }

        /// <summary>
        /// Gets the DICOM Dataset containing the pixel data.
        /// </summary>
        public DicomDataset Dataset { get; }

        /// <summary>
        /// Gets the transfer syntax used to encode the DICOM iamge pixel data
        /// </summary>
        public DicomTransferSyntax Syntax { get; private set; }

        /// <summary>
        /// Gets or sets the DICOM image width (columns) in pixels.
        /// </summary>
        public ushort Width
        {
            get
            {
                return Dataset.Get<ushort>(DicomTag.Columns);
            }
            set
            {
                Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.Columns, value));
            }
        }

        /// <summary>
        /// Gets or sets the DICOM image height (rows) in pixels.
        /// </summary>
        public ushort Height
        {
            get
            {
                return Dataset.Get<ushort>(DicomTag.Rows);
            }
            set
            {
                Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.Rows, value));
            }
        }


        /// <summary>
        /// Gets or sets DICOM image Number of frames. This value usually equals 1 for single frame images.
        /// </summary>
        public int NumberOfFrames
        {
            get
            {
                return Dataset.Get(DicomTag.NumberOfFrames, (ushort)1);
            }
            set
            {
                Dataset.AddOrUpdate(new DicomIntegerString(DicomTag.NumberOfFrames, value));
            }
        }

        /// <summary>
        /// Gets new instance of <seealso cref="BitDepth"/> using dataset information.
        /// </summary>
        public BitDepth BitDepth => new BitDepth(
            BitsAllocated,
            BitsStored,
            HighBit,
            PixelRepresentation == PixelRepresentation.Signed);

        /// <summary>
        /// Gets number of bits allocated per pixel sample (0028,0100).
        /// </summary>
        public ushort BitsAllocated => Dataset.Get<ushort>(DicomTag.BitsAllocated);

        /// <summary>
        /// Gets or sets number of bits stored per pixel sample (0028,0101).
        /// </summary>
        public ushort BitsStored
        {
            get
            {
                return Dataset.Get<ushort>(DicomTag.BitsStored);
            }
            set
            {
                if (value > BitsAllocated)
                    throw new DicomImagingException($"Value: {value} > Bits Allocated: {BitsAllocated}");

                Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.BitsStored, value));
            }
        }

        /// <summary>
        /// Gets or sets index of the most signficant bit (MSB) of pixel sample(0028,0102).
        /// </summary>
        public ushort HighBit
        {
            get
            {
                return Dataset.Get<ushort>(DicomTag.HighBit);
            }
            set
            {
                if (value >= BitsAllocated)
                    throw new DicomImagingException(
                        $"Value: {value} >= Bits Allocated: {BitsAllocated}");

                Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.HighBit, value));
            }
        }

        /// <summary>
        /// Gets or sets number of samples per pixel (0028,0002), usually 1 for grayscale and 3 for color (RGB and YBR.
        /// </summary> 
        public ushort SamplesPerPixel
        {
            get
            {
                return Dataset.Get(DicomTag.SamplesPerPixel, (ushort)1);
            }
            set
            {
                Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.SamplesPerPixel, value));
            }
        }

        /// <summary>
        /// Gets or sets, pixel Representation (0028,0103), represents signed/unsigned data of the pixel samples.
        /// </summary>
        public PixelRepresentation PixelRepresentation
        {
            get
            {
                return Dataset.Get<PixelRepresentation>(DicomTag.PixelRepresentation);
            }
            set
            {
                Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.PixelRepresentation, (ushort)value));
            }
        }

        /// <summary>
        /// Gets or sets planar Configuration (0028,0006), indicates whether the color pixel data are sent color-by-plane
        /// or color-by-pixel.
        /// </summary>
        public PlanarConfiguration PlanarConfiguration
        {
            get
            {
                return Dataset.Get(DicomTag.PlanarConfiguration, PlanarConfiguration.Interleaved);
            }
            set
            {
                Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.PlanarConfiguration, (ushort)value));
            }
        }

        /// <summary>
        /// Gets or sets photometric Interpretation.
        /// </summary>
        public PhotometricInterpretation PhotometricInterpretation
        {
            get
            {
                return Dataset.Get<PhotometricInterpretation>(DicomTag.PhotometricInterpretation, null);
            }
            set
            {
                Dataset.AddOrUpdate(new DicomCodeString(DicomTag.PhotometricInterpretation, value.Value));
            }
        }

        /// <summary>
        /// Gets lossy image compression (0028,2110) status, returns true if stored value is "01".
        /// </summary>
        public bool IsLossy => Dataset.Get<string>(DicomTag.LossyImageCompression, "00") == "01";

        /// <summary>
        /// Gets lossy image compression method (0028,2114).
        /// </summary>
        public string LossyCompressionMethod => Dataset.Get<string>(DicomTag.LossyImageCompressionMethod);

        /// <summary>
        /// Gets lossy image compression ratio (0028,2112).
        /// </summary>
        public decimal LossyCompressionRatio => Dataset.Get<decimal>(DicomTag.LossyImageCompressionRatio);

        /// <summary>
        /// Gets number of bytes allocated per pixel sample.
        /// </summary>
        public int BytesAllocated
        {
            get
            {
                var bytes = BitsAllocated / 8;
                if (BitsAllocated % 8 > 0) bytes++;
                return bytes;
            }
        }

        /// <summary>
        /// Gets uncompressed frame size in bytes.
        /// </summary>
        public int UncompressedFrameSize
        {
            get
            {
                if (BitsAllocated == 1)
                {
                    var bytes = Width * Height / 8;
                    if ((Width * Height) % 8 > 0) bytes++;
                    return bytes;
                }

                // Issue #471, handle special case with invalid uneven width for YBR_*_422 and YBR_PARTIAL_420 images
                var actualWidth = Width;
                if (actualWidth % 2 != 0 &&
                    (PhotometricInterpretation == PhotometricInterpretation.YbrFull422 ||
                     PhotometricInterpretation == PhotometricInterpretation.YbrPartial422 ||
                     PhotometricInterpretation == PhotometricInterpretation.YbrPartial420))
                {
                    ++actualWidth;
                }

                return BytesAllocated * SamplesPerPixel * actualWidth * Height;
            }
        }

        /// <summary>
        /// Gets palette color LUT, valid for PALETTE COLOR <seealso cref="PhotometricInterpretation"/>
        /// </summary>
        public Color32[] PaletteColorLUT => GetPaletteColorLUT();

        /// <summary>
        /// Extracts the palette color LUT from DICOM dataset, valid for PALETTE COLOR <seealso cref="PhotometricInterpretation"/>
        /// </summary>
        /// <returns>Palette color LUT</returns>
        /// <exception cref="DicomImagingException">Invalid photometric interpretation or plaette color lUT missing from database</exception>
        private Color32[] GetPaletteColorLUT()
        {
            if (PhotometricInterpretation != PhotometricInterpretation.PaletteColor)
                throw new DicomImagingException(
                    "Attempted to get Palette Color LUT from image with invalid photometric interpretation.");

            if (!Dataset.Contains(DicomTag.RedPaletteColorLookupTableDescriptor)) throw new DicomImagingException("Palette Color LUT missing from dataset.");

            var size = Dataset.Get<int>(DicomTag.RedPaletteColorLookupTableDescriptor, 0);
            var bits = Dataset.Get<int>(DicomTag.RedPaletteColorLookupTableDescriptor, 2);

            var r = Dataset.Get<byte[]>(DicomTag.RedPaletteColorLookupTableData);
            var g = Dataset.Get<byte[]>(DicomTag.GreenPaletteColorLookupTableData);
            var b = Dataset.Get<byte[]>(DicomTag.BluePaletteColorLookupTableData);

            // If the LUT size is 0, that means it's 65536 in size.
            if (size == 0) size = 65536;

            var lut = new Color32[size];

            if (r.Length == size)
            {
                // 8-bit LUT entries
                for (var i = 0; i < size; i++) lut[i] = new Color32(0xff, r[i], g[i], b[i]);
            }
            else
            {
                // 16-bit LUT entries... we only support 8-bit until someone can find a sample image with a 16-bit palette

                // 8-bit entries with 16-bits allocated
                var offset = 0;

                // 16-bit entries with 8-bits stored
                if (bits == 16) offset = 1;

                for (var i = 0; i < size; i++, offset += 2) lut[i] = new Color32(0xff, r[offset], g[offset], b[offset]);
            }

            return lut;
        }

        /// <summary>
        /// Abstract GetFrame method to extract specific frame byte buffer <paramref name="frame"/> dataset
        /// </summary>
        /// <param name="frame">Frame index</param>
        /// <returns>Frame byte buffer</returns>
        public abstract IByteBuffer GetFrame(int frame);

        /// <summary>
        /// Abstract AddFrame method to add new frame into dataset pixel dataset. New frame will be appended to existing frames.
        /// </summary>
        /// <param name="data">Frame byte buffer.</param>
        public abstract void AddFrame(IByteBuffer data);

        /// <summary>
        /// A factory method to initialize new instance of <seealso cref="DicomPixelData"/> implementation either 
        /// <seealso cref="OtherWordPixelData"/>, <seealso cref="OtherBytePixelData"/>, or <seealso cref="EncapsulatedPixelData"/>
        /// </summary>
        /// <param name="dataset">Source DICOM Dataset</param>
        /// <param name="newPixelData">true if new <seealso cref="DicomPixelData"/>will be created for current dataset,
        /// false to read <seealso cref="DicomPixelData"/> from <paramref name="dataset"/>.
        /// Default is false (read)</param>
        /// <returns>New instance of DicomPixelData</returns>
        public static DicomPixelData Create(DicomDataset dataset, bool newPixelData = false)
        {
            var syntax = dataset.InternalTransferSyntax;

            if (newPixelData)
            {
                if (syntax == DicomTransferSyntax.ImplicitVRLittleEndian) return new OtherWordPixelData(dataset, true);
                if (syntax.IsEncapsulated) return new EncapsulatedPixelData(dataset, true);

                var bitsAllocated = dataset.Get<ushort>(DicomTag.BitsAllocated);
                if (bitsAllocated > 16)
                    throw new DicomImagingException(
                        $"Cannot represent non-encapsulated data with Bits Allocated: {bitsAllocated} > 16");

                return bitsAllocated > 8
                    ? (DicomPixelData) new OtherWordPixelData(dataset, true)
                    : new OtherBytePixelData(dataset, true);
            }

            var item = dataset.Get<DicomItem>(DicomTag.PixelData);
            if (item == null) throw new DicomImagingException("DICOM dataset is missing pixel data element.");

            if (item is DicomOtherByte) return new OtherBytePixelData(dataset, false);
            if (item is DicomOtherWord) return new OtherWordPixelData(dataset, false);
            if (item is DicomOtherByteFragment || item is DicomOtherWordFragment) return new EncapsulatedPixelData(dataset, false);

            throw new DicomImagingException("Unexpected or unhandled pixel data element type: {0}", item.GetType());
        }

        /// <summary>
        /// Other Byte (OB) implementation of <seealso cref="DicomPixelData"/>
        /// </summary>
        private class OtherBytePixelData : DicomPixelData
        {
            /// <summary>
            /// Initialize new instance of OtherBytePixelData
            /// </summary>
            /// <param name="dataset">The source dataset to extract from or create new pixel data for</param>
            /// <param name="newPixelData">True to create new pixel data, false to read pixel data</param>
            public OtherBytePixelData(DicomDataset dataset, bool newPixelData)
                : base(dataset)
            {
                if (newPixelData)
                {
                    NumberOfFrames = 0;
                    Element = new DicomOtherByte(DicomTag.PixelData, new CompositeByteBuffer());
                    Dataset.AddOrUpdate(Element);
                }
                else Element = dataset.Get<DicomOtherByte>(DicomTag.PixelData);
            }

            /// <summary>
            /// The pixel data other byte (OB) element
            /// </summary>
            private DicomOtherByte Element { get; }

            public override IByteBuffer GetFrame(int frame)
            {
                if (frame < 0 || frame >= NumberOfFrames) throw new IndexOutOfRangeException("Requested frame out of range!");

                var offset = UncompressedFrameSize * frame;
                return new RangeByteBuffer(Element.Buffer, (uint)offset, (uint)UncompressedFrameSize);
            }

            public override void AddFrame(IByteBuffer data)
            {
                if (!(Element.Buffer is CompositeByteBuffer)) throw new DicomImagingException("Expected pixel data element to have a CompositeByteBuffer");

                var buffer = (CompositeByteBuffer)Element.Buffer;
                buffer.Buffers.Add(data);

                NumberOfFrames++;
            }
        }

        /// <summary>
        /// Other Word (OW) implementation of <seealso cref="DicomPixelData"/>
        /// </summary>
        private class OtherWordPixelData : DicomPixelData
        {
            /// <summary>
            /// Initialize new instance of OtherWordPixelData
            /// </summary>
            /// <param name="dataset">The source dataset to extract from or create new pixel data for</param>
            /// <param name="newPixelData">True to create new pixel data, false to read pixel data</param>
            public OtherWordPixelData(DicomDataset dataset, bool newPixelData)
                : base(dataset)
            {
                if (newPixelData)
                {
                    NumberOfFrames = 0;
                    Element = new DicomOtherWord(DicomTag.PixelData, new CompositeByteBuffer());
                    Dataset.AddOrUpdate(Element);
                }
                else Element = dataset.Get<DicomOtherWord>(DicomTag.PixelData);
            }

            /// <summary>
            /// The pixel data other word (OW) element
            /// </summary>
            private DicomOtherWord Element { get; }

            public override IByteBuffer GetFrame(int frame)
            {
                if (frame < 0 || frame >= NumberOfFrames) throw new IndexOutOfRangeException("Requested frame out of range!");

                var offset = UncompressedFrameSize * frame;
                IByteBuffer buffer = new RangeByteBuffer(Element.Buffer, (uint)offset, (uint)UncompressedFrameSize);

                // mainly for GE Private Implicit VR Big Endian
                if (Syntax.SwapPixelData) buffer = new SwapByteBuffer(buffer, 2);

                return buffer;
            }

            public override void AddFrame(IByteBuffer data)
            {
                if (!(Element.Buffer is CompositeByteBuffer)) throw new DicomImagingException("Expected pixel data element to have a CompositeByteBuffer.");

                var buffer = (CompositeByteBuffer)Element.Buffer;

                if (Syntax.SwapPixelData) data = new SwapByteBuffer(data, 2);

                buffer.Buffers.Add(data);
                NumberOfFrames++;
            }
        }

        /// <summary>
        /// Other Byte Fragment implementation of <seealso cref="DicomPixelData"/>, used for handling encapsulated (compressed)
        /// pixel data
        /// </summary>
        private class EncapsulatedPixelData : DicomPixelData
        {
            /// <summary>
            /// Initialize new instance of EncapsulatedPixelData
            /// </summary>
            /// <param name="dataset">The source dataset to extract from or create new pixel data for</param>
            /// <param name="newPixelData">True to create new pixel data, false to read pixel data</param>
            public EncapsulatedPixelData(DicomDataset dataset, bool newPixelData)
                : base(dataset)
            {
                if (newPixelData)
                {
                    NumberOfFrames = 0;
                    Element = new DicomOtherByteFragment(DicomTag.PixelData);
                    Dataset.AddOrUpdate(Element);
                }
                else Element = dataset.Get<DicomFragmentSequence>(DicomTag.PixelData);
            }

            /// <summary>
            /// The pixel data framgent sequence element
            /// </summary>
            private DicomFragmentSequence Element { get; }

            public override IByteBuffer GetFrame(int frame)
            {
                if (frame < 0 || frame >= NumberOfFrames) throw new IndexOutOfRangeException("Requested frame out of range!");

                IByteBuffer buffer = null;

                if (NumberOfFrames == 1)
                {
                    if (Element.Fragments.Count == 1) buffer = Element.Fragments[0];
                    else buffer = new CompositeByteBuffer(Element.Fragments.ToArray());
                }
                else if (Element.Fragments.Count == NumberOfFrames) buffer = Element.Fragments[frame];
                else if (Element.OffsetTable.Count == NumberOfFrames)
                {
                    var start = Element.OffsetTable[frame];
                    var stop = (Element.OffsetTable.Count == (frame + 1))
                                    ? uint.MaxValue
                                    : Element.OffsetTable[frame + 1];

                    var composite = new CompositeByteBuffer();

                    uint pos = 0;
                    var frag = 0;

                    while (pos < start && frag < Element.Fragments.Count)
                    {
                        pos += 8;
                        pos += Element.Fragments[frag].Size;
                        frag++;
                    }

                    if (pos != start) throw new DicomImagingException("Fragment start position does not match offset table.");

                    while (pos < stop && frag < Element.Fragments.Count)
                    {
                        composite.Buffers.Add(Element.Fragments[frag]);

                        pos += 8;
                        pos += Element.Fragments[frag].Size;
                        frag++;
                    }

                    if (pos < stop && stop != uint.MaxValue)
                        throw new DicomImagingException(
                            "Image frame truncated while reading fragments from offset table.");

                    buffer = composite;
                }
                else
                    throw new DicomImagingException(
                        "Support for multi-frame images with varying fragment sizes and no offset table has not been implemented.");

                // mainly for GE Private Implicit VR Little Endian
                if (!Syntax.IsEncapsulated && BitsAllocated == 16 && Syntax.SwapPixelData) buffer = new SwapByteBuffer(buffer, 2);

                return EndianByteBuffer.Create(buffer, Syntax.Endian, BytesAllocated);
            }

            public override void AddFrame(IByteBuffer data)
            {
                NumberOfFrames++;

                var pos = Element.Fragments.Sum(x => (long)x.Size + 8);
                if (pos < uint.MaxValue)
                {
                    Element.OffsetTable.Add((uint)pos);
                }
                else
                {
                    // do not create an offset table for very large datasets
                    Element.OffsetTable.Clear();
                }

                data = EndianByteBuffer.Create(data, Syntax.Endian, BytesAllocated);
                Element.Fragments.Add(data);
            }
        }
    }
}
