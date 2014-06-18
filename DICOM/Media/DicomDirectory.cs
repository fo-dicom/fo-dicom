using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Dicom.IO;
using Dicom.IO.Reader;
using Dicom.IO.Writer;

namespace Dicom.Media {
	public class DicomDirectory : DicomFile {
		#region Properties and Attributes

		private DicomSequence _directoryRecordSequence;

		private uint _fileOffset;

		public DicomDirectoryRecord RootDirectoryRecord { get; private set; }

		public DicomDirectoryRecordCollection RootDirectoryRecordCollection {
			get { return new DicomDirectoryRecordCollection(RootDirectoryRecord); }
		}

		public string FileSetID {
			get { return Dataset.Get<string>(DicomTag.FileSetID); }
			set {
				if (!string.IsNullOrWhiteSpace(value)) {
					Dataset.Add<string>(DicomTag.FileSetID, value);
				} else {
					throw new ArgumentException("FileSetId can only be a maxmimum of 16 characters", "value");
				}
			}
		}

		public string SourceApplicationEntityTitle {
			get { return FileMetaInfo.SourceApplicationEntityTitle; }
			set { FileMetaInfo.SourceApplicationEntityTitle = value; }
		}

		public DicomUID MediaStorageSOPInstanceUID {
			get { return FileMetaInfo.MediaStorageSOPInstanceUID; }
			set { FileMetaInfo.MediaStorageSOPInstanceUID = value; }
		}

		#endregion

		#region Constructors

		public DicomDirectory() : base() {
			FileMetaInfo.Add<byte>(DicomTag.FileMetaInformationVersion, new byte[] { 0x00, 0x01 });
			FileMetaInfo.MediaStorageSOPClassUID = DicomUID.MediaStorageDirectoryStorage;
			FileMetaInfo.MediaStorageSOPInstanceUID = DicomUID.Generate();
			FileMetaInfo.SourceApplicationEntityTitle = string.Empty;
			FileMetaInfo.TransferSyntax = DicomTransferSyntax.ImplicitVRLittleEndian;
			FileMetaInfo.ImplementationClassUID = DicomImplementation.ClassUID;
			FileMetaInfo.ImplementationVersionName = DicomImplementation.Version;

			_directoryRecordSequence = new DicomSequence(DicomTag.DirectoryRecordSequence);

			Dataset.Add<string>(DicomTag.FileSetID, string.Empty)
				   .Add<ushort>(DicomTag.FileSetConsistencyFlag, 0)
				   .Add<uint>(DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity, 0)
				   .Add<uint>(DicomTag.OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity, 0);
		}

		#endregion

		#region Save/Load Methods

		protected override void OnSave() {
			if (RootDirectoryRecord == null)
				throw new InvalidOperationException("No DICOM files added, cannot save DICOM directory");

			var calculator = new DicomWriteLengthCalculator(FileMetaInfo.TransferSyntax, DicomWriteOptions.Default);

			// ensure write length calculator does not include end of sequence item
			Dataset.Remove(DicomTag.DirectoryRecordSequence);

			_fileOffset = 128 + 4 + calculator.Calculate(FileMetaInfo) + calculator.Calculate(Dataset);

			//Add the offset for the Directory Record sequence tag itself
			_fileOffset += 4;//sequence element tag
			if (FileMetaInfo.TransferSyntax.IsExplicitVR) {
				_fileOffset += 2; // vr
				_fileOffset += 2; // padding
				_fileOffset += 4; // length
			} else {
				_fileOffset += 4; //length
			}

			_directoryRecordSequence.Items.Clear();
			AddDirectoryRecordsToSequenceItem(RootDirectoryRecord);
			Dataset.Add(_directoryRecordSequence);

			if (RootDirectoryRecord != null) {
				CalculateOffsets(calculator);

				SetOffsets(RootDirectoryRecord);

				Dataset.Add<uint>(DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity, RootDirectoryRecord.Offset);

				var lastRoot = RootDirectoryRecord;

				while (lastRoot.NextDirectoryRecord != null)
					lastRoot = lastRoot.NextDirectoryRecord;

				Dataset.Add<uint>(DicomTag.OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity, lastRoot.Offset);
			} else {
				Dataset.Add<uint>(DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity, 0);
				Dataset.Add<uint>(DicomTag.OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity, 0);
			}
		}

