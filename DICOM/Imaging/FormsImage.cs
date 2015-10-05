// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class FormsImage : IImage
    {
        #region FIELDS

        private readonly Image image;

        #endregion

        #region CONSTRUCTORS

        public FormsImage(int width, int height, int components, IntPtr pixelsPtr)
        {
            var stride = 4 * width; // TODO
            var format = components == 4 ? PixelFormat.Format32bppArgb : PixelFormat.Format32bppRgb;

            this.image = new Bitmap(width, height, stride, format, pixelsPtr);
        }

        #endregion

        #region METHODS

        public T As<T>()
        {
            return (T)(object)this.image;
        }

        #endregion
    }
}