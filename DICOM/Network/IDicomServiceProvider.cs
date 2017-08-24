// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#if !NET35
using System.Threading.Tasks;
#endif

namespace Dicom.Network
{
    /// <summary>
    /// Interface for DICOM service providers.
    /// </summary>
    public interface IDicomServiceProvider : IDicomService
    {
#if !NET35
        /// <summary>
        /// Callback to invoke when receiving an association request.
        /// </summary>
        /// <param name="association">DICOM association corresponding to the request.</param>
        Task OnReceiveAssociationRequestAsync(DicomAssociation association);

        /// <summary>
        /// Callback to invoke when receiving an association release request.
        /// </summary>
        Task OnReceiveAssociationReleaseRequestAsync();
#endif
    }
}
