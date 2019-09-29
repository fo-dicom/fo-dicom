using System.Text;
using System.Threading.Tasks;

namespace Dicom.Network
{
    public class DicomServiceCore : DicomService
    {
        public DicomServiceCore(INetworkStream stream, Encoding fallbackEncoding, Log.Logger log) : base(stream, fallbackEncoding, log)
        {
        }

        protected override async Task PerformDimseAsync(DicomMessage dimse)
        {
            if (dimse.Type == DicomCommandField.CMoveRequest && this is IDicomCMoveProviderAsync thisAsCMoveProviderAsync)
            {
                await foreach (var response in thisAsCMoveProviderAsync.OnCMoveRequestAsync(dimse as DicomCMoveRequest).ConfigureAwait(false))
                    await SendResponseAsync(response).ConfigureAwait(false);

                return;
            }

            if (dimse.Type == DicomCommandField.CFindRequest && this is IDicomCFindProviderAsync thisAsCFindProviderAsync)
            {
                await foreach (var response in thisAsCFindProviderAsync.OnCFindRequestAsync(dimse as DicomCFindRequest).ConfigureAwait(false))
                    await SendResponseAsync(response).ConfigureAwait(false);

                return;
            }
            if (dimse.Type == DicomCommandField.CGetRequest && this is IDicomCGetProviderAsync thisAsCGetProviderAsync)
            {
                await foreach (var response in thisAsCGetProviderAsync.OnCGetRequestAsync(dimse as DicomCGetRequest).ConfigureAwait(false))
                    await SendResponseAsync(response).ConfigureAwait(false);

                return;
            }

            await base.PerformDimseAsync(dimse);
        }
    }
}
