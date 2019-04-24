namespace Dicom.Network.Client
{
    public interface IInitialisationWithConnectionParameters
    {
        IDicomClientConnection Connection { get; set; }
    }
}