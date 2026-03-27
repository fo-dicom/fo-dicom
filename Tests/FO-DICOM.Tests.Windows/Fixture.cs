using Xunit.Abstractions;
using Xunit.Sdk;
using FellowOakDicom;
using System;

namespace FellowOakDicom.Tests
{
    public sealed class InitializationFixture : IDisposable
    {
        public InitializationFixture()
        {
            new DicomSetupBuilder()
                .RegisterServices(services =>
                    services.AddWinFormsImaging()
                )
                .Build();
        }

        public void Dispose()
        { }
    }


}
