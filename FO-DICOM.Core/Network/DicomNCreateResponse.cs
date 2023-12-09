// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Representation of the N-CREATE response.
    /// </summary>
    public sealed class DicomNCreateResponse : DicomResponse
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomNCreateResponse"/> class.
        /// </summary>
        /// <param name="command">N-CREATE response command.</param>
        public DicomNCreateResponse(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initizalizes a new instance of the <see cref="DicomNCreateResponse"/> class.
        /// </summary>
        /// <param name="request">Associated N-CREATE request.</param>
        /// <param name="status">Response status.</param>
        public DicomNCreateResponse(DicomNCreateRequest request, DicomStatus status)
            : base(request, status)
        {
            SOPInstanceUID = request.SOPInstanceUID;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the affected SOP instance UID.
        /// </summary>
        /// <remarks>In the N-CREATE response, Affected SOP Instance UID is optional, and <code>null</code> can thus be returned.</remarks>
        public DicomUID SOPInstanceUID
        {
            get => Command.GetSingleValueOrDefault<DicomUID>(DicomTag.AffectedSOPInstanceUID, null);
            private set
            {
                if (value == null)
                {
                    Command.Remove(DicomTag.AffectedSOPInstanceUID);
                }
                else
                {
                    Command.AddOrUpdate(DicomTag.AffectedSOPInstanceUID, value);
                }
            }
        }

        #endregion
    }
}
