namespace Dicom.Network.Client.States
{
    public interface IInitialisationWithAssociationParameters : IInitialisationWithConnectionParameters
    {
        DicomAssociation Association { get; set; }
    }
}
