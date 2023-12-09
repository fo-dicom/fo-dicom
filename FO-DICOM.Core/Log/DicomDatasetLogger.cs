// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace FellowOakDicom.Log
{
    /// <summary>
    /// DICOM dataset walker for logging.
    /// </summary>
    [SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem", Justification = "This class is explicitly designed to write a DICOM dataset to a logger")]
    public class DicomDatasetLogger : IDicomDatasetWalker
    {
        private readonly Microsoft.Extensions.Logging.ILogger _log;

        private readonly Microsoft.Extensions.Logging.LogLevel _level;

        private readonly int _width;

        private readonly int _value;

        private int _depth;

        private string _pad = string.Empty;

        /// <summary>
        /// Initializes an instance of <see cref="DicomDatasetLogger"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="level">Log level.</param>
        /// <param name="width">Maximum write width.</param>
        /// <param name="valueLength">Maximum value length.</param>
        public DicomDatasetLogger(Microsoft.Extensions.Logging.ILogger logger, Microsoft.Extensions.Logging.LogLevel level, int width = 128, int valueLength = 64)
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
                var val = string.Join("/", element.Get<string[]>());
                if (val.Length > (_value - sb.Length))
                {
                    sb.Append(val.Substring(0, _value - sb.Length));
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
                name.Substring(0, Math.Min(_width - sb.Length - 9, name.Length)));
            _log.Log(_level, sb.ToString());
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
            _log.Log(
                _level,
                "{Padding}{Tag} SQ {TagDictionaryEntryName}",
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
                 "{Padding}{Tag} {VrCode} {TagDictionaryEntryName} [{Offsets} offsets, {Fragments} fragments]",
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
            _pad = string.Empty.PadLeft(2 * _depth);
        }

        private void DecreaseDepth()
        {
            _depth--;
            _pad = string.Empty.PadLeft(2 * _depth);
        }

    }
}
