// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using FellowOakDicom.IO.Reader;
using FellowOakDicom.IO.Writer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FellowOakDicom.Media
{

    /// <summary>
    /// Class that holds the Record entries of an entry across the different levels 
    /// </summary>
    public class DicomDirectoryEntry
    {

        public DicomDirectoryRecord PatientRecord { get; set; }
        public DicomDirectoryRecord StudyRecord { get; set; }
        public DicomDirectoryRecord SeriesRecord { get; set; }
        public DicomDirectoryRecord InstanceRecord { get; set; }

    }


    /// <summary>
    /// Class for managing DICOM directory objects.
    /// </summary>
    public class DicomDirectory : DicomFile
    {
        #region Properties and Attributes

        private DicomSequence _directoryRecordSequence;

        private uint _fileOffset;

        /// <summary>
        /// Gets the root directory record.
        /// </summary>
        public DicomDirectoryRecord RootDirectoryRecord { get; private set; }

        /// <summary>
        /// Gets the root directory record collection.
        /// </summary>
        public DicomDirectoryRecordCollection RootDirectoryRecordCollection
            => new DicomDirectoryRecordCollection(RootDirectoryRecord);

        /// <summary>
        /// Gets or sets the file set ID.
        /// </summary>
        /// <exception cref="System.ArgumentException">If applied file set ID is null or empty.</exception>
        public string FileSetID
        {
            get => Dataset.GetSingleValueOrDefault(DicomTag.FileSetID, string.Empty);
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Dataset.AddOrUpdate(DicomTag.FileSetID, value);
                }
                else
                {
                    throw new ArgumentException("File Set ID must not be null or empty.", nameof(value));
                }
            }
        }

        /// <summary>
        /// Gets or sets the source application entity title.
        /// </summary>
        public string SourceApplicationEntityTitle
        {
            get => FileMetaInfo.SourceApplicationEntityTitle;
            set => FileMetaInfo.SourceApplicationEntityTitle = value;
        }

        /// <summary>
        /// Gets or sets the media storage SOP instance UID.
        /// </summary>
        public DicomUID MediaStorageSOPInstanceUID
        {
            get => FileMetaInfo.MediaStorageSOPInstanceUID;
            set => FileMetaInfo.MediaStorageSOPInstanceUID = value;
        }

        internal bool ValidateItems { get; set; } = true;

        /// <summary>
        /// Gets or sets if the content of DicomItems shall be validated as soon as they are added to the DicomDataset
        /// </summary>
        [Obsolete("Use this property with care. You can suppress validation, but be aware you might create invalid Datasets if you need to set this property.", false)]
        public bool AutoValidate
        {
            get => ValidateItems;
            set => ValidateItems = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomDirectory"/> class.
        /// </summary>
        /// <param name="explicitVr">Indicates whether or not Value Representation of the DICOM directory should be explicit.</param>
        public DicomDirectory(bool explicitVr = true)
        {
            FileMetaInfo.Version = new byte[] { 0x00, 0x01 };
            FileMetaInfo.MediaStorageSOPClassUID = DicomUID.MediaStorageDirectoryStorage;
            FileMetaInfo.MediaStorageSOPInstanceUID = DicomUID.Generate();
            FileMetaInfo.TransferSyntax = explicitVr
                                              ? DicomTransferSyntax.ExplicitVRLittleEndian
                                              : DicomTransferSyntax.ImplicitVRLittleEndian;
            FileMetaInfo.ImplementationClassUID = DicomImplementation.ClassUID;
            FileMetaInfo.ImplementationVersionName = DicomImplementation.Version;

            _directoryRecordSequence = new DicomSequence(DicomTag.DirectoryRecordSequence);

            Dataset.Add(DicomTag.FileSetID, string.Empty)
                .Add<ushort>(DicomTag.FileSetConsistencyFlag, 0)
                .Add(DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity, 0U)
                .Add(DicomTag.OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity, 0U)
                .Add(_directoryRecordSequence);
        }

        /// <summary>
        /// Creates an instance of the <see cref="DicomDirectory"/> class File Meta Information and DICOM dataset are not initialized.
        /// </summary>
        /// <remarks>Intended to be used e.g. by the static Open methods to construct an empty <see cref="DicomDirectory"/> object subject to filling.</remarks>
        private DicomDirectory()
        {
        }

        #endregion

        #region Save/Load Methods

        /// <summary>
        /// Read DICOM Directory.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <returns><see cref="DicomDirectory"/> instance.</returns>
        public static DicomDirectory Open(string fileName)
        {
            return Open(fileName, DicomEncoding.Default);
        }

        /// <summary>
        /// Read DICOM Directory.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="fallbackEncoding">Encoding to apply if it cannot be identified from DICOM directory.</param>
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <param name="readOption">The option how to deal with large DICOM tags like pixel data.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passend, then the default of 64k is used.</param>
        /// <returns><see cref="DicomDirectory"/> instance.</returns>
        public static new DicomDirectory Open(string fileName, Encoding fallbackEncoding, Func<ParseState, bool> stop = null, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException(nameof(fallbackEncoding));
            }
            var df = new DicomDirectory();

            try
            {
                df.File = Setup.ServiceProvider.GetService<IFileReferenceFactory>().Create(fileName);

                using var unvalidated = new UnvalidatedScope(df.Dataset);
                using var source = new FileByteSource(df.File, readOption, largeObjectSize);
                var reader = new DicomFileReader();
                var dirObserver = new DicomDirectoryReaderObserver(df.Dataset);

                var result = reader.Read(
                    source,
                    new DicomDatasetReaderObserver(df.FileMetaInfo),
                    new DicomReaderMultiObserver(
                        new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding),
                        dirObserver),
                    stop);

                df = FinalizeDicomDirectoryLoad(df, reader, dirObserver, result);

                return df;
            }
            catch (Exception e)
            {
                throw new DicomFileException(df, e.Message, e);
            }
        }

        /// <summary>
        /// Read DICOM Directory.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <returns><see cref="DicomDirectory"/> instance.</returns>
        public static DicomDirectory Open(Stream stream)
        {
            return Open(stream, DicomEncoding.Default);
        }

        /// <summary>
        /// Read DICOM Directory from stream.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <param name="fallbackEncoding">Encoding to apply if it cannot be identified from DICOM directory.</param>
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <param name="readOption">The option how to deal with large DICOM tags like pixel data.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passend, then the default of 64k is used.</param>
        /// <returns><see cref="DicomDirectory"/> instance.</returns>
        public static new DicomDirectory Open(Stream stream, Encoding fallbackEncoding, Func<ParseState, bool> stop = null, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException(nameof(fallbackEncoding));
            }
            var df = new DicomDirectory();

            try
            {
                var source = StreamByteSourceFactory.Create(stream, readOption, largeObjectSize: largeObjectSize);

                using var unvalidated = new UnvalidatedScope(df.Dataset);
                var reader = new DicomFileReader();
                var dirObserver = new DicomDirectoryReaderObserver(df.Dataset);

                var result = reader.Read(
                    source,
                    new DicomDatasetReaderObserver(df.FileMetaInfo),
                    new DicomReaderMultiObserver(
                        new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding),
                        dirObserver),
                    stop);

                df = FinalizeDicomDirectoryLoad(df, reader, dirObserver, result);

                return df;
            }
            catch (Exception e)
            {
                throw new DicomFileException(df, e.Message, e);
            }
        }

        /// <summary>
        /// Asynchronously read DICOM Directory.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <returns>Awaitable <see cref="DicomDirectory"/> instance.</returns>
        public static Task<DicomDirectory> OpenAsync(string fileName)
        {
            return OpenAsync(fileName, DicomEncoding.Default);
        }

        /// <summary>
        /// Asynchronously read DICOM Directory.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="fallbackEncoding">Encoding to apply if it cannot be identified from DICOM directory.</param>
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <param name="readOption">The option how to deal with large DICOM tags like pixel data.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passend, then the default of 64k is used.</param>
        /// <returns>Awaitable <see cref="DicomDirectory"/> instance.</returns>
        public static new async Task<DicomDirectory> OpenAsync(string fileName, Encoding fallbackEncoding, Func<ParseState, bool> stop = null, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException(nameof(fallbackEncoding));
            }
            var df = new DicomDirectory();

            try
            {
                df.File = Setup.ServiceProvider.GetService<IFileReferenceFactory>().Create(fileName);

                using var unvalidated = new UnvalidatedScope(df.Dataset);
                using var source = new FileByteSource(df.File, readOption, largeObjectSize);
                var reader = new DicomFileReader();
                var dirObserver = new DicomDirectoryReaderObserver(df.Dataset);

                var result =
                    await
                    reader.ReadAsync(
                        source,
                        new DicomDatasetReaderObserver(df.FileMetaInfo),
                        new DicomReaderMultiObserver(
                        new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding),
                        dirObserver),
                        stop).ConfigureAwait(false);

                df = FinalizeDicomDirectoryLoad(df, reader, dirObserver, result);

                return df;
            }
            catch (Exception e)
            {
                throw new DicomFileException(df, e.Message, e);
            }
        }

        /// <summary>
        /// Asynchronously read DICOM Directory from stream.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <returns>Awaitable <see cref="DicomDirectory"/> instance.</returns>
        public static Task<DicomDirectory> OpenAsync(Stream stream)
        {
            return OpenAsync(stream, DicomEncoding.Default);
        }

        /// <summary>
        /// Asynchronously read DICOM Directory from stream.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        /// <param name="fallbackEncoding">Encoding to apply if it cannot be identified from DICOM directory.</param>
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <param name="readOption">The option how to deal with large DICOM tags like pixel data.</param>
        /// <param name="largeObjectSize">Custom limit of what are large values and what are not. If 0 is passend, then the default of 64k is used.</param>
        /// <returns>Awaitable <see cref="DicomDirectory"/> instance.</returns>
        public new static async Task<DicomDirectory> OpenAsync(Stream stream, Encoding fallbackEncoding, Func<ParseState, bool> stop = null, FileReadOption readOption = FileReadOption.Default, int largeObjectSize = 0)
        {
            if (fallbackEncoding == null)
            {
                throw new ArgumentNullException(nameof(fallbackEncoding));
            }
            var df = new DicomDirectory();

            try
            {
                var source = StreamByteSourceFactory.Create(stream, readOption, largeObjectSize: largeObjectSize);

                using var unvalidatedScop = new UnvalidatedScope(df.Dataset);
                var reader = new DicomFileReader();
                var dirObserver = new DicomDirectoryReaderObserver(df.Dataset);

                var result =
                    await
                    reader.ReadAsync(
                        source,
                        new DicomDatasetReaderObserver(df.FileMetaInfo),
                        new DicomReaderMultiObserver(
                        new DicomDatasetReaderObserver(df.Dataset, fallbackEncoding),
                        dirObserver),
                        stop).ConfigureAwait(false);

                df = FinalizeDicomDirectoryLoad(df, reader, dirObserver, result);

                return df;
            }
            catch (Exception e)
            {
                throw new DicomFileException(df, e.Message, e);
            }
        }

        /// <summary>
        /// Method to call before performing the actual saving.
        /// </summary>
        protected override void OnSave()
        {
            if (RootDirectoryRecord == null)
            {
                throw new InvalidOperationException("No DICOM files added, cannot save DICOM directory");
            }

            _directoryRecordSequence.Items.Clear();
            var calculator = new DicomWriteLengthCalculator(FileMetaInfo.TransferSyntax, DicomWriteOptions.Default);

            //Add the offset for the Directory Record sequence tag itself
            if (FileMetaInfo.TransferSyntax.IsExplicitVR)
            {
                _fileOffset = 128 + calculator.Calculate(FileMetaInfo) + calculator.Calculate(Dataset);
                _fileOffset += 2; // vr
                _fileOffset += 2; // padding
                _fileOffset += 4; // length
            }
            else
            {
                _fileOffset = 128 + 4 + calculator.Calculate(FileMetaInfo) + calculator.Calculate(Dataset);

                _fileOffset += 4; //sequence element tag
                _fileOffset += 4; //length
            }

            AddDirectoryRecordsToSequenceItem(RootDirectoryRecord);

            if (RootDirectoryRecord != null)
            {
                CalculateOffsets(calculator);

                SetOffsets(RootDirectoryRecord);

                Dataset.AddOrUpdate(
                    DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity,
                    RootDirectoryRecord.Offset);

                var lastRoot = RootDirectoryRecord;

                while (lastRoot.NextDirectoryRecord != null)
                {
                    lastRoot = lastRoot.NextDirectoryRecord;
                }

                Dataset.AddOrUpdate(DicomTag.OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity, lastRoot.Offset);
            }
            else
            {
                Dataset.AddOrUpdate(DicomTag.OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity, 0U);
                Dataset.AddOrUpdate(DicomTag.OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity, 0U);
            }
        }

        #endregion

        #region Calculation Methods

        private void CalculateOffsets(DicomWriteLengthCalculator calculator)
        {
            foreach (var item in Dataset.GetDicomItem<DicomSequence>(DicomTag.DirectoryRecordSequence))
            {
                if (!(item is DicomDirectoryRecord record))
                {
                    throw new InvalidOperationException("Unexpected type for directory record: " + item.GetType());
                }

                record.Offset = _fileOffset;

                _fileOffset += 4 + 4; //Sequence item tag

                _fileOffset += calculator.Calculate(record);

                _fileOffset += 4 + 4; // Sequence Item Delimitation Item
            }

            _fileOffset += 4 + 4; // Sequence Delimitation Item
        }

        private void SetOffsets(DicomDirectoryRecord record)
        {
            if (record.NextDirectoryRecord != null)
            {
                record.AddOrUpdate(DicomTag.OffsetOfTheNextDirectoryRecord, record.NextDirectoryRecord.Offset);
                SetOffsets(record.NextDirectoryRecord);
            }
            else
            {
                record.AddOrUpdate(DicomTag.OffsetOfTheNextDirectoryRecord, 0U);
            }

            if (record.LowerLevelDirectoryRecord != null)
            {
                record.AddOrUpdate(
                    DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity,
                    record.LowerLevelDirectoryRecord.Offset);
                SetOffsets(record.LowerLevelDirectoryRecord);
            }
            else
            {
                record.AddOrUpdate(DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity, 0U);
            }
        }

        #endregion

        #region File system creator Methods

        /// <summary>
        /// Add new file to DICOM directory.
        /// </summary>
        /// <param name="dicomFile">DICOM file to add.</param>
        /// <param name="referencedFileId">Referenced file ID.</param>
        public DicomDirectoryEntry AddFile(DicomFile dicomFile, string referencedFileId = "")
        {
            if (dicomFile == null)
            {
                throw new ArgumentNullException(nameof(dicomFile));
            }

            return AddNewRecord(dicomFile.FileMetaInfo, dicomFile.Dataset, referencedFileId);
        }

        private DicomDirectoryEntry AddNewRecord(DicomFileMetaInformation metaFileInfo, DicomDataset dataset, string referencedFileId)
        {
            var patientRecord = CreatePatientRecord(dataset);
            var studyRecord = CreateStudyRecord(dataset, patientRecord);
            var seriesRecord = CreateSeriesRecord(dataset, studyRecord);
            var imageRecord = CreateImageRecord(metaFileInfo, dataset, seriesRecord, referencedFileId);
            return new DicomDirectoryEntry
            {
                PatientRecord = patientRecord,
                StudyRecord = studyRecord,
                SeriesRecord = seriesRecord,
                InstanceRecord = imageRecord
            };
        }

        private DicomDirectoryRecord CreateImageRecord(
            DicomFileMetaInformation metaFileInfo,
            DicomDataset dataset,
            DicomDirectoryRecord seriesRecord,
            string referencedFileId)
        {
            var currentImage = seriesRecord.LowerLevelDirectoryRecord;
            var imageInstanceUid = dataset.GetSingleValue<string>(DicomTag.SOPInstanceUID);

            while (currentImage != null)
            {
                if (currentImage.GetSingleValue<string>(DicomTag.ReferencedSOPInstanceUIDInFile) == imageInstanceUid)
                {
                    return currentImage;
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

            DicomDirectoryRecord newImage;
            if (metaFileInfo.MediaStorageSOPClassUID.StorageCategory == DicomStorageCategory.StructuredReport)
            {
                newImage = CreateRecordSequenceItem(DicomDirectoryRecordType.Report, dataset);
            }
            else if (metaFileInfo.MediaStorageSOPClassUID.StorageCategory == DicomStorageCategory.PresentationState)
            {
                newImage = CreateRecordSequenceItem(DicomDirectoryRecordType.PresentationState, dataset);
            }
            else
            {
                newImage = CreateRecordSequenceItem(DicomDirectoryRecordType.Image, dataset);
            }

            newImage.AddOrUpdate(DicomTag.ReferencedFileID, referencedFileId);
            using var unvalidated = new UnvalidatedScope(newImage);
            newImage.AddOrUpdate(DicomTag.ReferencedSOPClassUIDInFile, metaFileInfo.MediaStorageSOPClassUID.UID);
            newImage.AddOrUpdate(DicomTag.ReferencedSOPInstanceUIDInFile, metaFileInfo.MediaStorageSOPInstanceUID.UID);
            newImage.AddOrUpdate(DicomTag.ReferencedTransferSyntaxUIDInFile, metaFileInfo.TransferSyntax.UID);

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

            return newImage;
        }

        private DicomDirectoryRecord CreateSeriesRecord(DicomDataset dataset, DicomDirectoryRecord studyRecord)
        {
            var currentSeries = studyRecord.LowerLevelDirectoryRecord;
            var seriesInstanceUid = dataset.GetSingleValue<string>(DicomTag.SeriesInstanceUID);

            while (currentSeries != null)
            {
                if (currentSeries.GetSingleValue<string>(DicomTag.SeriesInstanceUID) == seriesInstanceUid)
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

            var newSeries = CreateRecordSequenceItem(DicomDirectoryRecordType.Series, dataset);
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

        private DicomDirectoryRecord CreateStudyRecord(DicomDataset dataset, DicomDirectoryRecord patientRecord)
        {
            var currentStudy = patientRecord.LowerLevelDirectoryRecord;
            var studyInstanceUid = dataset.GetSingleValue<string>(DicomTag.StudyInstanceUID);

            while (currentStudy != null)
            {
                if (currentStudy.GetSingleValue<string>(DicomTag.StudyInstanceUID) == studyInstanceUid)
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
            var newStudy = CreateRecordSequenceItem(DicomDirectoryRecordType.Study, dataset);
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

        private DicomDirectoryRecord CreatePatientRecord(DicomDataset dataset)
        {
            var patientId = dataset.GetSingleValueOrDefault(DicomTag.PatientID, string.Empty);
            var patientName = dataset.GetDicomItem<DicomPersonName>(DicomTag.PatientName);

            var currentPatient = RootDirectoryRecord;

            while (currentPatient != null)
            {
                var currPatId = currentPatient.GetSingleValueOrDefault(DicomTag.PatientID, string.Empty);
                var currPatName = currentPatient.GetDicomItem<DicomPersonName>(DicomTag.PatientName);

                if (currPatId == patientId && DicomPersonName.HaveSameContent(currPatName, patientName))
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

            var newPatient = CreateRecordSequenceItem(DicomDirectoryRecordType.Patient, dataset);
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


        private DicomDirectoryRecord CreateRecordSequenceItem(DicomDirectoryRecordType recordType, DicomDataset dataset)
        {
            if (recordType == null) throw new ArgumentNullException(nameof(recordType));
            if (dataset == null) throw new ArgumentNullException(nameof(dataset));

            var sequenceItem = new DicomDirectoryRecord(ValidateItems)
            {
                //add record item attributes
                { DicomTag.OffsetOfTheNextDirectoryRecord, 0U },
                { DicomTag.RecordInUseFlag, (ushort)0xFFFF },
                { DicomTag.OffsetOfReferencedLowerLevelDirectoryEntity, 0U },
                { DicomTag.DirectoryRecordType, recordType.ToString() },

                //copy the current dataset character set
                dataset.FirstOrDefault(d => d.Tag == DicomTag.SpecificCharacterSet)
            };

            using var unvalidated = new UnvalidatedScope(sequenceItem);
            foreach (var tag in recordType.Tags)
            {
                if (dataset.Contains(tag))
                {
                    sequenceItem.Add(dataset.GetDicomItem<DicomItem>(tag));
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Cannot find tag {tag} for record type {recordType}");
                }
            }

            return sequenceItem;
        }

        private static DicomDirectory FinalizeDicomDirectoryLoad(
            DicomDirectory df,
            DicomFileReader reader,
            DicomDirectoryReaderObserver dirObserver,
            DicomReaderResult result)
        {
            HandleOpenError(df, result);
            
            df.IsPartial = result == DicomReaderResult.Stopped || result == DicomReaderResult.Suspended;
            df.Format = reader.FileFormat;
            df.Dataset.InternalTransferSyntax = reader.Syntax;
            df._directoryRecordSequence = df.Dataset.GetDicomItem<DicomSequence>(DicomTag.DirectoryRecordSequence);
            df.RootDirectoryRecord = dirObserver.BuildDirectoryRecords();

            return df;
        }

        private void AddDirectoryRecordsToSequenceItem(DicomDirectoryRecord recordItem)
        {
            if (recordItem == null)
            {
                return;
            }

            _directoryRecordSequence.Items.Add(recordItem);
            if (recordItem.LowerLevelDirectoryRecord != null)
            {
                AddDirectoryRecordsToSequenceItem(recordItem.LowerLevelDirectoryRecord);
            }

            if (recordItem.NextDirectoryRecord != null)
            {
                AddDirectoryRecordsToSequenceItem(recordItem.NextDirectoryRecord);
            }
        }

        #endregion
    }
}
