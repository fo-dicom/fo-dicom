namespace Dicom.Network.Client.Events
{
    public class DicomAbortedEvent
    {
        public DicomAbortSource Source { get; }
        public DicomAbortReason Reason { get; }

        public DicomAbortedEvent(DicomAbortSource source, DicomAbortReason reason)
        {
            Source = source;
            Reason = reason;
        }
    }
}
