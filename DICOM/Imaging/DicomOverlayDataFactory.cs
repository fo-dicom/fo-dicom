﻿// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System.Drawing;

    using Dicom.Imaging.Mathematics;
    using Dicom.IO.Buffer;

    /// <summary>
    /// Bitmap related factory methods for <see cref="DicomOverlayData"/>.
    /// </summary>
    public class DicomOverlayDataFactory
    {
        #region METHODS

        /// <summary>
        /// Creates a DICOM overlay from a GDI+ Bitmap.
        /// </summary>
        /// <param name="ds">Dataset</param>
        /// <param name="bitmap">Bitmap</param>
        /// <param name="mask">Color mask for overlay</param>
        /// <returns>DICOM overlay</returns>
        public static DicomOverlayData FromBitmap(DicomDataset ds, Bitmap bitmap, Color mask)
        {
            ushort group = 0x6000;
            while (ds.Contains(new DicomTag(group, DicomTag.OverlayBitPosition.Element))) group += 2;

            var overlay = new DicomOverlayData(ds, group)
                              {
                                  Type = DicomOverlayType.Graphics,
                                  Rows = bitmap.Height,
                                  Columns = bitmap.Width,
                                  OriginX = 1,
                                  OriginY = 1,
                                  BitsAllocated = 1,
                                  BitPosition = 1
                              };

            var array = new BitList { Capacity = overlay.Rows * overlay.Columns };

            int p = 0;
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++, p++)
                {
                    if (bitmap.GetPixel(x, y).ToArgb() == mask.ToArgb()) array[p] = true;
                }
            }

            overlay.Data = EvenLengthBuffer.Create(new MemoryByteBuffer(array.Array));

            return overlay;
        }

        #endregion
    }
}