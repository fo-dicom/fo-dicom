// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System;

    public class FormsImageManager : ImageManager
    {
        #region FIELDS

        /// <summary>
        /// Single instance of the desktop I/O manager.
        /// </summary>
        public static readonly FormsImageManager Instance;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes the static fields of <see cref="FormsImageManager"/>
        /// </summary>
        static FormsImageManager()
        {
            Instance = new FormsImageManager();
        }

        /// <summary>
        /// Initializes a <see cref="FormsImageManager"/> object.
        /// </summary>
        private FormsImageManager()
        {
        }

        #endregion

        #region METHODS

        protected override IImage CreateImageImpl(int width, int height, int components, IntPtr pixelsPtr)
        {
            return new FormsImage(width, height, components, pixelsPtr);
        }

        #endregion
    }
}