﻿// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.IO;
using FellowOakDicom.Log;
using FellowOakDicom.Memory;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
        {
            services.TryAddSingleton<IFileReferenceFactory, FileReferenceFactory>();
            services.TryAddSingleton<IMemoryProvider, ArrayPoolMemoryProvider>();
            return services;
        }

        public static IServiceCollection AddDicomClient(this IServiceCollection services, Action<DicomClientOptions> options = null)
        {
            services.TryAddSingleton<DicomServiceDependencies>();
            services.TryAddSingleton<IDicomClientFactory, DefaultDicomClientFactory>();
            services.Configure(options ?? (o => { }));
            return services;
        }

        public static IServiceCollection AddDicomServer(this IServiceCollection services, Action<DicomServiceOptions> options = null)
        {
            services.TryAddSingleton<DicomServiceDependencies>();
            services.TryAddSingleton<DicomServerDependencies>();
            services.TryAddSingleton<IDicomServerRegistry, DefaultDicomServerRegistry>();
            services.TryAddSingleton<IDicomServerFactory, DefaultDicomServerFactory>();
            services.Configure(options ?? (o => { }));
            return services;
        }

        public static IServiceCollection AddTranscoderManager<TTranscoderManager>(this IServiceCollection services) where TTranscoderManager : class, ITranscoderManager
        {
            services.Replace(ServiceDescriptor.Singleton<ITranscoderManager, TTranscoderManager>());
            return services;
        }

        public static IServiceCollection AddImageManager<TImageManager>(this IServiceCollection services) where TImageManager : class, IImageManager
        {
            services.Replace(ServiceDescriptor.Singleton<IImageManager, TImageManager>());
            return services;
        }

        public static IServiceCollection AddLogManager<TLogManager>(this IServiceCollection services) where TLogManager : class, ILogManager
        {
            services.Replace(ServiceDescriptor.Singleton<ILogManager, TLogManager>());
            return services;
        }

        public static IServiceCollection AddNetworkManager<TNetworkManager>(this IServiceCollection services) where TNetworkManager : class, INetworkManager
        {
            services.Replace(ServiceDescriptor.Singleton<INetworkManager, TNetworkManager>());
            return services;
        }
    }
}
