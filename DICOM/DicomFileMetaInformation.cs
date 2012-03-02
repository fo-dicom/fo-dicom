using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom {
	public class DicomFileMetaInformation : DicomDataset {
		public DicomFileMetaInformation() : base() {
		}

		public DicomFileMetaInformation(DicomDataset dataset) : base() {
			MediaStorageSOPClassUID = dataset.Get<DicomUID>(DicomTag.SOPClassUID);
			MediaStorageSOPInstanceUID = dataset.Get<DicomUID>(DicomTag.SOPInstanceUID);
			TransferSyntax = dataset.InternalTransferSyntax;
			ImplementationClassUID = DicomImplementation.ClassUID;
			ImplementationVersionName = DicomImplementation.Version;

			var machine = Environment.MachineName;
			if (machine.Length > 16)
				machine = machine.Substring(0, 16);
			SourceApplicationEntityTitle = machine;
		}

		public DicomUID MediaStorageSOPClassUID {
			get { return Get<DicomUID>(DicomTag.MediaStorageSOPClassUID); }
			set { Add(DicomTag.MediaStorageSOPClassUID, value); }
		}

		public DicomUID MediaStorageSOPInstanceUID {
			get { return Get<DicomUID>(DicomTag.MediaStorageSOPInstanceUID); }
			set { Add(DicomTag.MediaStorageSOPInstanceUID, value); }
		}

		public DicomTransferSyntax TransferSyntax {
			get { return Get<DicomTransferSyntax>(DicomTag.TransferSyntaxUID); }
			set { Add(DicomTag.TransferSyntaxUID, value.UID); }
		}

		public DicomUID ImplementationClassUID {
			get { return Get<DicomUID>(DicomTag.ImplementationClassUID); }
			set { Add(DicomTag.ImplementationClassUID, value); }
		}

		public string ImplementationVersionName {
			get { return Get<string>(DicomTag.ImplementationVersionName); }
			set { Add(DicomTag.ImplementationVersionName, value); }
		}

		public string SourceApplicationEntityTitle {
			get { return Get<string>(DicomTag.SourceApplicationEntityTitle); }
			set { Add(DicomTag.SourceApplicationEntityTitle, value); }
		}
	}
}
