// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FellowOakDicom.Imaging.Mathematics;
using FellowOakDicom.IO.Buffer;

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// Representation of enumerated DICOM overlay types.
    /// </summary>
    public enum DicomOverlayType
    {
        /// <summary>Graphic overlay</summary>
        Graphics,

        /// <summary>Region of Interest</summary>
        ROI
    }

    /// <summary>
    /// DICOM image overlay class
    /// </summary>
    public class DicomOverlayData
    {
        #region Public Constructors

        /// <summary>
        /// Initializes overlay from DICOM dataset and overlay group.
        /// </summary>
        /// <param name="ds">Dataset</param>
        /// <param name="group">Overlay group</param>
        /// <exception cref="DicomImagingException">Thrown if the overlay data is insufficient.</exception>
        public DicomOverlayData(DicomDataset ds, ushort group)
        {
            Dataset = ds;
            Group = group;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the DICOM Dataset containing the overlay data.
        /// </summary>
        public DicomDataset Dataset { get; }

        /// <summary>
        /// Get the overlay group number.
        /// </summary>
        public ushort Group { get; }

        /// <summary>
        /// Gets or sets the number of rows in overlay
        /// </summary>
        public int Rows
        {
            get => Dataset.GetSingleValue<ushort>(OverlayTag(DicomTag.OverlayRows));
            set => Dataset.AddOrUpdate(OverlayTag(DicomTag.OverlayRows), (ushort)value);
        }

        /// <summary>
        /// Gets or sets the number of columns in overlay.
        /// </summary>
        public int Columns
        {
            get => Dataset.GetSingleValue<ushort>(OverlayTag(DicomTag.OverlayColumns));
            set => Dataset.AddOrUpdate(OverlayTag(DicomTag.OverlayColumns), (ushort)value);
        }

        /// <summary>
        /// Gets or sets the overlay type.
        /// </summary>
        public DicomOverlayType Type
        {
            get
            {
                var type = Dataset.GetSingleValue<string>(OverlayTag(DicomTag.OverlayType));
                if (type.StartsWith("R")) return DicomOverlayType.ROI;
                if (type.StartsWith("G")) return DicomOverlayType.Graphics;
                throw new DicomImagingException($"Unsupported overlay type: {type}");
            }
            set => Dataset.AddOrUpdate(
                    OverlayTag(DicomTag.OverlayType),
                    value.ToString().Substring(0, 1).ToUpperInvariant());
        }

        /// <summary>
        /// Gets or sets the number of bits allocated in overlay data.
        /// </summary>
        public int BitsAllocated
        {
            get => Dataset.GetValueOrDefault<ushort>(OverlayTag(DicomTag.OverlayBitsAllocated), 0, 1);
            set => Dataset.AddOrUpdate(OverlayTag(DicomTag.OverlayBitsAllocated), (ushort)value);
        }

        /// <summary>
        /// Gets or sets the bit position of embedded overlay.
        /// </summary>
        public int BitPosition
        {
            get => Dataset.GetValueOrDefault<ushort>(OverlayTag(DicomTag.OverlayBitPosition), 0, 0);
            set => Dataset.AddOrUpdate(OverlayTag(DicomTag.OverlayBitPosition), (ushort)value);
        }

        /// <summary>
        /// Gets or sets the description of the overlay.
        /// </summary>
        public string Description
        {
            get => Dataset.GetSingleValueOrDefault(OverlayTag(DicomTag.OverlayDescription), string.Empty);
            set => Dataset.AddOrUpdate(OverlayTag(DicomTag.OverlayDescription), value);
        }

        /// <summary>
        /// Gets or sets the overlay subtype.
        /// </summary>
        public string Subtype
        {
            get => Dataset.GetSingleValueOrDefault(OverlayTag(DicomTag.OverlaySubtype), string.Empty);
            set => Dataset.AddOrUpdate(OverlayTag(DicomTag.OverlaySubtype), value);
        }

        /// <summary>
        /// Gets or sets the overlay label.
        /// </summary>
        public string Label
        {
            get => Dataset.GetSingleValueOrDefault(OverlayTag(DicomTag.OverlayLabel), string.Empty);
            set => Dataset.AddOrUpdate(OverlayTag(DicomTag.OverlayLabel), value);
        }

        /// <summary>
        /// Gets or sets the number of frames in the overlay.
        /// </summary>
        public int NumberOfFrames
        {
            get => Dataset.GetValueOrDefault(OverlayTag(DicomTag.NumberOfFramesInOverlay), 0, 1);
            set => Dataset.AddOrUpdate(OverlayTag(DicomTag.NumberOfFramesInOverlay), value);
        }

        /// <summary>
        /// Gets or sets the first frame of the overlay (frames are numbered from 1).
        /// </summary>
        public int OriginFrame
        {
            get => Dataset.GetValueOrDefault<ushort>(OverlayTag(DicomTag.ImageFrameOrigin), 0, 1);
            set => Dataset.AddOrUpdate(OverlayTag(DicomTag.ImageFrameOrigin), (ushort)value);
        }

        /// <summary>
        /// Gets or sets the index of the first column of the overlay.
        /// </summary>
        public int OriginX
        {
            get => Dataset.GetValueOrDefault<short>(OverlayTag(DicomTag.OverlayOrigin), 1, 1);
            set => Dataset.AddOrUpdate(OverlayTag(DicomTag.OverlayOrigin), (short)OriginY, (short)value);
        }

        /// <summary>
        /// Gets or sets the index of the first row of the overlay.
        /// </summary>
        public int OriginY
        {
            get => Dataset.GetValueOrDefault<short>(OverlayTag(DicomTag.OverlayOrigin), 0, 1);
            set => Dataset.AddOrUpdate(OverlayTag(DicomTag.OverlayOrigin), (short)value, (short)OriginX);
        }

        /// <summary>
        /// Gets or sets the overlay data.
        /// </summary>
        public IByteBuffer Data
        {
            get => Load();
            set => Dataset.AddOrUpdate(new DicomOtherWord(OverlayTag(DicomTag.OverlayData), value));
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Gets the overlay data as <see cref="int"/> values.
        /// </summary>
        /// <param name="bg">Background color</param>
        /// <param name="fg">Foreground color</param>
        /// <returns>Overlay data</returns>
        public int[] GetOverlayDataS32(int bg, int fg)
        {
            var overlay = new int[Rows * Columns];
            var bits = new BitArray(Data.Data);
            if (bits.Length < overlay.Length)
            {
                throw new DicomDataException("Invalid overlay length: " + bits.Length);
            }

            for (int i = 0, c = overlay.Length; i < c; i++)
            {
                overlay[i] = bits.Get(i) ? fg : bg;
            }
            return overlay;
        }

        /// <summary>
        /// Gets all overlays in a DICOM dataset.
        /// </summary>
        /// <param name="ds">Dataset</param>
        /// <returns>Array of overlays</returns>
        public static DicomOverlayData[] FromDataset(DicomDataset ds)
        {
            var groups = new List<ushort>();
            groups.AddRange(
                ds.Where(x => x.Tag.Group >= 0x6000 && x.Tag.Group <= 0x60FF && x.Tag.Element == 0x0010)
                    .Select(x => x.Tag.Group));
            var overlays = new List<DicomOverlayData>();
            foreach (var group in groups)
            {
                // ensure that 6000 group is actually an overlay group, including containing bare minimum of attributes.
                if (ds.GetDicomItem<DicomElement>(new DicomTag(group, 0x0010)).ValueRepresentation != DicomVR.US ||
                    string.IsNullOrEmpty(ds.GetSingleValueOrDefault<string>(OverlayTag(group, DicomTag.OverlayType), null)) ||
                   ds.GetValueOrDefault<ushort>(OverlayTag(group, DicomTag.OverlayColumns), 0, 0) == 0 ||
                   ds.GetValueOrDefault<ushort>(OverlayTag(group, DicomTag.OverlayRows), 0, 0) == 0)
                {
                    continue;
                }

                var overlay = new DicomOverlayData(ds, group);
                overlays.Add(overlay);
            }

            return overlays.ToArray();
        }

        /// <summary>
        /// Checks whether a dataset contains embedded overlays.
        /// </summary>
        /// <param name="ds">Dataset to examine.</param>
        /// <returns>True if dataset contains embedded overlays, false otherwise.</returns>
        public static bool HasEmbeddedOverlays(DicomDataset ds)
        {
            var groups = new List<ushort>();
            groups.AddRange(
                ds.Where(x => x.Tag.Group >= 0x6000 && x.Tag.Group <= 0x60FF && x.Tag.Element == 0x0010)
                    .Select(x => x.Tag.Group));

            foreach (var group in groups)
            {
                if (!ds.Contains(new DicomTag(group, DicomTag.OverlayData.Element)))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Private Methods

        private static DicomTag OverlayTag(ushort group, DicomTag tag)
        {
            return new DicomTag(group, tag.Element);
        }

        private DicomTag OverlayTag(DicomTag tag)
        {
            return new DicomTag(Group, tag.Element);
        }

        private IByteBuffer Load()
        {
            var tag = OverlayTag(DicomTag.OverlayData);
            if (Dataset.Contains(tag))
            {
                var elem = Dataset.FirstOrDefault(x => x.Tag == tag) as DicomElement;
                return elem.Buffer;
            }
            else
            {
                // overlay embedded in high bits of pixel data
                if (Dataset.InternalTransferSyntax.IsEncapsulated)
                {
                    throw new DicomImagingException(
                        "Attempted to extract embedded overlay from compressed pixel data. Decompress pixel data before attempting this operation.");
                }

                var pixels = DicomPixelData.Create(Dataset);

                // (1,1) indicates top left pixel of image
                var ox = Math.Max(0, OriginX - 1);
                var oy = Math.Max(0, OriginY - 1);
                var ow = Columns;
                var oh = Rows;

                var frame = pixels.GetFrame(0);

                var bits = new BitList { Capacity = Rows * Columns };
                var mask = 1 << BitPosition;

                // Sanity check: do not collect overlay data if Overlay Bit Position is within the used pixel range. (#110)
                if (BitPosition <= pixels.HighBit && BitPosition > pixels.HighBit - pixels.BitsStored)
                {
                    // Do nothing
                }
                else if (pixels.BitsAllocated == 8)
                {
                    var data = IO.ByteConverter.ToArray<byte>(frame);

                    for (var y = 0; y < oh; y++)
                    {
                        var n = (y + oy) * pixels.Width + ox;
                        var i = y * Columns;
                        for (var x = 0; x < ow; x++)
                        {
                            if ((data[n] & mask) != 0)
                            {
                                bits[i] = true;
                            }

                            n++;
                            i++;
                        }
                    }
                }
                else if (pixels.BitsAllocated == 16)
                {
                    // we don't really care if the pixel data is signed or not
                    var data = IO.ByteConverter.ToArray<ushort>(frame);

                    for (var y = 0; y < oh; y++)
                    {
                        var n = (y + oy) * pixels.Width + ox;
                        var i = y * Columns;
                        for (var x = 0; x < ow; x++)
                        {
                            if ((data[n] & mask) != 0)
                            {
                                bits[i] = true;
                            }

                            n++;
                            i++;
                        }
                    }
                }
                else
                {
                    throw new DicomImagingException(
                        "Unable to extract embedded overlay from pixel data with bits stored greater than 16.");
                }

                return new MemoryByteBuffer(bits.Array);
            }
        }

        #endregion
    }
}
