using Dicom.IO.Writer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicom.Media
{
    public class DicomDirectory : DicomFile, IDisposable
    {
        #region Properties and Attributes

        private readonly DicomSequence _directoryRecordSequence;

        private uint _fileOffset;

        public DirectoryRecordSequenceItem RootDirectoryRecord { get; private set; }

        public DirectoryRecordCollection RootDirectoryRecordCollection
        {
            get { return new DirectoryRecordCollection(RootDirectoryRecord); }
        }

        public string FileSetId
        {
            get { return Dataset.Get<string>(DicomTag.FileSetID); }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Dataset.Add<string>(DicomTag.FileSetID, value);
                }
                else
                {
                    throw new ArgumentException("FileSetId can only be a maxmimum of 16 characters", "value");
                }
            }
        }

        public string SourceApplicationEntityTitle
        {
            get { return FileMetaInfo.SourceApplicationEntityTitle; }
            set { FileMetaInfo.SourceApplicationEntityTitle = value; }
        }

        public DicomUID MediaStorageSopInstanceUid
        {
            get { return FileMetaInfo.MediaStorageSOPInstanceUID; }
            set { FileMetaInfo.MediaStorageSOPInstanceUID = value; }
        }

        #endregion

        #region Constructors

        public DicomDirectory()
            : base()
        {
            FileMetaInfo.Add<byte>(DicomTag.FileMetaInformationVersion, new byte[] { 0x00, 0x01 });
            FileMetaInfo.MediaStorageSOPClassUID = DicomUID.MediaStorageDirectoryStorage;
            FileMetaInfo.MediaStorageSOPInstanceUID = DicomUID.Generate("MediaStorageSOPInstanceUID");
            FileMetaInfo.SourceApplicationEntityTitle = string.Empty;
            FileMetaInfo.TransferSyntax = DicomTransferSyntax.ImplicitVRLittleEndian;
            FileMetaInfo.ImplementationClassUID = DicomImplementation.ClassUID;
            FileMetaInfo.ImplementationVersionName = DicomImplementation.Version;

            _directoryRecordSequence = new DicomSequence(DicomTag.DirectoryRecordSequence);

            Dataset.Add<string>(DicomTag.FileSetID, string.Empty)
                   .Add<ushort>(DicomTag.FileSetConsistencyFlag, 0)
                   .Add<uint>(DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity, 0)
                   .Add<uint>(DicomTag.OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity, 0)
                   .Add(_directoryRecordSequence);

            DicomWriteOptions = new DicomWriteOptions();
        }

        public DicomDirectory(string fileName)
            : base()
        {
            DicomWriteOptions = new DicomWriteOptions();
            try
            {
                File = new IO.FileReference(fileName);

                using (var source = new IO.FileByteSource(File))
                {
                    var reader = new IO.Reader.DicomFileReader();

                    var datasetObserver = new IO.Reader.DicomDatasetReaderObserver(Dataset);
                    var dirObserver = new DicomDirectoryReaderObserver(Dataset);
                    
                    reader.Read(source,
                        new IO.Reader.DicomDatasetReaderObserver(FileMetaInfo),
                        new IO.Reader.DicomReaderMultiObserver(datasetObserver, dirObserver));

                    Format = reader.FileFormat;

                    Dataset.InternalTransferSyntax = reader.Syntax;


                    _directoryRecordSequence = Dataset.Get<DicomSequence>(DicomTag.DirectoryRecordSequence);

                    RootDirectoryRecord = dirObserver.BuildDirectoryRecords();
                }
            }
            catch (Exception e)
            {
                throw new DicomFileException(this, e.Message, e);
            }
        }
        #endregion

        #region Save/Load Methods

        public override void Save(string fileName)
        {
            if (RootDirectoryRecord == null)
                throw new InvalidOperationException("No DICOM files added, cannot save DICOM directory");

            _directoryRecordSequence.Items.Clear();
            var calculator = new DicomWriteLengthCalculator(FileMetaInfo.TransferSyntax, DicomWriteOptions);

            _fileOffset = 128 + 4 + calculator.Calculate(FileMetaInfo)
                + calculator.Calculate(Dataset);

            //Add the offset for the Directory Record sequence tag itself
            _fileOffset += 4;//sequence element tag
            if (FileMetaInfo.TransferSyntax.IsExplicitVR)
            {
                _fileOffset += 2;//vr
                _fileOffset += 4;//length
            }
            else
            {
                _fileOffset += 4;//length
            }

            AddDirectoryRecordsToSequenceItem(RootDirectoryRecord);

            if (RootDirectoryRecord != null)
            {
                CalculateOffsets(calculator);

                SetOffsets(RootDirectoryRecord);

                Dataset.Add<uint>(DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity, RootDirectoryRecord.Offset);

                var lastRoot = RootDirectoryRecord;

                while (lastRoot.NextDirectoryRecord != null)
                    lastRoot = lastRoot.NextDirectoryRecord;

                Dataset.Add<uint>(DicomTag.OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity, lastRoot.Offset);
            }
            else
            {
                Dataset.Add<uint>(DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity, 0);
                Dataset.Add<uint>(DicomTag.OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity, 0);
            }

            base.Save(fileName);
        }

        public override void BeginSave(string fileName, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }


        public static DicomDirectory OpenMedia(string fileName)
        {
            var dicomDirectory = new DicomDirectory(fileName);
            return dicomDirectory;
        }
        private void AddDirectoryRecordsToSequenceItem(DirectoryRecordSequenceItem recordItem)
        {
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

        private void CalculateOffsets(DicomWriteLengthCalculator calculator)
        {
            foreach (var item in Dataset.Get<DicomSequence>(DicomTag.DirectoryRecordSequence))
            {
                var record = item as DirectoryRecordSequenceItem;
                if (record == null)
                    throw new InvalidOperationException("Unexpected type for directory record: " + item.GetType());

                record.Offset = _fileOffset;

                _fileOffset += 4 + 4;//Sequence item tag;

                _fileOffset += calculator.Calculate(record);

                _fileOffset += 4 + 4; // Sequence Item Delimitation Item
            }

            _fileOffset += 4 + 4; // Sequence Delimitation Item
        }

        private void SetOffsets(DirectoryRecordSequenceItem record)
        {
            if (record.NextDirectoryRecord != null)
            {
                record.Add<uint>(DicomTag.OffsetOfTheNextDirectoryRecord, record.NextDirectoryRecord.Offset);
                SetOffsets(record.NextDirectoryRecord);
            }
            else
            {
                record.Add<uint>(DicomTag.OffsetOfTheNextDirectoryRecord, 0);
            }

            if (record.LowerLevelDirectoryRecord != null)
            {
                record.Add<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity, record.LowerLevelDirectoryRecord.Offset);
                SetOffsets(record.LowerLevelDirectoryRecord);
            }
            else
            {
                record.Add<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity, 0);
            }
        }

        #endregion

        #region File system creator Methods

        public void AddFile(DicomFile dicomFile, string referencedFileId = "")
        {
            if (dicomFile == null)
                throw new ArgumentNullException("dicomFile");

            AddNewRcord(dicomFile.FileMetaInfo, dicomFile.Dataset, referencedFileId);
        }

        private void AddNewRcord(DicomFileMetaInformation metaFileInfo, DicomDataset dataset, string referencedFileId)
        {
            DirectoryRecordSequenceItem patientRecord, studyRecord, seriesRecord;

            patientRecord = CreatePatientRecord(dataset);
            studyRecord = CreateStudyRecord(dataset, patientRecord);
            seriesRecord = CreateSeriesRecord(dataset, studyRecord);
            CreateImageRecord(metaFileInfo, dataset, seriesRecord, referencedFileId);
        }

        private void CreateImageRecord(DicomFileMetaInformation metaFileInfo, DicomDataset dataset, DirectoryRecordSequenceItem seriesRecord, string referencedFileId)
        {
            var currentImage = seriesRecord.LowerLevelDirectoryRecord;
            var imageInstanceUid = dataset.Get<string>(DicomTag.SOPInstanceUID);


            while (currentImage != null)
            {
                if (currentImage.Get<string>(DicomTag.ReferencedSOPInstanceUIDInFile) == imageInstanceUid)
                {
                    return;
                }

                if (currentImage.NextDirectoryRecord != null)
                {
                    currentImage = currentImage.NextDirectoryRecord;
                }
                else
                {
                    //no more patient records, break the loop
                    break;
                }
            }
            var newImage = CreateRecordSequenceItem(DirectoryRecordType.Image, dataset);
            newImage.Add(DicomTag.ReferencedFileID, referencedFileId);
            newImage.Add(DicomTag.ReferencedSOPClassUIDInFile, metaFileInfo.MediaStorageSOPClassUID.UID);
            newImage.Add(DicomTag.ReferencedSOPInstanceUIDInFile, metaFileInfo.MediaStorageSOPInstanceUID.UID);
            newImage.Add(DicomTag.ReferencedTransferSyntaxUIDInFile, metaFileInfo.TransferSyntax.UID);

            if (currentImage != null)
            {
                //study not found under patient record
                currentImage.NextDirectoryRecord = newImage;
            }
            else
            {
                //no studies record found under patient record
                seriesRecord.LowerLevelDirectoryRecord = newImage;
            }
        }

        private DirectoryRecordSequenceItem CreateSeriesRecord(DicomDataset dataset, DirectoryRecordSequenceItem studyRecord)
        {
            var currentSeries = studyRecord.LowerLevelDirectoryRecord;
            var seriesInstanceUid = dataset.Get<string>(DicomTag.SeriesInstanceUID);


            while (currentSeries != null)
            {
                if (currentSeries.Get<string>(DicomTag.SeriesInstanceUID) == seriesInstanceUid)
                {
                    return currentSeries;
                }

                if (currentSeries.NextDirectoryRecord != null)
                {
                    currentSeries = currentSeries.NextDirectoryRecord;
                }
                else
                {
                    //no more patient records, break the loop
                    break;
                }
            }

            var newSeries = CreateRecordSequenceItem(DirectoryRecordType.Series, dataset);
            if (currentSeries != null)
            {
                //series not found under study record
                currentSeries.NextDirectoryRecord = newSeries;
            }
            else
            {
                //no series record found under study record
                studyRecord.LowerLevelDirectoryRecord = newSeries;
            }
            return newSeries;
        }

        private DirectoryRecordSequenceItem CreateStudyRecord(DicomDataset dataset, DirectoryRecordSequenceItem patientRecord)
        {
            var currentStudy = patientRecord.LowerLevelDirectoryRecord;
            var studyInstanceUid = dataset.Get<string>(DicomTag.StudyInstanceUID);


            while (currentStudy != null)
            {
                if (currentStudy.Get<string>(DicomTag.StudyInstanceUID) == studyInstanceUid)
                {
                    return currentStudy;
                }

                if (currentStudy.NextDirectoryRecord != null)
                {
                    currentStudy = currentStudy.NextDirectoryRecord;
                }
                else
                {
                    //no more patient records, break the loop
                    break;
                }
            }
            var newStudy = CreateRecordSequenceItem(DirectoryRecordType.Study, dataset);
            if (currentStudy != null)
            {
                //study not found under patient record
                currentStudy.NextDirectoryRecord = newStudy;
            }
            else
            {
                //no studies record found under patient record
                patientRecord.LowerLevelDirectoryRecord = newStudy;
            }
            return newStudy;
        }

        private DirectoryRecordSequenceItem CreatePatientRecord(DicomDataset dataset)
        {
            var currentPatient = RootDirectoryRecord;
            var patientId = dataset.Get<string>(DicomTag.PatientID);
            var patientName = dataset.Get<string>(DicomTag.PatientName);

            while (currentPatient != null)
            {
                if (currentPatient.Get<string>(DicomTag.PatientID) == patientId
                    && currentPatient.Get<string>(DicomTag.PatientName) == patientName)
                {
                    return currentPatient;
                }

                if (currentPatient.NextDirectoryRecord != null)
                {
                    currentPatient = currentPatient.NextDirectoryRecord;
                }
                else
                {
                    //no more patient records, break the loop
                    break;
                }
            }
            var newPatient = CreateRecordSequenceItem(DirectoryRecordType.Patient, dataset);
            if (currentPatient != null)
            {
                //patient not found under root record
                currentPatient.NextDirectoryRecord = newPatient;
            }
            else
            {
                //no patients record found under root record
                RootDirectoryRecord = newPatient;
            }
            return newPatient;
        }

        private DirectoryRecordSequenceItem CreateRecordSequenceItem(DirectoryRecordType recordType, DicomDataset dataset)
        {
            if (recordType == null)
                throw new ArgumentNullException("recordType");
            if (dataset == null)
                throw new ArgumentNullException("dataset");

            var sequenceItem = new DirectoryRecordSequenceItem();

            //add record item attributes
            sequenceItem.Add<uint>(DicomTag.OffsetOfTheNextDirectoryRecord, 0);
            sequenceItem.Add<ushort>(DicomTag.RecordInUseFlag, 0xFFFF);
            sequenceItem.Add<uint>(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity, 0);
            sequenceItem.Add<string>(DicomTag.DirectoryRecordType, recordType.ToString());

            //copy the current dataset character set
            sequenceItem.Add(dataset.FirstOrDefault(d => d.Tag == DicomTag.SpecificCharacterSet));

            foreach (var tag in recordType.Tags)
            {
                if (dataset.Contains(tag))
                {
                    sequenceItem.Add(dataset.Get<DicomItem>(tag));
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Cannot find tag {0} for record type {1}", tag, recordType);
                }
            }

            return sequenceItem;
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
