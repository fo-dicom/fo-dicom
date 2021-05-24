using FellowOakDicom.Network.Client.Advanced.Connection;

namespace FellowOakDicom.Network.Client.Advanced.Association
{
    public class AdvancedDicomClientAssociationRequest
    {
        /// <summary>
        /// Gets or sets the connection
        /// </summary>
        public IAdvancedDicomClientConnection Connection { get; set; }

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

        public AdvancedDicomClientAssociationRequest()
        {
            PresentationContexts = new DicomPresentationContextCollection();
            ExtendedNegotiations = new DicomExtendedNegotiationCollection();
        }
    }
}