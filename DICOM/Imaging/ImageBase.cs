// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Dicom.Imaging.Render;
    using Dicom.IO;

    /// <summary>
    /// Base class for image implementations.
    /// </summary>
    /// <typeparam name="TImage">Image implementation type.</typeparam>
    public abstract class ImageBase<TImage> : IImage where TImage : class 
    {
        #region FIELDS

        protected readonly int width;

        protected readonly int height;

        protected PinnedIntArray pixels;

        protected TImage image;

        protected bool disposed;

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
            this.width = width;
            this.height = height;
            this.pixels = pixels;
            this.image = image;
            this.disposed = false;
        }

		/// <summary>
		/// Destructor to free up the image resources.
		/// </summary>
		~ImageBase()
		{
			this.Dispose(false);
		}
		
        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the array of pixels associated with the image.
        /// </summary>
        public PinnedIntArray Pixels
        {
            get
            {
                return this.pixels;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Cast <see cref="IImage"/> object to specific (real image) type.
        /// </summary>
        /// <typeparam name="T">Real image type to cast to.</typeparam>
        /// <returns><see cref="IImage"/> object as specific (real image) type.</returns>
        public virtual T As<T>()
        {
            if (this.image == null)
            {
                throw new DicomImagingException("Image has not yet been rendered.");
            }

            if (!typeof(T).GetTypeInfo().IsAssignableFrom(typeof(TImage).GetTypeInfo()))
            {
                throw new DicomImagingException(
                    "Cannot cast to '{0}'; type must be assignable from '{1}'",
                    typeof(T).Name,
                    typeof(TImage).Name);
            }

            return (T)(object)this.image;
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
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose resources.
        /// </summary>
        /// <param name="disposing">Dispose mode?</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed) return;

            this.image = null;

            if (this.pixels != null)
            {
                this.pixels.Dispose();
                this.pixels = null;
            }

            this.disposed = true;
        }

        protected static byte[] ToBytes(
            ref int width,
            ref int height,
            int components,
            bool flipX,
            bool flipY,
            int rotation,
            int[] data)
        {
            var processed = Rotate(ref width, ref height, rotation, data);
            processed = Flip(width, height, flipX, flipY, processed);

            // TODO Consider to make use of "components"
            var length = 4 * width * height;
            var bytes = new byte[length];

            Buffer.BlockCopy(processed, 0, bytes, 0, length);
            return bytes;
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
                var tmp = width;
                width = height;
                height = tmp;
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
                var tmp = width;
                width = height;
                height = tmp;
            }
            else
            {
                result = data;
            }
            return result;
        }

        private static int[] Flip(int w, int h, bool flipX, bool flipY, int[] p)
        {
            var i = 0;
            int[] tmp, result;

            if (flipX)
            {
                tmp = new int[w * h];
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
