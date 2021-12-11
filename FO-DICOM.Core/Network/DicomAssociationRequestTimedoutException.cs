// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network
{
    public class DicomAssociationRequestTimedoutException : DicomNetworkException
    {
        public DicomAssociationRequestTimedoutException(int timeout, int retryCount) 
            : base($"The Association Request timed out {retryCount} times, waiting {timeout} msec for the Associaton Response")
        {
        }
    }
}
