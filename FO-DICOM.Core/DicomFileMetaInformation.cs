// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using Microsoft.Extensions.DependencyInjection;

namespace FellowOakDicom
{

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
            // per default, we do turn off validation as most of the use cases are
            // creation of the meta-info from files or streams etc.
            ValidateItems = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomFileMetaInformation"/> class.
        /// </summary>
        /// <param name="dataset">
        /// The data set for which file meta information is required.
        /// </param>
        public DicomFileMetaInformation(DicomDataset dataset)
        {
            ValidateItems = dataset.ValidateItems;
            Version = new byte[] { 0x00, 0x01 };

            MediaStorageSOPClassUID = dataset.GetSingleValue<DicomUID>(DicomTag.SOPClassUID);
            MediaStorageSOPInstanceUID = dataset.GetSingleValue<DicomUID>(DicomTag.SOPInstanceUID);
            TransferSyntax = dataset.InternalTransferSyntax;

            ImplementationClassUID = DicomImplementation.ClassUID;
            ImplementationVersionName = DicomImplementation.Version;

            dataset.TryGetString(DicomTag.SourceApplicationEntityTitle, out var aet);
            if (aet != null)
            {
                SourceApplicationEntityTitle = aet;
            }

            if (dataset.TryGetSingleValue(DicomTag.SendingApplicationEntityTitle, out string sendingAETVal))
            {
                SendingApplicationEntityTitle = sendingAETVal;
            }
            if (dataset.TryGetSingleValue(DicomTag.ReceivingApplicationEntityTitle, out string receivingAETVal))
            {
                ReceivingApplicationEntityTitle = receivingAETVal;
            }
            if (dataset.TryGetSingleValue(DicomTag.PrivateInformationCreatorUID, out DicomUID privInfoCreator))
            {
                PrivateInformationCreatorUID = privInfoCreator;
            }
            if (dataset.TryGetValues(DicomTag.PrivateInformation, out byte[] privInfo))
            {
                PrivateInformation = privInfo;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomFileMetaInformation"/> class.
        /// </summary>
        /// <param name="metaInfo">DICOM file meta information to be updated.</param>
        public DicomFileMetaInformation(DicomFileMetaInformation metaInfo)
        {
            ValidateItems = metaInfo.ValidateItems;
            Version = new byte[] { 0x00, 0x01 };

            if (metaInfo.Contains(DicomTag.MediaStorageSOPClassUID)) MediaStorageSOPClassUID = metaInfo.MediaStorageSOPClassUID;
            if (metaInfo.Contains(DicomTag.MediaStorageSOPInstanceUID)) MediaStorageSOPInstanceUID = metaInfo.MediaStorageSOPInstanceUID;
            if (metaInfo.Contains(DicomTag.TransferSyntaxUID)) TransferSyntax = metaInfo.TransferSyntax;

            ImplementationClassUID = DicomImplementation.ClassUID;
            ImplementationVersionName = DicomImplementation.Version;

            var aet = metaInfo.Contains(DicomTag.SourceApplicationEntityTitle) ?
                metaInfo.SourceApplicationEntityTitle : null;
            if (aet != null) SourceApplicationEntityTitle = aet;

            if (metaInfo.Contains(DicomTag.SendingApplicationEntityTitle))
                SendingApplicationEntityTitle = metaInfo.SendingApplicationEntityTitle;
            if (metaInfo.Contains(DicomTag.ReceivingApplicationEntityTitle))
                ReceivingApplicationEntityTitle = metaInfo.ReceivingApplicationEntityTitle;
            if (metaInfo.Contains(DicomTag.PrivateInformationCreatorUID))
                PrivateInformationCreatorUID = metaInfo.PrivateInformationCreatorUID;
            if (metaInfo.Contains(DicomTag.PrivateInformation))
                PrivateInformation = metaInfo.PrivateInformation;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the file meta information version.
        /// </summary>
        public byte[] Version
        {
            get => GetValues<byte>(DicomTag.FileMetaInformationVersion);
            set => AddOrUpdate(DicomTag.FileMetaInformationVersion, value);
        }

        /// <summary>
        /// Gets or sets the Media Storage SOP Class UID.
        /// </summary>
        public DicomUID MediaStorageSOPClassUID
        {
            get => GetSingleValue<DicomUID>(DicomTag.MediaStorageSOPClassUID);
            set => AddOrUpdate(DicomTag.MediaStorageSOPClassUID, value);
        }

        /// <summary>
        /// Gets or sets the Media Storage SOP Instance UID.
        /// </summary>
        public DicomUID MediaStorageSOPInstanceUID
        {
            get => GetSingleValue<DicomUID>(DicomTag.MediaStorageSOPInstanceUID);
            set => AddOrUpdate(DicomTag.MediaStorageSOPInstanceUID, value);
        }

        /// <summary>
        /// Gets or sets the DICOM Part 10 dataset transfer syntax.
        /// </summary>
        public DicomTransferSyntax TransferSyntax
        {
            get => GetSingleValue<DicomTransferSyntax>(DicomTag.TransferSyntaxUID);
            set => AddOrUpdate(DicomTag.TransferSyntaxUID, value.UID);
        }

        /// <summary>
        /// Gets or sets the Implementation Class UID.
        /// </summary>
        public DicomUID ImplementationClassUID
        {
            get => GetSingleValue<DicomUID>(DicomTag.ImplementationClassUID);
            set => AddOrUpdate(DicomTag.ImplementationClassUID, value);
        }

        /// <summary>
        /// Gets or sets the Implementation Version Name.
        /// </summary>
        public string ImplementationVersionName
        {
            get => GetSingleValueOrDefault<string>(DicomTag.ImplementationVersionName, null);
            set => AddOrUpdate(DicomTag.ImplementationVersionName, value);
        }

        /// <summary>
        /// Gets or sets the Source Application Entity Title.
        /// </summary>
        public string SourceApplicationEntityTitle
        {
            get => GetSingleValueOrDefault<string>(DicomTag.SourceApplicationEntityTitle, null);
            set => AddOrUpdate(DicomTag.SourceApplicationEntityTitle, value);
        }

        /// <summary>
        /// Gets or sets the Sending Application Entity Title.
        /// </summary>
        public string SendingApplicationEntityTitle
        {
            get => GetSingleValueOrDefault<string>(DicomTag.SendingApplicationEntityTitle, null);
            set => AddOrUpdate(DicomTag.SendingApplicationEntityTitle, value);
        }

        /// <summary>
        /// Gets or sets the Receiving Application Entity Title (optional attribute).
        /// </summary>
        public string ReceivingApplicationEntityTitle
        {
            get => GetSingleValueOrDefault<string>(DicomTag.ReceivingApplicationEntityTitle, null);
            set => AddOrUpdate(DicomTag.ReceivingApplicationEntityTitle, value);
        }

        /// <summary>
        /// Gets or sets the Private Information Creator UID (optional attribute).
        /// </summary>
        public DicomUID PrivateInformationCreatorUID
        {
            get => GetSingleValueOrDefault<DicomUID>(DicomTag.PrivateInformationCreatorUID, null);
            set => AddOrUpdate(DicomTag.PrivateInformationCreatorUID, value);
        }

        /// <summary>
        /// Gets or sets the private information associated with <see cref="PrivateInformationCreatorUID"/>.
        /// Required if <see cref="PrivateInformationCreatorUID"/> is defined.
        /// </summary>
        public byte[] PrivateInformation
        {
            get => TryGetValues(DicomTag.PrivateInformation, out byte[] dummy) ? dummy : null;
            set => AddOrUpdate(DicomTag.PrivateInformation, value);
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
        public static string CreateSourceApplicationEntityTitle()
        {
            var machine = Setup.ServiceProvider.GetRequiredService<INetworkManager>().MachineName;
            if (machine != null && machine.Length > 16)
            {
                machine = machine.Substring(0, 16);
            }

            return machine;
        }

        protected override void ValidateTag(DicomTag tag)
        {
            if (tag.Group != 2)
            {
                throw new DicomDataException($"Tag with group ID {tag.Group} is not allowed in meta information.");
            }
        }

        #endregion
    }
}
