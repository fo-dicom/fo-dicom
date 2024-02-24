// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Reconstruction;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FellowOakDicom.Imaging
{
    public static class ReconstructionExtensions
    {
        public static Image<L8> AsImage(this Slice slice)
        {
            var buffer = new byte[slice.Columns * slice.Rows];
            slice.RenderIntoByteArray(buffer, slice.Columns);

            var image = Image.LoadPixelData<L8>(buffer, slice.Columns, slice.Rows);
            return image;
        }

    }
}
