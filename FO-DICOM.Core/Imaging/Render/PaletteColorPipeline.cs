﻿// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging.LUT;

namespace FellowOakDicom.Imaging.Render
{

    /// <summary>
    /// Palette color pipeline implementation of <see cref="IPipeline"/>
    /// </summary>
    public class PaletteColorPipeline : IPipeline
    {

        /// <summary>
        /// Initialize new instance of <see cref="PaletteColorPipeline"/> containing palette color LUT extracted from
        /// <paramref name="pixelData"/>
        /// </summary>
        /// <param name="pixelData">Dicom Pixel Data containing paletter color LUT</param>
        public PaletteColorPipeline(DicomPixelData pixelData)
        {
            var lut = pixelData.PaletteColorLUT;
            var first = pixelData.Dataset.GetValue<int>(DicomTag.RedPaletteColorLookupTableDescriptor, 1);

            LUT = new PaletteColorLUT(first, lut);
        }

        /// <summary>
        /// Get the <see cref="FellowOakDicom.Imaging.LUT.PaletteColorLUT"/>
        /// </summary>
        public ILUT LUT { get; private set; }
    }
}
