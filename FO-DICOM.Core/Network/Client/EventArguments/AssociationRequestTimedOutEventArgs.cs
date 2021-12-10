// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Network.Client.EventArguments
{
    public class AssociationRequestTimedOutEventArgs : EventArgs
    {

        /// <summary>
        /// Gets the timeout in msec that was exceded
        /// </summary>
        public int TimeoutInMs { get; }

        /// <summary>
        /// Gets the number of times the Association Request has timed out
        /// </summary>
        public int FailedCount { get; }

        /// <summary>
        /// Initializes an instance of <see cref="AssociationRequestTimedOutEventArgs"/>
        /// </summary>
        /// <param name="timeout"></param>
        public AssociationRequestTimedOutEventArgs(int timeout, int failed)
        {
            TimeoutInMs = timeout;
            FailedCount = failed;
        }
    }
}
