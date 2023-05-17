// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging
{
    /// <summary>
    /// Windows Forms-based image manager implementation.
    /// </summary>
    public sealed class WinFormsImageManager : IImageManager
    {

        public IImage CreateImage(int width, int height)
            => new WinFormsImage(width, height);

    }
}
