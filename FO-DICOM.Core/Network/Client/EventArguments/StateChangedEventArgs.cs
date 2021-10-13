// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network.Client.States;
using System;

namespace FellowOakDicom.Network.Client.EventArguments
{
    public class StateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the old state the DicomClient was in
        /// </summary>
        public DicomClientState OldState { get; }

        /// <summary>
        /// Gets the new state the DicomClient is in
        /// </summary>
        public DicomClientState NewState { get; set; }

        /// <summary>
        /// Initializes an instance of <see cref="StateChangedEventArgs"/>
        /// </summary>
        /// <param name="oldState"></param>
        /// <param name="newState"></param>
        public StateChangedEventArgs(DicomClientState oldState, DicomClientState newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}
