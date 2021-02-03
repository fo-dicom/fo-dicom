// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Imaging
{
    /// <summary>
    /// Windows Forms-based image manager implementation.
    /// </summary>
    public sealed class ImageSharpImageManager : IImageManager
    {

        public IImage CreateImage(int width, int height)
            => new ImageSharpImage(width, height);

    }
}
