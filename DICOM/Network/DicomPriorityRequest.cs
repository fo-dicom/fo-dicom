// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network
{
    public abstract class DicomPriorityRequest : DicomRequest
    {
        protected DicomPriorityRequest(DicomDataset command)
            : base(command)
        {
        }

        protected DicomPriorityRequest(DicomCommandField type, DicomUID affectedClassUid, DicomPriority priority)
            : base(type, affectedClassUid)
        {
            Priority = priority;
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
    }
}
