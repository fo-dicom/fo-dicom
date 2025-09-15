using FellowOakDicom;
using FellowOakDicom.Imaging;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Serialization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FO_DICOM.AotCompilation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // initialize
            new DicomSetupBuilder()
                .RegisterServices(s => s
                    .AddFellowOakDicom()
                    .AddImageManager<ImageSharpImageManager>()
                )
                .Build();
            // open a file
            var file = await DicomFile.OpenAsync("file.dcm");
            var patientid = file.Dataset.GetString(DicomTag.PatientID);

            // render the file
            var img = new DicomImage(file.Dataset);
            var image = img.RenderImage().AsSharpImage();

            // send the file
            var client = DicomClientFactory.Create("localhost", 104, false, "CLIENT", "SERVER");
            await client.AddRequestAsync(new DicomCStoreRequest(file));
            await client.SendAsync();

            // start a server
            var server = DicomServerFactory.Create<DemoServer>(105);

            // serialize a dataset
            var json = DicomJson.ConvertDicomToJson(file.Dataset);
        }
    }

    internal class DemoServer : DicomService, IDicomServiceProvider, IDicomCEchoProvider, IDicomCStoreProvider, IDicomCFindProvider, IDicomCMoveProvider
    {
        public DemoServer(INetworkStream stream, Encoding fallbackEncoding, ILogger logger, DicomServiceDependencies dependencies) : base(stream, fallbackEncoding, logger, dependencies)
        {
        }

        public Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request) => Task.FromResult(new DicomCEchoResponse(request, DicomStatus.Success));
        public async IAsyncEnumerable<DicomCFindResponse> OnCFindRequestAsync(DicomCFindRequest request) {
            yield return new DicomCFindResponse(request, DicomStatus.Success);
        }
        public async IAsyncEnumerable<DicomCMoveResponse> OnCMoveRequestAsync(DicomCMoveRequest request) {
            yield return new DicomCMoveResponse(request, DicomStatus.Success);
        }
        public void OnConnectionClosed(Exception exception) => throw new NotImplementedException();
        public Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request) => Task.FromResult(new DicomCStoreResponse(request, DicomStatus.Success));
        public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e) => throw new NotImplementedException();
        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason) => throw new NotImplementedException();
        public Task OnReceiveAssociationReleaseRequestAsync() => throw new NotImplementedException();
        public Task OnReceiveAssociationRequestAsync(DicomAssociation association) => throw new NotImplementedException();
    }
}
