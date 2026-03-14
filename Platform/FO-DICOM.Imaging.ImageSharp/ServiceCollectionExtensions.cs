// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.Media;
using Microsoft.Extensions.DependencyInjection;

namespace FellowOakDicom
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all available services for imaging and rendering with ImageSharp to fo-dicom
        /// </summary>
        public static ServiceCollection AddImageSharpImaging(this ServiceCollection services)
        {
            services.AddImageManager<ImageSharpImageManager>();
            services.AddSingleton<IIconGenerator, ImageSharpIconGenerator>();
            return services;
        }
    }
}
