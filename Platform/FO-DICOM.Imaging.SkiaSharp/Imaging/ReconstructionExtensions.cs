// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Reconstruction;
using SkiaSharp;

namespace FellowOakDicom.Imaging
{
    public static class ReconstructionExtensions
    {
        public static SKBitmap AsImage(this Slice slice)
        {
            var buffer = new byte[slice.Columns * slice.Rows];
            slice.RenderIntoByteArray(buffer, slice.Columns);

            var image = SKBitmap.FromImage(SKImage.FromPixelCopy(new SKImageInfo(slice.Columns, slice.Rows, SKColorType.Bgra8888), buffer));
            return image;
        }

    }
}
