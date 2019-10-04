// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Network.Client.EventArguments
{

    /// <summary>
    /// Container class for arguments associated with the <see cref="Dicom.Network.Client.DicomClient.AssociationAccepted"/> event.
    /// </summary>
    public class AssociationAcceptedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes an instance of the <see cref="AssociationAcceptedEventArgs"/> class.
        /// </summary>
        /// <param name="association">Accepted association.</param>
        public AssociationAcceptedEventArgs(DicomAssociation association)
        {
            Association = association;
        }

        /// <summary>
        /// Gets the accepted association.
        /// </summary>
        public DicomAssociation Association { get; }
    }
}
