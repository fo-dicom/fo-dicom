// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Represents a DICOM C-Store response to be returned to a C-Store SCU or a C-Store response that has been received from a C-Store SCP.
    /// </summary>
    public sealed class DicomCStoreResponse : DicomResponse
    {
        /// <summary>
        /// Constructor for DICOM C-Store response received from SCP.
        /// </summary>
        /// <remarks>
        /// In most use cases this constructor will only be called by the library.
        /// </remarks>
        /// <param name="command">DICOM Command Dataset</param>
        public DicomCStoreResponse(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes DICOM C-Store response to be returned to SCU.
        /// </summary>
        /// <param name="request">DICOM C-Store request being responded to</param>
        /// <param name="status">Status result of the C-Store operation</param>
        public DicomCStoreResponse(DicomCStoreRequest request, DicomStatus status)
            : base(request, status)
        {
        }
    }
}
