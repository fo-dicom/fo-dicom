// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// Planar Configuration (0028,0006) indicates whether the color pixel data are sent color-by-plane
    /// or color-by-pixel. This Attribute shall be present if Samples per Pixel (0028,0002) has a value
    /// greater than 1. It shall not be present otherwise.
    /// </summary>
    public enum PlanarConfiguration
    {
        /// <summary>
        /// The sample values for the first pixel are followed by the sample values for the second 
        /// pixel, etc. For RGB images, this means the order of the pixel values sent shall be R1, 
        /// G1, B1, R2, G2, B2, ..., etc.
        /// </summary>
        Interleaved = 0,

        /// <summary>
        /// Each color plane shall be sent contiguously. For RGB images, this means the order of 
        /// the pixel values sent is R1, R2, R3, ..., G1, G2, G3, ..., B1, B2, B3, etc.
        /// </summary>
        Planar = 1
    }
}
