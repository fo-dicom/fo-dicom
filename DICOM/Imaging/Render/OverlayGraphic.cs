// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Render
{
    using System;

#if !NET35
    using System.Threading.Tasks;
#endif

    /// <summary>
    /// The Overlay Graphic which render overlay over pixel data
    /// </summary>
    public class OverlayGraphic
    {
        #region Private Members

        private readonly SingleBitPixelData _originalData;

        private GrayscalePixelDataU8 _scaledData;

        private readonly int _offsetX;

        private readonly int _offsetY;

        private readonly int _color;

        private double _scale;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <seealso cref="OverlayGraphic"/>
        /// </summary>
        /// <param name="pixelData">Overlay pixel data</param>
        /// <param name="offsetx">X offset</param>
        /// <param name="offsety">Y offset</param>
        /// <param name="color">The color of the resulting overlay</param>
        public OverlayGraphic(SingleBitPixelData pixelData, int offsetx, int offsety, int color)
        {
            _originalData = pixelData;
            _scaledData = _originalData;
            _offsetX = offsetx;
            _offsetY = offsety;
            _color = color;
            _scale = 1.0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set overlay scale factor.
        /// </summary>
        /// <param name="scale">Scale factor.</param>
        public void Scale(double scale)
        {
            if (Math.Abs(scale - _scale) <= double.Epsilon) return;

            _scale = scale;
            _scaledData = null;
        }

        /// <summary>
        /// Render overlay graphic.
        /// </summary>
        /// <param name="pixels">Pixels subject to rendering.</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Render(int[] pixels, int width, int height)
        {
            if (_scaledData == null) _scaledData = (GrayscalePixelDataU8)_originalData.Rescale(_scale);

            var data = _scaledData.Data;

            var ox = (int)(_offsetX * _scale);
            var oy = (int)(_offsetY * _scale);

#if NET35
            for (var y = 0; y < _scaledData.Height; ++y)
#else
            Parallel.For(0, _scaledData.Height, y =>
#endif
            {
                if (oy + y >= height) return;
                for (int i = _scaledData.Width * y, e = i + _scaledData.Width, p = (oy + y) * width + ox, x = 0; i < e; i++, p++, x++)
                {
                    if (data[i] > 0)
                    {
                        if (ox + x >= width) break;
                        pixels[p] |= _color;
                    }
                }
            }
#if !NET35
            );
#endif
        }

        #endregion
    }
}
