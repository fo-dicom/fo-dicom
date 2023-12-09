// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network.Client.EventArguments
{

    /// <summary>
    /// Container class for arguments associated with the <see cref="DicomClient.AssociationRejected"/> event.
    /// </summary>
    public class AssociationRejectedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes an instance of the <see cref="AssociationRejectedEventArgs"/> class.
        /// </summary>
        /// <param name="result">Association rejection result.</param>
        /// <param name="source">Source of association rejection.</param>
        /// <param name="reason">Reason for association rejection.</param>
        public AssociationRejectedEventArgs(DicomRejectResult result, DicomRejectSource source,
            DicomRejectReason reason)
        {
            Result = result;
            Source = source;
            Reason = reason;
        }

        /// <summary>
        /// Gets the association rejection result.
        /// </summary>
        public DicomRejectResult Result { get; }

        /// <summary>
        /// Gets the source of the association rejection.
        /// </summary>
        public DicomRejectSource Source { get; }

        /// <summary>
        /// Gets the reason for the association rejection.
        /// </summary>
        public DicomRejectReason Reason { get; }
    }
}
