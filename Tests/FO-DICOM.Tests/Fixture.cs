// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Net.Http;
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
            serviceProviders.Register(TestCollections.Logging, collectionLogServiceProvider);

#if !NET462

            serviceCollection = new ServiceCollection()
                .AddFellowOakDicom()
                .AddTranscoderManager<NativeTranscoderManager>()
                .AddImageManager<ImageSharpImageManager>();

            var imageSharpServiceProvider = serviceCollection.BuildServiceProvider();
            serviceProviders.Register(TestCollections.ImageSharp, imageSharpServiceProvider);

            serviceCollection = new ServiceCollection()
                .AddFellowOakDicom()
                .AddTranscoderManager<NativeTranscoderManager>();
            var noTranscoderServiceProvider = serviceCollection.BuildServiceProvider();
            serviceProviders.Register(TestCollections.WithTranscoder, noTranscoderServiceProvider);

#endif

            DicomSetupBuilder.UseServiceProvider(serviceProviders);
        }

        public void Dispose()
        {
        }
    }

    public class HttpClientFixture : IDisposable
    {
        public HttpClientFixture()
        {
            HttpClient = new HttpClient();
        }

        public HttpClient HttpClient { get; }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }


    [CollectionDefinition(TestCollections.General)]
    public class GeneralCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition(TestCollections.Logging)]
    public class LoggingCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition(TestCollections.Network)]
    public class NetworkCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition(TestCollections.Imaging)]
    public class ImagingCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition(TestCollections.ImageSharp)]
    public class ImageSharpCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition(TestCollections.Validation)]
    public class ValidationCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition(TestCollections.WithTranscoder)]
    public class WithTranscoderCollection : ICollectionFixture<GlobalFixture>
    {
    }

    [CollectionDefinition(TestCollections.WithHttpClient)]
    public class WithHttpClientFixture : ICollectionFixture<HttpClientFixture>
    {
    }
}
