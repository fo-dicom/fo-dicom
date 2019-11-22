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
        {

        }

        public IServiceProvider ServiceProvider { get; private set; }

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



}
