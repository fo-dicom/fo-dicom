// Copyright (c) 2012-2016 fo-dicom contributors.
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
            Version = new byte[] { 0x00, 0x01 };
            ImplementationClassUID = DicomImplementation.ClassUID;
            ImplementationVersionName = DicomImplementation.Version;

            var machine = NetworkManager.MachineName;
            if (machine.Length > 16) machine = machine.Substring(0, 16);
            SourceApplicationEntityTitle = machine;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomFileMetaInformation"/> class.
        /// </summary>
        /// <param name="dataset">
        /// The metaInfo for which file meta information is required.
        /// </param>
        public DicomFileMetaInformation(DicomDataset dataset)
            : this()
        {
            MediaStorageSOPClassUID = dataset.Get<DicomUID>(DicomTag.SOPClassUID);
            MediaStorageSOPInstanceUID = dataset.Get<DicomUID>(DicomTag.SOPInstanceUID);
            TransferSyntax = dataset.InternalTransferSyntax;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomFileMetaInformation"/> class.
        /// </summary>
        /// <param name="metaInfo">
        /// DICOM file meta information to be copied.
        /// </param>
        public DicomFileMetaInformation(DicomFileMetaInformation metaInfo)
            : this()
        {
            if (metaInfo.Contains(DicomTag.FileMetaInformationVersion)) this.Version = metaInfo.Version;
            if (metaInfo.Contains(DicomTag.MediaStorageSOPClassUID)) this.MediaStorageSOPClassUID = metaInfo.MediaStorageSOPClassUID;
            if (metaInfo.Contains(DicomTag.MediaStorageSOPInstanceUID)) this.MediaStorageSOPInstanceUID = metaInfo.MediaStorageSOPInstanceUID;
            if (metaInfo.Contains(DicomTag.TransferSyntaxUID)) this.TransferSyntax = metaInfo.TransferSyntax;
            if (metaInfo.Contains(DicomTag.ImplementationClassUID)) this.ImplementationClassUID = metaInfo.ImplementationClassUID;
            if (metaInfo.Contains(DicomTag.ImplementationVersionName)) this.ImplementationVersionName = metaInfo.ImplementationVersionName;
            if (metaInfo.Contains(DicomTag.SourceApplicationEntityTitle)) this.SourceApplicationEntityTitle = metaInfo.SourceApplicationEntityTitle;
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
                Add(DicomTag.FileMetaInformationVersion, value);
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
                Add(DicomTag.MediaStorageSOPClassUID, value);
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
                Add(DicomTag.MediaStorageSOPInstanceUID, value);
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
                Add(DicomTag.TransferSyntaxUID, value.UID);
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
                Add(DicomTag.ImplementationClassUID, value);
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
                Add(DicomTag.ImplementationVersionName, value);
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
                Add(DicomTag.SourceApplicationEntityTitle, value);
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

        #endregion
    }
}
