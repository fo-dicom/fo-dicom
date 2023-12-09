// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Text;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Representation of a C-FIND response.
    /// </summary>
    /// <remarks>For completeness, a C-FIND response with status <see cref="DicomStatus.Success"/> should contain a 
    /// <see cref="DicomMessage.Dataset"/> describing the result(s) of the C-FIND request.
    /// </remarks>
    public sealed class DicomCFindResponse : DicomResponse
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DicomCFindResponse"/> class.
        /// </summary>
        /// <param name="command">Command associated with the C-FIND response.</param>
        public DicomCFindResponse(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomCFindResponse"/> class.
        /// </summary>
        /// <param name="request">C-FIND request for which the response should be made.</param>
        /// <param name="status">Response status.</param>
        public DicomCFindResponse(DicomCFindRequest request, DicomStatus status)
            : base(request, status)
        {
        }

        /// <summary>
        /// Gets or sets the number of remaining sub-operations.
        /// </summary>
        public int Remaining
        {
            get => Command.GetSingleValueOrDefault(DicomTag.NumberOfRemainingSuboperations, (ushort)0);
            set => Command.AddOrUpdate(DicomTag.NumberOfRemainingSuboperations, (ushort)value);
        }

        /// <summary>
        /// Gets or sets the number of completed sub-operations.
        /// </summary>
        public int Completed
        {
            get => Command.GetSingleValueOrDefault(DicomTag.NumberOfCompletedSuboperations, (ushort)0);
            set => Command.AddOrUpdate(DicomTag.NumberOfCompletedSuboperations, (ushort)value);
        }

        /// <summary>
        /// Gets or sets the number of warning sub-operations.
        /// </summary>
        public int Warnings
        {
            get => Command.GetSingleValueOrDefault(DicomTag.NumberOfWarningSuboperations, (ushort)0);
            set => Command.AddOrUpdate(DicomTag.NumberOfWarningSuboperations, (ushort)value);
        }

        /// <summary>
        /// Gets or sets the number of failed sub-operations.
        /// </summary>
        public int Failures
        {
            get => Command.GetSingleValueOrDefault(DicomTag.NumberOfFailedSuboperations, (ushort)0);
            set => Command.AddOrUpdate(DicomTag.NumberOfFailedSuboperations, (ushort)value);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} [{1}]: {2}", ToString(Type), RequestMessageID, Status.Description);
            if (Completed != 0)
            {
                sb.AppendFormat("\n\t\tCompleted:\t{0}", Completed);
            }

            if (Remaining != 0)
            {
                sb.AppendFormat("\n\t\tRemaining:\t{0}", Remaining);
            }

            if (Warnings != 0)
            {
                sb.AppendFormat("\n\t\tWarnings:\t{0}", Warnings);
            }

            if (Failures != 0)
            {
                sb.AppendFormat("\n\t\tFailures:\t{0}", Failures);
            }

            if (Status.State != DicomState.Pending && Status.State != DicomState.Success
                                                   && !string.IsNullOrEmpty(Status.ErrorComment))
            {
                sb.AppendFormat("\n\t\tError:\t\t{0}", Status.ErrorComment);
            }
            return sb.ToString();
        }

    }
}
