// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.AspNetCore.Server;
using FellowOakDicom.Network;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.AspNetCore
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection UseFellowOakDicom(this IServiceCollection services)
            => services.AddFellowOakDicom()
                .AddLogManager<DicomLogManager>()
                .AddTransient<IHostedService, DicomInitializationHelper>(provider => {
                    DicomSetupBuilder.UseServiceProvider(provider);
                    return new DicomInitializationHelper();
                    })
          ;


        public static IServiceCollection AddDicomServer(this IServiceCollection services, DicomServerServiceOptions options)
            => services
            .UseFellowOakDicom()
            .AddHostedService(services =>
            {
                var dicomService = new DicomServerService(services.GetRequiredService<IConfiguration>(), services.GetRequiredService<IDicomServerFactory>());
                dicomService.Options = options;
                return dicomService;
            });

        public static IServiceCollection AddDicomServer(this IServiceCollection services, Action<DicomServerServiceOptions> optionsAction)
            => services
            .UseFellowOakDicom()
            .AddHostedService(services =>
            {
                var dicomService = new DicomServerService(services.GetRequiredService<IConfiguration>(), services.GetRequiredService<IDicomServerFactory>());
                optionsAction(dicomService.Options);
                return dicomService;
            });

    }


    public class DicomInitializationHelper : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

}
