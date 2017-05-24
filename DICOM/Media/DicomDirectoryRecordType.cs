// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;

namespace Dicom.Media
{
    public class DicomDirectoryRecordType
    {
        #region Properties and Attributes

        private readonly string _recordName;

        private readonly ICollection<DicomTag> _tags = new HashSet<DicomTag>();

        public ICollection<DicomTag> Tags
        {
            get
            {
                return _tags;
            }
        }

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
                    _tags.Add(DicomTag.PatientID);
                    _tags.Add(DicomTag.PatientName);
                    _tags.Add(DicomTag.PatientBirthDate);
                    _tags.Add(DicomTag.PatientSex);
                    break;
                case "STUDY":
                    _tags.Add(DicomTag.StudyInstanceUID);
                    _tags.Add(DicomTag.StudyID);
                    _tags.Add(DicomTag.StudyDate);
                    _tags.Add(DicomTag.StudyTime);
                    _tags.Add(DicomTag.AccessionNumber);
                    _tags.Add(DicomTag.StudyDescription);
                    break;
                case "SERIES":
                    _tags.Add(DicomTag.SeriesInstanceUID);
                    _tags.Add(DicomTag.Modality);
                    _tags.Add(DicomTag.SeriesDate);
                    _tags.Add(DicomTag.SeriesTime);
                    _tags.Add(DicomTag.SeriesNumber);
                    _tags.Add(DicomTag.SeriesDescription);
                    break;
                case "IMAGE":
                    _tags.Add(DicomTag.InstanceNumber);
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