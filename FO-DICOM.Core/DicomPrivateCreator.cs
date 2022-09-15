// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Runtime.Serialization;

namespace FellowOakDicom
{

    [DataContract]
    public sealed class DicomPrivateCreator : IEquatable<DicomPrivateCreator>,
                                              IComparable<DicomPrivateCreator>,
                                              IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DicomPrivateCreator"/> class.
        /// </summary>
        /// <param name="creator">The textual value of the private creator</param>
        public DicomPrivateCreator(string creator)
        {
            Creator = creator;
        }

        [DataMember]
        public string Creator { get; private set; }

        /// <summary>
        /// Check if the given DicomItem is valid
        /// https://dicom.nema.org/dicom/2013/output/chtml/part05/sect_7.8.html private creator data item should be VR =LO and VM = 1
        /// /// </summary>
        /// <param name="dataElement"></param>
        /// <returns></returns>
        public static bool IsValid(DicomItem dataElement)
        {
            // if DicomPrivateCreatoe was also a DicomItem we could have just overrided Validate method.
            // making a simple change, while still moving the logic inside the class
            return dataElement is DicomLongString element
                   && element.Count == 1;
        }

        public int CompareTo(DicomPrivateCreator other)
        {
            return Creator.CompareTo(other.Creator);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (!(obj is DicomPrivateCreator)) throw new ArgumentException("Passed non-DicomPrivateCreator to comparer", nameof(obj));
            return CompareTo(obj as DicomPrivateCreator);
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, null)) return false;
            if (Object.ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals(obj as DicomPrivateCreator);
        }

        public bool Equals(DicomPrivateCreator other)
        {
            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return Creator.GetHashCode();
        }

        public override string ToString()
        {
            return Creator;
        }
    }
}
