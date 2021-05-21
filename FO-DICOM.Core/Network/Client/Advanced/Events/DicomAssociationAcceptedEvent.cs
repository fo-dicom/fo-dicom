// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Network.Client.Advanced.Events
{

    internal class DicomAssociationAcceptedEvent : IAdvancedDicomClientConnectionEvent
    {
        public DicomAssociation Association { get; }

        public DicomAssociationAcceptedEvent(DicomAssociation association)
        {
            Association = association ?? throw new ArgumentNullException(nameof(association));
        }
    }
}
