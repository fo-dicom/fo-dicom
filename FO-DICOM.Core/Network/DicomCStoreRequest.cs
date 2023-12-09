// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network.Client;
using System.Collections.Generic;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Represents a DICOM C-Store request to be sent to a C-Store SCP or a C-Store request that has been received from a C-Store SCU.
    /// </summary>
    public sealed class DicomCStoreRequest : DicomPriorityRequest
    {
        /// <summary>
        /// Constructor for DICOM C-Store request received from SCU.
        /// </summary>
        /// <remarks>
        /// In most use cases this constructor will only be called by the library.
        /// </remarks>
        /// <param name="command">DICOM Command Dataset</param>
        public DicomCStoreRequest(DicomDataset command)
            : base(command)
        {
        }

        /// <summary>
        /// Initializes DICOM C-Store request to be sent to SCP.
        /// </summary>
        /// <param name="file">DICOM file to be sent</param>
        /// <param name="priority">Priority of request</param>
        public DicomCStoreRequest(DicomFile file, DicomPriority priority = DicomPriority.Medium)
            : base(DicomCommandField.CStoreRequest, file.Dataset.GetSingleValue<DicomUID>(DicomTag.SOPClassUID), priority)
        {
            File = file;
            Dataset = file.Dataset;

            // for potentially invalid UID values, we have to disable validation
            using var unvalidated = new UnvalidatedScope(Command);
            SOPInstanceUID = File.Dataset.GetSingleValue<DicomUID>(DicomTag.SOPInstanceUID);
        }

        /// <summary>
        /// Initializes DICOM C-Store request to be sent to SCP.
        /// </summary>
        /// <param name="fileName">DICOM file to be sent</param>
        /// <param name="priority">Priority of request</param>
        public DicomCStoreRequest(string fileName, DicomPriority priority = DicomPriority.Medium)
            : this(DicomFile.Open(fileName), priority)
        {
        }

        /// <summary>Gets the DICOM file associated with this DICOM C-Store request.</summary>
        public DicomFile File { get; internal set; }

        /// <summary>Gets the SOP Instance UID of the DICOM file associated with this DICOM C-Store request.</summary>
        public DicomUID SOPInstanceUID
        {
            get => Command.GetSingleValue<DicomUID>(DicomTag.AffectedSOPInstanceUID);
            private set => Command.AddOrUpdate(DicomTag.AffectedSOPInstanceUID, value);
        }

        /// <summary>Gets the transfer syntax of the DICOM file associated with this DICOM C-Store request.</summary>
        public DicomTransferSyntax TransferSyntax
            =>
                File != null
                    ? (File.FileMetaInfo.Contains(DicomTag.TransferSyntaxUID)
                           ? File.FileMetaInfo.TransferSyntax
                           : File.Dataset.InternalTransferSyntax)
                    : null;

        /// <summary>
        /// Additional transfer syntaxes to propose in the association request.
        ///
        /// DICOM dataset will be transcoded on the fly if necessary.
        /// </summary>
        public DicomTransferSyntax[] AdditionalTransferSyntaxes { get; set; }

        /// <summary>
        /// If set, the default transfer syntax (Implicit VR Little Endian) will
        /// not be automatically proposed when associating with remote system. 
        /// This should only be used in exception cases (See PS3.5 section 10.1).
        /// </summary>
        public bool OmitImplicitVrTransferSyntaxInAssociationRequest { get; set; }

        /// <summary>
        /// Gets or sets the (optional) Common Extended Negotiation Service Class UID.
        /// </summary>
        public DicomUID CommonServiceClassUid { get; set; }

        /// <summary>
        /// Gets or sets the (optional) Common Extended Negotiation Related General SOP Class Identification
        /// </summary>
        public List<DicomUID> RelatedGeneralSopClasses { get; set; }

        /// <summary>
        /// Represents a callback method to be executed when the response for the DICOM C-Store request is received.
        /// </summary>
        /// <param name="request">Sent DICOM C-Store request</param>
        /// <param name="response">Received DICOM C-Store response</param>
        public delegate void ResponseDelegate(DicomCStoreRequest request, DicomCStoreResponse response);

        /// <summary>Delegate to be executed when the response for the DICOM C-Store request is received.</summary>
        public ResponseDelegate OnResponseReceived;

        /// <summary>
        /// Internal. Executes the DICOM C-Store response callback.
        /// </summary>
        /// <param name="service">DICOM SCP implementation</param>
        /// <param name="response">Received DICOM response</param>
        protected internal override void PostResponse(DicomService service, DicomResponse response)
        {
            try
            {
                OnResponseReceived?.Invoke(this, (DicomCStoreResponse)response);
            }
            catch
            {
                // ignore exceptions
            }
        }
    }
}
