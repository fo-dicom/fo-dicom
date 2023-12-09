// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.DependencyInjection;


namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// Manager for creation of image objects.
    /// </summary>
    public class ImageManager
    {

        #region METHODS

        /// <summary>
        /// Create <see cref="IImage"/> object.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <returns><see cref="IImage"/> object.</returns>
        public static IImage CreateImage(int width, int height)
            => Setup.ServiceProvider.GetService<IImageManager>().CreateImage(width, height);


        #endregion
    }
}
