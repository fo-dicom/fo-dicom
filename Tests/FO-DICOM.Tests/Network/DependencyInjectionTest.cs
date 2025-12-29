// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Codec;
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

        [Fact]
        public async Task DependencyPropertyHasValue()
        {
            var serviceCollection = new ServiceCollection()
                .AddFellowOakDicom()
                .AddTransient<ISomeInterface, SomeInterfaceImplementation>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var dicomServerFactory = serviceProvider.GetRequiredService<IDicomServerFactory>();
            var dicomClientFactory = serviceProvider.GetRequiredService<IDicomClientFactory>();

            using var server = dicomServerFactory.Create<EchoProviderWithDependency>(0);

            var client = dicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");

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

        [Fact]
        public void DependencyShouldNotOverwrite()
        {
            var serviceCollection = new ServiceCollection()
                .AddFellowOakDicom()
                .AddImageManager<MyCustomImageManager>()
                .AddTranscoderManager<MyCustomTranscoderManager>()
                .AddFellowOakDicom();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var imageService = serviceProvider.GetRequiredService<IImageManager>();
            var transcoderService = serviceProvider.GetRequiredService<ITranscoderManager>();
            Assert.IsType<MyCustomImageManager>(imageService);
            Assert.IsType<MyCustomTranscoderManager>(transcoderService);
        }

    }


    public class MyCustomImageManager : IImageManager
    {
        public IImage CreateImage(int width, int height) => throw new NotImplementedException();
    }

    public class MyCustomTranscoderManager : ITranscoderManager
    {
        public bool CanTranscode(DicomTransferSyntax inSyntax, DicomTransferSyntax outSyntax) => throw new NotImplementedException();
        public IDicomCodec GetCodec(DicomTransferSyntax syntax) => throw new NotImplementedException();
        public bool HasCodec(DicomTransferSyntax syntax) => throw new NotImplementedException();
        public void LoadCodecs(string path = null, string search = null) => throw new NotImplementedException();
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
