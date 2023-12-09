// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace FellowOakDicom.Tests.Network
{
    public class DependencyInjectionTest
    {

        public DependencyInjectionTest()
        {

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
