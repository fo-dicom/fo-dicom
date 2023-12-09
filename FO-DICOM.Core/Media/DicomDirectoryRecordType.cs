// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;

namespace FellowOakDicom.Media
{

    public class DicomDirectoryRecordType
    {

        #region Properties and Attributes

        private readonly string _recordName;

        public ICollection<DicomTag> Tags { get; } = new HashSet<DicomTag>();

        public static readonly DicomDirectoryRecordType Patient = new DicomDirectoryRecordType("PATIENT");

        public static readonly DicomDirectoryRecordType Study = new DicomDirectoryRecordType("STUDY");

        public static readonly DicomDirectoryRecordType Series = new DicomDirectoryRecordType("SERIES");

        public static readonly DicomDirectoryRecordType Image = new DicomDirectoryRecordType("IMAGE");

        public static readonly DicomDirectoryRecordType Report = new DicomDirectoryRecordType("SR DOCUMENT");

        public static readonly DicomDirectoryRecordType PresentationState = new DicomDirectoryRecordType("PRESENTATION");

        #endregion

        #region Initialization

        public DicomDirectoryRecordType(string recordName)
        {
            _recordName = recordName;

            switch (recordName)
            {
                case "PATIENT":
                    Tags.Add(DicomTag.PatientID);
                    Tags.Add(DicomTag.PatientName);
                    Tags.Add(DicomTag.PatientBirthDate);
                    Tags.Add(DicomTag.PatientSex);
                    break;
                case "STUDY":
                    Tags.Add(DicomTag.StudyInstanceUID);
                    Tags.Add(DicomTag.StudyID);
                    Tags.Add(DicomTag.StudyDate);
                    Tags.Add(DicomTag.StudyTime);
                    Tags.Add(DicomTag.AccessionNumber);
                    Tags.Add(DicomTag.StudyDescription);
                    break;
                case "SERIES":
                    Tags.Add(DicomTag.SeriesInstanceUID);
                    Tags.Add(DicomTag.Modality);
                    Tags.Add(DicomTag.SeriesDate);
                    Tags.Add(DicomTag.SeriesTime);
                    Tags.Add(DicomTag.SeriesNumber);
                    Tags.Add(DicomTag.SeriesDescription);
                    break;
                case "IMAGE":
                    Tags.Add(DicomTag.InstanceNumber);
                    break;
                case "SR DOCUMENT":
                    Tags.Add(DicomTag.InstanceNumber);
                    Tags.Add(DicomTag.CompletionFlag);
                    Tags.Add(DicomTag.VerificationFlag);
                    Tags.Add(DicomTag.ContentDate);
                    Tags.Add(DicomTag.ContentTime);
                    Tags.Add(DicomTag.VerificationDateTime);
                    Tags.Add(DicomTag.ConceptNameCodeSequence);
                    break;
                case "PRESENTATION":
                    Tags.Add(DicomTag.InstanceNumber);
                    Tags.Add(DicomTag.PresentationCreationDate);
                    Tags.Add(DicomTag.PresentationCreationTime);
                    Tags.Add(DicomTag.ReferencedSeriesSequence);
                    break;
                default:
                    break;
            }
        }

        #endregion

        public override string ToString()
        {
            return _recordName;
        }
    }
}
