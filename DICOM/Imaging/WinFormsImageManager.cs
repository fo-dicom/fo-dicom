﻿// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using Dicom.IO;

    /// <summary>
    /// Windows Forms-based image manager implementation.
    /// </summary>
    public sealed class WinFormsImageManager : ImageManager
    {
        #region FIELDS

        /// <summary>
        /// Single instance of the Windows Forms image manager.
        /// </summary>
        public static readonly ImageManager Instance;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static fields of <see cref="WinFormsImageManager"/>
        /// </summary>
        static WinFormsImageManager()
        {
            Instance = new WinFormsImageManager();
        }

        /// <summary>
        /// Initializes a <see cref="WinFormsImageManager"/> object.
        /// </summary>
        private WinFormsImageManager()
        {
        }

        #endregion

        #region METHODS

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
        protected override IImage CreateImageImpl(int width, int height, int components, bool flipX, bool flipY, int rotation, PinnedIntArray pixels)
        {
            return new WinFormsImage(width, height, components, flipX, flipY, rotation, pixels);
        }

        #endregion
    }
}