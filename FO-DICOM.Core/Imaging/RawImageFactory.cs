// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// BGRA byte array implementation of the <see cref="ImageFactory"/>.
    /// </summary>
    public sealed class RawImageFactory : IImageFactory
    {
 
        /// <summary>
        /// Create <see cref="IImage"/> object using the current implementation.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <returns><see cref="IImage"/> object using the current implementation.</returns>
        public IImage CreateImage(int width, int height)
            => new RawImage(width, height);

    }
}
