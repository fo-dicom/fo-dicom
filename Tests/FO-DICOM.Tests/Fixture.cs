// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging;
using FellowOakDicom.Tests.Network;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace FellowOakDicom.Tests
{
    public class GlobalFixture : IDisposable
    {
        public readonly IServiceProvider ServiceProvider;

        public GlobalFixture()
        {
            var serviceCollection = new ServiceCollection().AddFellowOakDicom();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public void Dispose()
        {
        }
    }

    public class ImageSharpFixture : IDisposable
    {
        public readonly IServiceProvider ServiceProvider;

        public ImageSharpFixture()
        {
            var serviceCollection = new ServiceCollection()
                .AddFellowOakDicom()
                .AddImageManager<ImageSharpImageManager>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public void Dispose()
        {
        }
    }


    public class DependencyFixture : IDisposable
    {
        public readonly IServiceProvider ServiceProvider;

        public DependencyFixture()
        {
            var serviceCollection = new ServiceCollection()
                .AddFellowOakDicom()
                .AddImageManager<ImageSharpImageManager>()
                .AddTransient<ISomeInterface, SomeInterfaceImplementation>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public void Dispose()
        {
        }

    }


    [CollectionDefinition("General")]
    public class GeneralCollection : ICollectionFixture<GlobalFixture>
    { }

    [CollectionDefinition("Network")]
    public class NetworkCollection : ICollectionFixture<GlobalFixture>
    { }

    [CollectionDefinition("Imaging")]
    public class ImagingCollection : ICollectionFixture<GlobalFixture>
    { }

    [CollectionDefinition("ImageSharp")]
    public class ImageSharpCollection : ICollectionFixture<ImageSharpFixture>
    {}

    [CollectionDefinition("Dependency")]
    public class DependencyCollection: ICollectionFixture<DependencyFixture>
    {

    }

    [CollectionDefinition("Validation")]
    public class ValidationCollection: ICollectionFixture<GlobalFixture>
    { }


}
