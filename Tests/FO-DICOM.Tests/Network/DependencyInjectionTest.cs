// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection("Dependency")]
    public class DependencyInjectionTest
    {

#if NET462
        [Fact(Skip = "Re-enable when ImageSharp strong names their assemblies")] // TODO re-enable this
#else
        [Fact]
#endif
        public async Task DependencyPropertyHasValue()
        {
            var port = Ports.GetNext();
            using (var server = DicomServer.Create<EchoProviderWithDependency>(port))
            {
                var client = new DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP");

                string value = string.Empty;
                var request = new DicomCEchoRequest();
                request.OnResponseReceived += (req, resp) =>
                    {
                        value = resp.Dataset.GetSingleValueOrDefault(DicomTag.PatientComments, string.Empty);
                    };

                await client.AddRequestAsync(request).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

                Assert.False(string.IsNullOrEmpty(value));
            }
        }

    }


    public class EchoProviderWithDependency : DicomCEchoProvider
    {
        public ISomeInterface InterfaceImplementation { get; set; }

        public EchoProviderWithDependency(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        public override Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
        {
            var response = new DicomCEchoResponse(request, DicomStatus.Success);
            if (InterfaceImplementation != null)
            {
                response.Dataset = new DicomDataset
                {
                    { DicomTag.PatientComments, InterfaceImplementation.GetValue() }
                };
            }
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
