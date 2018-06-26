// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Writer
{
    using System.Collections.Generic;

#if !NET35
    using System.Threading.Tasks;
#endif

    using Dicom.IO.Buffer;

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

            var buffer = element.Buffer;
            if (buffer is EndianByteBuffer)
            {
                var ebb = (EndianByteBuffer)buffer;
                if (ebb.Endian != Endian.LocalMachine && ebb.Endian == _target.Endian) buffer = ebb.Internal;
            }
            else if (_target.Endian != Endian.LocalMachine)
            {
                if (element.ValueRepresentation.UnitSize > 1) buffer = new SwapByteBuffer(buffer, element.ValueRepresentation.UnitSize);
            }

            WriteBuffer(_target, buffer, _options.LargeObjectSize);

            return true;
        }

#if !NET35
        /// <summary>
        /// Asynchronous handler for traversing a DICOM element.
        /// </summary>
        /// <param name="element">Element to traverse.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public async Task<bool> OnElementAsync(DicomElement element)
        {
            WriteTagHeader(element.Tag, element.ValueRepresentation, element.Length);

            var buffer = element.Buffer;
            if (buffer is EndianByteBuffer)
            {
                var ebb = (EndianByteBuffer)buffer;
                if (ebb.Endian != Endian.LocalMachine && ebb.Endian == _target.Endian) buffer = ebb.Internal;
            }
            else if (_target.Endian != Endian.LocalMachine)
            {
                if (element.ValueRepresentation.UnitSize > 1) buffer = new SwapByteBuffer(buffer, element.ValueRepresentation.UnitSize);
            }

            await WriteBufferAsync(_target, buffer, _options.LargeObjectSize).ConfigureAwait(false);
            return true;
        }
#endif

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

            var buffer = item;
            if (buffer is EndianByteBuffer)
            {
                var ebb = (EndianByteBuffer)buffer;
                if (ebb.Endian != Endian.LocalMachine && ebb.Endian == _target.Endian) buffer = ebb.Internal;
            }

            WriteBuffer(_target, buffer, _options.LargeObjectSize);

            return true;
        }

#if !NET35
        /// <summary>
        /// Asynchronous handler for traversing fragment item.
        /// </summary>
        /// <param name="item">Buffer containing the fragment item.</param>
        /// <returns>true if traversing completed without issues, false otherwise.</returns>
        public async Task<bool> OnFragmentItemAsync(IByteBuffer item)
        {
            WriteTagHeader(DicomTag.Item, DicomVR.NONE, item.Size);

            var buffer = item;
            if (buffer is EndianByteBuffer)
            {
                var ebb = (EndianByteBuffer)buffer;
                if (ebb.Endian != Endian.LocalMachine && ebb.Endian == _target.Endian) buffer = ebb.Internal;
            }

            await WriteBufferAsync(_target, buffer, _options.LargeObjectSize).ConfigureAwait(false);

            return true;
        }
#endif

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
                // Comply with CP-1066 (#597)
                if (vr.Is16bitLength && length > 0xfffe)
                {
                    vr = DicomVR.UN;
                }

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

        private static void WriteBuffer(IByteTarget target, IByteBuffer buffer, uint largeObjectSize)
        {
            var offset = 0;
            var remainingSize = buffer.Size;

            while (remainingSize > largeObjectSize)
            {
                var range = buffer.GetByteRange(offset, (int)largeObjectSize);
                target.Write(range, 0, largeObjectSize);

                offset += (int)largeObjectSize;
                remainingSize -= largeObjectSize;
            }

            target.Write(buffer.GetByteRange(offset, (int)remainingSize), 0, remainingSize);
        }

#if !NET35
        private static async Task WriteBufferAsync(IByteTarget target, IByteBuffer buffer, uint largeObjectSize)
        {
            var offset = 0;
            var remainingSize = buffer.Size;

            while (remainingSize > largeObjectSize)
            {
                var range = buffer.GetByteRange(offset, (int)largeObjectSize);
                await target.WriteAsync(range, 0, largeObjectSize).ConfigureAwait(false);

                offset += (int)largeObjectSize;
                remainingSize -= largeObjectSize;
            }

            target.Write(buffer.GetByteRange(offset, (int)remainingSize), 0, remainingSize);
        }
#endif
    }
}
