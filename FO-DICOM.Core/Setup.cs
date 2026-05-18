// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.IO;
using FellowOakDicom.Memory;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Network.Client.Advanced.Connection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;

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
        /// <param name="namedConfigurationSection"></param>
        /// <param name="configureServiceOptions"></param>
        /// <param name="configureClientOptions"></param>
        /// <param name="configureServerOptions"></param>
        public static IServiceCollection AddFellowOakDicom(this IServiceCollection services, IConfiguration namedConfigurationSection = null, Action<DicomServiceOptions> configureServiceOptions = null, Action<DicomClientOptions> configureClientOptions = null, Action<DicomServerOptions> configureServerOptions = null)
        {
            if (namedConfigurationSection == null)
            {
                services.AddOptions<DicomServiceOptions>();
                services.AddOptions<DicomClientOptions>();
                services.AddOptions<DicomServerOptions>();
            }
            else
            {
                services.AddOptions<DicomServiceOptions>()
                    .Bind(namedConfigurationSection.GetSection("DicomServiceOptions"));
                services.AddOptions<DicomClientOptions>()
                    .Bind(namedConfigurationSection.GetSection("DicomClientOptions"));
                services.AddOptions<DicomServerOptions>()
                    .Bind(namedConfigurationSection.GetSection("DicomServerOptions"));

            }
            if (configureServiceOptions != null)
            {
                services.Configure(configureServiceOptions);
            }
            if (configureClientOptions != null)
            {
                services.Configure(configureClientOptions);
            }
            if (configureServerOptions != null)
            {
                services.Configure(configureServerOptions);
            }

            services
                .TryAddInternals()
                .AddLogging()
                .TryAddTranscoderManager<DefaultTranscoderManager>()
                .TryAddImageManager<RawImageManager>()
                .TryAddNetworkManager<DesktopNetworkManager>()
                .AddDicomClient()
                .AddDicomServer();

            return services;
        }

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
        public static IServiceCollection AddTranscoderManager<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TTranscoderManager>(this IServiceCollection services) where TTranscoderManager : class, ITranscoderManager
        {
            services.Replace(ServiceDescriptor.Singleton<ITranscoderManager, TTranscoderManager>());
            return services;
        }

        /// <summary>
        /// Adds <see cref="ITranscoderManager"/> services to the specified <see cref="IServiceCollection" /> if they are not already registered.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection TryAddTranscoderManager<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TTranscoderManager>(this IServiceCollection services) where TTranscoderManager : class, ITranscoderManager
        {
            services.TryAddSingleton<ITranscoderManager, TTranscoderManager>();
            return services;
        }

        /// <summary>
        /// Adds <see cref="IImageManager"/> services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection AddImageManager<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImageManager>(this IServiceCollection services) where TImageManager : class, IImageManager
        {
            services.Replace(ServiceDescriptor.Singleton<IImageManager, TImageManager>());
            return services;
        }

        /// <summary>
        /// Adds <see cref="IImageManager"/> services to the specified <see cref="IServiceCollection" /> if they are not already registered.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection TryAddImageManager<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImageManager>(this IServiceCollection services) where TImageManager : class, IImageManager
        {
            services.TryAddSingleton<IImageManager, TImageManager>();
            return services;
        }

        /// <summary>
        /// Adds <see cref="INetworkManager"/> services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection AddNetworkManager<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TNetworkManager>(this IServiceCollection services) where TNetworkManager : class, INetworkManager
        {
            services.Replace(ServiceDescriptor.Singleton<INetworkManager, TNetworkManager>());
            return services;
        }

        /// <summary>
        /// Adds <see cref="INetworkManager"/> services to the specified <see cref="IServiceCollection" /> if they are not already registered.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection TryAddNetworkManager<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TNetworkManager>(this IServiceCollection services) where TNetworkManager : class, INetworkManager
        {
            services.TryAddSingleton<INetworkManager, TNetworkManager>();
            return services;
        }

    }
}
