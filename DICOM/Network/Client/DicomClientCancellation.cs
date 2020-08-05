// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading;

namespace Dicom.Network.Client
{
    /// <summary>
    /// Contains information pertaining to the cancellation behavior of a <see cref="DicomClient"/>
    /// </summary>
    public class DicomClientCancellation
    {
        /// <summary>
        /// The token that will be marked as cancelled
        /// </summary>
        public CancellationToken Token { get; }

        /// <summary>
        /// The mode that will decide how the cancel behavior should work
        /// </summary>
        public DicomClientCancellationMode Mode { get; }

        public DicomClientCancellation(CancellationToken token, DicomClientCancellationMode mode)
        {
            Token = token;
            Mode = mode;
        }
    }
}
