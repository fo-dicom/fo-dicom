// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using FellowOakDicom.Imaging.LUT;
using FellowOakDicom.Imaging.Mathematics;

namespace FellowOakDicom.Imaging.Render
{

    /// <summary>
    /// The Image Graphic implementation of <see cref="IGraphic"/>
    /// </summary>
    public class ImageGraphic : IGraphic
    {
        #region Protected Members

        protected IPixelData _originalData;

        protected IPixelData _scaledData;

        protected double _scaleFactor;

        protected int _rotation;

        protected bool _flipX;

        protected bool _flipY;

        protected int _offsetX;

        protected int _offsetY;

        protected int _zorder;

        protected bool _applyLut;

        protected List<OverlayGraphic> _overlays;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of pixel components (samples)
        /// </summary>
        public int Components => OriginalData.Components;

        /// <summary>
        /// Gets the original pixel data.
        /// </summary>
        public IPixelData OriginalData => _originalData;

        /// <inheritdoc />
        public int OriginalWidth => _originalData.Width;

        /// <inheritdoc />
        public int OriginalHeight => _originalData.Height;

        /// <inheritdoc />
        public int OriginalOffsetX => _offsetX;

        /// <inheritdoc />
        public int OriginalOffsetY => _offsetY;

        /// <inheritdoc />
        public double ScaleFactor => _scaleFactor;

        /// <summary>
        /// Gets scaled pixel data.
        /// </summary>
        public IPixelData ScaledData
        {
            get
            {
                if (_scaledData == null)
                {
                    _scaledData = (_scaleFactor - 1.0).IsNearlyZero()
                        ? _originalData
                        : OriginalData.Rescale(_scaleFactor);
                }

                return _scaledData;
            }
        }

        /// <inheritdoc />
        public int ScaledWidth => ScaledData.Width;

        /// <inheritdoc />
        public int ScaledHeight => ScaledData.Height;

        /// <inheritdoc />
        public int ScaledOffsetX => (int)(_offsetX * _scaleFactor);

        /// <inheritdoc />
        public int ScaledOffsetY => (int)(_offsetY * _scaleFactor);

        /// <inheritdoc />
        public int ZOrder
        {
            get => _zorder;
            set => _zorder = value;
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <see cref="ImageGraphic"/>
        /// </summary>
        /// <param name="pixelData">Pixel data</param>
        public ImageGraphic(IPixelData pixelData)
            : this()
        {
            _originalData = pixelData;
            Scale(1.0);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        protected ImageGraphic()
        {
            _zorder = 255;
            _applyLut = true;
            _overlays = new List<OverlayGraphic>();
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Add overlay to render over the resulted image 
        /// </summary>
        /// <param name="overlay">Overlay graphic </param>
        public void AddOverlay(OverlayGraphic overlay)
        {
            _overlays.Add(overlay);
            overlay.Scale(_scaleFactor);
        }

        /// <inheritdoc />
        public void Reset()
        {
            Scale(1.0);
            _rotation = 0;
            _flipX = false;
            _flipY = false;
        }

        /// <inheritdoc />
        public void Scale(double scale)
        {
            if ((scale - _scaleFactor).IsNearlyZero()) return;

            _scaleFactor = scale;
            _scaledData = null;

            foreach (var overlay in _overlays)
            {
                overlay.Scale(scale);
            }
        }

        /// <inheritdoc />
        public void BestFit(int width, int height)
        {
            double xF = (double)width / OriginalWidth;
            double yF = (double)height / OriginalHeight;
            Scale(Math.Min(xF, yF));
        }

        /// <inheritdoc />
        public void Rotate(int angle)
        {
            if (angle > 0)
            {
                if (angle <= 90) _rotation += 90;
                else if (angle <= 180) _rotation += 180;
                else if (angle <= 270) _rotation += 270;
            }
            else if (angle < 0)
            {
                if (angle >= -90) _rotation -= 90;
                else if (angle >= -180) _rotation -= 180;
                else if (angle >= -270) _rotation -= 270;
            }
            _rotation = _rotation % 360;
        }

        /// <inheritdoc />
        public void FlipX()
        {
            _flipX = !_flipX;
        }

        /// <inheritdoc />
        public void FlipY()
        {
            _flipY = !_flipY;
        }

        /// <inheritdoc />
        public void Transform(double scale, int rotation, bool flipx, bool flipy)
        {
            Scale(scale);
            Rotate(rotation);
            _flipX = flipx;
            _flipY = flipy;
        }

        /// <inheritdoc />
        public IImage RenderImage(ILUT lut)
        {
            if (_applyLut && lut != null && !lut.IsValid)
            {
                lut.Recalculate();
            }

            var image = ImageManager.CreateImage(ScaledWidth, ScaledHeight);

            var pixels = image.Pixels.Data;
            ScaledData.Render(_applyLut ? lut : null, pixels);

            foreach (var overlay in _overlays)
            {
                overlay.Render(pixels, ScaledWidth, ScaledHeight);
            }

            image.Render(Components, _flipX, _flipY, _rotation);

            return image;
        }

        #endregion
    }
}
