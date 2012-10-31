using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom {
#if NETFX_CORE
    using System.Runtime.Serialization;
    [DataContract]
#endif
	public sealed class DicomPrivateCreator : IEquatable<DicomPrivateCreator>, IComparable<DicomPrivateCreator>, IComparable {
		internal DicomPrivateCreator(string creator) {
			this.Creator = creator;
		}

#if NETFX_CORE
        [DataMember]
#endif
		public string Creator {
			get;
			private set;
		}

		public int CompareTo(DicomPrivateCreator other) {
			return Creator.CompareTo(other.Creator);
		}

		public int CompareTo(object obj) {
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (!(obj is DicomPrivateCreator))
				throw new ArgumentException("Passed non-DicomPrivateCreator to comparer", "obj");
			return CompareTo(obj as DicomPrivateCreator);
		}

		public override bool Equals(object obj) {
			if (Object.ReferenceEquals(obj, null))
				return false;
			if (Object.ReferenceEquals(this, obj))
				return true;
			if (this.GetType() != obj.GetType())
				return false;
			return Equals(obj as DicomPrivateCreator);
		}

		public bool Equals(DicomPrivateCreator other) {
			return CompareTo(other) == 0;
		}

		public override int GetHashCode() {
			return Creator.GetHashCode();
		}
	}
}
