// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.IO;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.DependencyInjection;

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

    }
    
    /// <summary>
    /// Setup helper methods for initializing library.
    /// </summary>
    internal static class Setup
    {
        private static IServiceProvider _serviceProvider;

        internal static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    new DicomSetupBuilder().Build();
                }

                return _serviceProvider;
            }
            private set => _serviceProvider = value;
        }

        public static void SetupDI(IServiceProvider serviceProvider) => ServiceProvider = serviceProvider;
    }

    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddFellowOakDicom(this IServiceCollection services)
            => services
                .AddInternals()    
                .AddTranscoderManager<WindowsTranscoderManager>()
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
                .AddSingleton<IDicomClientFactory, DicomClientFactory>()
                .Configure(options ?? (o => {}));
        
        public static IServiceCollection AddDicomServer(this IServiceCollection services, Action<DicomServiceOptions> options = null) 
            => services   
                .AddSingleton<IDicomServerRegistry, DicomServerRegistry>()
                .AddSingleton<IDicomServerFactory, DicomServerFactory>()
                .Configure(options ?? (o => {}));

        public static IServiceCollection AddTranscoderManager<TTranscoderManager>(this IServiceCollection services) where TTranscoderManager : class, ITranscoderManager
            => services.AddSingleton<ITranscoderManager, TTranscoderManager>();

        public static IServiceCollection AddImageManager<TImageManager>(this IServiceCollection services) where TImageManager : class, IImageManager
            => services.AddSingleton<IImageManager, TImageManager>();

        public static IServiceCollection AddLogManager<TLogManager>(this IServiceCollection services) where TLogManager : class, ILogManager
            => services.AddSingleton<ILogManager, TLogManager>();

        public static IServiceCollection AddNetworkManager<TNetworkManager>(this IServiceCollection services) where TNetworkManager : class, INetworkManager
            => services.AddSingleton<INetworkManager, TNetworkManager>();
    }
}
