// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// Pixel Representation (0028,0103) represents signed/unsigned data of the pixel samples.
    /// Each sample shall have the same pixel representation
    /// </summary>
    public enum PixelRepresentation
    {
        /// <summary>
        /// Unsigned integer
        /// </summary>
        Unsigned = 0,

        /// <summary>
        /// 2's complement (signed) integer
        /// </summary>
        Signed = 1
    }
}
