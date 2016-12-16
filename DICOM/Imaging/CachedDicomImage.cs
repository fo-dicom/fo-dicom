using Dicom;
using Dicom.Imaging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dicom.Imaging
{

    public class CachedDicomImage : DicomImage, IDisposable
    {

        private bool _disposed;
        private IImage _cachedImage;


        static CachedDicomImage()
        {
        }

        /// <summary>
        /// Clears and disposes the cached IImage
        /// </summary>
        public void ClearCachedImage()
        {
            if (_cachedImage != null)
                _cachedImage.Dispose();
            _cachedImage = null;
        }

        public CachedDicomImage(DicomDataset dataset, int frame = 0)
            : base(dataset, frame)
        {

        }

        public CachedDicomImage(string fileName, int frame = 0)
            : base(fileName, frame)
        {

        }

        ~CachedDicomImage()
        {
            Dispose(false);
        }

        public override double WindowCenter
        {
            get { return base.WindowCenter; }
            set
            {
                base.WindowCenter = value;
                ClearCachedImage();
            }
        }

        public override double WindowWidth
        {
            get { return base.WindowWidth; }
            set
            {
                base.WindowWidth = value;
                ClearCachedImage();
            }
        }

        public override Color32[] GrayscaleColorMap
        {
            get { return base.GrayscaleColorMap; }
            set
            {
                base.GrayscaleColorMap = value;
                ClearCachedImage();
            }
        }

        public override int OverlayColor
        {
            get { return base.OverlayColor; }
            set
            {
                base.OverlayColor = value;
                ClearCachedImage();
            }
        }

        public override double Scale
        {
            get { return base.Scale; }
            set
            {
                base.Scale = value;
                ClearCachedImage();
            }
        }

        public override bool ShowOverlays
        {
            get { return base.ShowOverlays; }
            set
            {
                base.ShowOverlays = value;
                ClearCachedImage();
            }
        }

        public override IImage RenderImage(int frame = 0)
        {
            lock (this)
            {
                if (frame != CurrentFrame)
                    ClearCachedImage();

                if (_cachedImage != null)
                    _cachedImage = base.RenderImage(frame);

                return _cachedImage;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                ClearCachedImage();
                _disposed = true;
            }

        }

        public void Dispose()
        {
            Dispose(true);
        }

    }
}
