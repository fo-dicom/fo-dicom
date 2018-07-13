// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
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
        /// <returns><see cref="IImage"/> object.</returns>
        public static IImage CreateImage(int width, int height)
        {
            return implementation.CreateImageImpl(width, height);
        }

        /// <summary>
        /// Create <see cref="IImage"/> object using the current implementation.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <returns><see cref="IImage"/> object using the current implementation.</returns>
        protected abstract IImage CreateImageImpl(int width, int height);

        #endregion
    }
}
