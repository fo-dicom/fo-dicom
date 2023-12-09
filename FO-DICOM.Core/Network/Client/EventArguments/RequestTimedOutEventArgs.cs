// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network.Client.EventArguments
{

    public class RequestTimedOutEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the DICOM request that timed out.
        /// </summary>
        public DicomRequest Request { get; }

        /// <summary>
        /// Gets the timeout duration that was exceeded
        /// </summary>
        public TimeSpan Timeout { get; }

        /// <summary>
        /// Initializes an instance of <see cref="RequestTimedOutEventArgs"/>
        /// </summary>
        /// <param name="request"></param>
        /// <param name="timeout"></param>
        public RequestTimedOutEventArgs(DicomRequest request, TimeSpan timeout)
        {
            Request = request;
            Timeout = timeout;
        }
    }
}
