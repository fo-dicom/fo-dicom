using System;
using FellowOakDicom.Imaging;
using FellowOakDicom.Tests.Network;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FellowOakDicom.Tests
{

    public class GlobalFixture : IDisposable
    {

        public GlobalFixture()
        {
            new DicomSetupBuilder()
                .RegisterServices(s => s
                .AddDefaultDicomServices())
                .Build();
        }

        public void Dispose()
        { }
    }

    public class ImageSharpFixture : IDisposable
    {
        public ImageSharpFixture()
        {
            new DicomSetupBuilder()
                .RegisterServices(s => s
                .AddDefaultDicomServices()
                .UseImageManager<ImageSharpImageManager>())
                .Build();
        }

        public void Dispose()
        { }
    }


    public class DependencyFixture : IDisposable
    {
        public DependencyFixture()
        {
            new DicomSetupBuilder()
                .RegisterServices(s => s
                .AddDefaultDicomServices()
                .AddTransient<ISomeInterface, SomeInterfaceImplementation>()
                .UseImageManager<ImageSharpImageManager>())
                .Build();
        }

        public void Dispose()
        { }
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
