// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network.Client.EventArguments
{
    /// <summary>
    /// Container class for arguments associated with the <see cref="Dicom.Network.Client.DicomClient."/> event.
    /// </summary>
    public class AssociationRequestEventArgs : EventArgs
    {
        /// <summary>
        /// Provides a handle to instance object of RequestAssociation <see cref="AssociationRequestEventArgs"/> class.
        /// </summary>
        /// <param name="association">Association request.</param>
        public AssociationRequestEventArgs(DicomAssociation association)
        {
            Association = association;
        }

        /// <summary>
        /// Gets the association prior to submission.
        /// </summary>
        public DicomAssociation Association { get; }
    }
}
