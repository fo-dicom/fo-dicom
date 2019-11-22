using System;
using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests
{

    public class GlobalFixture : IDisposable
    {

        public GlobalFixture()
        {
            new DicomSetupBuilder()
                .RegisterServices(x
                => x.UseImageManager<WinFormsImageManager>()
                )
                .Build();
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


}
