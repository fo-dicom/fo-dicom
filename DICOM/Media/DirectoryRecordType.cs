using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Media
{
    public class DirectoryRecordType
    {
        #region Properties and Attributes
        private readonly string _recordName;
        private readonly ICollection<DicomTag> _tags = new HashSet<DicomTag>();

        public ICollection<DicomTag> Tags { get { return _tags; } }

        public static DirectoryRecordType Patient = new DirectoryRecordType("PATIENT");
        public static DirectoryRecordType Study = new DirectoryRecordType("STUDY");
        public static DirectoryRecordType Series = new DirectoryRecordType("SERIES");
        public static DirectoryRecordType Image = new DirectoryRecordType("IMAGE");

        #endregion

        #region Initialization
        public DirectoryRecordType(string recordName)
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
