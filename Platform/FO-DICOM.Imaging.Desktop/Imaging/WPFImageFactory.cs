// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// WPF based implementation of the <see cref="ImageFactory"/>.
    /// </summary>
    public sealed class WPFImageFactory : IImageFactory
    {

        public IImage CreateImage(int width, int height)
            => new WPFImage(width, height);

    }
}