		public new static DicomDirectory Open(string fileName) {
			var df = new DicomDirectory();

			// reset datasets
			df.FileMetaInfo.Clear();
			df.Dataset.Clear();

			try {
				df.File = new FileReference(fileName);

				using (var source = new FileByteSource(df.File)) {
					DicomFileReader reader = new DicomFileReader();

					var datasetObserver = new DicomDatasetReaderObserver(df.Dataset);
					var dirObserver = new DicomDirectoryReaderObserver(df.Dataset);

					reader.Read(source,
						new DicomDatasetReaderObserver(df.FileMetaInfo),
						new DicomReaderMultiObserver(datasetObserver, dirObserver));

					df.Format = reader.FileFormat;

					df.Dataset.InternalTransferSyntax = reader.Syntax;

					df._directoryRecordSequence = df.Dataset.Get<DicomSequence>(DicomTag.DirectoryRecordSequence);
					df.RootDirectoryRecord = dirObserver.BuildDirectoryRecords();

					return df;
				}
			} catch (Exception e) {
				throw new DicomFileException(df, e.Message, e);
			}
		}

		public new static IAsyncResult BeginOpen(string fileName, AsyncCallback callback, object state) {
			var df = new DicomDirectory();

			// reset datasets
			df.FileMetaInfo.Clear();
			df.Dataset.Clear();

			df.File = new FileReference(fileName);

			FileByteSource source = new FileByteSource(df.File);

			EventAsyncResult result = new EventAsyncResult(callback, state);

			DicomFileReader reader = new DicomFileReader();

			var datasetObserver = new DicomDatasetReaderObserver(df.Dataset);
			var dirObserver = new DicomDirectoryReaderObserver(df.Dataset);

			reader.BeginRead(source,
				new DicomDatasetReaderObserver(df.FileMetaInfo),
				new DicomReaderMultiObserver(datasetObserver, dirObserver),
				OnReadComplete, new Tuple<DicomFileReader, DicomDirectory, DicomDirectoryReaderObserver, EventAsyncResult>(reader, df, dirObserver, result));

			return result;
		}
		private static void OnReadComplete(IAsyncResult result) {
			var state = result.AsyncState as Tuple<DicomFileReader, DicomDirectory, DicomDirectoryReaderObserver, EventAsyncResult>;

			Exception e = null;
			try {
				state.Item1.EndRead(result);

				// ensure that file handles are closed
				var source = (FileByteSource)state.Item1.Source;
				source.Dispose();

				state.Item2.Format = state.Item1.FileFormat;
				state.Item2.Dataset.InternalTransferSyntax = state.Item1.Syntax;

				state.Item2._directoryRecordSequence = state.Item2.Dataset.Get<DicomSequence>(DicomTag.DirectoryRecordSequence);
				state.Item2.RootDirectoryRecord = state.Item3.BuildDirectoryRecords();
			} catch (Exception ex) {
				state.Item2.Format = state.Item1.FileFormat;
				e = ex;
			}

			state.Item4.InternalState = new Tuple<DicomDirectory, Exception>(state.Item2, e);
			state.Item4.Set();
		}

		public new static DicomDirectory EndOpen(IAsyncResult result) {
			result.AsyncWaitHandle.WaitOne();

			EventAsyncResult eventResult = result as EventAsyncResult;
			var state = eventResult.InternalState as Tuple<DicomDirectory, Exception>;

			if (state.Item2 != null)
				throw new DicomFileException(state.Item1, state.Item2.Message, state.Item2);

			return state.Item1;
		}

		private void AddDirectoryRecordsToSequenceItem(DicomDirectoryRecord recordItem) {
			if (recordItem == null)
				return;

			_directoryRecordSequence.Items.Add(recordItem);
			if (recordItem.LowerLevelDirectoryRecord != null)
				AddDirectoryRecordsToSequenceItem(recordItem.LowerLevelDirectoryRecord);

			if (recordItem.NextDirectoryRecord != null)
				AddDirectoryRecordsToSequenceItem(recordItem.NextDirectoryRecord);
		}

