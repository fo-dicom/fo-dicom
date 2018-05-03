// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Text;

    /// <summary>
    /// Representation of a C-GET response.
    /// </summary>
    public sealed class DicomCGetResponse : DicomResponse
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomCGetResponse"/> class.
        /// </summary>
        /// <param name="command">
        /// The command associated with the C-GET operation.
        /// </param>
        public DicomCGetResponse(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomCGetResponse"/> class.
        /// </summary>
        /// <param name="request">
        /// The C-GET request associated with the response.
        /// </param>
        /// <param name="status">
        /// The status of the response.
        /// </param>
        public DicomCGetResponse(DicomCGetRequest request, DicomStatus status)
            : base(request, status)
        {
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the number of remaining sub-operations.
        /// </summary>
        public int Remaining
        {
            get
            {
                return Command.Get(DicomTag.NumberOfRemainingSuboperations, (ushort)0);
            }
            set
            {
                Command.AddOrUpdate(DicomTag.NumberOfRemainingSuboperations, (ushort)value);
            }
        }

        /// <summary>
        /// Gets or sets the number of completed sub-operations.
        /// </summary>
        public int Completed
        {
            get
            {
                return Command.Get(DicomTag.NumberOfCompletedSuboperations, (ushort)0);
            }
            set
            {
                Command.AddOrUpdate(DicomTag.NumberOfCompletedSuboperations, (ushort)value);
            }
        }

        /// <summary>
        /// Gets or sets the number of warning sub-operations.
        /// </summary>
        public int Warnings
        {
            get
            {
                return Command.Get(DicomTag.NumberOfWarningSuboperations, (ushort)0);
            }
            set
            {
                Command.AddOrUpdate(DicomTag.NumberOfWarningSuboperations, (ushort)value);
            }
        }

        /// <summary>
        /// Gets or sets the number of failed sub-operations.
        /// </summary>
        public int Failures
        {
            get
            {
                return Command.Get(DicomTag.NumberOfFailedSuboperations, (ushort)0);
            }
            set
            {
                Command.AddOrUpdate(DicomTag.NumberOfFailedSuboperations, (ushort)value);
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Formatted output of the <see cref="DicomCGetResponse"/> item.
        /// </summary>
        /// <returns>Formatted output of the <see cref="DicomCGetResponse"/> item.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} [{1}]: {2}", ToString(Type), RequestMessageID, Status.Description);
            if (Completed != 0) sb.AppendFormat("\n\t\tCompleted:	{0}", Completed);
            if (Remaining != 0) sb.AppendFormat("\n\t\tRemaining:	{0}", Remaining);
            if (Warnings != 0) sb.AppendFormat("\n\t\tWarnings:	{0}", Warnings);
            if (Failures != 0) sb.AppendFormat("\n\t\tFailures:	{0}", Failures);
            if (Status.State != DicomState.Pending && Status.State != DicomState.Success)
            {
                if (!String.IsNullOrEmpty(Status.ErrorComment)) sb.AppendFormat("\n\t\tError:		{0}", Status.ErrorComment);
                if (Command.Contains(DicomTag.OffendingElement))
                {
                    string[] tags = Command.Get<string[]>(DicomTag.OffendingElement);
                    if (tags.Length > 0)
                    {
                        sb.Append("\n\t\tTags:		");
                        foreach (var tag in tags) sb.AppendFormat(" {0}", tag);
                    }
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}
