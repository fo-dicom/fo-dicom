// Copyright (c) 2012-2022 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using System;
using System.Threading.Tasks;

namespace FellowOakDicom.Dump
{

    internal class DumpWalker : IDicomDatasetWalker
    {
        private int _level = 0;

        private readonly Action<string, string, string, string> _addAction;

        public DumpWalker(Action<string, string, string, string> addAction)
        {
            _addAction = addAction;
            Level = 0;
        }

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                Indent = string.Empty.PadLeft(4 * _level);
            }
        }

        private string Indent { get; set; }

        public void OnBeginWalk()
        { /* nothing to do here */ }

        public bool OnElement(DicomElement element)
        {
            var tag = string.Format(
                "{0}{1}  {2}",
                Indent,
                element.Tag.ToString().ToUpperInvariant(),
                element.Tag.DictionaryEntry.Name);

            string value = "<large value not displayed>";
            if (element.Length <= 2048)
            {
                value = string.Join("\\", element.Get<string[]>());
            }

            if (element.ValueRepresentation == DicomVR.UI && element.Count > 0)
            {
                var uid = element.Get<DicomUID>(0);
                var name = uid.Name;
                if (name != "Unknown")
                {
                    value = $"{value} ({name})";
                }
            }

            _addAction(tag, element.ValueRepresentation.Code, element.Length.ToString(), value);
            return true;
        }

        public Task<bool> OnElementAsync(DicomElement element)
        {
            return Task.FromResult(OnElement(element));
        }

        public bool OnBeginSequence(DicomSequence sequence)
        {
            var tag = string.Format(
                "{0}{1}  {2}",
                Indent,
                sequence.Tag.ToString().ToUpperInvariant(),
                sequence.Tag.DictionaryEntry.Name);

            _addAction(tag, "SQ", string.Empty, string.Empty);

            Level++;
            return true;
        }

        public bool OnBeginSequenceItem(DicomDataset dataset)
        {
            var tag = $"{Indent}Sequence Item:";

            _addAction(tag, string.Empty, string.Empty, string.Empty);

            Level++;
            return true;
        }

        public bool OnEndSequenceItem()
            => ReduceLevel();

        public bool OnEndSequence()
            => ReduceLevel();

        private bool ReduceLevel()
        {
            Level--;
            return true;
        }

        public bool OnBeginFragment(DicomFragmentSequence fragment)
        {
            var tag = string.Format(
                "{0}{1}  {2}",
                Indent,
                fragment.Tag.ToString().ToUpperInvariant(),
                fragment.Tag.DictionaryEntry.Name);

            _addAction(tag, fragment.ValueRepresentation.Code, string.Empty, string.Empty);

            Level++;
            return true;
        }

        public bool OnFragmentItem(IByteBuffer item)
        {
            if (item != null)
            {
                var tag = $"{Indent}Fragment";

                _addAction(tag, string.Empty, item.Size.ToString(), string.Empty);
            }
            return true;
        }

        public Task<bool> OnFragmentItemAsync(IByteBuffer item)
        {
            return Task.FromResult(OnFragmentItem(item));
        }

        public bool OnEndFragment()
            => ReduceLevel();

        public void OnEndWalk()
        { /* nothing to do here */ }

    }
}
