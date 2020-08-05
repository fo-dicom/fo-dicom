// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    /// <summary>
    /// BGRA byte array implementation of the <see cref="ImageManager"/>.
    /// </summary>
    public sealed class RawImageManager : ImageManager
    {
        #region FIELDS

        /// <summary>
        /// Single instance of the BGRA byte array image manager.
        /// </summary>
        public static readonly ImageManager Instance;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static fields of <see cref="RawImageManager"/>
        /// </summary>
        static RawImageManager()
        {
            Instance = new RawImageManager();
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets whether or not this type is classified as a default manager.
        /// </summary>
        public override bool IsDefault
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Create <see cref="IImage"/> object using the current implementation.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <returns><see cref="IImage"/> object using the current implementation.</returns>
        protected override IImage CreateImageImpl(int width, int height)
        {
            return new RawImage(width, height);
        }

        #endregion
    }
}
