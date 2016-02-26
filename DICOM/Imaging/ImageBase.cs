// Copyright (c) 2012-2016 fo-dicom contributors.
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

        #endregion
    }
}