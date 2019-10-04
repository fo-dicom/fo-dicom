// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Network.Client.Events
{

    internal class DicomAssociationAcceptedEvent
    {
        public DicomAssociation Association { get; }

        public DicomAssociationAcceptedEvent(DicomAssociation association)
        {
            Association = association ?? throw new ArgumentNullException(nameof(association));
        }
    }
}
