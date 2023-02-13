// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Reflection;
using FellowOakDicom.Imaging.Render;
using FellowOakDicom.IO;

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// Base class for image implementations.
    /// </summary>
    /// <typeparam name="TImage">Image implementation type.</typeparam>
    public abstract class ImageBase<TImage> : IImage where TImage : class 
    {
        #region FIELDS

        protected readonly int _width;

        protected readonly int _height;

        protected PinnedIntArray _pixels;

        protected TImage _image;

        protected bool _disposed;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="ImageBase{TImage}"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="pixels">Array of pixels.</param>
        /// <param name="image">Image object.</param>
        protected ImageBase(int width, int height, PinnedIntArray pixels, TImage image)
        {
            _width = width;
            _height = height;
            _pixels = pixels;
            _image = image;
            _disposed = false;
        }

		/// <summary>
		/// Destructor to free up the image resources.
		/// </summary>
		~ImageBase()
		{
			Dispose(false);
		}

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the array of pixels associated with the image.
        /// </summary>
        public PinnedIntArray Pixels => _pixels;

        public int Height => _height;

        public int Width => _width;

        #endregion

        #region METHODS

        /// <summary>
        /// Cast <see cref="IImage"/> object to specific (real image) type.
        /// </summary>
        /// <typeparam name="T">Real image type to cast to.</typeparam>
        /// <returns><see cref="IImage"/> object as specific (real image) type.</returns>
        public virtual T As<T>()
        {
            if (_image == null)
            {
                throw new DicomImagingException("Image has not yet been rendered.");
            }

            if (!typeof(T).GetTypeInfo().IsAssignableFrom(typeof(TImage).GetTypeInfo()))
            {
                throw new DicomImagingException($"Cannot cast to '{typeof(T).Name}'; type must be assignable from '{typeof(TImage).Name}'");
            }

            return (T)(object)_image;
        }

        /// <summary>
        /// Renders the image given the specified parameters.
        /// </summary>
        /// <param name="components">Number of components.</param>
        /// <param name="flipX">Flip image in X direction?</param>
        /// <param name="flipY">Flip image in Y direction?</param>
        /// <param name="rotation">Image rotation.</param>
        public abstract void Render(int components, bool flipX, bool flipY, int rotation);

        /// <summary>
        /// Draw graphics onto existing image.
        /// </summary>
        /// <param name="graphics">Graphics to draw.</param>
        public abstract void DrawGraphics(IEnumerable<IGraphic> graphics);

        /// <summary>
        /// Creates a deep copy of the image.
        /// </summary>
        /// <returns>Deep copy of this image.</returns>
        public abstract IImage Clone();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose resources.
        /// </summary>
        /// <param name="disposing">Dispose mode?</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _image = null;

            if (_pixels != null)
            {
                _pixels.Dispose();
                _pixels = null;
            }

            _disposed = true;
        }

        protected static byte[] ToBytes(ref int width, ref int height, int components, bool flipX, bool flipY, int rotation, int[] data)
        {
            var processed = Rotate(ref width, ref height, rotation, data);
            processed = Flip(width, height, flipX, flipY, processed);

            // TODO Consider to make use of "components"
            var length = 4 * width * height;
            var bytes = new byte[length];

            Buffer.BlockCopy(processed, 0, bytes, 0, length);
            return bytes;
        }

        public Color32 GetPixel(int x, int y)
        {
            if (x >= 0 && x < _width && y >= 0 && y < _height)
            {
                return new Color32(_pixels.Data[x + y * _width]);
            }
            return Color32.Black;
        }

        private static int[] Rotate(ref int width, ref int height, int angle, int[] data)
        {
            int[] result;
            angle %= 360;

            var i = 0;
            if (angle > 0 && angle <= 90)
            {
                result = new int[width * height];
                for (var x = 0; x < width; x++)
                {
                    for (var y = height - 1; y >= 0; y--, i++)
                    {
                        result[i] = data[y * width + x];
                    }
                }
                (height, width) = (width, height);
            }
            else if (angle > 90 && angle <= 180)
            {
                result = new int[width * height];
                for (var y = height - 1; y >= 0; y--)
                {
                    for (var x = width - 1; x >= 0; x--, i++)
                    {
                        result[i] = data[y * width + x];
                    }
                }
            }
            else if (angle > 180 && angle <= 270)
            {
                result = new int[width * height];
                for (var x = width - 1; x >= 0; x--)
                {
                    for (var y = 0; y < height; y++, i++)
                    {
                        result[i] = data[y * width + x];
                    }
                }
                (height, width) = (width, height);
            }
            else
            {
                result = data;
            }
            return result;
        }

        private static int[] Flip(int w, int h, bool flipX, bool flipY, int[] p)
        {
            // TODO: optimize this method, so that the image is not processed twice
            // if both flipX and flipY are set.
            // maybe int xStart, xEnd, xStep, yStart, yEnd, yStep. and then one loop with these parameters
            // is some parallell execution allowed here?
            int[] tmp, result;

            if (flipX)
            {
                tmp = new int[w * h];
                var i = 0;
                for (var y = 0; y < h; y++)
                {
                    for (var x = w - 1; x >= 0; x--, i++)
                    {
                        tmp[i] = p[y * w + x];
                    }
                }
            }
            else
            {
                tmp = p;
            }
            if (flipY)
            {
                var i = 0;
                result = new int[w * h];
                for (var y = h - 1; y >= 0; y--)
                {
                    for (var x = 0; x < w; x++, i++)
                    {
                        result[i] = tmp[y * w + x];
                    }
                }
            }
            else
            {
                result = tmp;
            }

            return result;
        }

        #endregion
    }
}
