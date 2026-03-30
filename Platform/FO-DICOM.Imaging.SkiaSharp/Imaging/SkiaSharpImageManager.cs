// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging
{
    public sealed class SkiaSharpImageManager : IImageManager
    {

        public IImage CreateImage(int width, int height)
            => new SkiaSharpImage(width, height);

    }
}
