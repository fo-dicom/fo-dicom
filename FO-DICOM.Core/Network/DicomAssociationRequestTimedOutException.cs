// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    public class DicomAssociationRequestTimedOutException : DicomNetworkException
    {
        public DicomAssociationRequestTimedOutException(int timeoutInMs, int retryCount) 
            : base($"The association request timed out {retryCount} times, waiting {timeoutInMs}ms each time for the association response")
        {
        }
    }
}
