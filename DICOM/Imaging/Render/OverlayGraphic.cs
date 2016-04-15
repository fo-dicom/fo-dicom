// Copyright (c) 2012-2016 fo-dicom contributors.
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

        private SingleBitPixelData _originalData;

        private IPixelData _scaledData;

        private int _offsetX;

        private int _offsetY;

        private int _color;

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

        public void Scale(double scale)
        {
            if (Math.Abs(scale - _scale) <= Double.Epsilon) return;

            _scale = scale;
            _scaledData = null;
        }

        public void Render(int[] pixels, int width, int height)
        {
            byte[] data = null;

            if (_scaledData == null) _scaledData = _originalData.Rescale(_scale);

            data = (_scaledData as GrayscalePixelDataU8).Data;

            int ox = (int)(_offsetX * _scale);
            int oy = (int)(_offsetY * _scale);

#if NET35
            for (var y = 0; y < _scaledData.Height; ++y)
#else
                Parallel.For(0, _scaledData.Height, y =>
#endif
            {
                if ((oy + y) >= height) return;
                for (int i = _scaledData.Width * y, e = i + _scaledData.Width, x = 0; i < e; i++, x++)
                {
                    if (data[i] > 0)
                    {
                        if ((ox + x) >= width) break;
                        int p = (oy * width) + ox + i;
                        pixels[p] = _color;
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
