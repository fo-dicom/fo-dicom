// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.AspNetCore.Configs;
using FellowOakDicom.AspNetCore.Server;
using FellowOakDicom.Network;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.AspNetCore
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Adds default implementations of all required services to the collection if the services haven't already been registered
        /// </summary>
        public static IServiceCollection UseFellowOakDicom(this IServiceCollection services)
            => services.AddFellowOakDicom()
                .AddTransient<IHostedService, DicomInitializationHelper>(provider => {
                    DicomSetupBuilder.UseServiceProvider(provider);
                    return new DicomInitializationHelper();
                    })
          ;

        #region Add DicomServer with own class implementation

        public static IServiceCollection AddDicomServer<T>(
            this IServiceCollection services,
            IConfiguration configurationRoot,
            Action<DicomConfiguration> configureAction = null) where T : DicomService, IDicomServiceProvider
        {
            var dicomConfiguration = new DicomConfiguration();
            configurationRoot?.GetSection(DicomConfiguration.SectionName).Bind(dicomConfiguration);
            configureAction?.Invoke(dicomConfiguration);

            services
                .AddSingleton(Options.Create(dicomConfiguration))
                .AddSingleton(Options.Create(dicomConfiguration.ServerOptions))
                .AddSingleton(Options.Create(dicomConfiguration.ClientOptions))
                .AddSingleton(Options.Create(dicomConfiguration.ServiceOptions))
                .UseFellowOakDicom()
                .AddOptions()
                .AddTransient<IHostedService>(s =>
                {
                    var dicomService = new DicomServerService<T>(s.GetRequiredService<IDicomServerFactory>(), s.GetRequiredService<IOptions<DicomConfiguration>>());
                    return dicomService;
                });

            return services;
        }

        public static IServiceCollection AddDicomServer<T>(this IServiceCollection services) where T : DicomService, IDicomServiceProvider
            => services
            .UseFellowOakDicom()
            .AddTransient<IHostedService>(s =>
            {
                var dicomService = new DicomServerService<T>(s.GetRequiredService<IDicomServerFactory>(), s.GetRequiredService<IOptions<DicomConfiguration>>());
                return dicomService;
            });

        #endregion

        #region Add General Purpose Service

        public static IServiceCollection AddDicomServer(this IServiceCollection services, Action<DicomServiceBuilder> builderAction)
            => services
            .UseFellowOakDicom()
            .AddTransient<IHostedService>(s =>
            {
                var builder = new DicomServiceBuilder();
                builderAction(builder);
                var dicomService = new GeneralPurposeDicomServerService(s.GetRequiredService<IDicomServerFactory>(), builder, s.GetRequiredService<IOptions<DicomConfiguration>>());
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
