using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced
{
    public static class AdvancedDicomClientAssociationExtensions 
    {
        public static async Task<DicomCEchoResponse> SendEchoRequestAsync(this IAdvancedDicomClientAssociation association, DicomCEchoRequest dicomRequest, CancellationToken cancellationToken)
        {
            await foreach (var response in association.SendRequestAsync(dicomRequest, cancellationToken))
            {
                return response as DicomCEchoResponse;
            }

            throw new DicomNetworkException($"C-ECHO request {dicomRequest.MessageID} failed: no DICOM events received");
        }
    }
}