// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.LUT;

namespace FellowOakDicom.Imaging.Render
{

    /// <summary>
    /// Palette color pipeline implementation of <see cref="IPipeline"/>
    /// </summary>
    public class PaletteColorPipeline : IPipeline
    {

        private readonly DicomPixelData _pixelData;
        private ILUT _cachedLUT;

        /// <summary>
        /// Initialize new instance of <see cref="PaletteColorPipeline"/> containing palette color LUT extracted from
        /// <paramref name="pixelData"/>
        /// </summary>
        /// <param name="pixelData">Dicom Pixel Data containing paletter color LUT</param>
        public PaletteColorPipeline(DicomPixelData pixelData)
        {
            _pixelData = pixelData;
        }

        /// <summary>
        /// Get the <see cref="FellowOakDicom.Imaging.LUT.PaletteColorLUT"/>
        /// </summary>
        public ILUT LUT => _cachedLUT ??= BuildColorLUT();

        /// <inheritdoc />
        public void ClearCache()
        {
            _cachedLUT = null;
        }

        private ILUT BuildColorLUT()
        {
            var lut = _pixelData.PaletteColorLUT;
            var first = _pixelData.Dataset.GetValue<int>(DicomTag.RedPaletteColorLookupTableDescriptor, 1);

           return new PaletteColorLUT(first, lut);
        }

    }
}
