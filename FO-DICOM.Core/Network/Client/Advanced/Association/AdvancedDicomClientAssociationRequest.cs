// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network.Client.Advanced.Association
{
    /// <summary>
    /// Contains all the possible parameters that can change how a DICOM association is requested
    /// </summary>
    public class AdvancedDicomClientAssociationRequest
    {
        /// <summary>
        /// Gets the calling application entity.
        /// </summary>
        public string CallingAE { get; set; }

        /// <summary>
        /// Gets the called application entity.
        /// </summary>
        public string CalledAE { get; set; }

        /// <summary>
        /// Gets or sets the supported maximum number of asynchronous operations invoked.
        /// </summary>
        public int MaxAsyncOpsInvoked { get; set; } = 1;

        /// <summary>
        /// Gets or sets the supported maximum number of asynchronous operations performed.
        /// </summary>
        public int MaxAsyncOpsPerformed { get; set; } = 1;

        /// <summary>
        /// Gets the supported presentation contexts.
        /// </summary>
        public DicomPresentationContextCollection PresentationContexts { get; }

        /// <summary>
        /// Gets the (common) extended negotiations
        /// </summary>
        public DicomExtendedNegotiationCollection ExtendedNegotiations { get; }

        /// <summary>
        /// Gets or sets the user identity negotiation information
        /// </summary>
        public DicomUserIdentityNegotiation UserIdentityNegotiation { get; set; }

        public AdvancedDicomClientAssociationRequest()
        {
            PresentationContexts = new DicomPresentationContextCollection();
            ExtendedNegotiations = new DicomExtendedNegotiationCollection();
        }
    }
}