// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.NativeCodec;
using FellowOakDicom.Log;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FellowOakDicom.Tests
{
    /// <summary>
    /// A fixture that configures the default services used in FellowOakDicom.
    /// </summary>
    public class GlobalFixture : FixtureBase<ConsoleLogManager>
    {
    }

    /// <summary>
    /// A fixture that configures the default services used in FellowOakDicom,
    /// except for the LogManager, which is replaced by a the CollectingConsoleLogManager
    /// to allow testing log messages.
    /// </summary>
    public class LogCollectingFixture : FixtureBase<CollectingConsoleLogManager>
    {
    }

    public class FixtureBase<TLogManager> : IDisposable where TLogManager : class, ILogManager
    {
        protected FixtureBase()
        {
            var serviceCollection = new ServiceCollection().AddFellowOakDicom();
            // a ConsoleLogManager is already added
            if (typeof(TLogManager) != typeof(ConsoleLogManager))
            {
                serviceCollection = serviceCollection.AddLogManager<TLogManager>();
            }

            var defaultServiceProvider = serviceCollection.BuildServiceProvider();
            var serviceProviders = new TestServiceProviderHost(defaultServiceProvider);

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
    public class LoggingCollection : ICollectionFixture<LogCollectingFixture>
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
