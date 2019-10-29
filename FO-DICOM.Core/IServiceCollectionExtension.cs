// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.IO;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FellowOakDicom
{

    public static class IServiceCollectionExtension
    {

        public static IServiceCollection AddDefaultDicomServices(this IServiceCollection services)
            => services
            .AddSingleton<IFileReferenceFactory, FileReferenceFactory>()
            .AddTransient<IFileReference, FileReference>()
            .UseImageManager<RawImageManager>();

        public static IServiceCollection UseTranscoderManager<TT>(this IServiceCollection services)
            // todo: set implementation of TranscoderManager
            => services;

        public static IServiceCollection UseImageManager<TI>(this IServiceCollection services) where TI : class, IImageManager
            => services.AddSingleton<IImageManager, TI>();


    }
}
