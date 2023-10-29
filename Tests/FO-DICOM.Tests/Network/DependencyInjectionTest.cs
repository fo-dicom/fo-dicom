// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Imaging;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;

namespace FellowOakDicom.Tests.Network
{
    [Collection(TestCollections.General)]
    public class DependencyInjectionTest
    {
        private readonly GlobalFixture _globalFixture;

        public DependencyInjectionTest(GlobalFixture globalFixture)
        {
            _globalFixture = globalFixture ?? throw new ArgumentNullException(nameof(globalFixture));
        }

#if NET462
        [Fact(Skip = "Re-enable when ImageSharp strong names their assemblies")] // TODO re-enable this
#else
        [Fact]
#endif
        public async Task DependencyPropertyHasValue()
        {
            var port = Ports.GetNext();
            var serviceCollection = new ServiceCollection()
                .AddFellowOakDicom()
                .AddTransient<ISomeInterface, SomeInterfaceImplementation>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dicomServerFactory = serviceProvider.GetRequiredService<IDicomServerFactory>();
            var dicomClientFactory = serviceProvider.GetRequiredService<IDicomClientFactory>();

            using var server = dicomServerFactory.Create<EchoProviderWithDependency>(port);

            var client = dicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");

            string value = string.Empty;
            var request = new DicomCEchoRequest();
            request.OnResponseReceived += (req, resp) =>
                {
                    value = resp.Dataset.GetSingleValueOrDefault(DicomTag.PatientComments, string.Empty);
                };

            await client.AddRequestAsync(request);
            await client.SendAsync();

            Assert.False(string.IsNullOrEmpty(value));
        }

        [FactForNetCore]
        public async Task StaticServiceProviderShouldBeSetWithHostedService()
        {
            try
            {
                // Arrange
                var services = new ServiceCollection();
                services.AddFellowOakDicom().AddImageManager<ImageSharpImageManager>();
                await using var serviceProvider = services.BuildServiceProvider();

                // Run hosted services like a real .NET application would
                var hostedServices = serviceProvider.GetRequiredService<IEnumerable<IHostedService>>();
                foreach (var hostedService in hostedServices)
                {
                    await hostedService.StartAsync(CancellationToken.None);
                }

                // Act
                var dicomFile = await DicomFile.OpenAsync("Test Data/TestPattern_RGB.dcm");
                var dicomImage = new DicomImage(dicomFile.Dataset);
                using var image = dicomImage.RenderImage();

                // Assert
                // The default is RawImage, so if the static Setup.ServiceProvider was not set correctly, this would fail
                Assert.IsType<ImageSharpImage>(image);
            }
            finally
            {
                // To avoid interference with other tests, we must reset the static service provider host to its original value
                DicomSetupBuilder.UseServiceProvider(_globalFixture.TestServiceProviderHost);
            }
        }

    }


    public class EchoProviderWithDependency : DicomCEchoProvider
    {
        private readonly ISomeInterface _someInterface;

        public EchoProviderWithDependency(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
            DicomServiceDependencies dependencies,
            ISomeInterface someInterface)
            : base(stream, fallbackEncoding, log, dependencies)
        {
            _someInterface = someInterface ?? throw new ArgumentNullException(nameof(someInterface));
        }

        public override Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
        {
            var response = new DicomCEchoResponse(request, DicomStatus.Success)
            {
                Dataset = new DicomDataset {
                    { DicomTag.PatientComments, _someInterface.GetValue() }
                }
            };
            return Task.FromResult(response);
        }

    }


    public interface ISomeInterface
    {
        string GetValue();
    }

    public class SomeInterfaceImplementation : ISomeInterface
    {
        public string GetValue() => nameof(SomeInterfaceImplementation);
    }

}
