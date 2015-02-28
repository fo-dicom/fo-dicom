using System;
using System.Collections.Generic;
using Dicom;
using Dicom.IO.Buffer;

namespace Store.DICOM.Dump
{
	internal class DicomDumpWalker : IDicomDatasetWalker
	{
		#region FIELDS

		private int _level;

		#endregion

		#region CONSTRUCTORS

		public DicomDumpWalker(ICollection<DicomTextItem> textItems)
		{
			TextItems = textItems;
			_level = 0;
		}
		
		#endregion

		#region PROPERTIES

		public ICollection<DicomTextItem> TextItems { get; private set; }

		#endregion

		#region METHODS

		public void OnBeginWalk(DicomDatasetWalker walker, DicomDatasetWalkerCallback callback)
		{
		}

		public bool OnElement(DicomElement element)
		{
			var tag = String.Format("{0}  {1}", element.Tag.ToString().ToUpper(), element.Tag.DictionaryEntry.Name);

			var value = element.Length <= 2048 ? String.Join("\\", element.Get<string[]>()) : "<large value not displayed>";

			if (element.ValueRepresentation == DicomVR.UI && element.Count > 0)
			{
				var uid = element.Get<DicomUID>(0);
				var name = uid.Name;
				if (name != "Unknown")
					value = String.Format("{0} ({1})", value, name);
			}

			TextItems.Add(new DicomTextItem(_level, tag, element.ValueRepresentation.Code, element.Length, value));
			return true;
		}

		public bool OnBeginSequence(DicomSequence sequence)
		{
			var tag = String.Format("{0}  {1}", sequence.Tag.ToString().ToUpper(), sequence.Tag.DictionaryEntry.Name);

			TextItems.Add(new DicomTextItem(_level++, tag, "SQ"));
			return true;
		}

		public bool OnBeginSequenceItem(DicomDataset dataset)
		{
			TextItems.Add(new DicomTextItem(_level++, "Sequence Item:"));
			return true;
		}

		public bool OnEndSequenceItem()
		{
			--_level;
			return true;
		}

		public bool OnEndSequence()
		{
			--_level;
			return true;
		}

		public bool OnBeginFragment(DicomFragmentSequence fragment)
		{
			var tag = String.Format("{0}  {1}", fragment.Tag.ToString().ToUpper(), fragment.Tag.DictionaryEntry.Name);

			TextItems.Add(new DicomTextItem(_level++, tag, fragment.ValueRepresentation.Code));
			return true;
		}

		public bool OnFragmentItem(IByteBuffer item)
		{
			TextItems.Add(new DicomTextItem(_level, "Fragment", String.Empty, item.Size));
			return true;
		}

		public bool OnEndFragment()
		{
			--_level;
			return true;
		}

		public void OnEndWalk()
		{
		}
		
		#endregion
	}
}