		#endregion

		#region Calculation Methods

		private void CalculateOffsets(DicomWriteLengthCalculator calculator) {
			foreach (var item in Dataset.Get<DicomSequence>(DicomTag.DirectoryRecordSequence)) {
				var record = item as DicomDirectoryRecord;
				if (record == null)
					throw new InvalidOperationException("Unexpected type for directory record: " + item.GetType());

				record.Offset = _fileOffset;

				_fileOffset += 4 + 4;//Sequence item tag;

				_fileOffset += calculator.Calculate(record);

				_fileOffset += 4 + 4; // Sequence Item Delimitation Item
			}

			_fileOffset += 4 + 4; // Sequence Delimitation Item
		}

		private void SetOffsets(DicomDirectoryRecord record) {
			if (record.NextDirectoryRecord != null) {
				record.Add<uint>(DicomTag.OffsetOfTheNextDirectoryRecord, record.NextDirectoryRecord.Offset);
				SetOffsets(record.NextDirectoryRecord);
			} else {
				record.Add<uint>(DicomTag.OffsetOfTheNextDirectoryRecord, 0);
			}

			if (record.LowerLevelDirectoryRecord != null) {
				record.Add<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity, record.LowerLevelDirectoryRecord.Offset);
				SetOffsets(record.LowerLevelDirectoryRecord);
			} else {
				record.Add<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity, 0);
			}
		}

		#endregion

		#region File system creator Methods

		public void AddFile(DicomFile dicomFile, string referencedFileId = "") {
			if (dicomFile == null)
				throw new ArgumentNullException("dicomFile");

			AddNewRcord(dicomFile.FileMetaInfo, dicomFile.Dataset, referencedFileId);
		}

		private void AddNewRcord(DicomFileMetaInformation metaFileInfo, DicomDataset dataset, string referencedFileId) {
			DicomDirectoryRecord patientRecord, studyRecord, seriesRecord;

			patientRecord = CreatePatientRecord(dataset);
			studyRecord = CreateStudyRecord(dataset, patientRecord);
			seriesRecord = CreateSeriesRecord(dataset, studyRecord);
			CreateImageRecord(metaFileInfo, dataset, seriesRecord, referencedFileId);
		}

		private void CreateImageRecord(DicomFileMetaInformation metaFileInfo, DicomDataset dataset, DicomDirectoryRecord seriesRecord, string referencedFileId) {
			var currentImage = seriesRecord.LowerLevelDirectoryRecord;
			var imageInstanceUid = dataset.Get<string>(DicomTag.SOPInstanceUID);


			while (currentImage != null) {
				if (currentImage.Get<string>(DicomTag.ReferencedSOPInstanceUIDInFile) == imageInstanceUid) {
					return;
				}

				if (currentImage.NextDirectoryRecord != null) {
					currentImage = currentImage.NextDirectoryRecord;
				} else {
					//no more patient records, break the loop
					break;
				}
			}
			var newImage = CreateRecordSequenceItem(DicomDirectoryRecordType.Image, dataset);
			newImage.Add(DicomTag.ReferencedFileID, referencedFileId);
			newImage.Add(DicomTag.ReferencedSOPClassUIDInFile, metaFileInfo.MediaStorageSOPClassUID.UID);
			newImage.Add(DicomTag.ReferencedSOPInstanceUIDInFile, metaFileInfo.MediaStorageSOPInstanceUID.UID);
			newImage.Add(DicomTag.ReferencedTransferSyntaxUIDInFile, metaFileInfo.TransferSyntax.UID);

			if (currentImage != null) {
				//study not found under patient record
				currentImage.NextDirectoryRecord = newImage;
			} else {
				//no studies record found under patient record
				seriesRecord.LowerLevelDirectoryRecord = newImage;
			}
		}

		private DicomDirectoryRecord CreateSeriesRecord(DicomDataset dataset, DicomDirectoryRecord studyRecord) {
			var currentSeries = studyRecord.LowerLevelDirectoryRecord;
			var seriesInstanceUid = dataset.Get<string>(DicomTag.SeriesInstanceUID);


			while (currentSeries != null) {
				if (currentSeries.Get<string>(DicomTag.SeriesInstanceUID) == seriesInstanceUid) {
					return currentSeries;
				}

				if (currentSeries.NextDirectoryRecord != null) {
					currentSeries = currentSeries.NextDirectoryRecord;
				} else {
					//no more patient records, break the loop
					break;
				}
			}

			var newSeries = CreateRecordSequenceItem(DicomDirectoryRecordType.Series, dataset);
			if (currentSeries != null) {
				//series not found under study record
				currentSeries.NextDirectoryRecord = newSeries;
			} else {
				//no series record found under study record
				studyRecord.LowerLevelDirectoryRecord = newSeries;
			}
			return newSeries;
		}

		private DicomDirectoryRecord CreateStudyRecord(DicomDataset dataset, DicomDirectoryRecord patientRecord) {
			var currentStudy = patientRecord.LowerLevelDirectoryRecord;
			var studyInstanceUid = dataset.Get<string>(DicomTag.StudyInstanceUID);


			while (currentStudy != null) {
				if (currentStudy.Get<string>(DicomTag.StudyInstanceUID) == studyInstanceUid) {
					return currentStudy;
				}

				if (currentStudy.NextDirectoryRecord != null) {
					currentStudy = currentStudy.NextDirectoryRecord;
				} else {
					//no more patient records, break the loop
					break;
				}
			}
			var newStudy = CreateRecordSequenceItem(DicomDirectoryRecordType.Study, dataset);
			if (currentStudy != null) {
				//study not found under patient record
				currentStudy.NextDirectoryRecord = newStudy;
			} else {
				//no studies record found under patient record
				patientRecord.LowerLevelDirectoryRecord = newStudy;
			}
			return newStudy;
		}

		private DicomDirectoryRecord CreatePatientRecord(DicomDataset dataset) {
			var currentPatient = RootDirectoryRecord;
			var patientId = dataset.Get<string>(DicomTag.PatientID);
			var patientName = dataset.Get<string>(DicomTag.PatientName);

			while (currentPatient != null) {
				if (currentPatient.Get<string>(DicomTag.PatientID) == patientId
					&& currentPatient.Get<string>(DicomTag.PatientName) == patientName) {
					return currentPatient;
				}

				if (currentPatient.NextDirectoryRecord != null) {
					currentPatient = currentPatient.NextDirectoryRecord;
				} else {
					//no more patient records, break the loop
					break;
				}
			}
			var newPatient = CreateRecordSequenceItem(DicomDirectoryRecordType.Patient, dataset);
			if (currentPatient != null) {
				//patient not found under root record
				currentPatient.NextDirectoryRecord = newPatient;
			} else {
				//no patients record found under root record
				RootDirectoryRecord = newPatient;
			}
			return newPatient;
		}

		private DicomDirectoryRecord CreateRecordSequenceItem(DicomDirectoryRecordType recordType, DicomDataset dataset) {
			if (recordType == null)
				throw new ArgumentNullException("recordType");
			if (dataset == null)
				throw new ArgumentNullException("dataset");

			var sequenceItem = new DicomDirectoryRecord();

			//add record item attributes
			sequenceItem.Add<uint>(DicomTag.OffsetOfTheNextDirectoryRecord, 0);
			sequenceItem.Add<ushort>(DicomTag.RecordInUseFlag, 0xFFFF);
			sequenceItem.Add<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity, 0);
			sequenceItem.Add<string>(DicomTag.DirectoryRecordType, recordType.ToString());

			//copy the current dataset character set
			sequenceItem.Add(dataset.FirstOrDefault(d => d.Tag == DicomTag.SpecificCharacterSet));

			foreach (var tag in recordType.Tags) {
				if (dataset.Contains(tag)) {
					sequenceItem.Add(dataset.Get<DicomItem>(tag));
				} else {
					System.Diagnostics.Debug.WriteLine("Cannot find tag {0} for record type {1}", tag, recordType);
				}
			}

			return sequenceItem;
		}
		#endregion
	}
}
