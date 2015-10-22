// Copyright (c) 2012-2015 fo-dicom contributors.
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

        protected DicomRequest(DicomCommandField type, DicomUID affectedClassUid, DicomPriority priority)
            : base()
        {
            Type = type;
            SOPClassUID = affectedClassUid;
            MessageID = GetNextMessageID();
            Priority = priority;
            Dataset = null;
        }

        public DicomPriority Priority
        {
            get
            {
                return Command.Get<DicomPriority>(DicomTag.Priority);
            }
            set
            {
                Command.Add(DicomTag.Priority, (ushort)value);
            }
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
