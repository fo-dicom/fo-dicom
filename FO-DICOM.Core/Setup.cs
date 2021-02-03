// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.IO;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace FellowOakDicom
{
    public class DicomSetupBuilder
    {
        private readonly IServiceCollection _serviceCollection;

        public DicomSetupBuilder()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddFellowOakDicom();
        }

        public void Build()
        {
            var provider = _serviceCollection.BuildServiceProvider();
            Setup.SetupDI(provider);
        }

        public DicomSetupBuilder RegisterServices(Action<IServiceCollection> registerAction)
        {
            registerAction?.Invoke(_serviceCollection);
            return this;
        }

        public static void UseServiceProvider(IServiceProvider provider) => Setup.SetupDI(provider);
        public static void UseServiceProvider(IServiceProviderHost provider) => Setup.SetupDI(provider);

    }

    /// <summary>
    /// Setup helper methods for initializing library.
    /// </summary>
    internal static class Setup
    {
        private static IServiceProviderHost _serviceProviderHost;

        internal static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProviderHost == null)
                {
                    new DicomSetupBuilder().Build();
                }

                return _serviceProviderHost.GetServiceProvider();
            }
            private set => _serviceProviderHost = new DefaultServiceProviderHost(value);
        }

        public static void SetupDI(IServiceProvider serviceProvider) => ServiceProvider = serviceProvider;

        public static void SetupDI(IServiceProviderHost serviceProviderHost) => _serviceProviderHost = serviceProviderHost;
    }

    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddFellowOakDicom(this IServiceCollection services)
            => services
                .AddInternals()    
                .AddTranscoderManager<DefaultTranscoderManager>()
                .AddImageManager<RawImageManager>()
                .AddLogManager<ConsoleLogManager>()
                .AddNetworkManager<DesktopNetworkManager>()
                .AddDicomClient()
                .AddDicomServer();

        private static IServiceCollection AddInternals(this IServiceCollection services)
            => services
                .AddSingleton<IFileReferenceFactory, FileReferenceFactory>();
        
        public static IServiceCollection AddDicomClient(this IServiceCollection services, Action<DicomClientOptions> options = null) 
            => services   
                .AddSingleton<IDicomClientFactory, DefaultDicomClientFactory>()
                .Configure(options ?? (o => {}));
        
        public static IServiceCollection AddDicomServer(this IServiceCollection services, Action<DicomServiceOptions> options = null) 
            => services   
                .AddSingleton<IDicomServerRegistry, DefaultDicomServerRegistry>()
                .AddSingleton<IDicomServerFactory, DefaultDicomServerFactory>()
                .Configure(options ?? (o => {}));

        public static IServiceCollection AddTranscoderManager<TTranscoderManager>(this IServiceCollection services) where TTranscoderManager : class, ITranscoderManager
            => services.Replace<ITranscoderManager, TTranscoderManager>(ServiceLifetime.Singleton);

        public static IServiceCollection AddImageManager<TImageManager>(this IServiceCollection services) where TImageManager : class, IImageManager
            => services.Replace<IImageManager, TImageManager>(ServiceLifetime.Singleton);

        public static IServiceCollection AddLogManager<TLogManager>(this IServiceCollection services) where TLogManager : class, ILogManager
            => services.Replace<ILogManager, TLogManager>(ServiceLifetime.Singleton);

        public static IServiceCollection AddNetworkManager<TNetworkManager>(this IServiceCollection services) where TNetworkManager : class, INetworkManager
            => services.Replace<INetworkManager, TNetworkManager>(ServiceLifetime.Singleton);

        // Helper methods

        public static IServiceCollection Replace<TService, TImplementation>(
            this IServiceCollection services,
            ServiceLifetime lifetime) where TService : class where TImplementation : class, TService
        {
            var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));

            services.Remove(descriptorToRemove);

            var descriptorToAdd = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);

            services.Add(descriptorToAdd);

            return services;
        }

    }
}
