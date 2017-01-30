// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Render
{
    using System;
    using System.Collections.Generic;

    using Dicom.Imaging.LUT;

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
        public int Components
        {
            get
            {
                return OriginalData.Components;
            }
        }

        /// <summary>
        /// Original pixel data
        /// </summary>
        public IPixelData OriginalData
        {
            get
            {
                return _originalData;
            }
        }

        public int OriginalWidth
        {
            get
            {
                return _originalData.Width;
            }
        }

        public int OriginalHeight
        {
            get
            {
                return _originalData.Height;
            }
        }

        public int OriginalOffsetX
        {
            get
            {
                return _offsetX;
            }
        }

        public int OriginalOffsetY
        {
            get
            {
                return _offsetY;
            }
        }

        public double ScaleFactor
        {
            get
            {
                return _scaleFactor;
            }
        }

        /// <summary>
        /// Scaled pixel data
        /// </summary>
        public IPixelData ScaledData
        {
            get
            {
                if (_scaledData == null)
                {
                    if (Math.Abs(_scaleFactor - 1.0) <= Double.Epsilon) _scaledData = _originalData;
                    else _scaledData = OriginalData.Rescale(_scaleFactor);
                }
                return _scaledData;
            }
        }

        public int ScaledWidth
        {
            get
            {
                return ScaledData.Width;
            }
        }

        public int ScaledHeight
        {
            get
            {
                return ScaledData.Height;
            }
        }

        public int ScaledOffsetX
        {
            get
            {
                return (int)(_offsetX * _scaleFactor);
            }
        }

        public int ScaledOffsetY
        {
            get
            {
                return (int)(_offsetY * _scaleFactor);
            }
        }

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
            if (Math.Abs(scale - _scaleFactor) <= Double.Epsilon) return;

            _scaleFactor = scale;
            _scaledData = null;

            foreach (var overlay in _overlays)
            {
                overlay.Scale(scale);
            }
        }

        public void BestFit(int width, int height)
        {
            double xF = (double)width / (double)OriginalWidth;
            double yF = (double)height / (double)OriginalHeight;
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
            if (this._applyLut && lut != null && !lut.IsValid)
            {
                lut.Recalculate();
            }

            var image = ImageManager.CreateImage(this.ScaledWidth, this.ScaledHeight);

            var pixels = image.Pixels.Data;
            this.ScaledData.Render(this._applyLut ? lut : null, pixels);

            foreach (var overlay in this._overlays)
            {
                overlay.Render(pixels, this.ScaledWidth, this.ScaledHeight);
            }

            image.Render(this.Components, this._flipX, this._flipY, this._rotation);

            return image;
        }

        #endregion
    }
}
