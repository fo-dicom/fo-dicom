using FellowOakDicom.Network.Client.Advanced.Connection;
using System;

namespace FellowOakDicom.Network.Client.Advanced
{
    public class AdvancedDicomClientAssociationRequest
    {
        /// <summary>
        /// Gets or sets the connection request, specifying where and how to connect to when trying to open the association
        /// </summary>
        public AdvancedDicomClientConnectionRequest Connection { get; set; }

        /// <summary>
        /// Gets the calling application entity.
        /// </summary>
        public string CallingAE { get; internal set; }

        /// <summary>
        /// Gets the called application entity.
        /// </summary>
        public string CalledAE { get; internal set; }

        /// <summary>
        /// Gets or sets the supported maximum number of asynchronous operations invoked.
        /// </summary>
        public int MaxAsyncOpsInvoked { get; set; }

        /// <summary>
        /// Gets or sets the supported maximum number of asynchronous operations performed.
        /// </summary>
        public int MaxAsyncOpsPerformed { get; set; }
        
        /// <summary>
        /// Gets the supported presentation contexts.
        /// </summary>
        public DicomPresentationContextCollection PresentationContexts { get; }

        /// <summary>
        /// Gets the (common) extended negotiations
        /// </summary>
        public DicomExtendedNegotiationCollection ExtendedNegotiations { get; }

        /// <summary>
        /// Gets or sets the timeout (in ms) to wait for a response after sending an association request
        /// </summary>
        public TimeSpan AssociationRequestTimeout { get; set; } = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Gets or sets the timeout (in ms) to wait for a response after sending an association release request
        /// </summary>
        public TimeSpan AssociationReleaseTimeout { get; set; } = TimeSpan.FromSeconds(10);

        public AdvancedDicomClientAssociationRequest()
        {
            PresentationContexts = new DicomPresentationContextCollection();
            ExtendedNegotiations = new DicomExtendedNegotiationCollection();
        }
    }
}