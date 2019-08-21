// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network.Client.EventArguments
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
