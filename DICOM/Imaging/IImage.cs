// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Imaging
{
    using System.Collections.Generic;

    using Dicom.Imaging.Render;
    using Dicom.IO;

    /// <summary>
    /// Image interface.
    /// </summary>
    public interface IImage : IDisposable
    {
        #region PROPERTIES

        /// <summary>
        /// Gets the array of pixels associated with the image.
        /// </summary>
        PinnedIntArray Pixels { get; }

        #endregion

        #region METHODS

        /// <summary>
        /// Cast <see cref="IImage"/> object to specific (real image) type.
        /// </summary>
        /// <typeparam name="T">Real image type to cast to.</typeparam>
        /// <returns><see cref="IImage"/> object as specific (real image) type.</returns>
        T As<T>();

        /// <summary>
        /// Renders the image given the specified parameters.
        /// </summary>
        /// <param name="components">Number of components.</param>
        /// <param name="flipX">Flip image in X direction?</param>
        /// <param name="flipY">Flip image in Y direction?</param>
        /// <param name="rotation">Image rotation.</param>
        void Render(int components, bool flipX, bool flipY, int rotation);

        /// <summary>
        /// Draw graphics onto existing image.
        /// </summary>
        /// <param name="graphics">Graphics to draw.</param>
        void DrawGraphics(IEnumerable<IGraphic> graphics);

        /// <summary>
        /// Creates a deep copy of the image.
        /// </summary>
        /// <returns>Deep copy of this image.</returns>
        IImage Clone();

        #endregion
    }
}
