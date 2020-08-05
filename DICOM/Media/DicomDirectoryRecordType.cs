// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;

namespace Dicom.Media
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
