// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using Dicom.IO;

    /// <summary>
    /// Manager for creation of image objects.
    /// </summary>
    public abstract class ImageManager : IClassifiedManager
    {
        #region FIELDS

        /// <summary>
        /// Image manager implementation.
        /// </summary>
        private static ImageManager implementation;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Static constructor to identify the default manager setting.
        /// </summary>
        static ImageManager()
        {
            SetImplementation(Setup.GetDefaultPlatformInstance<ImageManager>());
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets whether or not this type is classified as a default manager.
        /// </summary>
        public abstract bool IsDefault { get; }

        #endregion

        #region METHODS

        /// <summary>
        /// Set image manager implementation to use.
        /// </summary>
        /// <param name="impl">Implementation to use.</param>
        public static void SetImplementation(ImageManager impl)
        {
            implementation = impl;
        }

        /// <summary>
        /// Create <see cref="IImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="components">Number of components.</param>
        /// <param name="flipX">Flip image in X direction?</param>
        /// <param name="flipY">Flip image in Y direction?</param>
        /// <param name="rotation">Image rotation.</param>
        /// <param name="pixels">Array of pixels.</param>
        /// <returns><see cref="IImage"/> object.</returns>
        public static IImage CreateImage(int width, int height, int components, bool flipX, bool flipY, int rotation, PinnedIntArray pixels)
        {
            return implementation.CreateImageImpl(width, height, components, flipX, flipY, rotation, pixels);
        }

        /// <summary>
        /// Create <see cref="IImage"/> object using the current implementation.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="components">Number of components.</param>
        /// <param name="flipX">Flip image in X direction?</param>
        /// <param name="flipY">Flip image in Y direction?</param>
        /// <param name="rotation">Image rotation.</param>
        /// <param name="pixels">Array of pixels.</param>
        /// <returns><see cref="IImage"/> object using the current implementation.</returns>
        protected abstract IImage CreateImageImpl(int width, int height, int components, bool flipX, bool flipY, int rotation, PinnedIntArray pixels);

        #endregion
    }
}