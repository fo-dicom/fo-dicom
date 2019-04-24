namespace Dicom.Network.Client.States
{
    public interface IInitialisationWithConnectionParameters
    {
        IDicomClientConnection Connection { get; set; }
    }
}