// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO;
using Microsoft.Extensions.DependencyInjection;

namespace FellowOakDicom
{

    public static class IServiceCollectionExtension
    {

        public static IServiceCollection AddDefaultDicomServices(this IServiceCollection services)
        {
            services.AddSingleton<IFileReferenceFactory, FileReferenceFactory>();
            services.AddTransient<IFileReference, FileReference>();
            return services;
        }


    }
}
