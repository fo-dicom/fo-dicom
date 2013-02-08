using System;
using System.Linq;

namespace Store.DICOM.Dump
{
	public class DicomTextItem
	{
		private readonly string _tag;
		private readonly uint _length;

		#region FIELDS

		#endregion

		#region CONSTRUCTORS

		public DicomTextItem(int level, string tag, string vr = null, uint length = 0, string value = null)
		{
			if (tag == null) throw new ArgumentNullException("tag");
			Level = level;
			_tag = tag;
			ValueRepresentation = vr ?? String.Empty;
			_length = length;
			Value = value ?? String.Empty;
		}

		#endregion

		#region PROPERTIES

		public int Level { get; private set; }
		public string ValueRepresentation { get; private set; }
		public string Value { get; private set; }

		public string Tag
		{
			get { return String.Concat(Enumerable.Repeat("    ", Level)) + _tag; }
		}

		public string Length
		{
			get { return _length > 0 ? _length.ToString() : String.Empty; }
		}

		#endregion
	}
}