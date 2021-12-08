// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network
{
    public class DicomAssociationRequestTimedoutException : DicomNetworkException
    {
        public DicomAssociationRequestTimedoutException() 
            : base("Association Request timed out waiting for response")
        {
        }
    }
}
