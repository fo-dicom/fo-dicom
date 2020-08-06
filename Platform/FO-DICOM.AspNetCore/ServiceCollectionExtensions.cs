// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.AspNetCore.Server;
using Microsoft.Extensions.DependencyInjection;

namespace FellowOakDicom.AspNetCore
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection UseFellowOakDicom(this IServiceCollection services)
            => services.AddFellowOakDicom()
                .AddLogManager<DicomLogManager>()
          ;


        public static IServiceCollection AddDicomServer(this IServiceCollection services)
            => services
            .UseFellowOakDicom()
            .AddHostedService<DicomServerService>();

    }
}
