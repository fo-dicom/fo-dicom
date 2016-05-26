// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network
{
    /// <summary>
    /// Base class for DIMSE-C and DIMSE-N request items.
    /// </summary>
    public abstract class DicomRequest : DicomMessage
    {
        #region FIELDS

        private static volatile ushort _messageId = 1;

        private static object _lock = new object();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomRequest"/> class.
        /// </summary>
        /// <param name="command">
        /// Dataset representing the request command.
        /// </param>
        protected DicomRequest(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomRequest"/> class.
        /// </summary>
        /// <param name="type">
        /// The command field type.
        /// </param>
        /// <param name="requestedClassUid">
        /// The requested Class Uid.
        /// </param>
        protected DicomRequest(DicomCommandField type, DicomUID requestedClassUid)
        {
            Type = type;
            SOPClassUID = requestedClassUid;
            MessageID = GetNextMessageID();
            Dataset = null;
        }

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the request message ID.
        /// </summary>
        public ushort MessageID
        {
            get
            {
                return Command.Get<ushort>(DicomTag.MessageID);
            }
            set
            {
                Command.Add(DicomTag.MessageID, value);
            }
        }

        #endregion

        /// <summary>
        /// Operation to perform after response has been made.
        /// </summary>
        /// <param name="service">Active DICOM service.</param>
        /// <param name="response">Response to be post-processed.</param>
        internal abstract void PostResponse(DicomService service, DicomResponse response);

        /// <summary>
        /// Global message ID generator.
        /// </summary>
        /// <returns>A "unique" message ID.</returns>
        private static ushort GetNextMessageID()
        {
            lock (_lock)
            {
                if (_messageId == UInt16.MaxValue) _messageId = 1;
                return _messageId++;
            }
        }
    }
}
