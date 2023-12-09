// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Linq;
using FellowOakDicom.IO.Buffer;

namespace FellowOakDicom.Imaging
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
            get => Dataset.GetSingleValue<ushort>(DicomTag.Columns);
            set => Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.Columns, value));
        }

        /// <summary>
        /// Gets or sets the DICOM image height (rows) in pixels.
        /// </summary>
        public ushort Height
        {
            get => Dataset.GetSingleValue<ushort>(DicomTag.Rows);
            set => Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.Rows, value));
        }


        /// <summary>
        /// Gets or sets DICOM image Number of frames. This value usually equals 1 for single frame images.
        /// </summary>
        public int NumberOfFrames
        {
            get => Dataset.GetSingleValueOrDefault(DicomTag.NumberOfFrames, 1);
            set => Dataset.AddOrUpdate(new DicomIntegerString(DicomTag.NumberOfFrames, value));
        }

        /// <summary>
        /// Gets new instance of <see cref="BitDepth"/> using dataset information.
        /// </summary>
        public BitDepth BitDepth => new BitDepth(
            BitsAllocated,
            BitsStored,
            HighBit,
            PixelRepresentation == PixelRepresentation.Signed);

        /// <summary>
        /// Gets number of bits allocated per pixel sample (0028,0100).
        /// </summary>
        public ushort BitsAllocated => Dataset.GetSingleValue<ushort>(DicomTag.BitsAllocated);

        /// <summary>
        /// Gets or sets number of bits stored per pixel sample (0028,0101).
        /// </summary>
        public ushort BitsStored
        {
            get => Dataset.GetSingleValue<ushort>(DicomTag.BitsStored);
            set
            {
                if (value > BitsAllocated)
                {
                    throw new DicomImagingException($"Value: {value} > Bits Allocated: {BitsAllocated}");
                }

                Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.BitsStored, value));
            }
        }

        /// <summary>
        /// Gets or sets index of the most signficant bit (MSB) of pixel sample(0028,0102).
        /// </summary>
        public ushort HighBit
        {
            get => Dataset.GetSingleValue<ushort>(DicomTag.HighBit);
            set
            {
                if (value >= BitsAllocated)
                {
                    throw new DicomImagingException($"Value: {value} >= Bits Allocated: {BitsAllocated}");
                }

                Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.HighBit, value));
            }
        }

        /// <summary>
        /// Gets or sets number of samples per pixel (0028,0002), usually 1 for grayscale and 3 for color (RGB and YBR.
        /// </summary> 
        public ushort SamplesPerPixel
        {
            get => Dataset.GetSingleValueOrDefault(DicomTag.SamplesPerPixel, (ushort)1);
            set => Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.SamplesPerPixel, value));
        }

        /// <summary>
        /// Gets or sets, pixel Representation (0028,0103), represents signed/unsigned data of the pixel samples.
        /// </summary>
        public PixelRepresentation PixelRepresentation
        {
            get => Dataset.GetSingleValue<PixelRepresentation>(DicomTag.PixelRepresentation);
            set => Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.PixelRepresentation, (ushort)value));
        }

        /// <summary>
        /// Gets or sets planar Configuration (0028,0006), indicates whether the color pixel data are sent color-by-plane
        /// or color-by-pixel.
        /// </summary>
        public PlanarConfiguration PlanarConfiguration
        {
            get => Dataset.GetSingleValueOrDefault(DicomTag.PlanarConfiguration, PlanarConfiguration.Interleaved);
            set => Dataset.AddOrUpdate(new DicomUnsignedShort(DicomTag.PlanarConfiguration, (ushort)value));
        }

        /// <summary>
        /// Gets or sets photometric Interpretation.
        /// </summary>
        public PhotometricInterpretation PhotometricInterpretation
        {
            get => Dataset.GetSingleValueOrDefault<PhotometricInterpretation>(DicomTag.PhotometricInterpretation, null);
            set => Dataset.AddOrUpdate(new DicomCodeString(DicomTag.PhotometricInterpretation, value.Value));
        }

        /// <summary>
        /// Gets lossy image compression (0028,2110) status, returns true if stored value is "01".
        /// </summary>
        public bool IsLossy => Dataset.TryGetSingleValue(DicomTag.LossyImageCompression, out string dummy) && dummy == "01";

        /// <summary>
        /// Gets lossy image compression method (0028,2114).
        /// </summary>
        public string LossyCompressionMethod => Dataset.GetSingleValue<string>(DicomTag.LossyImageCompressionMethod);

        /// <summary>
        /// Gets lossy image compression ratio (0028,2112).
        /// </summary>
        public decimal LossyCompressionRatio => Dataset.GetSingleValue<decimal>(DicomTag.LossyImageCompressionRatio);

        /// <summary>
        /// Gets number of bytes allocated per pixel sample.
        /// </summary>
        public int BytesAllocated => (BitsAllocated - 1) / 8 + 1;

        /// <summary>
        /// Gets uncompressed frame size in bytes.
        /// </summary>
        public int UncompressedFrameSize
        {
            get
            {
                if (BitsAllocated == 1)
                {
                    return (Width * Height - 1) / 8 + 1;
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
                
                // Issue #645, handle special case with uncompressed YBR_FULL_422 images
                // chrominance channels are downsampled to 2 (nominally still 3)
                if (PhotometricInterpretation == PhotometricInterpretation.YbrFull422)
                {
                    if (Syntax == DicomTransferSyntax.ExplicitVRBigEndian ||
                        Syntax == DicomTransferSyntax.ExplicitVRLittleEndian ||
                        Syntax == DicomTransferSyntax.ImplicitVRBigEndian ||
                        Syntax == DicomTransferSyntax.ImplicitVRLittleEndian)
                    {
                        return BytesAllocated * 2 * actualWidth * Height;
                    }
                }

                return BytesAllocated * SamplesPerPixel * actualWidth * Height;
            }
        }

        /// <summary>
        /// Gets palette color LUT, valid for PALETTE COLOR <see cref="PhotometricInterpretation"/>
        /// </summary>
        public Color32[] PaletteColorLUT => GetPaletteColorLUT();

        /// <summary>
        /// Extracts the palette color LUT from DICOM dataset, valid for PALETTE COLOR <see cref="PhotometricInterpretation"/>
        /// </summary>
        /// <returns>Palette color LUT</returns>
        /// <exception cref="DicomImagingException">Invalid photometric interpretation or plaette color lUT missing from database</exception>
        private Color32[] GetPaletteColorLUT()
        {
            if (PhotometricInterpretation != PhotometricInterpretation.PaletteColor)
            {
                throw new DicomImagingException("Attempted to get Palette Color LUT from image with invalid photometric interpretation.");
            }

            if (!Dataset.Contains(DicomTag.RedPaletteColorLookupTableDescriptor))
            {
                throw new DicomImagingException("Palette Color LUT missing from dataset.");
            }

            var size = Dataset.GetValue<int>(DicomTag.RedPaletteColorLookupTableDescriptor, 0);
            var bits = Dataset.GetValue<int>(DicomTag.RedPaletteColorLookupTableDescriptor, 2);

            var r = Dataset.GetValues<byte>(DicomTag.RedPaletteColorLookupTableData);
            var g = Dataset.GetValues<byte>(DicomTag.GreenPaletteColorLookupTableData);
            var b = Dataset.GetValues<byte>(DicomTag.BluePaletteColorLookupTableData);

            // If the LUT size is 0, that means it's 65536 in size.
            if (size == 0)
            {
                size = 65536;
            }

            var lut = new Color32[size];

            if (r.Length == size)
            {
                // 8-bit LUT entries
                for (var i = 0; i < size; i++)
                {
                    lut[i] = new Color32(0xff, r[i], g[i], b[i]);
                }
            }
            else
            {
                // 16-bit LUT entries... we only support 8-bit until someone can find a sample image with a 16-bit palette

                // 8-bit entries with 16-bits allocated
                var offset = 0;

                // 16-bit entries with 8-bits stored
                if (bits == 16)
                {
                    offset = 1;
                }

                for (var i = 0; i < size; i++, offset += 2)
                {
                    lut[i] = new Color32(0xff, r[offset], g[offset], b[offset]);
                }
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
        /// A factory method to initialize new instance of <see cref="DicomPixelData"/> implementation either 
        /// <see cref="OtherWordPixelData"/>, <see cref="OtherBytePixelData"/>, or <see cref="EncapsulatedPixelData"/>
        /// </summary>
        /// <param name="dataset">Source DICOM Dataset</param>
        /// <param name="newPixelData">true if new <see cref="DicomPixelData"/>will be created for current dataset,
        /// false to read <see cref="DicomPixelData"/> from <paramref name="dataset"/>.
        /// Default is false (read)</param>
        /// <returns>New instance of DicomPixelData</returns>
        public static DicomPixelData Create(DicomDataset dataset, bool newPixelData = false)
        {
            if (newPixelData)
            {
                var syntax = dataset.InternalTransferSyntax;
                var bitsAllocated = dataset.GetSingleValue<ushort>(DicomTag.BitsAllocated);

                if (syntax.IsEncapsulated)
                {
                    if (bitsAllocated > 16)
                    {
                        throw new DicomImagingException($"Cannot represent pixel data with Bits Allocated: {bitsAllocated} > 16");
                    }

                    return new EncapsulatedPixelData(dataset, bitsAllocated);
                }
                else if (syntax == DicomTransferSyntax.ImplicitVRLittleEndian)
                {
                    //  DICOM 3.5 A.1
                    return new OtherWordPixelData(dataset, true);
                }
                else
                {
                    //  DICOM 3.5 A.2
                    return bitsAllocated > 8
                        ? (DicomPixelData)new OtherWordPixelData(dataset, true)
                        : new OtherBytePixelData(dataset, true);
                }
            }

            var item = dataset.GetDicomItem<DicomItem>(DicomTag.PixelData);
            if (item == null)
            {
                throw new DicomImagingException("DICOM dataset is missing pixel data element.");
            }

            if (item is DicomOtherByte)
            {
                return new OtherBytePixelData(dataset, false);
            }

            if (item is DicomOtherWord)
            {
                return new OtherWordPixelData(dataset, false);
            }

            if (item is DicomOtherByteFragment || item is DicomOtherWordFragment)
            {
                return new EncapsulatedPixelData(dataset);
            }

            throw new DicomImagingException($"Unexpected or unhandled pixel data element type: {item.GetType()}");
        }

        /// <summary>
        /// Other Byte (OB) implementation of <see cref="DicomPixelData"/>
        /// </summary>
        private sealed class OtherBytePixelData : DicomPixelData
        {
            #region FIELDS

            /// <summary>
            /// The pixel data other byte (OB) element
            /// </summary>
            private readonly DicomOtherByte _element;
            private readonly IByteBuffer _padingByteBuffer = new MemoryByteBuffer(new byte[1] { DicomVR.OB.PaddingValue });

            #endregion

            #region CONSTRUCTORS

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
                    _element = new DicomOtherByte(DicomTag.PixelData, new CompositeByteBuffer());
                    Dataset.AddOrUpdate(_element);
                }
                else
                {
                    _element = dataset.GetDicomItem<DicomOtherByte>(DicomTag.PixelData);
                }
            }

            #endregion

            #region METODS

            /// <inheritdoc />
            public override IByteBuffer GetFrame(int frame)
            {
                if (frame < 0 || frame >= NumberOfFrames)
                {
                    throw new IndexOutOfRangeException("Requested frame out of range!");
                }

                var offset = (long)UncompressedFrameSize * frame;
                return new RangeByteBuffer(_element.Buffer, offset, UncompressedFrameSize);
            }

            /// <inheritdoc />
            public override void AddFrame(IByteBuffer data)
            {
                var buffer = _element.Buffer as CompositeByteBuffer ??
                    throw new DicomImagingException("Expected pixel data element to have a CompositeByteBuffer");

                buffer.Buffers.Remove(_padingByteBuffer);
                buffer.Buffers.Add(data);
                if (buffer.Size % 2 == 1)
                {
                    buffer.Buffers.Add(_padingByteBuffer);
                }

                NumberOfFrames++;
            }

            #endregion
        }

        /// <summary>
        /// Other Word (OW) implementation of <see cref="DicomPixelData"/>
        /// </summary>
        private sealed class OtherWordPixelData : DicomPixelData
        {
            #region FIELDS

            /// <summary>
            /// The pixel data other word (OW) element
            /// </summary>
            private readonly DicomOtherWord _element;

            #endregion

            #region CONSTRUCTORS

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
                    _element = new DicomOtherWord(DicomTag.PixelData, new CompositeByteBuffer());
                    Dataset.AddOrUpdate(_element);
                }
                else
                {
                    _element = dataset.GetDicomItem<DicomOtherWord>(DicomTag.PixelData);
                }
            }

            #endregion

            #region METHODS

            /// <inheritdoc />
            public override IByteBuffer GetFrame(int frame)
            {
                if (frame < 0 || frame >= NumberOfFrames)
                {
                    throw new IndexOutOfRangeException("Requested frame out of range!");
                }

                var offset = (long)UncompressedFrameSize * frame;
                IByteBuffer buffer = new RangeByteBuffer(_element.Buffer, offset, UncompressedFrameSize);

                // mainly for GE Private Implicit VR Big Endian
                if (Syntax.SwapPixelData) { buffer = new SwapByteBuffer(buffer, 2); }

                return buffer;
            }

            /// <inheritdoc />
            public override void AddFrame(IByteBuffer data)
            {
                var buffer = (_element.Buffer as CompositeByteBuffer)
                    ?? throw new DicomImagingException("Expected pixel data element to have a CompositeByteBuffer.");

                if (Syntax.SwapPixelData)
                {
                    data = new SwapByteBuffer(data, 2);
                }

                buffer.Buffers.Add(data);
                NumberOfFrames++;
            }

            #endregion
        }

        /// <summary>
        /// Other Byte/Word Fragment implementation of <see cref="DicomPixelData"/>, used for handling encapsulated (compressed)
        /// pixel data
        /// </summary>
        private sealed class EncapsulatedPixelData : DicomPixelData
        {
            #region FIELDS

            /// <summary>
            /// The pixel data fragment sequence element
            /// </summary>
            private readonly DicomFragmentSequence _element;

            #endregion

            #region CONSTRUCTORS

            /// <summary>
            /// Initialize new instance of EncapsulatedPixelData with new empty pixel data.
            /// </summary>
            /// <param name="dataset">The source dataset where to create new pixel data.</param>
            /// <param name="bitsAllocated">Bits allocated for the pixel data.</param>
            public EncapsulatedPixelData(DicomDataset dataset, int bitsAllocated)
                : base(dataset)
            {
                NumberOfFrames = 0;

                _element = bitsAllocated > 8
                    ? (DicomFragmentSequence)new DicomOtherWordFragment(DicomTag.PixelData)
                    : new DicomOtherByteFragment(DicomTag.PixelData);

                Dataset.AddOrUpdate(_element);
            }

            /// <summary>
            /// Initialize new instance of EncapsulatedPixelData based on existing pixel data.
            /// </summary>
            /// <param name="dataset">The source dataset to extract pixel data from.</param>
            public EncapsulatedPixelData(DicomDataset dataset)
                : base(dataset)
            {
                _element = dataset.GetDicomItem<DicomFragmentSequence>(DicomTag.PixelData);
            }

            #endregion

            #region METHODS

            /// <inheritdoc />
            public override IByteBuffer GetFrame(int frame)
            {
                if (frame < 0 || frame >= NumberOfFrames)
                {
                    throw new IndexOutOfRangeException("Requested frame out of range!");
                }

                IByteBuffer buffer;

                var fragments = _element.Fragments;
                
                // GH-1586 Ignore last fragment if it is empty
                if (fragments.Count > 0 && fragments[fragments.Count - 1].Size == 0)
                {
                    fragments.RemoveAt(fragments.Count - 1);
                }
                
                if (NumberOfFrames == 1)
                {
                    buffer = fragments.Count == 1
                        ? fragments[0]
                        : new CompositeByteBuffer(fragments);
                }
                else if (fragments.Count == NumberOfFrames)
                {
                    buffer = fragments[frame];
                }
                else if (_element.OffsetTable.Count == NumberOfFrames)
                {
                    var start = _element.OffsetTable[frame];
                    var stop = _element.OffsetTable.Count == frame + 1
                                    ? uint.MaxValue
                                    : _element.OffsetTable[frame + 1];

                    var composite = new CompositeByteBuffer();

                    long pos = 0;
                    var frag = 0;

                    while (pos < start && frag < fragments.Count)
                    {
                        pos += 8;
                        pos += fragments[frag].Size;
                        frag++;
                    }

                    if (pos != start)
                    {
                        throw new DicomImagingException("Fragment start position does not match offset table.");
                    }

                    while (pos < stop && frag < fragments.Count)
                    {
                        composite.Buffers.Add(fragments[frag]);

                        pos += 8;
                        pos += fragments[frag].Size;
                        frag++;
                    }

                    if (pos < stop && stop != uint.MaxValue)
                    {
                        throw new DicomImagingException("Image frame truncated while reading fragments from offset table.");
                    }

                    buffer = composite;
                }
                else
                {
                    throw new DicomImagingException("Support for multi-frame images with varying fragment sizes and no offset table has not been implemented.");
                }

                // mainly for GE Private Implicit VR Little Endian
                if (!Syntax.IsEncapsulated && BitsAllocated == 16 && Syntax.SwapPixelData)
                {
                    buffer = new SwapByteBuffer(buffer, 2);
                }

                return EndianByteBuffer.Create(buffer, Syntax.Endian, BytesAllocated);
            }

            /// <inheritdoc />
            public override void AddFrame(IByteBuffer data)
            {
                NumberOfFrames++;

                long pos = 0;
                if (_element.Fragments.Any())
                {
                    pos = _element.OffsetTable.Last() + _element.Fragments.Last().Size + 8;
                }

                if (pos < uint.MaxValue)
                {
                    _element.OffsetTable.Add((uint)pos);
                }
                else
                {
                    // do not create an offset table for very large datasets
                    _element.OffsetTable.Clear();
                }

                data = EndianByteBuffer.Create(data, Syntax.Endian, BytesAllocated);
                _element.Fragments.Add(data);
            }

            #endregion
        }
    }
}
