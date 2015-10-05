// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System;

    public abstract class ImageManager
    {
        #region FIELDS

        private static ImageManager implementation;

        #endregion

        #region CONSTRUCTORS

        static ImageManager()
        {
            SetImplementation(FormsImageManager.Instance);
        }

        #endregion

        #region METHODS

        public static void SetImplementation(ImageManager impl)
        {
            implementation = impl;
        }

        public IImage CreateImage(int width, int height, int components, IntPtr pixelsPtr)  // TODO PinnedIntArray?
        {
            return implementation.CreateImageImpl(width, height, components, pixelsPtr);
        }

        protected abstract IImage CreateImageImpl(int width, int height, int components, IntPtr pixelsPtr);

        #endregion
    }
}