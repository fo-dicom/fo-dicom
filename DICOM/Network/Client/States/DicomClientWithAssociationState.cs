// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network.Client.States
{
    public abstract class DicomClientWithAssociationState : DicomClientWithConnectionState
    {
        /// <summary>
        /// Gets the currently active association between the client and the server
        /// </summary>
        public DicomAssociation Association { get; set; }

        protected DicomClientWithAssociationState(IInitialisationWithAssociationParameters initialisationParameters) : base(initialisationParameters)
        {
            if (initialisationParameters == null) throw new ArgumentNullException(nameof(initialisationParameters));
            Association = initialisationParameters.Association ?? throw new ArgumentNullException(nameof(initialisationParameters.Association));
        }
    }
}
