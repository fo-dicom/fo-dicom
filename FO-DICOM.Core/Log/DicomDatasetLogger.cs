// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Text;
#if !NET35
using System.Threading.Tasks;
#endif
using FellowOakDicom.IO.Buffer;

namespace FellowOakDicom.Log
{

    /// <summary>
    /// DICOM dataset walker for logging.
    /// </summary>
    public class DicomDatasetLogger : IDicomDatasetWalker
    {
        private Logger _log;

        private LogLevel _level;

        private int _width = 128;

        private int _value = 64;

        private int _depth = 0;

        private string _pad = String.Empty;

        /// <summary>
        /// Initializes an instance of <see cref="DicomDatasetLogger"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="level">Log level.</param>
        /// <param name="width">Maximum write width.</param>
        /// <param name="valueLength">Maximum value length.</param>
        public DicomDatasetLogger(Logger logger, LogLevel level, int width = 128, int valueLength = 64)
        {
            _log = logger;
            _level = level;
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
            StringBuilder sb = new StringBuilder();
            if (_depth > 0) sb.Append(_pad).Append("> ");
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
                if (val.Length > (_value - 2 - sb.Length))
                {
                    sb.Append(val.Substring(0, _value - 2 - sb.Length));
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
                var val = String.Join("/", element.Get<string[]>());
                if (val.Length > (_value - sb.Length))
                {
                    sb.Append(val.Substring(0, _value - sb.Length));
                }
                else
                {
                    sb.Append(val);
                }
            }
            while (sb.Length < _value) sb.Append(' ');
            sb.Append('#');
            string name = element.Tag.DictionaryEntry.Keyword;
            sb.AppendFormat(
                "{0,6}, {1}",
                element.Length,
                name.Substring(0, System.Math.Min(_width - sb.Length - 9, name.Length)));
            _log.Log(_level, sb.ToString());
            return true;
        }

#if !NET35
        /// <summary>
        /// Asynchronous handler for traversing a DICOM element.
        /// </summary>
        /// <param name="element">Element to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public Task<bool> OnElementAsync(DicomElement element)
        {
            return Task.FromResult(this.OnElement(element));
        }
#endif

        /// <summary>
        /// Handler for traversing beginning of sequence.
        /// </summary>
        /// <param name="sequence">Sequence to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public bool OnBeginSequence(DicomSequence sequence)
        {
            _log.Log(
                _level,
                "{padding}{tag} SQ {tagDictionaryEntryName}",
                (_depth > 0) ? _pad + "> " : "",
                sequence.Tag,
                sequence.Tag.DictionaryEntry.Name);
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
            _log.Log(_level, _pad + "Item:");
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
            _log.Log(
                _level,
                "{padding}{tag} {vrCode} {tagDictionaryEntryName} [{offsets} offsets, {fragments} fragments]",
                (_depth > 0) ? _pad + "> " : "",
                fragment.Tag,
                fragment.ValueRepresentation.Code,
                fragment.Tag.DictionaryEntry.Name,
                fragment.OffsetTable.Count,
                fragment.Fragments.Count);
            return true;
        }

        /// <summary>
        /// Handler for traversing fragment item.
        /// </summary>
        /// <param name="item">Buffer containing the fragment item.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public bool OnFragmentItem(IByteBuffer item)
        {
            return true;
        }

#if !NET35
        /// <summary>
        /// Asynchronous handler for traversing fragment item.
        /// </summary>
        /// <param name="item">Buffer containing the fragment item.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public Task<bool> OnFragmentItemAsync(IByteBuffer item)
        {
            return Task.FromResult(this.OnFragmentItem(item));
        }
#endif

        /// <summary>
        /// Handler for traversing end of fragment.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public bool OnEndFragment()
        {
            return true;
        }

        /// <summary>
        /// Handler for end of traversal.
        /// </summary>
        public void OnEndWalk()
        {
        }

        private void IncreaseDepth()
        {
            _depth++;

            _pad = String.Empty;
            for (int i = 0; i < _depth; i++) _pad += "  ";
        }

        private void DecreaseDepth()
        {
            _depth--;

            _pad = String.Empty;
            for (int i = 0; i < _depth; i++) _pad += "  ";
        }
    }
}
