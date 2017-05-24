// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using Dicom.Network;

    /// <summary>
    /// Representation of the file meta information in a DICOM Part 10 file.
    /// </summary>
    public class DicomFileMetaInformation : DicomDataset
    {
        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomFileMetaInformation"/> class.
        /// </summary>
        public DicomFileMetaInformation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomFileMetaInformation"/> class.
        /// </summary>
        /// <param name="dataset">
        /// The data set for which file meta information is required.
        /// </param>
        public DicomFileMetaInformation(DicomDataset dataset)
        {
            this.Version = new byte[] { 0x00, 0x01 };

            this.MediaStorageSOPClassUID = dataset.Get<DicomUID>(DicomTag.SOPClassUID);
            this.MediaStorageSOPInstanceUID = dataset.Get<DicomUID>(DicomTag.SOPInstanceUID);
            this.TransferSyntax = dataset.InternalTransferSyntax;

            this.ImplementationClassUID = DicomImplementation.ClassUID;
            this.ImplementationVersionName = DicomImplementation.Version;

            var aet = CreateSourceApplicationEntityTitle();
            if (aet != null) this.SourceApplicationEntityTitle = aet;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomFileMetaInformation"/> class.
        /// </summary>
        /// <param name="metaInfo">DICOM file meta information to be updated.</param>
        public DicomFileMetaInformation(DicomFileMetaInformation metaInfo)
        {
            this.Version = new byte[] { 0x00, 0x01 };

            if (metaInfo.Contains(DicomTag.MediaStorageSOPClassUID)) this.MediaStorageSOPClassUID = metaInfo.MediaStorageSOPClassUID;
            if (metaInfo.Contains(DicomTag.MediaStorageSOPInstanceUID)) this.MediaStorageSOPInstanceUID = metaInfo.MediaStorageSOPInstanceUID;
            if (metaInfo.Contains(DicomTag.TransferSyntaxUID)) this.TransferSyntax = metaInfo.TransferSyntax;

            this.ImplementationClassUID = DicomImplementation.ClassUID;
            this.ImplementationVersionName = DicomImplementation.Version;

            var aet = CreateSourceApplicationEntityTitle();
            if (aet != null) this.SourceApplicationEntityTitle = aet;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the file meta information version.
        /// </summary>
        public byte[] Version
        {
            get
            {
                return Get<byte[]>(DicomTag.FileMetaInformationVersion);
            }
            set
            {
                AddOrUpdate(DicomTag.FileMetaInformationVersion, value);
            }
        }

        /// <summary>
        /// Gets or sets the Media Storage SOP Class UID.
        /// </summary>
        public DicomUID MediaStorageSOPClassUID
        {
            get
            {
                return Get<DicomUID>(DicomTag.MediaStorageSOPClassUID);
            }
            set
            {
                AddOrUpdate(DicomTag.MediaStorageSOPClassUID, value);
            }
        }

        /// <summary>
        /// Gets or sets the Media Storage SOP Instance UID.
        /// </summary>
        public DicomUID MediaStorageSOPInstanceUID
        {
            get
            {
                return Get<DicomUID>(DicomTag.MediaStorageSOPInstanceUID);
            }
            set
            {
                AddOrUpdate(DicomTag.MediaStorageSOPInstanceUID, value);
            }
        }

        /// <summary>
        /// Gets or sets the DICOM Part 10 dataset transfer syntax.
        /// </summary>
        public DicomTransferSyntax TransferSyntax
        {
            get
            {
                return Get<DicomTransferSyntax>(DicomTag.TransferSyntaxUID);
            }
            set
            {
                AddOrUpdate(DicomTag.TransferSyntaxUID, value.UID);
            }
        }

        /// <summary>
        /// Gets or sets the Implementation Class UID.
        /// </summary>
        public DicomUID ImplementationClassUID
        {
            get
            {
                return Get<DicomUID>(DicomTag.ImplementationClassUID);
            }
            set
            {
                AddOrUpdate(DicomTag.ImplementationClassUID, value);
            }
        }

        /// <summary>
        /// Gets or sets the Implementation Version Name.
        /// </summary>
        public string ImplementationVersionName
        {
            get
            {
                return Get<string>(DicomTag.ImplementationVersionName, null);
            }
            set
            {
                AddOrUpdate(DicomTag.ImplementationVersionName, value);
            }
        }

        /// <summary>
        /// Gets or sets the Source Application Entity Title.
        /// </summary>
        public string SourceApplicationEntityTitle
        {
            get
            {
                return Get<string>(DicomTag.SourceApplicationEntityTitle, null);
            }
            set
            {
                AddOrUpdate(DicomTag.SourceApplicationEntityTitle, value);
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return "DICOM File Meta Info";
        }

        /// <summary>
        /// Create a source application title from the machine name.
        /// </summary>
        /// <returns>
        /// The machine name truncated to a maximum of 16 characters.
        /// </returns>
        private static string CreateSourceApplicationEntityTitle()
        {
            var machine = NetworkManager.MachineName;
            if (machine != null && machine.Length > 16)
            {
                machine = machine.Substring(0, 16);
            }

            return machine;
        }

        #endregion
    }
}
