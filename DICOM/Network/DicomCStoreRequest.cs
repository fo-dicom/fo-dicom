using System;

namespace Dicom.Network {
	public class DicomCStoreRequest : DicomRequest {
		public DicomCStoreRequest(DicomDataset command) : base(command) {
		}

		public DicomCStoreRequest(DicomFile file, DicomPriority priority = DicomPriority.Medium) : base(DicomCommandField.CStoreRequest, file.FileMetaInfo.MediaStorageSOPClassUID, priority) {
			File = file;
			Dataset = file.Dataset;
			AffectedSOPClassUID = File.FileMetaInfo.MediaStorageSOPClassUID;
			SOPInstanceUID = File.FileMetaInfo.MediaStorageSOPInstanceUID;
		}

		public DicomCStoreRequest(string fileName, DicomPriority priority = DicomPriority.Medium) : this(DicomFile.Open(fileName), priority) {
		}

		public DicomFile File {
			get;
			internal set;
		}

		public DicomUID SOPInstanceUID {
			get { return Command.Get<DicomUID>(DicomTag.AffectedSOPInstanceUID); }
			private set { Command.Add(DicomTag.AffectedSOPInstanceUID, value); }
		}

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

		public delegate void ResponseDelegate(DicomCStoreRequest request, DicomCStoreResponse response);

		public ResponseDelegate OnResponseReceived;

		internal override void PostResponse(DicomService service, DicomResponse response) {
			try {
				if (OnResponseReceived != null)
					OnResponseReceived(this, (DicomCStoreResponse)response);
			} catch {
			}
		}
	}
}
