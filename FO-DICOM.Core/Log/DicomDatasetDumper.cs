// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using System;
using System.Text;
using System.Threading.Tasks;

namespace FellowOakDicom.Log
{

    public class DicomDatasetDumper : IDicomDatasetWalker
    {
        private readonly StringBuilder _log;

        private readonly int _width;

        private readonly int _value;

        private int _depth;

        private string _pad = string.Empty;

        public DicomDatasetDumper(StringBuilder log, int width = 128, int valueLength = 82)
        {
            _log = log;
            _width = width;
            _value = valueLength;
        }

        /// <summary>
        /// Handler for beginning the traversal.
        /// </summary>
        public void OnBeginWalk()
        {
        }

        /// <summary>
        /// Handler for traversing a DICOM element.
        /// </summary>
        /// <param name="element">Element to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public bool OnElement(DicomElement element)
        {
            var sb = new StringBuilder();
            if (_depth > 0)
            {
                sb.Append(_pad).Append("> ");
            }

            sb.Append(element.Tag);
            sb.Append(' ');
            sb.Append(element.ValueRepresentation.Code);
            sb.Append(' ');
            if (element.Length == 0)
            {
                sb.Append("(no value available)");
            }
            else if (element.ValueRepresentation.IsString)
            {
                sb.Append('[');
                string val = element.Get<string>();
                var maxLengthToWrite = (_value - 2 - sb.Length);
                if (val.Length > maxLengthToWrite && maxLengthToWrite > 0)
                {
                    sb.Append(val.Substring(0, maxLengthToWrite));
                    sb.Append(')');
                }
                else
                {
                    sb.Append(val);
                    sb.Append(']');
                }
            }
            else if (element.Length >= 1024)
            {
                sb.Append("<skipping large element>");
            }
            else
            {
                var val = string.Join("/", element.Get<string[]>());
                var maxLengthToWrite = _value - sb.Length;
                if (val.Length > maxLengthToWrite && maxLengthToWrite > 0)
                {
                    sb.Append(val.Substring(0, maxLengthToWrite));
                }
                else
                {
                    sb.Append(val);
                }
            }
            while (sb.Length < _value)
            {
                sb.Append(' ');
            }

            sb.Append('#');
            string name = element.Tag.DictionaryEntry.Keyword;
            sb.AppendFormat(
                "{0,6}, {1}",
                element.Length,
                name.Substring(0, Math.Max(0, Math.Min(_width - sb.Length - 9, name.Length))));
            _log.AppendLine(sb.ToString());
            return true;
        }

        /// <summary>
        /// Asynchronous handler for traversing a DICOM element.
        /// </summary>
        /// <param name="element">Element to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public Task<bool> OnElementAsync(DicomElement element) => Task.FromResult(OnElement(element));

        /// <summary>
        /// Handler for traversing beginning of sequence.
        /// </summary>
        /// <param name="sequence">Sequence to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public bool OnBeginSequence(DicomSequence sequence)
        {
            _log.AppendFormat(
                "{0}{1} SQ {2}",
                (_depth > 0) ? _pad + "> " : "",
                sequence.Tag,
                sequence.Tag.DictionaryEntry.Name).AppendLine();
            IncreaseDepth();
            return true;
        }

        /// <summary>
        /// Handler for traversing beginning of sequence item.
        /// </summary>
        /// <param name="dataset">Item dataset.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public bool OnBeginSequenceItem(DicomDataset dataset)
        {
            _log.AppendLine(_pad + "Item:");
            IncreaseDepth();
            return true;
        }

        /// <summary>
        /// Handler for traversing end of sequence item.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public bool OnEndSequenceItem()
        {
            DecreaseDepth();
            return true;
        }

        /// <summary>
        /// Handler for traversing end of sequence.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public bool OnEndSequence()
        {
            DecreaseDepth();
            return true;
        }

        /// <summary>
        /// Handler for traversing beginning of fragment.
        /// </summary>
        /// <param name="fragment">Fragment sequence.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public bool OnBeginFragment(DicomFragmentSequence fragment)
        {
            _log.AppendFormat(
                "{0}{1} {2} {3} [{4} offsets, {5} fragments]",
                (_depth > 0) ? _pad + "> " : "",
                fragment.Tag,
                fragment.ValueRepresentation.Code,
                fragment.Tag.DictionaryEntry.Name,
                fragment.OffsetTable.Count,
                fragment.Fragments.Count).AppendLine();
            return true;
        }

        /// <summary>
        /// Handler for traversing fragment item.
        /// </summary>
        /// <param name="item">Buffer containing the fragment item.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public bool OnFragmentItem(IByteBuffer item) => true;

        /// <summary>
        /// Asynchronous handler for traversing fragment item.
        /// </summary>
        /// <param name="item">Buffer containing the fragment item.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public Task<bool> OnFragmentItemAsync(IByteBuffer item) => Task.FromResult(OnFragmentItem(item));

        /// <summary>
        /// Handler for traversing end of fragment.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public bool OnEndFragment() => true;

        /// <summary>
        /// Handler for end of traversal.
        /// </summary>
        public void OnEndWalk()
        {
        }

        private void IncreaseDepth()
        {
            _depth++;

            _pad = "".PadRight(_depth * 2);
        }

        private void DecreaseDepth()
        {
            _depth--;

            _pad = "".PadRight(_depth * 2);
        }
    }
}
