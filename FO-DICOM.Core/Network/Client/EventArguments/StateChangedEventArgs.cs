// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using FellowOakDicom.Network.Client.States;

namespace FellowOakDicom.Network.Client.EventArguments
{

    public class StateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the old state the DicomClient was in
        /// </summary>
        public IDicomClientState OldState { get; }

        /// <summary>
        /// Gets the new state the DicomClient is in
        /// </summary>
        public IDicomClientState NewState { get; set; }

        /// <summary>
        /// Initializes an instance of <see cref="StateChangedEventArgs"/>
        /// </summary>
        /// <param name="oldState"></param>
        /// <param name="newState"></param>
        public StateChangedEventArgs(IDicomClientState oldState, IDicomClientState newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}
