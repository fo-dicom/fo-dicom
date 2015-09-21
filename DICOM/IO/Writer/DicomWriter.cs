// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;

using Dicom.IO.Buffer;

namespace Dicom.IO.Writer
{
    /// <summary>
    /// DICOM object writer.
    /// </summary>
    public class DicomWriter : IDicomDatasetWalker
    {
        private const uint UndefinedLength = 0xffffffff;

        private DicomTransferSyntax _syntax;

        private readonly DicomWriteOptions _options;

        private readonly IByteTarget _target;

        private Stack<DicomSequence> _sequences;

        /// <summary>
        /// Initializes an instance of <see cref="DicomWriter"/>.
        /// </summary>
        /// <param name="syntax">Writer transfer syntax.</param>
        /// <param name="options">Writer options.</param>
        /// <param name="target">Target to which to write the DICOM object.</param>
        public DicomWriter(DicomTransferSyntax syntax, DicomWriteOptions options, IByteTarget target)
        {
            _syntax = syntax;
            _options = options ?? DicomWriteOptions.Default;
            _target = target;
        }

        /// <summary>
        /// Gets or sets the DICOM transfer syntax to apply in writing.
        /// </summary>
        public DicomTransferSyntax Syntax
        {
            get
            {
                return _syntax;
            }
            set
            {
                _syntax = value;
            }
        }

        /// <summary>
        /// Handler for beginning the traversal.
        /// </summary>
        public void OnBeginWalk()
        {
            _target.Endian = _syntax.Endian;
            _sequences = new Stack<DicomSequence>();
        }

        /// <summary>
        /// Handler for traversing a DICOM element.
        /// </summary>
        /// <param name="element">Element to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="IDicomDatasetWalker.OnBeginWalk"/> before returning.</remarks>
        public bool OnElement(DicomElement element)
        {
            WriteTagHeader(element.Tag, element.ValueRepresentation, element.Length);

            IByteBuffer buffer = element.Buffer;
            if (buffer is EndianByteBuffer)
            {
                EndianByteBuffer ebb = buffer as EndianByteBuffer;
                if (ebb.Endian != Endian.LocalMachine && ebb.Endian == _target.Endian) buffer = ebb.Internal;
            }
            else if (_target.Endian != Endian.LocalMachine)
            {
                if (element.ValueRepresentation.UnitSize > 1) buffer = new SwapByteBuffer(buffer, element.ValueRepresentation.UnitSize);
            }

            _target.Write(buffer.Data, 0, buffer.Size);
            if (element.Length >= _options.LargeObjectSize)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Handler for traversing beginning of sequence.
        /// </summary>
        /// <param name="sequence">Sequence to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="IDicomDatasetWalker.OnBeginWalk"/> before returning.</remarks>
        public bool OnBeginSequence(DicomSequence sequence)
        {
            uint length = UndefinedLength;

            if (_options.ExplicitLengthSequences || sequence.Tag.IsPrivate)
            {
                DicomWriteLengthCalculator calc = new DicomWriteLengthCalculator(_syntax, _options);
                length = calc.Calculate(sequence);
            }

            _sequences.Push(sequence);

            WriteTagHeader(sequence.Tag, DicomVR.SQ, length);
            return true;
        }

        /// <summary>
        /// Handler for traversing beginning of sequence item.
        /// </summary>
        /// <param name="dataset">Item dataset.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="IDicomDatasetWalker.OnBeginWalk"/> before returning.</remarks>
        public bool OnBeginSequenceItem(DicomDataset dataset)
        {
            uint length = UndefinedLength;

            if (_options.ExplicitLengthSequenceItems)
            {
                DicomWriteLengthCalculator calc = new DicomWriteLengthCalculator(_syntax, _options);
                length = calc.Calculate(dataset);
            }

            WriteTagHeader(DicomTag.Item, DicomVR.NONE, length);
            return true;
        }

        /// <summary>
        /// Handler for traversing end of sequence item.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="IDicomDatasetWalker.OnBeginWalk"/> before returning.</remarks>
        public bool OnEndSequenceItem()
        {
            DicomSequence sequence = _sequences.Peek();

            if (!_options.ExplicitLengthSequenceItems)
            {
                WriteTagHeader(DicomTag.ItemDelimitationItem, DicomVR.NONE, 0);
            }

            return true;
        }

        /// <summary>
        /// Handler for traversing end of sequence.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="IDicomDatasetWalker.OnBeginWalk"/> before returning.</remarks>
        public bool OnEndSequence()
        {
            DicomSequence sequence = _sequences.Pop();

            if (!_options.ExplicitLengthSequences && !sequence.Tag.IsPrivate)
            {
                WriteTagHeader(DicomTag.SequenceDelimitationItem, DicomVR.NONE, 0);
            }

            return true;
        }

        /// <summary>
        /// Handler for traversing beginning of fragment.
        /// </summary>
        /// <param name="fragment">Fragment sequence.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="IDicomDatasetWalker.OnBeginWalk"/> before returning.</remarks>
        public bool OnBeginFragment(DicomFragmentSequence fragment)
        {
            WriteTagHeader(fragment.Tag, fragment.ValueRepresentation, UndefinedLength);
            WriteTagHeader(DicomTag.Item, DicomVR.NONE, (uint)(fragment.OffsetTable.Count * 4));
            foreach (uint offset in fragment.OffsetTable) _target.Write(offset);
            return true;
        }

        /// <summary>
        /// Handler for traversing fragment item.
        /// </summary>
        /// <param name="item">Buffer containing the fragment item.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="IDicomDatasetWalker.OnBeginWalk"/> before returning.</remarks>
        public bool OnFragmentItem(IByteBuffer item)
        {
            WriteTagHeader(DicomTag.Item, DicomVR.NONE, item.Size);

            IByteBuffer buffer = item;
            if (buffer is EndianByteBuffer)
            {
                EndianByteBuffer ebb = buffer as EndianByteBuffer;
                if (ebb.Endian != Endian.LocalMachine && ebb.Endian == _target.Endian) buffer = ebb.Internal;
            }

            _target.Write(buffer.Data, 0, buffer.Size);
            if (item.Size >= _options.LargeObjectSize)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Handler for traversing end of fragment.
        /// </summary>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        /// <remarks>On false return value, the method will invoke the callback method passed in <see cref="IDicomDatasetWalker.OnBeginWalk"/> before returning.</remarks>
        public bool OnEndFragment()
        {
            WriteTagHeader(DicomTag.SequenceDelimitationItem, DicomVR.NONE, 0);
            return true;
        }

        /// <summary>
        /// Handler for end of traversal.
        /// </summary>
        public void OnEndWalk()
        {
            _sequences = null;
        }

        /// <summary>
        /// Write tag header.
        /// </summary>
        /// <param name="tag">DICOM tag.</param>
        /// <param name="vr">Value Representation.</param>
        /// <param name="length">Element length.</param>
        private void WriteTagHeader(DicomTag tag, DicomVR vr, uint length)
        {
            _target.Write(tag.Group);
            _target.Write(tag.Element);

            if (_syntax.IsExplicitVR && vr != DicomVR.NONE)
            {
                _target.Write((byte)vr.Code[0]);
                _target.Write((byte)vr.Code[1]);

                if (vr.Is16bitLength)
                {
                    _target.Write((ushort)length);
                }
                else
                {
                    _target.Write((ushort)0);
                    _target.Write(length);
                }
            }
            else
            {
                _target.Write(length);
            }
        }
    }
}
