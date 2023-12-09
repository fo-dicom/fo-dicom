// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Linq;
using FellowOakDicom.Imaging.LUT;

namespace FellowOakDicom.Imaging.Render
{

    /// <summary>
    /// The Composite Graphic implementation of <see cref="IGraphic"/> which layers graphics one over the other
    /// </summary>
    public class CompositeGraphic : IGraphic
    {
        #region Private Members

        private readonly List<IGraphic> _layers = new List<IGraphic>();

        #endregion

        #region Public Constructor

        /// <summary>
        /// Initialize new instance of <see cref="CompositeGraphic"/>
        /// </summary>
        /// <param name="bg">background (initial) graphic layer</param>
        public CompositeGraphic(IGraphic bg)
        {
            _layers.Add(bg);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The backgroun graphic layer
        /// </summary>
        public IGraphic BackgroundLayer
        {
            get
            {
                return _layers[0];
            }
        }

        public int OriginalWidth
        {
            get
            {
                return BackgroundLayer.OriginalWidth;
            }
        }

        public int OriginalHeight
        {
            get
            {
                return BackgroundLayer.OriginalHeight;
            }
        }

        public int OriginalOffsetX
        {
            get
            {
                return 0;
            }
        }

        public int OriginalOffsetY
        {
            get
            {
                return 0;
            }
        }

        public double ScaleFactor
        {
            get
            {
                return BackgroundLayer.ScaleFactor;
            }
        }

        public int ScaledWidth
        {
            get
            {
                return BackgroundLayer.ScaledWidth;
            }
        }

        public int ScaledHeight
        {
            get
            {
                return BackgroundLayer.ScaledHeight;
            }
        }

        public int ScaledOffsetX
        {
            get
            {
                return 0;
            }
        }

        public int ScaledOffsetY
        {
            get
            {
                return 0;
            }
        }

        public int ZOrder
        {
            get
            {
                return 0;
            }
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Add new graphic layer to the existing layers according to its Z Order
        /// </summary>
        /// <param name="layer">The layer graphic instance</param>
        public void AddLayer(IGraphic layer)
        {
            _layers.Add(layer);
            _layers.Sort(
                delegate(IGraphic a, IGraphic b)
                    {
                        if (b.ZOrder > a.ZOrder) return 1;
                        else if (a.ZOrder > b.ZOrder) return -1;
                        else return 0;
                    });
        }

        public void Reset()
        {
            foreach (IGraphic graphic in _layers) graphic.Reset();
        }

        public void Scale(double scale)
        {
            foreach (IGraphic graphic in _layers) graphic.Scale(scale);
        }

        public void BestFit(int width, int height)
        {
            foreach (IGraphic graphic in _layers) graphic.BestFit(width, height);
        }

        public void Rotate(int angle)
        {
            foreach (IGraphic graphic in _layers) graphic.Rotate(angle);
        }

        public void FlipX()
        {
            foreach (IGraphic graphic in _layers) graphic.FlipX();
        }

        public void FlipY()
        {
            foreach (IGraphic graphic in _layers) graphic.FlipY();
        }

        public void Transform(double scale, int rotation, bool flipx, bool flipy)
        {
            foreach (IGraphic graphic in _layers) graphic.Transform(scale, rotation, flipx, flipy);
        }

        public IImage RenderImage(ILUT lut)
        {
            var img = BackgroundLayer.RenderImage(lut);
            if (_layers.Count > 1)
            {
                img.DrawGraphics(_layers.Skip(1));
            }
            return img;
        }

        #endregion
    }
}
