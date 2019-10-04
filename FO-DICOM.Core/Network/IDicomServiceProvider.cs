// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

namespace FellowOakDicom.Network
{

    /// <summary>
    /// Interface for DICOM service providers.
    /// </summary>
    public interface IDicomServiceProvider : IDicomService
    {
        /// <summary>
        /// Callback to invoke when receiving an association request.
        /// </summary>
        /// <param name="association">DICOM association corresponding to the request.</param>
        Task OnReceiveAssociationRequestAsync(DicomAssociation association);

        /// <summary>
        /// Callback to invoke when receiving an association release request.
        /// </summary>
        Task OnReceiveAssociationReleaseRequestAsync();
    }
}
