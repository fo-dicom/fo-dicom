using System;

namespace Dicom.Network.Client.Events
{
    internal class DicomAssociationAcceptedEvent
    {
        public DicomAssociation Association { get; }

        public DicomAssociationAcceptedEvent(DicomAssociation association)
        {
            Association = association ?? throw new ArgumentNullException(nameof(association));
        }
    }
}
