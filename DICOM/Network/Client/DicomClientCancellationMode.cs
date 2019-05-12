namespace Dicom.Network.Client
{
    /// <summary>
    /// Specifies the behavior of <see cref="DicomClient"/> when its tasks should be canceled
    /// </summary>
    public enum DicomClientCancellationMode
    {
        /// <summary>
        /// Upon cancellation, stop sending requests but wait for all sent requests to complete, then gracefully release the association and close the connection.
        /// While this cancellation mode is very friendly towards the DICOM server, it might take a while (e.g. when you have a lot of pending C-MOVE requests).
        /// </summary>
        WaitForSentRequestsToCompleteAndThenReleaseAssociation,

        /// <summary>
        /// Upon cancellation, stop sending requests, immediately gracefully release the association and close the connection. Pending requests may or may not complete.
        /// This cancellation mode will NOT wait for pending requests to complete, but will respect the <see cref="DicomClient.AssociationReleaseTimeoutInMs"/> timeout.
        /// </summary>
        ImmediatelyReleaseAssociation,

        /// <summary>
        /// Upon cancellation, immediately abort the association and close the connection.
        /// This cancellation mode guarantees the fastest shutdown.
        /// </summary>
        ImmediatelyAbortAssociation
    }
}
