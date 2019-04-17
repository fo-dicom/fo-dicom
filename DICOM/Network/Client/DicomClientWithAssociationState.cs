using System;

namespace Dicom.Network.Client
{
    public abstract class DicomClientWithAssociationState : DicomClientWithConnectionState
    {
        /// <summary>
        /// Gets the currently active association between the client and the server
        /// </summary>
        public DicomAssociation Association { get; set; }

        protected DicomClientWithAssociationState(IInitialisationWithAssociationParameters initialisationParameters) : base((IInitialisationWithConnectionParameters) initialisationParameters)
        {
            if (initialisationParameters == null) throw new ArgumentNullException(nameof(initialisationParameters));
            Association = initialisationParameters.Association ?? throw new ArgumentNullException(nameof(initialisationParameters.Association));
        }
    }
}