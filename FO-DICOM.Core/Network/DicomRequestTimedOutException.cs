// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network
{
    public class DicomRequestTimedOutException : DicomNetworkException
    {
        public DicomRequestTimedOutException(DicomRequest request, TimeSpan timeout)
            : base($"DICOM Request Timed Out [request: [{request.MessageID}]; timeout: {timeout.TotalMilliseconds}]ms")
        {
            Request = request;
            TimeOut = timeout;
        }

        public DicomRequest Request { get; }

        public TimeSpan TimeOut { get; }
    }
}
