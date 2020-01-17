// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    using System;

    /// <summary>
    /// Evaluation release placeholder for <see cref="ImageManager"/> implementation.
    /// </summary>
    public class UnityImageManager : ImageManager
    {
        #region PROPERTIES

        /// <summary>
        /// Gets whether or not this type is classified as a default manager.
        /// </summary>
        public override bool IsDefault => true;

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
            throw new NotSupportedException("Image creation is not supported in EVALUATION release of fo-dicom for Unity.");
        }

        #endregion
    }
}
