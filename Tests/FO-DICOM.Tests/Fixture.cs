// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.NativeCodec;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace FellowOakDicom.Tests
{
    /// <summary>
    /// A fixture that configures the default services used in FellowOakDicom.
    /// </summary>
    public class GlobalFixture : IDisposable
    {

        public GlobalFixture()
        {
            var serviceCollection = new ServiceCollection()
                .AddFellowOakDicom();

            var defaultServiceProvider = serviceCollection.BuildServiceProvider();
            var serviceProviders = new TestServiceProviderHost(defaultServiceProvider);

            serviceCollection = new ServiceCollection()
                .AddFellowOakDicom()
                .AddLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddProvider(CollectingLoggerProvider.Instance);
                });

            var collectionLogServiceProvider = serviceCollection.BuildServiceProvider();
            serviceProviders.Register("Logging", collectionLogServiceProvider);

#if !NET462

            serviceCollection = new ServiceCollection()
                .AddFellowOakDicom()
                .AddTranscoderManager<NativeTranscoderManager>()
                .AddImageManager<ImageSharpImageManager>();

            var imageSharpServiceProvider = serviceCollection.BuildServiceProvider();
            serviceProviders.Register("ImageSharp", imageSharpServiceProvider);

            serviceCollection = new ServiceCollection()
                .AddFellowOakDicom()
                .AddTranscoderManager<NativeTranscoderManager>();
            var noTranscoderServiceProvider = serviceCollection.BuildServiceProvider();
            serviceProviders.Register("WithTranscoder", noTranscoderServiceProvider);

#endif

            DicomSetupBuilder.UseServiceProvider(serviceProviders);
        }

        public void Dispose()
        {
        }
    }


    [CollectionDefinition("General")]
    public class GeneralCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition("Logging")]
    public class LoggingCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition("Network")]
    public class NetworkCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition("Imaging")]
    public class ImagingCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition("ImageSharp")]
    public class ImageSharpCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition("Validation")]
    public class ValidationCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition("WithTranscoder")]
    public class WithTranscoderCollection : ICollectionFixture<GlobalFixture>
    {
    }
}
