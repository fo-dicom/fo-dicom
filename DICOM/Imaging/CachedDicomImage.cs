using Dicom;
using Dicom.Imaging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dicom.Imaging
{

    public class CachedDicomImage : IDisposable
    {

        private DicomImage _dicomImage;
        private bool _disposed;
        private IImage _cachedImage;
        private int _frame;


        static CachedDicomImage()
        {
        }


        public CachedDicomImage(DicomDataset dataset, int frame = 0)
        {
            _dicomImage = new DicomImage(dataset, frame);
            _frame = frame;
        }

        public CachedDicomImage(string fileName, int frame = 0)
        {
            _dicomImage = new DicomImage(fileName, frame);
            _frame = frame;
        }

        ~CachedDicomImage()
        {
            Dispose(false);
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

        public double WindowCenter
        {
            get { return _dicomImage.WindowCenter; }
            set
            {
                _dicomImage.WindowCenter = value;
                ClearCachedImage();
            }
        }

        public double WindowWidth
        {
            get { return _dicomImage.WindowWidth; }
            set
            {
                _dicomImage.WindowWidth = value;
                ClearCachedImage();
            }
        }

        public Color32[] GrayscaleColorMap
        {
            get { return _dicomImage.GrayscaleColorMap; }
            set
            {
                _dicomImage.GrayscaleColorMap = value;
                ClearCachedImage();
            }
        }

        public int OverlayColor
        {
            get { return _dicomImage.OverlayColor; }
            set
            {
                _dicomImage.OverlayColor = value;
                ClearCachedImage();
            }
        }

        public double Scale
        {
            get { return _dicomImage.Scale; }
            set
            {
                _dicomImage.Scale = value;
                ClearCachedImage();
            }
        }

        public bool ShowOverlays
        {
            get { return _dicomImage.ShowOverlays; }
            set
            {
                _dicomImage.ShowOverlays = value;
                ClearCachedImage();
            }
        }

        public IImage RenderImage()
        {
            lock (this)
            {

                if (_cachedImage != null)
                    _cachedImage = _dicomImage.RenderImage(_frame);

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
