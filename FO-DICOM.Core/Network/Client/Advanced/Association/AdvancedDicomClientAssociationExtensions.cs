// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        /// <param name="request">The request to send</param>
        /// <param name="cancellationToken">The token that cancels the request</param>
        /// <returns>A single C-ECHO response</returns>
        public static async Task<DicomCEchoResponse> SendCEchoRequestAsync(
            this IAdvancedDicomClientAssociation association, 
            DicomCEchoRequest request,
            CancellationToken cancellationToken)
        {
            await foreach (var response in association.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                return response as DicomCEchoResponse;
            }

            throw new DicomNetworkException($"C-ECHO request {request} failed: not a single DICOM response received");
        }
        
        /// <summary>
        /// Sends a C-STORE request over the provided association
        /// </summary>
        /// <param name="association">The association with another AE to send to the request to</param>
        /// <param name="request">The request to send</param>
        /// <param name="cancellationToken">The token that cancels the request</param>
        /// <returns>A single C-STORE response</returns>
        public static async Task<DicomCStoreResponse> SendCStoreRequestAsync(
            this IAdvancedDicomClientAssociation association, 
            DicomCStoreRequest request,
            CancellationToken cancellationToken)
        {
            await foreach (var response in association.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                return response as DicomCStoreResponse;
            }

            throw new DicomNetworkException($"C-STORE request {request} failed: not a single DICOM response received");
        }
        
        /// <summary>
        /// Sends a C-FIND request over the provided association
        /// </summary>
        /// <param name="association">The association with another AE to send to the request to</param>
        /// <param name="request">The request to send</param>
        /// <param name="cancellationToken">The token that cancels the request</param>
        /// <returns>(Typically) One pending C-FIND response per SOP instance, and lastly one success C-FIND response</returns>
        public static async IAsyncEnumerable<DicomCFindResponse> SendCFindRequestAsync(
            this IAdvancedDicomClientAssociation association, 
            DicomCFindRequest request,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (var response in association.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                yield return response as DicomCFindResponse;
            }
        }
        
        /// <summary>
        /// Sends a C-MOVE request over the provided association
        /// </summary>
        /// <param name="association">The association with another AE to send to the request to</param>
        /// <param name="request">The request to send</param>
        /// <param name="cancellationToken">The token that cancels the request</param>
        /// <returns>(Typically) One pending C-MOVE response per stored SOP instance, and lastly one success C-MOVE response</returns>
        public static async IAsyncEnumerable<DicomCMoveResponse> SendCMoveRequestAsync(
            this IAdvancedDicomClientAssociation association, 
            DicomCMoveRequest request,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (var response in association.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                yield return response as DicomCMoveResponse;
            }
        }
    }
}
