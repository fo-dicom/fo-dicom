namespace FellowOakDicom.Network.Client.Advanced
{
    public class OpenAssociationRequest
    {
        public OpenConnectionRequest ConnectionToOpen { get; set; }
        
        public DicomAssociation AssociationToOpen { get; set; }
    }
}