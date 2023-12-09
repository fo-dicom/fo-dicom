// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network.Client
{
    /// <summary>
    /// Specifies the behavior of <see cref="DicomClient"/> when its tasks should be canceled
    /// </summary>
    public enum DicomClientCancellationMode
    {
        /// <summary>
        /// Upon cancellation, stop sending requests, immediately gracefully release the association and close the connection. Pending requests may or may not complete.
        /// This cancellation mode will NOT wait for pending requests to complete, but will respect the <see cref="DicomClientOptions.AssociationReleaseTimeoutInMs"/> timeout.
        /// </summary>
        ImmediatelyReleaseAssociation,

        /// <summary>
        /// Upon cancellation, immediately abort the association and close the connection.
        /// This cancellation mode guarantees the fastest shutdown.
        /// </summary>
        ImmediatelyAbortAssociation
    }
}
