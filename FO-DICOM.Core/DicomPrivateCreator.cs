// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

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
