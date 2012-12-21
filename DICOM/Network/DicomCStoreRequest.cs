using System;

namespace Dicom.Network {
	/// <summary>
	/// Represents a DICOM C-Store request to be sent to a C-Store SCP or a C-Store request that has been received from a C-Store SCU.
	/// </summary>
	/// <example>
	/// The following example shows how to use the <see cref="DicomClient"/> class to send DICOM C-Store requests to a DICOM C-Store SCP.
	/// <code>
	/// var client = new DicomClient();
	/// 
	/// // queue C-Store request to send DICOM file
	/// client.Add(new DicomCStoreRequest(@"test1.dcm") {
	///		OnResponseReceived = (DicomCStoreRequest req, DicomCStoreResponse rsp) => {
	///			Console.WriteLine("{0}: {1}", req.SOPInstanceUID, rsp.Status);
	///		}
	/// });
	/// 
	/// // queue C-Store request with additional proposed transfer syntaxes
	/// client.Add(new DicomCStoreRequest(@"test2.dcm") {
	///		AdditionalTransferSyntaxes = new DicomTransferSyntax[] {
	///			DicomTransferSyntax.JPEGLSLossless,
	///			DicomTransferSyntax.JPEG2000Lossless
	///		}
	/// });
	/// 
	/// // connect and send queued requests
	/// client.Send("127.0.0.1", 12345, false, "SCU", "ANY-SCP");
	/// </code>
	/// </example>
	public class DicomCStoreRequest : DicomRequest {
		/// <summary>
		/// Constructor for DICOM C-Store request received from SCU.
		/// </summary>
		/// <remarks>
		/// In most use cases this constructor will only be called by the library.
		/// </remarks>
		/// <param name="command">DICOM Command Dataset</param>
		public DicomCStoreRequest(DicomDataset command) : base(command) {
		}

		/// <summary>
		/// Initializes DICOM C-Store request to be sent to SCP.
		/// </summary>
		/// <param name="file">DICOM file to be sent</param>
		/// <param name="priority">Priority of request</param>
		public DicomCStoreRequest(DicomFile file, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.CStoreRequest, file.Dataset.Get<DicomUID>(DicomTag.SOPClassUID), priority) {
			File = file;
			Dataset = file.Dataset;
			SOPInstanceUID = File.Dataset.Get<DicomUID>(DicomTag.SOPInstanceUID);
		}

		/// <summary>
		/// Initializes DICOM C-Store request to be sent to SCP.
		/// </summary>
		/// <param name="file">DICOM file to be sent</param>
		/// <param name="priority">Priority of request</param>
		public DicomCStoreRequest(string fileName, DicomPriority priority = DicomPriority.Medium) : this(DicomFile.Open(fileName), priority) {
		}

		/// <summary>Gets the DICOM file associated with this DICOM C-Store request.</summary>
		public DicomFile File {
			get;
			internal set;
		}

		/// <summary>Gets the SOP Instance UID of the DICOM file associated with this DICOM C-Store request.</summary>
		public DicomUID SOPInstanceUID {
			get { return Command.Get<DicomUID>(DicomTag.AffectedSOPInstanceUID); }
			private set { Command.Add(DicomTag.AffectedSOPInstanceUID, value); }
		}

		/// <summary>Gets the transfer syntax of the DICOM file associated with this DICOM C-Store request.</summary>
		public DicomTransferSyntax TransferSyntax {
			get { return File.FileMetaInfo.TransferSyntax; }
		}

		/// <summary>
		/// Additional transfer syntaxes to propose in the association request.
		/// 
		/// DICOM dataset will be transcoded on the fly if necessary.
		/// </summary>
		public DicomTransferSyntax[] AdditionalTransferSyntaxes {
			get;
			set;
		}

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
		internal override void PostResponse(DicomService service, DicomResponse response) {
			try {
				if (OnResponseReceived != null)
					OnResponseReceived(this, (DicomCStoreResponse)response);
			} catch {
			}
		}
	}
}
