// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Mathematics;
using FellowOakDicom.IO.Buffer;
using FellowOakDicom.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace FellowOakDicom.IO.Reader
{

    /// <summary>
    /// DICOM reader implementation.
    /// </summary>
    internal class DicomReader : IDicomReader
    {
        private readonly IMemoryProvider _memoryProvider;

        #region FIELDS

        private readonly Dictionary<uint, string> _private;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of <see cref="DicomReader"/>.
        /// </summary>
        public DicomReader(IMemoryProvider memoryProvider)
        {
            _memoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
            _private = new Dictionary<uint, string>();
            Dictionary = DicomDictionary.Default;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets whether value representation is explicit or not.
        /// </summary>
        public bool IsExplicitVR { get; set; }

        public bool IsDeflated { get; set; }

        /// <summary>
        /// Gets or sets the DICOM dictionary to be used by the reader.
        /// </summary>
        public DicomDictionary Dictionary { get; set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Perform DICOM reading of a byte source.
        /// </summary>
        /// <param name="source">Byte source to read.</param>
        /// <param name="observer">Reader observer.</param>
        /// <param name="stop">Criterion at which to stop.</param>
        /// <returns>Reader resulting status.</returns>
        public DicomReaderResult Read(IByteSource source, IDicomReaderObserver observer, Func<ParseState, bool> stop = null)
        {
            var worker = new DicomReaderWorker(observer, stop, Dictionary, IsExplicitVR, IsDeflated, _private, _memoryProvider);
            return worker.DoWork(source);
        }

        /// <summary>
        /// Asynchronously perform DICOM reading of a byte source.
        /// </summary>
        /// <param name="source">Byte source to read.</param>
        /// <param name="observer">Reader observer.</param>
        /// <param name="stop">Criterion at which to stop.</param>
        /// <returns>Awaitable reader resulting status.</returns>
        public Task<DicomReaderResult> ReadAsync(IByteSource source, IDicomReaderObserver observer, Func<ParseState, bool> stop = null)
        {
            var worker = new DicomReaderWorker(observer, stop, Dictionary, IsExplicitVR, IsDeflated, _private, _memoryProvider);
            return worker.DoWorkAsync(source);
        }

        #endregion

        #region INNER TYPES

        /// <summary>
        /// Support class performing the actual reading.
        /// </summary>
        private sealed class DicomReaderWorker
        {

            #region FIELDS

            /// <summary>
            /// Defined value for undefined length.
            /// </summary>
            private const uint _undefinedLength = 0xffffffff;

            private readonly IDicomReaderObserver _observer;

            private readonly Func<ParseState, bool> _stop;

            private readonly DicomDictionary _dictionary;

            private readonly Dictionary<uint, string> _private;
            private readonly IMemoryProvider _memoryProvider;

            private readonly bool _isExplicitVR;

            private readonly bool _isDeflated;

            private DicomReaderResult _result;

            private DicomTag _previousTag;

            #endregion

            #region CONSTRUCTORS

            /// <summary>
            /// Initializes an instance of <see cref="DicomReaderWorker"/>.
            /// </summary>
            internal DicomReaderWorker(
                IDicomReaderObserver observer,
                Func<ParseState, bool> stop,
                DicomDictionary dictionary,
                bool isExplicitVR,
                bool isDeflated,
                Dictionary<uint, string> @private,
                IMemoryProvider memoryProvider)
            {
                _observer = observer;
                _stop = stop;
                _dictionary = dictionary;
                _isExplicitVR = isExplicitVR;
                _isDeflated = isDeflated;
                _private = @private;
                _memoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
            }

            #endregion

            #region METHODS

            /// <summary>
            /// Read the byte source.
            /// </summary>
            /// <param name="source">Byte source to read.</param>
            /// <returns>Read result.</returns>
            internal DicomReaderResult DoWork(IByteSource source)
            {
                source = ConvertSource(source);
                ParseDataset(source, out DicomTag tag, 0, 0, false);

                if (tag == DicomTag.SequenceDelimitationItem && _result == DicomReaderResult.Processing && source.IsEOF)
                {
                    _result = DicomReaderResult.Success;
                }

                return _result;
            }

            /// <summary>
            /// Asynchronously read the byte source.
            /// </summary>
            /// <param name="source">Byte source to read.</param>
            /// <returns>Awaitable read result.</returns>
            internal async Task<DicomReaderResult> DoWorkAsync(IByteSource source)
            {
                source = ConvertSource(source);
                var tag = await ParseDatasetAsync(source, 0, 0, false).ConfigureAwait(false);

                if (tag == DicomTag.SequenceDelimitationItem && _result == DicomReaderResult.Processing && source.IsEOF)
                {
                    _result = DicomReaderResult.Success;
                }

                return _result;
            }

            private IByteSource ConvertSource(IByteSource source)
            {
                return _isDeflated ? Decompress(source) : source;
            }

            private void ParseDataset(IByteSource source, out DicomTag tag, int sequenceDepth, long positionEnd, bool handleBadPrivateSequence)
            {
                tag = null;
                _result = DicomReaderResult.Processing;

                using var vrMemory = _memoryProvider.Provide(2);
                while (!source.IsEOF && (positionEnd == 0 || source.Position < positionEnd) && _result == DicomReaderResult.Processing)
                {
                    var positionElement = source.Position;
                    if (!ParseTag(source, out tag, out var entry, sequenceDepth, positionElement))
                    {
                        return;
                    }
                    if (!ParseVR(source, vrMemory, ref tag, ref entry, out var vr, handleBadPrivateSequence))
                    {
                        return;
                    }
                    if (!ParseLength(source, tag, ref vr, out var length, handleBadPrivateSequence))
                    {
                        return;
                    }
                    if (!ParseValue(source, vrMemory, tag, vr, length, sequenceDepth, positionElement, positionEnd, handleBadPrivateSequence))
                    {
                        return;
                    }
                }

                if ((positionEnd > 0 && source.Position >= positionEnd))
                {
                    // end of explicit length sequence item, so return without changing the _result
                    return;
                }

                if (_result != DicomReaderResult.Processing)
                {
                    return;
                }

                // end of processing
                _result = DicomReaderResult.Success;
            }

            private async Task<DicomTag> ParseDatasetAsync(IByteSource source, int sequenceDepth, long positionEnd, bool handleBadPrivateSequence)
            {
                _result = DicomReaderResult.Processing;

                using var vrMemory = _memoryProvider.Provide(2);
                while (!source.IsEOF && (positionEnd == 0 || source.Position < positionEnd) && _result == DicomReaderResult.Processing)
                {
                    var positionElement = source.Position;
                    if (!ParseTag(source, out var tag, out var entry, sequenceDepth, positionElement))
                    {
                        return tag;
                    }
                    if (!ParseVR(source, vrMemory, ref tag, ref entry, out var vr, handleBadPrivateSequence))
                    {
                        return tag;
                    }
                    if (!ParseLength(source, tag, ref vr, out var length, handleBadPrivateSequence))
                    {
                        return tag;
                    }
                    if (!await ParseValueAsync(source, vrMemory, tag, vr, length, sequenceDepth, positionElement, positionEnd, handleBadPrivateSequence).ConfigureAwait(false))
                    {
                        return tag;
                    }
                }

                if ((positionEnd > 0 && source.Position >= positionEnd))
                {
                    // end of explicit length sequence item, so return without changing _result
                    return null;
                }

                if (_result != DicomReaderResult.Processing)
                {
                    return null;
                }

                // end of processing
                _result = DicomReaderResult.Success;
                return null;
            }


            private IByteSource Decompress(IByteSource source)
            {
                var compressed = source.GetStream();

                var decompressed = new MemoryStream();
                using (var decompressor = new DeflateStream(compressed, CompressionMode.Decompress, true))
                {
                    decompressor.CopyTo(decompressed);
                }

                decompressed.Seek(0, SeekOrigin.Begin);
                return new StreamByteSource(decompressed);
            }


            private bool ParseTag(IByteSource source, out DicomTag tag, out DicomDictionaryEntry entry, int sequenceDepth, long positionElement)
            {
                tag = null;
                entry = null;

                if (!source.Require(4))
                {
                    _result = DicomReaderResult.Suspended;
                    return false;
                }

                var group = source.GetUInt16();
                var element = source.GetUInt16();
                DicomPrivateCreator creator = null;

                // according to
                // http://dicom.nema.org/medical/dicom/current/output/chtml/part05/sect_7.8.html
                // The requirements of this section do not allow any use of elements in the ranges 
                // (gggg,0001-000F) and (gggg,0100-0FFF) where gggg is odd.
                // So element at [0x0100-0x0FFF] should not have a creator
                if (@group.IsOdd() && element >= 0x1000)
                {
                    var card = (uint)(@group << 16) + (uint)(element >> 8);
                    if (_private.TryGetValue(card, out string pvt))
                    {
                        creator = _dictionary.GetPrivateCreator(pvt);
                    }
                }

                tag = creator == null
                    ? DicomTagsIndex.LookupOrCreate(group, element)
                    : new DicomTag(@group, element, creator);
                entry = _dictionary[tag];

                if (!tag.IsPrivate && entry != null && entry.MaskTag == null)
                {
                    tag = entry.Tag; // Use dictionary tag
                }

                if (_stop?.Invoke(new ParseState { PreviousTag = _previousTag, Tag = tag, SequenceDepth = sequenceDepth }) ?? false)
                {
                    // if a stop is requested, then move back to the beginning of the tag.
                    _result = DicomReaderResult.Stopped;
                    source.GoTo(positionElement);
                    return false;
                }

                _previousTag = tag;

                return true;
            }

            private bool ParseVR(IByteSource source, IMemory vrMemory, ref DicomTag tag, ref DicomDictionaryEntry entry, out DicomVR vr, bool handleBadPrivateSequence)
            {
                vr = null;
                if (tag == DicomTag.Item || tag == DicomTag.ItemDelimitationItem
                    || tag == DicomTag.SequenceDelimitationItem)
                {
                    vr = DicomVR.NONE;
                    return true;
                }

                if (_isExplicitVR || handleBadPrivateSequence)
                {
                    if (!source.Require(2))
                    {
                        _result = DicomReaderResult.Suspended;
                        vr = DicomVR.NONE;
                        return false;
                    }

                    var markedPosition = source.Position;

                    if (source.GetBytes(vrMemory.Bytes, 0, 2) != 2 || !DicomVR.TryParse(vrMemory.Bytes, out vr))
                    {
                        // If the VR is 0x2020, try to use the first known VR of the tag (issue #179)
                        if (entry != null && (vrMemory.Bytes[0] == 0x20 && vrMemory.Bytes[1] == 0x20))
                        {
                            vr = entry.ValueRepresentations.FirstOrDefault();
                        }
                        else
                        {
                            // unable to parse VR; rewind VR bytes for continued attempt to interpret the data.
                            vr = DicomVR.Implicit;
                            source.GoTo(markedPosition);
                        }
                    }
                }
                else
                {
                    if (entry != null)
                    {
                        if (entry == DicomDictionary.UnknownTag)
                        {
                            vr = DicomVR.UN;
                        }
                        else if (entry.ValueRepresentations.Contains(DicomVR.OB)
                                 && entry.ValueRepresentations.Contains(DicomVR.OW))
                        {
                            vr = DicomVR.OW; // ???
                        }
                        else
                        {
                            vr = entry.ValueRepresentations.FirstOrDefault();
                        }
                    }
                }

                vr ??= DicomVR.UN;

                if (vr == DicomVR.UN)
                {
                    if (tag.Element == 0x0000)
                    {
                        // Group Length to UL
                        // change 20161216: if changing from UN to UL then ParseLength causes a error, since length in UL is 2 bytes while length in UN is 6 bytes. 
                        // so the source hat UN and coded the length in 6 bytes. if here the VR was changed to UL then ParseLength would only read 2 bytes and the parser is then wrong.
                        // but no worry: in ParseValue in the first lines there is a lookup in the Dictionary of DicomTags and there the VR is changed to UL so that the value is finally interpreted correctly as UL.
                        vr = DicomVR.UL;
                        return true;
                    }
                    if (_isExplicitVR ^ handleBadPrivateSequence)
                    {
                        return true;
                    }
                }

                if (tag.IsPrivate)
                {
                    // according to
                    // http://dicom.nema.org/medical/dicom/current/output/chtml/part05/sect_7.8.html
                    // Private Creator Data Elements numbered (gggg,0010-00FF) (gggg is odd)
                    // The VR of the private identification code shall be LO (Long String) and the VM shall be equal to 1.
                    if (tag.Element >= 0x0010 && tag.Element <= 0x00ff && vr == DicomVR.UN)
                    {
                        vr = DicomVR.LO; // force private creator to LO
                    }
                }
                return true;
            }

            private bool ParseLength(IByteSource source, DicomTag tag, ref DicomVR vr, out uint length, bool handleBadPrivateSequence)
            {
                length = 0;
                if (tag == DicomTag.Item || tag == DicomTag.ItemDelimitationItem
                    || tag == DicomTag.SequenceDelimitationItem)
                {
                    if (!source.Require(4))
                    {
                        _result = DicomReaderResult.Suspended;
                        return false;
                    }

                    length = source.GetUInt32();
                    return true;
                }

                if (_isExplicitVR || handleBadPrivateSequence)
                {
                    if (vr == DicomVR.Implicit)
                    {
                        if (!source.Require(4))
                        {
                            _result = DicomReaderResult.Suspended;
                            return false;
                        }

                        length = source.GetUInt32();

                        // assume that undefined length in implicit VR element is SQ
                        if (length == _undefinedLength)
                        {
                            vr = DicomVR.SQ;
                        }
                    }
                    else if (vr.Is16bitLength)
                    {
                        if (!source.Require(2))
                        {
                            _result = DicomReaderResult.Suspended;
                            return false;
                        }

                        length = source.GetUInt16();
                    }
                    else
                    {
                        if (!source.Require(6))
                        {
                            _result = DicomReaderResult.Suspended;
                            return false;
                        }

                        //Check for old files made with incorrect Data Element
                        //Prior versions of fo-dicom may have mistakenly used 2 bytes as a length of SV and UV (Is16bitLength = true)
                        if (vr == DicomVR.UV || vr == DicomVR.SV)
                        {
                            length = source.GetUInt16();
                        }
                        else
                        {
                            source.Skip(2);
                        }

                        if (length == 0)
                        {
                            length = source.GetUInt32();

                            // CP-246 (#177) handling
                            // assume that Undefined Length in explicit datasets with VR UN are sequences 
                            // According to CP-246 the sequence shall be handled as ILE, but this will be handled later...
                            // in the current code this needs to be restricted to privates 
                            if (length == _undefinedLength && vr == DicomVR.UN && tag.IsPrivate)
                            {
                                vr = DicomVR.SQ;
                            }
                        }
                    }
                }
                else
                {
                    if (!source.Require(4))
                    {
                        _result = DicomReaderResult.Suspended;
                        return false;
                    }

                    length = source.GetUInt32();

                    // assume that undefined length in implicit dataset is SQ
                    if (length == _undefinedLength && vr == DicomVR.UN)
                    {
                        vr = DicomVR.SQ;
                    }
                }

                return true;
            }

            private bool ParseValue(IByteSource source, IMemory vrMemory, DicomTag tag, DicomVR vr, uint length, int sequenceDepth, long positionElement, long positionEnd, bool handleBadPrivateSequence)
            {
                // check dictionary for VR after reading length to handle 16-bit lengths
                // check before reading value to handle SQ elements
                var parsedVR = vr;

                // check dictionary for VR after reading length to handle 16-bit lengths
                // check before reading value to handle SQ elements
                if (vr == DicomVR.Implicit || (vr == DicomVR.UN && (_isExplicitVR ^ handleBadPrivateSequence)))
                {
                    var entry = _dictionary[tag];
                    if (entry != null)
                    {
                        vr = entry.ValueRepresentations.FirstOrDefault();
                    }

                    vr ??= DicomVR.UN;
                }

                if (tag == DicomTag.ItemDelimitationItem || tag == DicomTag.SequenceDelimitationItem)
                {
                    // end of sequence item
                    return false;
                }

                if (vr == DicomVR.SQ && tag.IsPrivate && length > 0)
                {
                    if (!IsPrivateSequence(source))
                    {
                        vr = DicomVR.UN;
                    }
                    else if (IsPrivateSequenceBad(source, length, _isExplicitVR, vrMemory))
                    {
                        handleBadPrivateSequence = true;
                    }
                }

                // Fix to handle sequence items not associated with any sequence (#364)
                if (tag.Equals(DicomTag.Item))
                {
                    source.GoTo(positionElement);
                    vr = DicomVR.SQ;
                    length = _undefinedLength;
                }

                if (vr == DicomVR.SQ)
                {
                    long endOfValue = positionEnd;
                    // start of sequence
                    _observer.OnBeginSequence(source, positionElement, tag, length);
                    if (length == 0)
                    {
                        endOfValue = source.Position;
                    }
                    else if (length != _undefinedLength)
                    {
                        endOfValue = source.Position + length;
                    }

                    var last = source.Position;

                    // Conformance with CP-246 (#177)
                    var needtoChangeEndian = false;
                    if (parsedVR == DicomVR.UN && !tag.IsPrivate)
                    {
                        needtoChangeEndian = source.Endian == Endian.Big;
                    }
                    if (needtoChangeEndian)
                    {
                        source.Endian = Endian.Little;
                    }

                    ParseItemSequence(source, sequenceDepth, endOfValue, handleBadPrivateSequence);

                    if (needtoChangeEndian)
                    {
                        source.Endian = Endian.Big;
                    }

                    // Aeric Sylvan - https://github.com/rcd/fo-dicom/issues/62#issuecomment-46248073
                    // Fix reading of SQ with parsed VR of UN
                    if (source.Position > last || length == 0)
                    {
                        return true;
                    }

                    vr = parsedVR;
                }

                if (length == _undefinedLength)
                {
                    _observer.OnBeginFragmentSequence(source, positionElement, tag, vr);
                    ParseFragmentSequence(source, vr);
                    return true;
                }

                if (!source.Require(length))
                {
                    _result = DicomReaderResult.Suspended;
                    return false;
                }

                var buffer = source.GetBuffer(length);

                if (buffer != null)
                {
                    if (!vr.IsString)
                    {
                        buffer = EndianByteBuffer.Create(buffer, source.Endian, vr.UnitSize);
                    }
                    _observer.OnElement(source, positionElement, tag, vr, buffer);
                }

                // parse private creator value and add to lookup table
                // according to
                // http://dicom.nema.org/medical/dicom/current/output/chtml/part05/sect_7.8.html
                // Private Creator Data Elements numbered (gggg,0010-00FF) (gggg is odd)
                // The VR of the private identification code shall be LO (Long String) and the VM shall be equal to 1.
                if (tag.IsPrivate && tag.Element >= 0x0010 && tag.Element <= 0x00ff)
                {
                    var creator =
                        DicomEncoding.Default.GetString(buffer.Data, 0, buffer.Data.Length)
                            .TrimEnd((char)DicomVR.LO.PaddingValue);
                    var card = (uint)(tag.Group << 16) + tag.Element;

                    _private[card] = creator;
                }

                return true;
            }

            private async Task<bool> ParseValueAsync(IByteSource source, IMemory vrMemory, DicomTag tag, DicomVR vr, uint length, int sequenceDepth, long positionElement, long positionEnd, bool handleBadPrivateSequence)
            {
                // check dictionary for VR after reading length to handle 16-bit lengths
                // check before reading value to handle SQ elements
                var parsedVR = vr;

                // check dictionary for VR after reading length to handle 16-bit lengths
                // check before reading value to handle SQ elements
                if (vr == DicomVR.Implicit || (vr == DicomVR.UN && (_isExplicitVR ^ handleBadPrivateSequence)))
                {
                    var entry = _dictionary[tag];
                    if (entry != null)
                    {
                        vr = entry.ValueRepresentations.FirstOrDefault();
                    }

                    vr ??= DicomVR.UN;
                }

                if (tag == DicomTag.ItemDelimitationItem || tag == DicomTag.SequenceDelimitationItem)
                {
                    // end of sequence item
                    return false;
                }

                if (vr == DicomVR.SQ && tag.IsPrivate && length > 0)
                {
                    if (!IsPrivateSequence(source))
                    {
                        vr = DicomVR.UN;
                    }
                    else if (IsPrivateSequenceBad(source, length, _isExplicitVR, vrMemory))
                    {
                        handleBadPrivateSequence = true;
                    }
                }

                // Fix to handle sequence items not associated with any sequence (#364)
                if (tag.Equals(DicomTag.Item))
                {
                    source.GoTo(positionElement);
                    vr = DicomVR.SQ;
                    length = _undefinedLength;
                }

                if (vr == DicomVR.SQ)
                {
                    long endOfValue = positionEnd;
                    // start of sequence
                    _observer.OnBeginSequence(source, positionElement, tag, length);
                    if (length == 0)
                    {
                        endOfValue = source.Position;
                    }
                    else if (length != _undefinedLength)
                    {
                        endOfValue = source.Position + length;
                    }

                    var last = source.Position;

                    // Conformance with CP-246 (#177)
                    var needtoChangeEndian = false;
                    if (parsedVR == DicomVR.UN && !tag.IsPrivate)
                    {
                        needtoChangeEndian = source.Endian == Endian.Big;
                    }
                    if (needtoChangeEndian)
                    {
                        source.Endian = Endian.Little;
                    }

                    await ParseItemSequenceAsync(source, sequenceDepth, endOfValue, handleBadPrivateSequence).ConfigureAwait(false);

                    if (needtoChangeEndian)
                    {
                        source.Endian = Endian.Big;
                    }

                    // Aeric Sylvan - https://github.com/rcd/fo-dicom/issues/62#issuecomment-46248073
                    // Fix reading of SQ with parsed VR of UN
                    if (source.Position > last || length == 0)
                    {
                        return true;
                    }

                    vr = parsedVR;
                }

                if (length == _undefinedLength)
                {
                    _observer.OnBeginFragmentSequence(source, positionElement, tag, vr);
                    await ParseFragmentSequenceAsync(source, vr).ConfigureAwait(false);
                    return true;
                }

                if (!source.Require(length))
                {
                    _result = DicomReaderResult.Suspended;
                    return false;
                }

                var buffer = await source.GetBufferAsync(length).ConfigureAwait(false);

                if (!vr.IsString)
                {
                    buffer = EndianByteBuffer.Create(buffer, source.Endian, vr.UnitSize);
                }
                _observer.OnElement(source, positionElement, tag, vr, buffer);

                // parse private creator value and add to lookup table
                // according to
                // http://dicom.nema.org/medical/dicom/current/output/chtml/part05/sect_7.8.html
                // Private Creator Data Elements numbered (gggg,0010-00FF) (gggg is odd)
                // The VR of the private identification code shall be LO (Long String) and the VM shall be equal to 1.
                if (tag.IsPrivate && tag.Element >= 0x0010 && tag.Element <= 0x00ff)
                {
                    var creator =
                        DicomEncoding.Default.GetString(buffer.Data, 0, buffer.Data.Length)
                            .TrimEnd((char)DicomVR.LO.PaddingValue);
                    var card = (uint)(tag.Group << 16) + tag.Element;

                    _private[card] = creator;
                }

                return true;
            }

            private void ParseItemSequence(IByteSource source, int sequenceDepth, long positionOfValueEnd, bool handleBadPrivateSequence)
            {
                _result = DicomReaderResult.Processing;

                while (!source.IsEOF && (positionOfValueEnd == 0 || source.Position < positionOfValueEnd))
                {
                    var positionItem = source.Position;
                    if (!ParseItemSequenceTag(source, out var tag, out var length, positionOfValueEnd))
                    {
                        return;
                    }
                    // #64, in case explicit length has been specified despite occurrence of Sequence Delimitation Item
                    if (tag == DicomTag.SequenceDelimitationItem)
                    {
                        continue;
                    }

                    if (!ParseItemSequenceValue(source, tag, length, sequenceDepth, positionItem, positionOfValueEnd, handleBadPrivateSequence))
                    {
                        return;
                    }
                }

                // end of explicit length sequence
                _observer.OnEndSequence();
            }

            private async Task ParseItemSequenceAsync(IByteSource source, int sequenceDepth, long endOfValue, bool handleBadPrivateSequence)
            {
                _result = DicomReaderResult.Processing;

                while (!source.IsEOF && (endOfValue == 0 || source.Position < endOfValue))
                {
                    var positionItem = source.Position;
                    if (!ParseItemSequenceTag(source, out var tag, out var length, endOfValue))
                    {
                        return;
                    }
                    // #64, in case explicit length has been specified despite occurrence of Sequence Delimitation Item
                    if (tag == DicomTag.SequenceDelimitationItem)
                    {
                        continue;
                    }

                    if (!await ParseItemSequenceValueAsync(source, tag, length, sequenceDepth, positionItem, endOfValue, handleBadPrivateSequence).ConfigureAwait(false))
                    {
                        return;
                    }
                }

                // end of explicit length sequence
                _observer.OnEndSequence();
            }

            private bool ParseItemSequenceTag(IByteSource source, out DicomTag tag, out uint length, long endOfValue)
            {
                tag = null;
                length = 0;
                long positionItemSequence = source.Position;

                if (!source.Require(8))
                {
                    _result = DicomReaderResult.Suspended;
                    return false;
                }

                var group = source.GetUInt16();
                var element = source.GetUInt16();

                tag = DicomTagsIndex.LookupOrCreate(group, element);

                if (tag != DicomTag.Item && tag != DicomTag.SequenceDelimitationItem)
                {
                    // assume invalid sequence
                    source.GoTo(positionItemSequence);
                    _observer.OnEndSequence();
                    return false;
                }

                length = source.GetUInt32();

                if (tag == DicomTag.SequenceDelimitationItem)
                {
                    // #64, in case explicit length has been specified despite occurrence of Sequence Delimitation Item
                    if ((endOfValue > 0 && source.Position >= endOfValue))
                    {
                        return true;
                    }

                    // end of sequence
                    _observer.OnEndSequence();
                    return false;
                }

                return true;
            }

            private bool ParseItemSequenceValue(IByteSource source, DicomTag tag, uint length, int sequenceDepth, long positionItem, long positionOfValueEnd, bool handleBadPrivateSequence)
            {
                long endOfValue = positionOfValueEnd;
                if (length != _undefinedLength)
                {
                    if (!source.Require(length))
                    {
                        _result = DicomReaderResult.Suspended;
                        return false;
                    }

                    endOfValue = source.Position + length;
                }

                _observer.OnBeginSequenceItem(source, positionItem, length);

                ParseDataset(source, out _, sequenceDepth + 1, endOfValue, handleBadPrivateSequence);
                // bugfix k-pacs. there a sequence was not ended by ItemDelimitationItem>SequenceDelimitationItem, but directly with SequenceDelimitationItem
                bool isEndSequence = (tag == DicomTag.SequenceDelimitationItem);

                _observer.OnEndSequenceItem();

                if (isEndSequence)
                {
                    // end of sequence
                    _observer.OnEndSequence();
                    return false;
                }

                return true;
            }

            private async Task<bool> ParseItemSequenceValueAsync(IByteSource source, DicomTag tag, uint length, int sequenceDepth, long positionItem, long positionEndOfValue, bool handleBadPrivateSequence)
            {
                long endOfValue = positionEndOfValue;
                if (length != _undefinedLength)
                {
                    if (!source.Require(length))
                    {
                        _result = DicomReaderResult.Suspended;
                        return false;
                    }

                    endOfValue = source.Position + length;
                }

                _observer.OnBeginSequenceItem(source, positionItem, length);

                await ParseDatasetAsync(source, sequenceDepth + 1, endOfValue, handleBadPrivateSequence).ConfigureAwait(false);
                // bugfix k-pacs. there a sequence was not ended by ItemDelimitationItem>SequenceDelimitationItem, but directly with SequenceDelimitationItem
                bool isEndSequence = (tag == DicomTag.SequenceDelimitationItem);

                _observer.OnEndSequenceItem();

                if (isEndSequence)
                {
                    // end of sequence
                    _observer.OnEndSequence();
                    return false;
                }

                return true;
            }

            private void ParseFragmentSequence(IByteSource source, DicomVR vr)
            {
                _result = DicomReaderResult.Processing;

                bool isFirstFragment = true;
                while (!source.IsEOF)
                {
                    var positionFragment = source.Position;
                    if (!ParseFragmentSequenceTag(source, out var length))
                    {
                        return;
                    }
                    if (!ParseFragmentSequenceValue(source, vr, length, positionFragment, isFirstFragment))
                    {
                        return;
                    }

                    isFirstFragment = false;

                    /*
                     * #1339
                     * Edge case: usually fragment sequences are ended with a SequenceDelimitationItem
                     * but sometimes this is missing.
                     * If this is the end of the DICOM file anyway, it is safe to end the fragment sequence
                     */
                    if (source.IsEOF)
                    {
                        _observer.OnEndFragmentSequence();
                    }
                }
            }

            private async Task ParseFragmentSequenceAsync(IByteSource source, DicomVR vr)
            {
                _result = DicomReaderResult.Processing;

                bool isFirstFragment = true;
                while (!source.IsEOF)
                {
                    var positionFragment = source.Position;
                    if (!ParseFragmentSequenceTag(source, out var length))
                    {
                        return;
                    }
                    if (!await ParseFragmentSequenceValueAsync(source, vr, length, positionFragment, isFirstFragment).ConfigureAwait(false))
                    {
                        return;
                    }

                    isFirstFragment = false;

                    /*
                     * #1339
                     * Edge case: usually fragment sequences are ended with a SequenceDelimitationItem
                     * but sometimes this is missing.
                     * If this is the end of the DICOM file anyway, it is safe to end the fragment sequence
                     */
                    if (source.IsEOF)
                    {
                        _observer.OnEndFragmentSequence();
                    }
                }
            }

            private bool ParseFragmentSequenceTag(IByteSource source, out uint length)
            {
                length = 0;

                if (!source.Require(8))
                {
                    _result = DicomReaderResult.Suspended;
                    return false;
                }

                var group = source.GetUInt16();
                var element = source.GetUInt16();

                if ((group, element) == (DicomTag.Item.Group, DicomTag.Item.Element))
                {
                    length = source.GetUInt32();
                    return true;
                }
                else if ((group, element) == (DicomTag.SequenceDelimitationItem.Group, DicomTag.SequenceDelimitationItem.Element))
                {
                    length = source.GetUInt32();

                    // end of fragment
                    _observer.OnEndFragmentSequence();
                    return false;
                }
                else
                {
                    throw new DicomReaderException($"Unexpected tag in DICOM fragment sequence: {new DicomTag(group, element)}");
                }
            }

            private bool ParseFragmentSequenceValue(IByteSource source, DicomVR vr, uint length, long positionFragment, bool firstFragment)
            {
                if (!source.Require(length))
                {
                    _result = DicomReaderResult.Suspended;
                    return false;
                }

                var buffer = source.GetBuffer(length);
                buffer = EndianByteBuffer.Create(buffer, source.Endian, firstFragment ? 4 : vr.UnitSize);
                _observer.OnFragmentSequenceItem(source, positionFragment, buffer);

                return true;
            }

            private async Task<bool> ParseFragmentSequenceValueAsync(IByteSource source, DicomVR vr, uint length, long positionFragment, bool firstFragment)
            {
                if (!source.Require(length))
                {
                    _result = DicomReaderResult.Suspended;
                    return false;
                }

                var buffer = await source.GetBufferAsync(length).ConfigureAwait(false);
                buffer = EndianByteBuffer.Create(buffer, source.Endian, firstFragment ? 4 : vr.UnitSize);
                _observer.OnFragmentSequenceItem(source, positionFragment, buffer);

                return true;
            }


            private static bool IsPrivateSequence(IByteSource source)
            {
                // TODO: peek
                var positionCurrent = source.Position;
                try
                {
                    var group = source.GetUInt16();
                    var element = source.GetUInt16();
                    if (
                        (group, element) == (DicomTag.Item.Group, DicomTag.Item.Element)
                        ||
                        (group, element) == (DicomTag.SequenceDelimitationItem.Group, DicomTag.SequenceDelimitationItem.Element)
                        )
                    {
                        return true;
                    }
                }
                finally
                {
                    source.GoTo(positionCurrent);
                }

                return false;
            }

            private bool IsPrivateSequenceBad(IByteSource source, uint count, bool isExplicitVR, IMemory vrMemory)
            {
                var positionCurrent = source.Position;
                try
                {
                    // Skip "item" tags; continue skipping until length is non-zero (#223)
                    // Using & instead of && enforces RHS to be evaluated regardless of LHS
                    uint length;
                    while (source.GetUInt16() == DicomTag.Item.Group &&
                           source.GetUInt16() == DicomTag.Item.Element &&
                           (length = source.GetUInt32()) < uint.MaxValue)   // Dummy condition to ensure that length is included in parsing
                    {
                        // Length non-zero, end skipping (#223)
                        if (length > 0)
                        {
                            break;
                        }

                        // Handle scenario where last tag is private sequence with empty items (#487)
                        count -= 8;
                        if (count <= 0)
                        {
                            return false;
                        }
                    }

                    source.GetUInt16(); // group
                    source.GetUInt16(); // element

                    if (source.GetBytes(vrMemory.Bytes, 0, 2) == 2 && DicomVR.TryParse(vrMemory.Bytes, out DicomVR _))
                    {
                        return !isExplicitVR;
                    }
                    // unable to parse VR
                    if (isExplicitVR)
                    {
                        return true;
                    }
                }
                finally
                {
                    source.GoTo(positionCurrent);
                }

                return false;
            }

            #endregion
        }

        #endregion
    }
}
