// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.IO;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.DependencyInjection;

namespace FellowOakDicom
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddFellowOakDicom(this IServiceCollection services)
            => services
                .AddInternals()
                .AddTranscoderManager<WindowsTranscoderManager>()
                .AddImageManager<RawImageManager>()
                .AddLogManager<ConsoleLogManager>()
                .AddNetworkManager<DesktopNetworkManager>();

        private static IServiceCollection AddInternals(this IServiceCollection services)
            => services
                .AddSingleton<IFileReferenceFactory, FileReferenceFactory>()
                .AddSingleton<IDicomClientFactory, DicomClientFactory>();

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
