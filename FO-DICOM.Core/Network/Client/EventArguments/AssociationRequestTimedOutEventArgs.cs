// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Network.Client.EventArguments
{
    public class AssociationRequestTimedOutEventArgs : EventArgs
    {

        /// <summary>
        /// Gets the timeout in ms that was exceeded
        /// </summary>
        public int TimeoutInMs { get; }

        /// <summary>
        /// Gets the number of times this association request has timed out so far
        /// </summary>
        public int Attempt { get; }

        /// <summary>
        /// Gets the maximum number of times this association request is allowed to time out before giving up 
        /// </summary>
        public int MaximumNumberOfAttempts { get; }

        /// <summary>
        /// Initializes an instance of <see cref="AssociationRequestTimedOutEventArgs"/>
        /// </summary>
        /// <param name="timeout"></param>
        public AssociationRequestTimedOutEventArgs(int timeout, int attempt, int maximumNumberOfAttempts)
        {
            TimeoutInMs = timeout;
            Attempt = attempt;
            MaximumNumberOfAttempts = maximumNumberOfAttempts;
        }
    }
}
