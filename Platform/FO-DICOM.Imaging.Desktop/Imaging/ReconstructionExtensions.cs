// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Reconstruction;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FellowOakDicom.Imaging.Desktop.Imaging
{
    public static class ReconstructionExtensions
    {

        public static WriteableBitmap AsWpfBitmap(this Slice slice)
        {
            var dpi = 25.4 / slice.Spacing;
            var bitmap = new WriteableBitmap(slice.Columns, slice.Rows, dpi, dpi, PixelFormats.Gray8, null);
            var buffer = new byte[bitmap.BackBufferStride * bitmap.PixelHeight];

            slice.RenderIntoByteArray(buffer, bitmap.BackBufferStride);

            bitmap.WritePixels(new System.Windows.Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight), buffer, bitmap.BackBufferStride, 0);
            return bitmap;
        }


        public static Bitmap AsBitmap(this Slice slice)
        {
            var bitmap = new Bitmap(slice.Columns, slice.Rows, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            var bmpData = bitmap.LockBits(new Rectangle(0, 0, slice.Columns, slice.Rows), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            var buffer = new byte[bmpData.Stride * bmpData.Height];
            slice.RenderIntoByteArray(buffer, bmpData.Stride);

            System.Runtime.InteropServices.Marshal.Copy(buffer, 0, bmpData.Scan0, buffer.Length);

            bitmap.UnlockBits(bmpData);

            //// The following code is to modify the index table of generating bitmaps, modified from the pseudoquoity to grayscale
            System.Drawing.Imaging.ColorPalette tempPalette;
            using (var tempBmp = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format8bppIndexed))
            {
                tempPalette = tempBmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                tempPalette.Entries[i] = System.Drawing.Color.FromArgb(i, i, i);
            }
            bitmap.Palette = tempPalette;

            return bitmap;
        }


    }
}
