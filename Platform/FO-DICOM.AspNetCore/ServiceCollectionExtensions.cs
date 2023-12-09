// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

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
                .AddTransient<IHostedService, DicomInitializationHelper>(provider => {
                    DicomSetupBuilder.UseServiceProvider(provider);
                    return new DicomInitializationHelper();
                    })
          ;

        #region Add DicomServer with own class implementation

        public static IServiceCollection AddDicomServer<T>(this IServiceCollection services, DicomServerServiceOptions options) where T : DicomService, IDicomServiceProvider
            => services
            .UseFellowOakDicom()
            .AddTransient<IHostedService>(s =>
            {
                var dicomService = new DicomServerService<T>(s.GetRequiredService<IConfiguration>(), s.GetRequiredService<IDicomServerFactory>())
                {
                    Options = options
                };
                return dicomService;
            });

        public static IServiceCollection AddDicomServer<T>(this IServiceCollection services, Action<DicomServerServiceOptions> optionsAction) where T : DicomService, IDicomServiceProvider
            => services
            .UseFellowOakDicom()
            .AddTransient<IHostedService>(s =>
            {
                var dicomService = new DicomServerService<T>(s.GetRequiredService<IConfiguration>(), s.GetRequiredService<IDicomServerFactory>());
                optionsAction(dicomService.Options);
                return dicomService;
            });

        #endregion

        #region Add General Purpose Service

        public static IServiceCollection AddDicomServer(this IServiceCollection services, Action<DicomServerServiceOptions> optionsAction, Action<DicomServiceBuilder> builderAction)
            => services
            .UseFellowOakDicom()
            .AddTransient<IHostedService>(s =>
            {
                var builder = new DicomServiceBuilder();
                builderAction(builder);
                var dicomService = new GeneralPurposeDicomServerService(s.GetRequiredService<IConfiguration>(), s.GetRequiredService<IDicomServerFactory>(), builder);
                optionsAction(dicomService.Options);
                return dicomService;
            });

        #endregion
    }


    public class DicomInitializationHelper : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

}
