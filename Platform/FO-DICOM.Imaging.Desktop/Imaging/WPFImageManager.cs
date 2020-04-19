// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// WPF based implementation of the <see cref="ImageManager"/>.
    /// </summary>
    public sealed class WPFImageManager : IImageManager
    {

        public IImage CreateImage(int width, int height)
            => new WPFImage(width, height);

    }
}
