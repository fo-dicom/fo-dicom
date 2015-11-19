// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System;

    using Dicom.IO;

    /// <summary>
    /// Base class for image implementations where underlying image type is disposable.
    /// </summary>
    /// <typeparam name="TImage">Disposable image implementation type.</typeparam>
    public abstract class ImageDisposableBase<TImage> : ImageBase<TImage> where TImage : class, IDisposable
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="ImageDisposableBase{TImage}"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="pixels">Array of pixels.</param>
        /// <param name="image">Image object.</param>
        protected ImageDisposableBase(int width, int height, PinnedIntArray pixels, TImage image)
            : base(width, height, pixels, image)
        {
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Dispose resources.
        /// </summary>
        /// <param name="disposing">Dispose mode?</param>
        protected override void Dispose(bool disposing)
        {
            if (this.disposed) return;

            if (this.image != null)
            {
                this.image.Dispose();
                this.image = null;
            }

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