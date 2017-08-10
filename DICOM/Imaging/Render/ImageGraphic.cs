// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;

using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render
{
    /// <summary>
    /// The Image Graphic implementation of <seealso cref="IGraphic"/>
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
        /// Number of pixel componenets (samples)
        /// </summary>
        public int Components => OriginalData.Components;

        /// <summary>
        /// Original pixel data
        /// </summary>
        public IPixelData OriginalData => _originalData;

        public int OriginalWidth => _originalData.Width;

        public int OriginalHeight => _originalData.Height;

        public int OriginalOffsetX => _offsetX;

        public int OriginalOffsetY => _offsetY;

        public double ScaleFactor => _scaleFactor;

        /// <summary>
        /// Scaled pixel data
        /// </summary>
        public IPixelData ScaledData
        {
            get
            {
                if (_scaledData == null)
                {
                    _scaledData = Math.Abs(_scaleFactor - 1.0) <= double.Epsilon
                        ? _originalData
                        : OriginalData.Rescale(_scaleFactor);
                }

                return _scaledData;
            }
        }

        public int ScaledWidth => ScaledData.Width;

        public int ScaledHeight => ScaledData.Height;

        public int ScaledOffsetX => (int)(_offsetX * _scaleFactor);

        public int ScaledOffsetY => (int)(_offsetY * _scaleFactor);

        public int ZOrder
        {
            get
            {
                return _zorder;
            }
            set
            {
                _zorder = value;
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <seealso cref="ImageGraphic"/>
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

        public void Reset()
        {
            Scale(1.0);
            _rotation = 0;
            _flipX = false;
            _flipY = false;
        }

        public void Scale(double scale)
        {
            if (Math.Abs(scale - _scaleFactor) <= double.Epsilon) return;

            _scaleFactor = scale;
            _scaledData = null;

            foreach (var overlay in _overlays)
            {
                overlay.Scale(scale);
            }
        }

        public void BestFit(int width, int height)
        {
            double xF = (double)width / OriginalWidth;
            double yF = (double)height / OriginalHeight;
            Scale(Math.Min(xF, yF));
        }

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
            if (angle != 0)
            {
                if (_rotation >= 360) _rotation -= 360;
                else if (_rotation < 0) _rotation += 360;
            }
        }

        public void FlipX()
        {
            _flipX = !_flipX;
        }

        public void FlipY()
        {
            _flipY = !_flipY;
        }

        public void Transform(double scale, int rotation, bool flipx, bool flipy)
        {
            Scale(scale);
            Rotate(rotation);
            _flipX = flipx;
            _flipY = flipy;
        }

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
