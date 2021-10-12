// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced.Association
{
    public static class AdvancedDicomClientAssociationExtensions 
    {
        /// <summary>
        /// Sends a C-ECHO request over the provided association
        /// </summary>
        /// <param name="association">The association with another AE to send to the request to</param>
        /// <param name="dicomRequest">The echo request to send</param>
        /// <param name="cancellationToken">The token that cancels the request</param>
        /// <returns></returns>
        /// <exception cref="DicomNetworkException"></exception>
        public static async Task<DicomCEchoResponse> SendEchoRequestAsync(this IAdvancedDicomClientAssociation association, DicomCEchoRequest dicomRequest, CancellationToken cancellationToken)
        {
            await foreach (var response in association.SendRequestAsync(dicomRequest, cancellationToken).ConfigureAwait(false))
            {
                return response as DicomCEchoResponse;
            }

            throw new DicomNetworkException($"C-ECHO request {dicomRequest.MessageID} failed: no DICOM events received");
        }
    }
}