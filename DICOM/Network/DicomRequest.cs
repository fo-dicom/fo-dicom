// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network
{
    public abstract class DicomRequest : DicomMessage
    {
        protected DicomRequest(DicomDataset command)
            : base(command)
        {
        }

        protected DicomRequest(DicomCommandField type, DicomUID affectedClassUid)
            : base()
        {
            Type = type;
            SOPClassUID = affectedClassUid;
            MessageID = GetNextMessageID();
            Dataset = null;
        }

        internal abstract void PostResponse(DicomService service, DicomResponse response);

        private static volatile ushort _messageId = 1;

        private static object _lock = new object();

        internal ushort GetNextMessageID()
        {
            lock (_lock)
            {
                if (_messageId == UInt16.MaxValue) _messageId = 1;
                return _messageId++;
            }
        }
    }
}
