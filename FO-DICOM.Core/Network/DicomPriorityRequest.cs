// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Abstract base class for DICOM requests with priority.
    /// </summary>
    public abstract class DicomPriorityRequest : DicomRequest
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DicomPriorityRequest"/> base class.
        /// </summary>
        /// <param name="command">Command dataset.</param>
        protected DicomPriorityRequest(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomPriorityRequest"/> base class.
        /// </summary>
        /// <param name="type">Type of command (command field).</param>
        /// <param name="requestedClassUid">Requested/affected SOP Class UID</param>
        /// <param name="priority">Request priority.</param>
        protected DicomPriorityRequest(DicomCommandField type, DicomUID requestedClassUid, DicomPriority priority)
            : base(type, requestedClassUid)
        {
            Priority = priority;
        }

        /// <summary>
        /// Gets or sets the command priority.
        /// </summary>
        public DicomPriority Priority
        {
            get => Command.GetSingleValue<DicomPriority>(DicomTag.Priority);
            protected set => Command.AddOrUpdate(DicomTag.Priority, (ushort)value);
        }
    }
}
