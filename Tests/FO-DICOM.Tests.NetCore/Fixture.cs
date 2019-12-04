using FellowOakDicom.Imaging;
using System;
using Xunit;

namespace FellowOakDicom.Tests
{

    public class GlobalFixture : IDisposable
    {

        public GlobalFixture()
        {
            //var serviceCollection = new ServiceCollection();
            //serviceCollection.AddDefaultDicomServices();

            //ServiceProvider = serviceCollection.BuildServiceProvider();
            //Setup.SetupDI(ServiceProvider);
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


}
