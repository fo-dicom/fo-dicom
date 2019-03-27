// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render
{
    /// <summary>
    /// Palette color pipeline implementation of <seealso cref="IPipeline"/>
    /// </summary>
    public class PaletteColorPipeline : IPipeline
    {

        /// <summary>
        /// Initialize new instance of <seealso cref="PaletteColorPipeline"/> containing palette color LUT extracted from
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
        /// Get the <seealso cref="PaletteColorLUT"/>
        /// </summary>
        public ILUT LUT { get; private set; }
    }
}
