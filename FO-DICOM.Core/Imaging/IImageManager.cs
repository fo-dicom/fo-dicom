// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging
{
    public interface IImageManager
    {

        IImage CreateImage(int width, int height);

    }
}
