// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.IO;
using FellowOakDicom.Log;
using FellowOakDicom.Memory;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Network.Client.Advanced.Connection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;

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
        /// <summary>
        /// Adds default implementations of all required services to the collection if the services haven't already been registered
        /// </summary>
        public static IServiceCollection AddFellowOakDicom(this IServiceCollection services)
            => services
                .TryAddInternals()
                .AddLogging()
                .TryAddTranscoderManager<DefaultTranscoderManager>()
                .TryAddImageManager<RawImageManager>()
                .TryAddNetworkManager<DesktopNetworkManager>()
                .AddDicomClient()
                .AddDicomServer();

        private static IServiceCollection TryAddInternals(this IServiceCollection services)
        {
            services.TryAddSingleton<IFileReferenceFactory, FileReferenceFactory>();
            services.TryAddSingleton<IMemoryProvider, ArrayPoolMemoryProvider>();
            return services;
        }

        /// <summary>
        /// Adds DicomClient services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="options">The <see cref="DicomClientOptions"/> configuration delegate.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection AddDicomClient(this IServiceCollection services, Action<DicomClientOptions> options = null)
        {
            services.TryAddSingleton<DicomServiceDependencies>();
            services.TryAddSingleton<IDicomClientFactory, DefaultDicomClientFactory>();
            services.TryAddSingleton<IAdvancedDicomClientConnectionFactory, DefaultAdvancedDicomClientConnectionFactory>();
            services.AddOptions<DicomClientOptions>();
            services.AddOptions<DicomServiceOptions>();
            if (options != null)
            {
                services.Configure(options);
            }
            return services;
        }

        /// <summary>
        /// Adds DicomServer services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="options">The <see cref="DicomServerOptions"/> configuration delegate.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection AddDicomServer(this IServiceCollection services, Action<DicomServerOptions> options = null)
        {
            services.TryAddSingleton<DicomServiceDependencies>();
            services.TryAddSingleton<DicomServerDependencies>();
            services.TryAddSingleton<IDicomServerRegistry, DefaultDicomServerRegistry>();
            services.TryAddSingleton<IDicomServerFactory, DefaultDicomServerFactory>();
            services.AddOptions<DicomServerOptions>();
            services.AddOptions<DicomServiceOptions>();
            if (options != null)
            {
                services.Configure(options);
            }
            return services;
        }

        /// <summary>
        /// Adds <see cref="ITranscoderManager"/> services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection AddTranscoderManager<TTranscoderManager>(this IServiceCollection services) where TTranscoderManager : class, ITranscoderManager
        {
            services.Replace(ServiceDescriptor.Singleton<ITranscoderManager, TTranscoderManager>());
            return services;
        }

        /// <summary>
        /// Adds <see cref="ITranscoderManager"/> services to the specified <see cref="IServiceCollection" /> if they are not already registered.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection TryAddTranscoderManager<TTranscoderManager>(this IServiceCollection services) where TTranscoderManager : class, ITranscoderManager
        {
            services.TryAddSingleton<ITranscoderManager, TTranscoderManager>();
            return services;
        }

        /// <summary>
        /// Adds <see cref="TImageManager"/> services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection AddImageManager<TImageManager>(this IServiceCollection services) where TImageManager : class, IImageManager
        {
            services.Replace(ServiceDescriptor.Singleton<IImageManager, TImageManager>());
            return services;
        }

        /// <summary>
        /// Adds <see cref="TImageManager"/> services to the specified <see cref="IServiceCollection" /> if they are not already registered.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection TryAddImageManager<TImageManager>(this IServiceCollection services) where TImageManager : class, IImageManager
        {
            services.TryAddSingleton<IImageManager, TImageManager>();
            return services;
        }

        /// <summary>
        /// Adds <see cref="TNetworkManager"/> services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection AddNetworkManager<TNetworkManager>(this IServiceCollection services) where TNetworkManager : class, INetworkManager
        {
            services.Replace(ServiceDescriptor.Singleton<INetworkManager, TNetworkManager>());
            return services;
        }

        /// <summary>
        /// Adds <see cref="TNetworkManager"/> services to the specified <see cref="IServiceCollection" /> if they are not already registered.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection TryAddNetworkManager<TNetworkManager>(this IServiceCollection services) where TNetworkManager: class, INetworkManager
        {
            services.TryAddSingleton<INetworkManager, TNetworkManager>();
            return services;
        }


        [Obsolete("Fellow Oak DICOM now supports Microsoft.Extensions.Logging")]
        public static IServiceCollection AddLogManager<TLogManager>(this IServiceCollection services) where TLogManager : class, ILogManager
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, FellowOakDicomLoggerProvider>());
            services.Replace(ServiceDescriptor.Singleton<ILogManager, TLogManager>());
            return services;
        }
    }
}
