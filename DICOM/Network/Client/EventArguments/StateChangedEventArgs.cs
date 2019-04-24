using System;

namespace Dicom.Network.Client
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