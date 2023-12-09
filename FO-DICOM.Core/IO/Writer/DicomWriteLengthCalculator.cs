// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using FellowOakDicom.IO.Buffer;
using System.Linq;

namespace FellowOakDicom.IO.Writer
{

    public class DicomWriteLengthCalculator
    {

        private readonly DicomTransferSyntax _syntax;

        private readonly DicomWriteOptions _options;

        public DicomWriteLengthCalculator(DicomTransferSyntax syntax, DicomWriteOptions options)
        {
            _syntax = syntax;
            _options = options;
        }

        public uint Calculate(IEnumerable<DicomItem> items)
        {
            return (uint)items.Sum(Calculate);
        }

        public long Calculate(DicomItem item)
        {
            // skip group lengths if not writing to file
            if (!_options.KeepGroupLengths && item.Tag.Element == 0x0000)
            {
                return 0;
            }

            uint length = 0;
            length += 4; // tag

            if (_syntax.IsExplicitVR)
            {
                length += 2; // vr
                if (item.ValueRepresentation.Is16bitLength)
                {
                    length += 2; // length
                }
                else
                {
                    length += 2; // reserved
                    length += 4; // length
                }
            }
            else
            {
                length += 4; // length
            }

            if (item is DicomElement element)
            {
                length += (uint)element.Buffer.Size;
            }
            else if (item is DicomFragmentSequence fragmentSq)
            {
                // fragment item (offset table)
                length += 4; // tag
                length += 4; // length
                length += (uint)(fragmentSq.OffsetTable.Count * 4);

                foreach (IByteBuffer fragment in fragmentSq)
                {
                    // fragment item
                    length += 4; // tag
                    length += 4; // length
                    length += (uint)fragment.Size;
                }

                // sequence delimitation item
                length += 4; // tag
                length += 4; // length
            }
            else if (item is DicomSequence sq)
            {
                length += Calculate(sq);
            }

            return length;
        }

        public uint Calculate(DicomSequence sq)
        {
            uint length = 0;

            foreach (DicomDataset sqi in sq)
            {
                // sequence item
                length += 4; // tag
                length += 4; // length

                length += Calculate(sqi);

                if (!_options.ExplicitLengthSequenceItems)
                {
                    // sequence item delimitation item
                    length += 4; // tag
                    length += 4; // length
                }
            }

            if (!_options.ExplicitLengthSequences && !sq.Tag.IsPrivate)
            {
                // sequence delimitation item
                length += 4; // tag
                length += 4; // length
            }

            return length;
        }
    }
}
