// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    public abstract class DicomPriorityRequest : DicomRequest
    {
        protected DicomPriorityRequest(DicomDataset command)
            : base(command)
        {
        }

        protected DicomPriorityRequest(DicomCommandField type, DicomUID requestedClassUid, DicomPriority priority)
            : base(type, requestedClassUid)
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
                Command.AddOrUpdate(DicomTag.Priority, (ushort)value);
            }
        }
    }
}
