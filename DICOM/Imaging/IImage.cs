// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System.Collections.Generic;

    using Dicom.Imaging.Render;

    /// <summary>
    /// Image interface.
    /// </summary>
    public interface IImage
    {
        #region METHODS

        /// <summary>
        /// Cast <see cref="IImage"/> object to specific (real image) type.
        /// </summary>
        /// <typeparam name="T">Real image type to cast to.</typeparam>
        /// <returns><see cref="IImage"/> object as specific (real image) type.</returns>
        T As<T>();

        /// <summary>
        /// Draw graphics onto existing image.
        /// </summary>
        /// <param name="graphics">Graphics to draw.</param>
        void DrawGraphics(IEnumerable<IGraphic> graphics);

        #endregion
    }
}