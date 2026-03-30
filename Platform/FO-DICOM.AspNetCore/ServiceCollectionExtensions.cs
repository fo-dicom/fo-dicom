// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.AspNetCore.Configs;
using FellowOakDicom.AspNetCore.Server;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
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
        public static IServiceCollection UseFellowOakDicom(this IServiceCollection services, IConfiguration namedConfigurationSection = null, Action<DicomServiceOptions> configureServiceOptions = null, Action<DicomClientOptions> configureClientOptions = null, Action<DicomServerOptions> configureServerOptions = null)
            => services.AddFellowOakDicom(namedConfigurationSection, configureServiceOptions, configureClientOptions, configureServerOptions)
                .AddTransient<IHostedService, DicomInitializationHelper>(provider => {
                    DicomSetupBuilder.UseServiceProvider(provider);
                    return new DicomInitializationHelper();
                    })
          ;

        #region Add DicomServer with own class implementation

        public static IServiceCollection AddDicomServer<T>(
            this IServiceCollection services,
            IConfiguration namedConfigurationSection = null,
            Action<ServerConfiguration> configureAction = null) where T : DicomService, IDicomServiceProvider
        {
            var optionsBuilder = services.AddOptions<ServerConfiguration>();
            if (namedConfigurationSection != null )
            {
                optionsBuilder.Bind(namedConfigurationSection);
            }
            if (configureAction == null)
            {
                optionsBuilder.Configure(configureAction);
            }

            services
                .UseFellowOakDicom()
                .AddOptions()
                .AddTransient<IHostedService>(s =>
                {
                    var dicomService = new DicomServerService<T>(s.GetRequiredService<IDicomServerFactory>(), s.GetRequiredService<IOptions<ServerConfiguration>>());
                    return dicomService;
                });

            return services;
        }

        #endregion

        #region Add General Purpose Service

        public static IServiceCollection AddDicomServer(this IServiceCollection services, Action<DicomServiceBuilder> builderAction)
            => services
            .UseFellowOakDicom()
            .AddTransient<IHostedService>(s =>
            {
                var builder = new DicomServiceBuilder();
                builderAction(builder);
                var dicomService = new GeneralPurposeDicomServerService(s.GetRequiredService<IDicomServerFactory>(), builder, s.GetRequiredService<IOptions<ServerConfiguration>>());
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
