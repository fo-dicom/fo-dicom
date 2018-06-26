﻿// Copyright (c) 2012-2018 fo-dicom contributors.
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

        private static readonly object _lock = new object();

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
            get => Command.GetSingleValue<ushort>(DicomTag.MessageID);
            protected set => Command.AddOrUpdate(DicomTag.MessageID, value);
        }

        #endregion

        /// <summary>
        /// Operation to perform after response has been made.
        /// </summary>
        /// <param name="service">Active DICOM service.</param>
        /// <param name="response">Response to be post-processed.</param>
        protected internal abstract void PostResponse(DicomService service, DicomResponse response);

        /// <summary>
        /// Global message ID generator.
        /// </summary>
        /// <returns>A "unique" message ID.</returns>
        private static ushort GetNextMessageID()
        {
            lock (_lock)
            {
                if (_messageId == ushort.MaxValue) _messageId = 1;
                return _messageId++;
            }
        }

        /// <summary>
        /// Create presentation of the request using specific transfer syntax instead of relying on the default offered Explicit Little Endian, Implicit Littile Endian sequeunce
        /// </summary>
        /// <param name="transferSyntaxes">The list of proposed transfer syntaxes</param>
        public void CreatePresentationContext(params DicomTransferSyntax[] transferSyntaxes)
        {
            if(transferSyntaxes.Length == 0)
            {
                throw new ArgumentException("Proposed Transfer Syntaxes array can't be empty");
            }

            this.PresentationContext = new DicomPresentationContext(0, this.SOPClassUID);

            foreach (var tx in transferSyntaxes)
            {
                this.PresentationContext.AddTransferSyntax(tx);
            }
        }
    }
}
