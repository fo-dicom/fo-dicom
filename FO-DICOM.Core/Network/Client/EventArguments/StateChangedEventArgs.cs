// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network.Client.States;
using System;

namespace FellowOakDicom.Network.Client.EventArguments
{
    [Obsolete(nameof(StateChangedEventArgs) + " is an artifact of an older state-based implementation of the DicomClient and will be deleted in the future. It only exists today for backwards compatibility purposes")]
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
