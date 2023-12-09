// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Text;

namespace FellowOakDicom.Network
{
    public sealed class DicomCMoveResponse : DicomResponse
    {
        public DicomCMoveResponse(DicomDataset command)
            : base(command)
        {
        }

        public DicomCMoveResponse(DicomCMoveRequest request, DicomStatus status)
            : base(request, status)
        {
        }

        public int Remaining
        {
            get => Command.GetSingleValueOrDefault(DicomTag.NumberOfRemainingSuboperations, (ushort)0);
            set => Command.AddOrUpdate(DicomTag.NumberOfRemainingSuboperations, (ushort)value);
        }

        public int Completed
        {
            get => Command.GetSingleValueOrDefault(DicomTag.NumberOfCompletedSuboperations, (ushort)0);
            set => Command.AddOrUpdate(DicomTag.NumberOfCompletedSuboperations, (ushort)value);
        }

        public int Warnings
        {
            get => Command.GetSingleValueOrDefault(DicomTag.NumberOfWarningSuboperations, (ushort)0);
            set => Command.AddOrUpdate(DicomTag.NumberOfWarningSuboperations, (ushort)value);
        }

        public int Failures
        {
            get => Command.GetSingleValueOrDefault(DicomTag.NumberOfFailedSuboperations, (ushort)0);
            set => Command.AddOrUpdate(DicomTag.NumberOfFailedSuboperations, (ushort)value);
        }

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

            if (Status.State != DicomState.Pending && Status.State != DicomState.Success)
            {
                if (!string.IsNullOrEmpty(Status.ErrorComment))
                {
                    sb.AppendFormat("\n\t\tError:\t\t{0}", Status.ErrorComment);
                }

                if (Command.Contains(DicomTag.OffendingElement))
                {
                    string[] tags = Command.GetValues<string>(DicomTag.OffendingElement);
                    if (tags.Length > 0)
                    {
                        sb.Append("\n\t\tTags:\t\t");
                        foreach (var tag in tags)
                        {
                            sb.AppendFormat(" {0}", tag);
                        }
                    }
                }
            }
            return sb.ToString();
        }
    }
}
