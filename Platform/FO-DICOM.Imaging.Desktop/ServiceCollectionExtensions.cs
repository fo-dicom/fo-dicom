// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.Media;
using Microsoft.Extensions.DependencyInjection;

namespace FellowOakDicom
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all available services for imaging and rendering with WinForms to fo-dicom
        /// </summary>
        public static IServiceCollection AddWinFormsImaging(this IServiceCollection services)
        {
            services.AddImageManager<WinFormsImageManager>();
            services.AddSingleton<IIconGenerator, DesktopIconGenerator>();
            return services;
        }

        /// <summary>
        /// Registers all available services for imaging and rendering with WPF to fo-dicom
        /// </summary>
        public static IServiceCollection AddWPFImaging(this IServiceCollection services)
        {
            services.AddImageManager<WPFImageManager>();
            services.AddSingleton<IIconGenerator, DesktopIconGenerator>();
            return services;
        }

    }
}
