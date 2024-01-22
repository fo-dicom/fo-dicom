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
using System.Text;
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
        private class DicomReaderWorker
        {
            /// <summary>
            /// Available parse stages.
            /// </summary>
            private enum ParseStage
            {
                Tag,
                VR,
                Length,
                Value
            }

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

            private bool _isExplicitVR;

            private readonly bool _isDeflated;

            private int _sequenceDepth;

            private ParseStage _parseStage;

            private DicomTag _tag;

            private DicomDictionaryEntry _entry;

            private DicomVR _vr;

            private uint _length;

            private DicomReaderResult _result;

            private bool _implicit;

            private bool _badPrivateSequence;

            private int _badPrivateSequenceDepth;

            private int _fragmentItem;

            private readonly object _locker;
            
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
                _locker = new object();
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
                ResetState();
                source = ConvertSource(source);
                ParseDataset(source);

                if (_tag == DicomTag.SequenceDelimitationItem && _result == DicomReaderResult.Processing && source.IsEOF)
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
                ResetState();
                source = ConvertSource(source);
                await ParseDatasetAsync(source).ConfigureAwait(false);

                if (_tag == DicomTag.SequenceDelimitationItem && _result == DicomReaderResult.Processing && source.IsEOF)
                {
                    _result = DicomReaderResult.Success;
                }

                return _result;
            }

            private IByteSource ConvertSource(IByteSource source)
            {
                return _isDeflated ? Decompress(source) : source;
            }

            private void ParseDataset(IByteSource source)
            {
                _result = DicomReaderResult.Processing;

                using var vrMemory = _memoryProvider.Provide(2);
                while (!source.IsEOF && !source.HasReachedMilestone() && _result == DicomReaderResult.Processing)
                {
                    if (!ParseTag(source))
                    {
                        return;
                    }
                    if (!ParseVR(source, vrMemory))
                    {
                        return;
                    }
                    if (!ParseLength(source))
                    {
                        return;
                    }
                    if (!ParseValue(source, vrMemory))
                    {
                        return;
                    }
                }

                if (source.HasReachedMilestone())
                {
                    // end of explicit length sequence item
                    source.PopMilestone();
                    return;
                }

                if (_result != DicomReaderResult.Processing)
                {
                    return;
                }

                // end of processing
                _result = DicomReaderResult.Success;
            }

            private async Task ParseDatasetAsync(IByteSource source)
            {
                _result = DicomReaderResult.Processing;

                using var vrMemory = _memoryProvider.Provide(2);
                while (!source.IsEOF && !source.HasReachedMilestone() && _result == DicomReaderResult.Processing)
                {
                    if (!ParseTag(source))
                    {
                        return;
                    }
                    if (!ParseVR(source, vrMemory))
                    {
                        return;
                    }
                    if (!ParseLength(source))
                    {
                        return;
                    }
                    if (!await ParseValueAsync(source, vrMemory).ConfigureAwait(false))
                    {
                        return;
                    }
                }

                if (source.HasReachedMilestone())
                {
                    // end of explicit length sequence item
                    source.PopMilestone();
                    return;
                }

                if (_result != DicomReaderResult.Processing)
                {
                    return;
                }

                // end of processing
                _result = DicomReaderResult.Success;
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


            private bool ParseTag(IByteSource source)
            {
                if (_parseStage == ParseStage.Tag)
                {
                    source.Mark();

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
                    // So element at [0x0100-0x0FFF] should not has a creator
                    if (@group.IsOdd() && element >= 0x1000)
                    {
                        var card = (uint)(@group << 16) + (uint)(element >> 8);
                        lock (_locker)
                        {
                            if (_private.TryGetValue(card, out string pvt))
                            {
                                creator = _dictionary.GetPrivateCreator(pvt);
                            }
                        }
                    }

                    _tag = creator == null 
                        ? DicomTagsIndex.LookupOrCreate(group, element)
                        : new DicomTag(@group, element, creator);
                    _entry = _dictionary[_tag];

                    if (!_tag.IsPrivate && _entry != null && _entry.MaskTag == null)
                    {
                        _tag = _entry.Tag; // Use dictionary tag
                    }

                    if (_stop != null 
                        && _stop(new ParseState { PreviousTag = _previousTag, Tag = _tag, SequenceDepth = _sequenceDepth }))
                    {
                        _result = DicomReaderResult.Stopped;
                        return false;
                    }
                    
                    _previousTag = _tag;

                    _parseStage = ParseStage.VR;
                }
                return true;
            }

            private bool ParseVR(IByteSource source, IMemory vrMemory)
            {
                while (_parseStage == ParseStage.VR)
                {
                    if (_tag == DicomTag.Item || _tag == DicomTag.ItemDelimitationItem
                        || _tag == DicomTag.SequenceDelimitationItem)
                    {
                        _vr = DicomVR.NONE;
                        _parseStage = ParseStage.Length;
                        break;
                    }

                    if (_isExplicitVR || _badPrivateSequence)
                    {
                        if (!source.Require(2))
                        {
                            _result = DicomReaderResult.Suspended;
                            return false;
                        }

                        source.Mark();

                        if (source.GetBytes(vrMemory.Bytes, 0, 2) != 2 || !DicomVR.TryParse(vrMemory.Bytes, out _vr))
                        {
                            // If the VR is an empty string, try to use the first known VR of the tag
                            string vr = Encoding.UTF8.GetString(vrMemory.Bytes, 0, 2).Trim();
                            if(_entry != null && string.IsNullOrEmpty(vr))
                            {
                                _vr = _entry.ValueRepresentations.FirstOrDefault();    
                            }
                            else
                            {
                                // unable to parse VR; rewind VR bytes for continued attempt to interpret the data.
                                _vr = DicomVR.Implicit;
                                source.Rewind();
                            }
                        }
                    }
                    else
                    {
                        if (_entry != null)
                        {
                            if (_entry == DicomDictionary.UnknownTag)
                            {
                                _vr = DicomVR.UN;
                            }
                            else if (_entry.ValueRepresentations.Contains(DicomVR.OB)
                                     && _entry.ValueRepresentations.Contains(DicomVR.OW))
                            {
                                _vr = DicomVR.OW; // ???
                            }
                            else
                            {
                                _vr = _entry.ValueRepresentations.FirstOrDefault();
                            }
                        }
                    }

                    if (_vr == null)
                    {
                        _vr = DicomVR.UN;
                    }

                    _parseStage = ParseStage.Length;

                    if (_vr == DicomVR.UN)
                    {
                        if (_tag.Element == 0x0000)
                        {
                            // Group Length to UL
                            // change 20161216: if changing from UN to UL then ParseLength causes a error, since length in UL is 2 bytes while length in UN is 6 bytes. 
                            // so the source hat UN and coded the length in 6 bytes. if here the VR was changed to UL then ParseLength would only read 2 bytes and the parser is then wrong.
                            // but no worry: in ParseValue in the first lines there is a lookup in the Dictionary of DicomTags and there the VR is changed to UL so that the value is finally interpreted correctly as UL.
                            // _vr = DicomVR.UL;
                            break;
                        }
                        if (_isExplicitVR)
                        {
                            break;
                        }
                    }

                    if (_tag.IsPrivate)
                    {
                        // according to
                        // http://dicom.nema.org/medical/dicom/current/output/chtml/part05/sect_7.8.html
                        // Private Creator Data Elements numbered (gggg,0010-00FF) (gggg is odd)
                        // The VR of the private identification code shall be LO (Long String) and the VM shall be equal to 1.
                        if (_tag.Element >= 0x0010 && _tag.Element <= 0x00ff && _vr == DicomVR.UN)
                        {
                            _vr = DicomVR.LO; // force private creator to LO
                        }
                    }
                }
                return true;
            }

            private bool ParseLength(IByteSource source)
            {
                while (_parseStage == ParseStage.Length)
                {
                    if (_tag == DicomTag.Item || _tag == DicomTag.ItemDelimitationItem
                        || _tag == DicomTag.SequenceDelimitationItem)
                    {
                        if (!source.Require(4))
                        {
                            _result = DicomReaderResult.Suspended;
                            return false;
                        }

                        _length = source.GetUInt32();

                        _parseStage = ParseStage.Value;
                        break;
                    }

                    if (_isExplicitVR || _badPrivateSequence)
                    {
                        if (_vr == DicomVR.Implicit)
                        {
                            if (!source.Require(4))
                            {
                                _result = DicomReaderResult.Suspended;
                                return false;
                            }

                            _length = source.GetUInt32();

                            // assume that undefined length in implicit VR element is SQ
                            if (_length == _undefinedLength)
                            {
                                _vr = DicomVR.SQ;
                            }
                        }
                        else if (_vr.Is16bitLength)
                        {
                            if (!source.Require(2))
                            {
                                _result = DicomReaderResult.Suspended;
                                return false;
                            }

                            _length = source.GetUInt16();
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
                            if (_vr == DicomVR.UV || _vr == DicomVR.SV)
                            {
                                _length = source.GetUInt16();
                            }
                            else
                            {
                                source.Skip(2);
                            }

                            if (_length == 0)
                            {
                                _length = source.GetUInt32();

                                // CP-246 (#177) handling
                                // assume that Undefined Length in explicit datasets with VR UN are sequences 
                                // According to CP-246 the sequence shall be handled as ILE, but this will be handled later...
                                // in the current code this needs to be restricted to privates 
                                if (_length == _undefinedLength && _vr == DicomVR.UN && _tag.IsPrivate)
                                {
                                    _vr = DicomVR.SQ;
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

                        _length = source.GetUInt32();

                        // assume that undefined length in implicit dataset is SQ
                        if (_length == _undefinedLength && _vr == DicomVR.UN)
                        {
                            _vr = DicomVR.SQ;
                        }
                    }

                    _parseStage = ParseStage.Value;
                }
                return true;
            }

            private bool ParseValue(IByteSource source, IMemory vrMemory)
            {
                if (_parseStage == ParseStage.Value)
                {
                    // check dictionary for VR after reading length to handle 16-bit lengths
                    // check before reading value to handle SQ elements
                    var parsedVR = _vr;

                    // check dictionary for VR after reading length to handle 16-bit lengths
                    // check before reading value to handle SQ elements
                    if (_vr == DicomVR.Implicit || (_vr == DicomVR.UN && _isExplicitVR))
                    {
                        var entry = _dictionary[_tag];
                        if (entry != null)
                        {
                            _vr = entry.ValueRepresentations.FirstOrDefault();
                        }

                        if (_vr == null)
                        {
                            _vr = DicomVR.UN;
                        }
                    }

                    if (_tag == DicomTag.ItemDelimitationItem || _tag == DicomTag.SequenceDelimitationItem)
                    {
                        // end of sequence item
                        return false;
                    }

                    while (_vr == DicomVR.SQ && _tag.IsPrivate && _length > 0)
                    {
                        if (!IsPrivateSequence(source))
                        {
                            _vr = DicomVR.UN;
                            break;
                        }

                        if (IsPrivateSequenceBad(source, _length, _isExplicitVR, vrMemory))
                        {
                            _badPrivateSequence = true;
                            // store the depth of the bad sequence, we only want to switch back once we've processed
                            // the entire sequence, regardless of any sub-sequences.
                            _badPrivateSequenceDepth = _sequenceDepth;
                            _isExplicitVR = !_isExplicitVR;
                        }
                        break;
                    }

                    var curIndex = source.Position;
                    // Fix to handle sequence items not associated with any sequence (#364)
                    if (_tag.Equals(DicomTag.Item))
                    {
                        source.Rewind();
                        _vr = DicomVR.SQ;
                    }

                    if (_vr == DicomVR.SQ)
                    {
                        // start of sequence
                        _observer.OnBeginSequence(source, _tag, _length);
                        _parseStage = ParseStage.Tag;
                        if (_length == 0)
                        {
                            _implicit = false;
                            source.PushMilestone((uint)(source.Position - curIndex));
                        }
                        else if (_length != _undefinedLength)
                        {
                            _implicit = false;
                            source.PushMilestone(_length);
                        }
                        else
                        {
                            _implicit = true;
                        }
                        var last = source.Position;

                        // Conformance with CP-246 (#177)
                        var needtoChangeEndian = false;
                        if (parsedVR == DicomVR.UN && !_tag.IsPrivate)
                        {
                            _implicit = true;
                            needtoChangeEndian = source.Endian == Endian.Big;
                        }
                        if (needtoChangeEndian)
                        {
                            source.Endian = Endian.Little;
                        }

                        ParseItemSequence(source);

                        if (needtoChangeEndian)
                        {
                            source.Endian = Endian.Big;
                        }

                        // Aeric Sylvan - https://github.com/rcd/fo-dicom/issues/62#issuecomment-46248073
                        // Fix reading of SQ with parsed VR of UN
                        if (source.Position > last || _length == 0)
                        {
                            return true;
                        }

                        _parseStage = ParseStage.Value;
                        _vr = parsedVR;
                    }

                    if (_length == _undefinedLength)
                    {
                        _observer.OnBeginFragmentSequence(source, _tag, _vr);
                        _parseStage = ParseStage.Tag;
                        ParseFragmentSequence(source);
                        return true;
                    }

                    if (!source.Require(_length))
                    {
                        _result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var buffer = source.GetBuffer(_length);

                    if (buffer != null)
                    {
                        if (!_vr.IsString)
                        {
                            buffer = EndianByteBuffer.Create(buffer, source.Endian, _vr.UnitSize);
                        }
                        _observer.OnElement(source, _tag, _vr, buffer);
                    }

                    // parse private creator value and add to lookup table
                    // according to
                    // http://dicom.nema.org/medical/dicom/current/output/chtml/part05/sect_7.8.html
                    // Private Creator Data Elements numbered (gggg,0010-00FF) (gggg is odd)
                    // The VR of the private identification code shall be LO (Long String) and the VM shall be equal to 1.
                    if (_tag.IsPrivate && _tag.Element >= 0x0010 && _tag.Element <= 0x00ff)
                    {
                        var creator =
                            DicomEncoding.Default.GetString(buffer.Data, 0, buffer.Data.Length)
                                .TrimEnd((char)DicomVR.LO.PaddingValue);
                        var card = (uint)(_tag.Group << 16) + _tag.Element;

                        lock (_locker)
                        {
                            _private[card] = creator;
                        }
                    }

                    ResetState();
                }
                return true;
            }

            private async Task<bool> ParseValueAsync(IByteSource source, IMemory vrMemory)
            {
                if (_parseStage == ParseStage.Value)
                {
                    // check dictionary for VR after reading length to handle 16-bit lengths
                    // check before reading value to handle SQ elements
                    var parsedVR = _vr;

                    // check dictionary for VR after reading length to handle 16-bit lengths
                    // check before reading value to handle SQ elements
                    if (_vr == DicomVR.Implicit || (_vr == DicomVR.UN && _isExplicitVR))
                    {
                        var entry = _dictionary[_tag];
                        if (entry != null)
                        {
                            _vr = entry.ValueRepresentations.FirstOrDefault();
                        }

                        if (_vr == null)
                        {
                            _vr = DicomVR.UN;
                        }
                    }

                    if (_tag == DicomTag.ItemDelimitationItem || _tag == DicomTag.SequenceDelimitationItem)
                    {
                        // end of sequence item
                        return false;
                    }

                    while (_vr == DicomVR.SQ && _tag.IsPrivate && _length > 0)
                    {
                        if (!IsPrivateSequence(source))
                        {
                            _vr = DicomVR.UN;
                            break;
                        }

                        if (IsPrivateSequenceBad(source, _length, _isExplicitVR, vrMemory))
                        {
                            _badPrivateSequence = true;
                            // store the depth of the bad sequence, we only want to switch back once we've processed
                            // the entire sequence, regardless of any sub-sequences.
                            _badPrivateSequenceDepth = _sequenceDepth;
                            _isExplicitVR = !_isExplicitVR;
                        }
                        break;
                    }

                    var curIndex = source.Position;
                    // Fix to handle sequence items not associated with any sequence (#364)
                    if (_tag.Equals(DicomTag.Item))
                    {
                        source.Rewind();
                        _vr = DicomVR.SQ;
                    }

                    if (_vr == DicomVR.SQ)
                    {
                        // start of sequence
                        _observer.OnBeginSequence(source, _tag, _length);
                        _parseStage = ParseStage.Tag;
                        if (_length == 0)
                        {
                            _implicit = false;
                            source.PushMilestone((uint)(source.Position - curIndex));
                        }
                        else if (_length != _undefinedLength)
                        {
                            _implicit = false;
                            source.PushMilestone(_length);
                        }
                        else
                        {
                            _implicit = true;
                        }
                        var last = source.Position;

                        // Conformance with CP-246 (#177)
                        var needtoChangeEndian = false;
                        if (parsedVR == DicomVR.UN && !_tag.IsPrivate)
                        {
                            _implicit = true;
                            needtoChangeEndian = source.Endian == Endian.Big;
                        }
                        if (needtoChangeEndian)
                        {
                            source.Endian = Endian.Little;
                        }

                        await ParseItemSequenceAsync(source).ConfigureAwait(false);

                        if (needtoChangeEndian)
                        {
                            source.Endian = Endian.Big;
                        }

                        // Aeric Sylvan - https://github.com/rcd/fo-dicom/issues/62#issuecomment-46248073
                        // Fix reading of SQ with parsed VR of UN
                        if (source.Position > last || _length == 0)
                        {
                            return true;
                        }

                        _parseStage = ParseStage.Value;
                        _vr = parsedVR;
                    }

                    if (_length == _undefinedLength)
                    {
                        _observer.OnBeginFragmentSequence(source, _tag, _vr);
                        _parseStage = ParseStage.Tag;
                        await ParseFragmentSequenceAsync(source).ConfigureAwait(false);
                        return true;
                    }

                    if (!source.Require(_length))
                    {
                        _result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var buffer = await source.GetBufferAsync(_length).ConfigureAwait(false);

                    if (!_vr.IsString)
                    {
                        buffer = EndianByteBuffer.Create(buffer, source.Endian, _vr.UnitSize);
                    }
                    _observer.OnElement(source, _tag, _vr, buffer);

                    // parse private creator value and add to lookup table
                    // according to
                    // http://dicom.nema.org/medical/dicom/current/output/chtml/part05/sect_7.8.html
                    // Private Creator Data Elements numbered (gggg,0010-00FF) (gggg is odd)
                    // The VR of the private identification code shall be LO (Long String) and the VM shall be equal to 1.
                    if (_tag.IsPrivate && _tag.Element >= 0x0010 && _tag.Element <= 0x00ff)
                    {
                        var creator =
                            DicomEncoding.Default.GetString(buffer.Data, 0, buffer.Data.Length)
                                .TrimEnd((char)DicomVR.LO.PaddingValue);
                        var card = (uint)(_tag.Group << 16) + _tag.Element;

                        lock (_locker)
                        {
                            _private[card] = creator;
                        }
                    }

                    ResetState();
                }
                return true;
            }

            private void ParseItemSequence(IByteSource source)
            {
                _result = DicomReaderResult.Processing;

                while (!source.IsEOF && !source.HasReachedMilestone())
                {
                    if (!ParseItemSequenceTag(source))
                    {
                        return;
                    }
                    if (!ParseItemSequenceValue(source))
                    {
                        return;
                    }
                }

                ParseItemSequencePostProcess(source);
            }

            private async Task ParseItemSequenceAsync(IByteSource source)
            {
                _result = DicomReaderResult.Processing;

                while (!source.IsEOF && !source.HasReachedMilestone())
                {
                    if (!ParseItemSequenceTag(source))
                    {
                        return;
                    }
                    if (!await ParseItemSequenceValueAsync(source).ConfigureAwait(false))
                    {
                        return;
                    }
                }

                ParseItemSequencePostProcess(source);
            }

            private bool ParseItemSequenceTag(IByteSource source)
            {
                if (_parseStage == ParseStage.Tag)
                {
                    source.Mark();

                    if (!source.Require(8))
                    {
                        _result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var group = source.GetUInt16();
                    var element = source.GetUInt16();

                    _tag = DicomTagsIndex.LookupOrCreate(group, element);

                    if (_tag != DicomTag.Item && _tag != DicomTag.SequenceDelimitationItem)
                    {
                        // assume invalid sequence
                        source.Rewind();
                        if (!_implicit)
                        {
                            source.PopMilestone();
                        }
                        _observer.OnEndSequence();
                        // #565 Only reset the badPrivate sequence if we're in the correct depth
                        // This prevents prematurely resetting in case of sub-sequences contained in the bad private sequence
                        if (_badPrivateSequence && _sequenceDepth == _badPrivateSequenceDepth)
                        {
                            _isExplicitVR = !_isExplicitVR;
                            _badPrivateSequence = false;
                        }
                        return false;
                    }

                    _length = source.GetUInt32();

                    if (_tag == DicomTag.SequenceDelimitationItem)
                    {
                        // #64, in case explicit length has been specified despite occurrence of Sequence Delimitation Item
                        if (source.HasReachedMilestone() && source.MilestonesCount > _sequenceDepth)
                        {
                            ResetState();
                            return true;
                        }

                        // end of sequence
                        _observer.OnEndSequence();
                        // #565 Only reset the badPrivate sequence if we're in the correct depth
                        // This prevents prematurely resetting in case of sub-sequences contained in the bad private sequence
                        if (_badPrivateSequence && _sequenceDepth == _badPrivateSequenceDepth)
                        {
                            _isExplicitVR = !_isExplicitVR;
                            _badPrivateSequence = false;
                        }
                        ResetState();
                        return false;
                    }

                    _parseStage = ParseStage.Value;
                }
                return true;
            }

            private bool ParseItemSequenceValue(IByteSource source)
            {
                if (_parseStage == ParseStage.Value)
                {
                    if (_length != _undefinedLength)
                    {
                        if (!source.Require(_length))
                        {
                            _result = DicomReaderResult.Suspended;
                            return false;
                        }

                        source.PushMilestone(_length);
                    }

                    _observer.OnBeginSequenceItem(source, _length);

                    ResetState();
                    ++_sequenceDepth;
                    ParseDataset(source);
                    --_sequenceDepth;
                    // bugfix k-pacs. there a sequence was not ended by ItemDelimitationItem>SequenceDelimitationItem, but directly with SequenceDelimitationItem
                    bool isEndSequence = (_tag == DicomTag.SequenceDelimitationItem);
                    ResetState();

                    _observer.OnEndSequenceItem();

                    if (isEndSequence)
                    {
                        // end of sequence
                        _observer.OnEndSequence();
                        // #565 Only reset the badPrivate sequence if we're in the correct depth
                        // This prevents prematurely resetting in case of sub-sequences contained in the bad private sequence
                        if (_badPrivateSequence && _sequenceDepth == _badPrivateSequenceDepth)
                        {
                            _isExplicitVR = !_isExplicitVR;
                            _badPrivateSequence = false;
                        }
                        ResetState();
                        return false;
                    }
                }
                return true;
            }

            private async Task<bool> ParseItemSequenceValueAsync(IByteSource source)
            {
                if (_parseStage == ParseStage.Value)
                {
                    if (_length != _undefinedLength)
                    {
                        if (!source.Require(_length))
                        {
                            _result = DicomReaderResult.Suspended;
                            return false;
                        }

                        source.PushMilestone(_length);
                    }

                    _observer.OnBeginSequenceItem(source, _length);

                    ResetState();
                    ++_sequenceDepth;
                    await ParseDatasetAsync(source).ConfigureAwait(false);
                    --_sequenceDepth;
                    // bugfix k-pacs. there a sequence was not ended by ItemDelimitationItem>SequenceDelimitationItem, but directly with SequenceDelimitationItem
                    bool isEndSequence = (_tag == DicomTag.SequenceDelimitationItem);
                    ResetState();

                    _observer.OnEndSequenceItem();

                    if (isEndSequence)
                    {
                        // end of sequence
                        _observer.OnEndSequence();
                        // #565 Only reset the badPrivate sequence if we're in the correct depth
                        // This prevents prematurely resetting in case of sub-sequences contained in the bad private sequence
                        if (_badPrivateSequence && _sequenceDepth == _badPrivateSequenceDepth)
                        {
                            _isExplicitVR = !_isExplicitVR;
                            _badPrivateSequence = false;
                        }
                        ResetState();
                        return false;
                    }
                }
                return true;
            }

            private void ParseItemSequencePostProcess(IByteSource source)
            {
                // end of explicit length sequence
                if (source.HasReachedMilestone())
                {
                    source.PopMilestone();
                }

                _observer.OnEndSequence();
                // #565 Only reset the badPrivate sequence if we're in the correct depth
                // This prevents prematurely resetting in case of sub-sequences contained in the bad private sequence
                if (_badPrivateSequence && _sequenceDepth == _badPrivateSequenceDepth)
                {
                    _isExplicitVR = !_isExplicitVR;
                    _badPrivateSequence = false;
                }
            }

            private void ParseFragmentSequence(IByteSource source)
            {
                _result = DicomReaderResult.Processing;

                while (!source.IsEOF)
                {
                    if (!ParseFragmentSequenceTag(source))
                    {
                        return;
                    }
                    if (!ParseFragmentSequenceValue(source))
                    {
                        return;
                    }
                    
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

            private async Task ParseFragmentSequenceAsync(IByteSource source)
            {
                _result = DicomReaderResult.Processing;

                while (!source.IsEOF)
                {
                    if (!ParseFragmentSequenceTag(source))
                    {
                        return;
                    }
                    if (!await ParseFragmentSequenceValueAsync(source).ConfigureAwait(false))
                    {
                        return;
                    }
                    
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

            private bool ParseFragmentSequenceTag(IByteSource source)
            {
                if (_parseStage == ParseStage.Tag)
                {
                    source.Mark();

                    if (!source.Require(8))
                    {
                        _result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var group = source.GetUInt16();
                    var element = source.GetUInt16();

                    var tag = new DicomTag(@group, element);

                    if (tag != DicomTag.Item && tag != DicomTag.SequenceDelimitationItem)
                    {
                        throw new DicomReaderException($"Unexpected tag in DICOM fragment sequence: {tag}");
                    }

                    _length = source.GetUInt32();

                    if (tag == DicomTag.SequenceDelimitationItem)
                    {
                        // end of fragment
                        _observer.OnEndFragmentSequence();
                        _fragmentItem = 0;
                        ResetState();
                        return false;
                    }

                    _fragmentItem++;
                    _parseStage = ParseStage.Value;
                }
                return true;
            }

            private bool ParseFragmentSequenceValue(IByteSource source)
            {
                if (_parseStage == ParseStage.Value)
                {
                    if (!source.Require(_length))
                    {
                        _result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var buffer = source.GetBuffer(_length);
                    buffer = EndianByteBuffer.Create(buffer, source.Endian, _fragmentItem == 1 ? 4 : _vr.UnitSize);
                    _observer.OnFragmentSequenceItem(source, buffer);

                    _parseStage = ParseStage.Tag;
                }
                return true;
            }

            private async Task<bool> ParseFragmentSequenceValueAsync(IByteSource source)
            {
                if (_parseStage == ParseStage.Value)
                {
                    if (!source.Require(_length))
                    {
                        _result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var buffer = await source.GetBufferAsync(_length).ConfigureAwait(false);
                    buffer = EndianByteBuffer.Create(buffer, source.Endian, _fragmentItem == 1 ? 4 : _vr.UnitSize);
                    _observer.OnFragmentSequenceItem(source, buffer);

                    _parseStage = ParseStage.Tag;
                }
                return true;
            }

            private void ResetState()
            {
                _parseStage = ParseStage.Tag;
                _tag = null;
                _entry = null;
                _vr = null;
                _length = 0;
            }

            private static bool IsPrivateSequence(IByteSource source)
            {
                source.Mark();

                try
                {
                    var group = source.GetUInt16();
                    var element = source.GetUInt16();
                    var tag = new DicomTag(group, element);

                    if (tag == DicomTag.Item || tag == DicomTag.SequenceDelimitationItem)
                    {
                        return true;
                    }
                }
                finally
                {
                    source.Rewind();
                }

                return false;
            }

            private bool IsPrivateSequenceBad(IByteSource source, uint count, bool isExplicitVR, IMemory vrMemory)
            {
                source.Mark();

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

                    if (source.GetBytes(vrMemory.Bytes, 0, 2) == 2 && DicomVR.TryParse(vrMemory.Bytes, out DicomVR dummy))
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
                    source.Rewind();
                }

                return false;
            }

            #endregion
        }

        #endregion
    }
}
