// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Reader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

#if NET35
    using Unity.IO.Compression;
#else
    using System.IO.Compression;
    using System.Threading.Tasks;
#endif

    using Dicom.Imaging.Mathematics;
    using Dicom.IO.Buffer;

    /// <summary>
    /// DICOM reader implementation.
    /// </summary>
    public class DicomReader : IDicomReader
    {
        #region FIELDS

        private readonly Dictionary<uint, string> _private;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of <see cref="DicomReader"/>.
        /// </summary>
        public DicomReader()
        {
            _private = new Dictionary<uint, string>();
            this.Dictionary = DicomDictionary.Default;
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
            var worker = new DicomReaderWorker(observer, stop, this.Dictionary, this.IsExplicitVR, this.IsDeflated, this._private);
            return worker.DoWork(source);
        }

#if !NET35
        /// <summary>
        /// Asynchronously perform DICOM reading of a byte source.
        /// </summary>
        /// <param name="source">Byte source to read.</param>
        /// <param name="observer">Reader observer.</param>
        /// <param name="stop">Criterion at which to stop.</param>
        /// <returns>Awaitable reader resulting status.</returns>
        public Task<DicomReaderResult> ReadAsync(IByteSource source, IDicomReaderObserver observer, Func<ParseState, bool> stop = null)
        {
            var worker = new DicomReaderWorker(observer, stop, this.Dictionary, this.IsExplicitVR, this.IsDeflated, this._private);
            return worker.DoWorkAsync(source);
        }
#endif
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
            private const uint UndefinedLength = 0xffffffff;

            private readonly IDicomReaderObserver observer;

            private readonly Func<ParseState, bool> stop;

            private readonly DicomDictionary dictionary;

            private readonly Dictionary<uint, string> _private;

            private bool isExplicitVR;

            private bool isDeflated;

            private int sequenceDepth;

            private ParseStage parseStage;

            private DicomTag _tag;

            private DicomDictionaryEntry _entry;

            private DicomVR _vr;

            private uint length;

            private DicomReaderResult result;

            private bool _implicit;

            private bool badPrivateSequence;

            private int badPrivateSequenceDepth;

            private int fragmentItem;

            private readonly object locker;

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
                Dictionary<uint, string> @private)
            {
                this.observer = observer;
                this.stop = stop;
                this.dictionary = dictionary;
                this.isExplicitVR = isExplicitVR;
                this.isDeflated = isDeflated;
                this._private = @private;
                this.locker = new object();
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
                this.ResetState();
                this.ParseDataset(source);
                return this.result;
            }

#if !NET35
            /// <summary>
            /// Asynchronously read the byte source.
            /// </summary>
            /// <param name="source">Byte source to read.</param>
            /// <returns>Awaitable read result.</returns>
            internal async Task<DicomReaderResult> DoWorkAsync(IByteSource source)
            {
                this.ResetState();
                await this.ParseDatasetAsync(source).ConfigureAwait(false);
                return this.result;
            }
#endif

            private void ParseDataset(IByteSource source)
            {
                if (this.isDeflated)
                {
                    source = this.Decompress(source);
                }

                this.result = DicomReaderResult.Processing;

                while (!source.IsEOF && !source.HasReachedMilestone() && this.result == DicomReaderResult.Processing)
                {
                    if (!this.ParseTag(source)) return;
                    if (!this.ParseVR(source)) return;
                    if (!this.ParseLength(source)) return;
                    if (!this.ParseValue(source)) return;
                }

                if (source.HasReachedMilestone())
                {
                    // end of explicit length sequence item
                    source.PopMilestone();
                    return;
                }

                if (this.result != DicomReaderResult.Processing) return;

                // end of processing
                this.result = DicomReaderResult.Success;
            }

#if !NET35
            private async Task ParseDatasetAsync(IByteSource source)
            {
                if (this.isDeflated)
                {
                    source = this.Decompress(source);
                }

                this.result = DicomReaderResult.Processing;

                while (!source.IsEOF && !source.HasReachedMilestone() && this.result == DicomReaderResult.Processing)
                {
                    if (!this.ParseTag(source)) return;
                    if (!this.ParseVR(source)) return;
                    if (!this.ParseLength(source)) return;
                    if (!await this.ParseValueAsync(source).ConfigureAwait(false)) return;
                }

                if (source.HasReachedMilestone())
                {
                    // end of explicit length sequence item
                    source.PopMilestone();
                    return;
                }

                if (this.result != DicomReaderResult.Processing) return;

                // end of processing
                this.result = DicomReaderResult.Success;
            }
#endif

            private IByteSource Decompress(IByteSource source)
            {
                using (var compressed = new MemoryStream())
                {
                    // It is implicitly assumed that the rest of the byte source is compressed.
                    while (!source.IsEOF)
                    {
                        compressed.WriteByte(source.GetUInt8());
                    }

                    compressed.Seek(0, SeekOrigin.Begin);

                    var decompressed = new MemoryStream();
                    using (var decompressor = new DeflateStream(compressed, CompressionMode.Decompress, true))
                    {
                        decompressor.CopyTo(decompressed);
                    }

                    decompressed.Seek(0, SeekOrigin.Begin);
                    return new StreamByteSource(decompressed);
                }
            }

            private bool ParseTag(IByteSource source)
            {
                if (this.parseStage == ParseStage.Tag)
                {
                    source.Mark();

                    if (!source.Require(4))
                    {
                        this.result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var group = source.GetUInt16();
                    var element = source.GetUInt16();
                    DicomPrivateCreator creator = null;

                    if (@group.IsOdd() && element > 0x00ff)
                    {
                        var card = (uint)(@group << 16) + (uint)(element >> 8);
                        lock (this.locker)
                        {
                            string pvt;
                            if (this._private.TryGetValue(card, out pvt))
                            {
                                creator = this.dictionary.GetPrivateCreator(pvt);
                            }
                        }
                    }

                    this._tag = new DicomTag(@group, element, creator);
                    this._entry = this.dictionary[this._tag];

                    if (!this._tag.IsPrivate && this._entry != null && this._entry.MaskTag == null)
                    {
                        this._tag = this._entry.Tag; // Use dictionary tag
                    }

                    if (this.stop != null
                        && this.stop(new ParseState { Tag = this._tag, SequenceDepth = this.sequenceDepth }))
                    {
                        this.result = DicomReaderResult.Stopped;
                        return false;
                    }

                    this.parseStage = ParseStage.VR;
                }
                return true;
            }

            private bool ParseVR(IByteSource source)
            {
                while (this.parseStage == ParseStage.VR)
                {
                    if (this._tag == DicomTag.Item || this._tag == DicomTag.ItemDelimitationItem
                        || this._tag == DicomTag.SequenceDelimitationItem)
                    {
                        this._vr = DicomVR.NONE;
                        this.parseStage = ParseStage.Length;
                        break;
                    }

                    if (this.isExplicitVR || this.badPrivateSequence)
                    {
                        if (!source.Require(2))
                        {
                            this.result = DicomReaderResult.Suspended;
                            return false;
                        }

                        source.Mark();
                        var bytes = source.GetBytes(2);
                        var vr = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

                        if (string.IsNullOrEmpty(vr.Trim()))
                        {
                            if (this._entry != null)
                            {
                                this._vr = this._entry.ValueRepresentations.FirstOrDefault();
                            }
                        }
                        else if (!DicomVR.TryParse(vr, out this._vr))
                        {
                            // unable to parse VR; rewind VR bytes for continued attempt to interpret the data.
                            this._vr = DicomVR.Implicit;
                            source.Rewind();
                        }
                    }
                    else
                    {
                        if (this._entry != null)
                        {
                            if (this._entry == DicomDictionary.UnknownTag)
                            {
                                this._vr = DicomVR.UN;
                            }
                            else if (this._entry.ValueRepresentations.Contains(DicomVR.OB)
                                     && this._entry.ValueRepresentations.Contains(DicomVR.OW))
                            {
                                this._vr = DicomVR.OW; // ???
                            }
                            else
                            {
                                this._vr = this._entry.ValueRepresentations.FirstOrDefault();
                            }
                        }
                    }

                    if (this._vr == null)
                    {
                        this._vr = DicomVR.UN;
                    }

                    this.parseStage = ParseStage.Length;

                    if (this._vr == DicomVR.UN)
                    {
                        if (this._tag.Element == 0x0000)
                        {
                            // Group Length to UL
                            // change 20161216: if changing from UN to UL then ParseLength causes a error, since length in UL is 2 bytes while length in UN is 6 bytes. 
                            // so the source hat UN and coded the length in 6 bytes. if here the VR was changed to UL then ParseLength would only read 2 bytes and the parser is then wrong.
                            // but no worry: in ParseValue in the first lines there is a lookup in the Dictionary of DicomTags and there the VR is changed to UL so that the value is finally interpreted correctly as UL.
                           // this._vr = DicomVR.UL;
                            break;
                        }
                        if (this.isExplicitVR)
                        {
                            break;
                        }
                    }

                    if (this._tag.IsPrivate)
                    {
                        if (this._tag.Element != 0x0000 && this._tag.Element <= 0x00ff && this._vr == DicomVR.UN)
                        {
                            this._vr = DicomVR.LO; // force private creator to LO
                        }
                    }
                }
                return true;
            }

            private bool ParseLength(IByteSource source)
            {
                while (this.parseStage == ParseStage.Length)
                {
                    if (this._tag == DicomTag.Item || this._tag == DicomTag.ItemDelimitationItem
                        || this._tag == DicomTag.SequenceDelimitationItem)
                    {
                        if (!source.Require(4))
                        {
                            this.result = DicomReaderResult.Suspended;
                            return false;
                        }

                        this.length = source.GetUInt32();

                        this.parseStage = ParseStage.Value;
                        break;
                    }

                    if (this.isExplicitVR || this.badPrivateSequence)
                    {
                        if (this._vr == DicomVR.Implicit)
                        {
                            if (!source.Require(4))
                            {
                                this.result = DicomReaderResult.Suspended;
                                return false;
                            }

                            this.length = source.GetUInt32();

                            // assume that undefined length in implicit VR element is SQ
                            if (this.length == UndefinedLength)
                            {
                                this._vr = DicomVR.SQ;
                            }
                        }
                        else if (this._vr.Is16bitLength)
                        {
                            if (!source.Require(2))
                            {
                                this.result = DicomReaderResult.Suspended;
                                return false;
                            }

                            this.length = source.GetUInt16();
                        }
                        else
                        {
                            if (!source.Require(6))
                            {
                                this.result = DicomReaderResult.Suspended;
                                return false;
                            }

                            source.Skip(2);
                            this.length = source.GetUInt32();

                            // CP-246 (#177) handling
                            // assume that Undefined Length in explicit datasets with VR UN are sequences 
                            // According to CP-246 the sequence shall be handled as ILE, but this will be handled later...
                            // in the current code this needs to be restricted to privates 
                            if (this.length == UndefinedLength && this._vr == DicomVR.UN && this._tag.IsPrivate)
                            {
                                this._vr = DicomVR.SQ;
                            }
                        }
                    }
                    else
                    {
                        if (!source.Require(4))
                        {
                            this.result = DicomReaderResult.Suspended;
                            return false;
                        }

                        this.length = source.GetUInt32();

                        // assume that undefined length in implicit dataset is SQ
                        if (this.length == UndefinedLength && this._vr == DicomVR.UN)
                        {
                            this._vr = DicomVR.SQ;
                        }
                    }

                    this.parseStage = ParseStage.Value;
                }
                return true;
            }

            private bool ParseValue(IByteSource source)
            {
                if (this.parseStage == ParseStage.Value)
                {
                    // check dictionary for VR after reading length to handle 16-bit lengths
                    // check before reading value to handle SQ elements
                    var parsedVR = this._vr;

                    // check dictionary for VR after reading length to handle 16-bit lengths
                    // check before reading value to handle SQ elements
                    if (this._vr == DicomVR.Implicit || (this._vr == DicomVR.UN && this.isExplicitVR))
                    {
                        var entry = this.dictionary[this._tag];
                        if (entry != null)
                        {
                            this._vr = entry.ValueRepresentations.FirstOrDefault();
                        }

                        if (this._vr == null)
                        {
                            this._vr = DicomVR.UN;
                        }
                    }

                    if (this._tag == DicomTag.ItemDelimitationItem || this._tag == DicomTag.SequenceDelimitationItem)
                    {
                        // end of sequence item
                        return false;
                    }

                    while (this._vr == DicomVR.SQ && this._tag.IsPrivate && this.length > 0)
                    {
                        if (!IsPrivateSequence(source))
                        {
                            this._vr = DicomVR.UN;
                            break;
                        }

                        if (IsPrivateSequenceBad(source, this.length, this.isExplicitVR))
                        {
                            this.badPrivateSequence = true;
                            // store the depth of the bad sequence, we only want to switch back once we've processed
                            // the entire sequence, regardless of any sub-sequences.
                            this.badPrivateSequenceDepth = this.sequenceDepth;
                            this.isExplicitVR = !this.isExplicitVR;
                        }
                        break;
                    }

                    // Fix to handle sequence items not associated with any sequence (#364)
                    if (_tag.Equals(DicomTag.Item))
                    {
                        source.Rewind();
                        _vr = DicomVR.SQ;
                    }

                    if (this._vr == DicomVR.SQ)
                    {
                        // start of sequence
                        this.observer.OnBeginSequence(source, this._tag, this.length);
                        this.parseStage = ParseStage.Tag;
                        if (this.length != UndefinedLength)
                        {
                            this._implicit = false;
                            source.PushMilestone(this.length);
                        }
                        else
                        {
                            this._implicit = true;
                        }
                        var last = source.Position;

                        // Conformance with CP-246 (#177)
                        var needtoChangeEndian = false;
                        if (parsedVR == DicomVR.UN && !this._tag.IsPrivate)
                        {
                            this._implicit = true;
                            needtoChangeEndian = source.Endian == Endian.Big;
                        }
                        if (needtoChangeEndian)
                        {
                            source.Endian = Endian.Little;
                        }

                        this.ParseItemSequence(source);

                        if (needtoChangeEndian)
                        {
                            source.Endian = Endian.Big;
                        }

                        // Aeric Sylvan - https://github.com/rcd/fo-dicom/issues/62#issuecomment-46248073
                        // Fix reading of SQ with parsed VR of UN
                        if (source.Position > last || this.length == 0)
                        {
                            return true;
                        }

                        this.parseStage = ParseStage.Value;
                        this._vr = parsedVR;
                    }

                    if (this.length == UndefinedLength)
                    {
                        this.observer.OnBeginFragmentSequence(source, this._tag, this._vr);
                        this.parseStage = ParseStage.Tag;
                        this.ParseFragmentSequence(source);
                        return true;
                    }

                    if (!source.Require(this.length))
                    {
                        this.result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var buffer = source.GetBuffer(this.length);

                    if (!this._vr.IsString)
                    {
                        buffer = EndianByteBuffer.Create(buffer, source.Endian, this._vr.UnitSize);
                    }
                    this.observer.OnElement(source, this._tag, this._vr, buffer);

                    // parse private creator value and add to lookup table
                    if (this._tag.IsPrivate && this._tag.Element != 0x0000 && this._tag.Element <= 0x00ff)
                    {
                        var creator =
                            DicomEncoding.Default.GetString(buffer.Data, 0, buffer.Data.Length)
                                .TrimEnd((char)DicomVR.LO.PaddingValue);
                        var card = (uint)(this._tag.Group << 16) + this._tag.Element;

                        lock (this.locker)
                        {
                            this._private[card] = creator;
                        }
                    }

                    this.ResetState();
                }
                return true;
            }

#if !NET35
            private async Task<bool> ParseValueAsync(IByteSource source)
            {
                if (this.parseStage == ParseStage.Value)
                {
                    // check dictionary for VR after reading length to handle 16-bit lengths
                    // check before reading value to handle SQ elements
                    var parsedVR = this._vr;

                    // check dictionary for VR after reading length to handle 16-bit lengths
                    // check before reading value to handle SQ elements
                    if (this._vr == DicomVR.Implicit || (this._vr == DicomVR.UN && this.isExplicitVR))
                    {
                        var entry = this.dictionary[this._tag];
                        if (entry != null)
                        {
                            this._vr = entry.ValueRepresentations.FirstOrDefault();
                        }

                        if (this._vr == null)
                        {
                            this._vr = DicomVR.UN;
                        }
                    }

                    if (this._tag == DicomTag.ItemDelimitationItem || this._tag == DicomTag.SequenceDelimitationItem)
                    {
                        // end of sequence item
                        return false;
                    }

                    while (this._vr == DicomVR.SQ && this._tag.IsPrivate && this.length > 0)
                    {
                        if (!IsPrivateSequence(source))
                        {
                            this._vr = DicomVR.UN;
                            break;
                        }

                        if (IsPrivateSequenceBad(source, this.length, this.isExplicitVR))
                        {
                            this.badPrivateSequence = true;
                            // store the depth of the bad sequence, we only want to switch back once we've processed
                            // the entire sequence, regardless of any sub-sequences.
                            this.badPrivateSequenceDepth = this.sequenceDepth;
                            this.isExplicitVR = !this.isExplicitVR;
                        }
                        break;
                    }

                    // Fix to handle sequence items not associated with any sequence (#364)
                    if (_tag.Equals(DicomTag.Item))
                    {
                        source.Rewind();
                        _vr = DicomVR.SQ;
                    }

                    if (this._vr == DicomVR.SQ)
                    {
                        // start of sequence
                        this.observer.OnBeginSequence(source, this._tag, this.length);
                        this.parseStage = ParseStage.Tag;
                        if (this.length != UndefinedLength)
                        {
                            this._implicit = false;
                            source.PushMilestone(this.length);
                        }
                        else
                        {
                            this._implicit = true;
                        }
                        var last = source.Position;

                        // Conformance with CP-246 (#177)
                        var needtoChangeEndian = false;
                        if (parsedVR == DicomVR.UN && !this._tag.IsPrivate)
                        {
                            this._implicit = true;
                            needtoChangeEndian = source.Endian == Endian.Big;
                        }
                        if (needtoChangeEndian)
                        {
                            source.Endian = Endian.Little;
                        }

                        await this.ParseItemSequenceAsync(source).ConfigureAwait(false);

                        if (needtoChangeEndian)
                        {
                            source.Endian = Endian.Big;
                        }

                        // Aeric Sylvan - https://github.com/rcd/fo-dicom/issues/62#issuecomment-46248073
                        // Fix reading of SQ with parsed VR of UN
                        if (source.Position > last || this.length == 0)
                        {
                            return true;
                        }

                        this.parseStage = ParseStage.Value;
                        this._vr = parsedVR;
                    }

                    if (this.length == UndefinedLength)
                    {
                        this.observer.OnBeginFragmentSequence(source, this._tag, this._vr);
                        this.parseStage = ParseStage.Tag;
                        await this.ParseFragmentSequenceAsync(source).ConfigureAwait(false);
                        return true;
                    }

                    if (!source.Require(this.length))
                    {
                        this.result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var buffer = await source.GetBufferAsync(this.length).ConfigureAwait(false);

                    if (!this._vr.IsString)
                    {
                        buffer = EndianByteBuffer.Create(buffer, source.Endian, this._vr.UnitSize);
                    }
                    this.observer.OnElement(source, this._tag, this._vr, buffer);

                    // parse private creator value and add to lookup table
                    if (this._tag.IsPrivate && this._tag.Element != 0x0000 && this._tag.Element <= 0x00ff)
                    {
                        var creator =
                            DicomEncoding.Default.GetString(buffer.Data, 0, buffer.Data.Length)
                                .TrimEnd((char)DicomVR.LO.PaddingValue);
                        var card = (uint)(this._tag.Group << 16) + this._tag.Element;

                        lock (this.locker)
                        {
                            this._private[card] = creator;
                        }
                    }

                    this.ResetState();
                }
                return true;
            }
#endif

            private void ParseItemSequence(IByteSource source)
            {
                this.result = DicomReaderResult.Processing;

                while (!source.IsEOF && !source.HasReachedMilestone())
                {
                    if (!this.ParseItemSequenceTag(source)) return;
                    if (!this.ParseItemSequenceValue(source)) return;
                }

                this.ParseItemSequencePostProcess(source);
            }

#if !NET35
            private async Task ParseItemSequenceAsync(IByteSource source)
            {
                this.result = DicomReaderResult.Processing;

                while (!source.IsEOF && !source.HasReachedMilestone())
                {
                    if (!this.ParseItemSequenceTag(source)) return;
                    if (!await this.ParseItemSequenceValueAsync(source).ConfigureAwait(false)) return;
                }

                this.ParseItemSequencePostProcess(source);
            }
#endif

            private bool ParseItemSequenceTag(IByteSource source)
            {
                if (this.parseStage == ParseStage.Tag)
                {
                    source.Mark();

                    if (!source.Require(8))
                    {
                        this.result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var group = source.GetUInt16();
                    var element = source.GetUInt16();

                    this._tag = new DicomTag(@group, element);

                    if (this._tag != DicomTag.Item && this._tag != DicomTag.SequenceDelimitationItem)
                    {
                        // assume invalid sequence
                        source.Rewind();
                        if (!this._implicit)
                        {
                            source.PopMilestone();
                        }
                        this.observer.OnEndSequence();
                        // #565 Only reset the badPrivate sequence if we're in the correct depth
                        // This prevents prematurely resetting in case of sub-sequences contained in the bad private sequence
                        if (this.badPrivateSequence && this.sequenceDepth == this.badPrivateSequenceDepth)
                        {
                            this.isExplicitVR = !this.isExplicitVR;
                            this.badPrivateSequence = false;
                        }
                        return false;
                    }

                    this.length = source.GetUInt32();

                    if (this._tag == DicomTag.SequenceDelimitationItem)
                    {
                        // #64, in case explicit length has been specified despite occurrence of Sequence Delimitation Item
                        if (source.HasReachedMilestone() && source.MilestonesCount > this.sequenceDepth)
                        {
                            this.ResetState();
                            return true;
                        }

                        // end of sequence
                        this.observer.OnEndSequence();
                        // #565 Only reset the badPrivate sequence if we're in the correct depth
                        // This prevents prematurely resetting in case of sub-sequences contained in the bad private sequence
                        if (this.badPrivateSequence && this.sequenceDepth == this.badPrivateSequenceDepth)
                        {
                            this.isExplicitVR = !this.isExplicitVR;
                            this.badPrivateSequence = false;
                        }
                        this.ResetState();
                        return false;
                    }

                    this.parseStage = ParseStage.Value;
                }
                return true;
            }

            private bool ParseItemSequenceValue(IByteSource source)
            {
                if (this.parseStage == ParseStage.Value)
                {
                    if (this.length != UndefinedLength)
                    {
                        if (!source.Require(this.length))
                        {
                            this.result = DicomReaderResult.Suspended;
                            return false;
                        }

                        source.PushMilestone(this.length);
                    }

                    this.observer.OnBeginSequenceItem(source, this.length);

                    this.ResetState();
                    ++this.sequenceDepth;
                    this.ParseDataset(source);
                    --this.sequenceDepth;
                    // bugfix k-pacs. there a sequence was not ended by ItemDelimitationItem>SequenceDelimitationItem, but directly with SequenceDelimitationItem
                    bool isEndSequence = (this._tag == DicomTag.SequenceDelimitationItem);
                    this.ResetState();

                    this.observer.OnEndSequenceItem();

                    if (isEndSequence)
                    {
                        // end of sequence
                        this.observer.OnEndSequence();
                        // #565 Only reset the badPrivate sequence if we're in the correct depth
                        // This prevents prematurely resetting in case of sub-sequences contained in the bad private sequence
                        if (this.badPrivateSequence && this.sequenceDepth == this.badPrivateSequenceDepth)
                        {
                            this.isExplicitVR = !this.isExplicitVR;
                            this.badPrivateSequence = false;
                        }
                        this.ResetState();
                        return false;
                    }
                }
                return true;
            }

#if !NET35
            private async Task<bool> ParseItemSequenceValueAsync(IByteSource source)
            {
                if (this.parseStage == ParseStage.Value)
                {
                    if (this.length != UndefinedLength)
                    {
                        if (!source.Require(this.length))
                        {
                            this.result = DicomReaderResult.Suspended;
                            return false;
                        }

                        source.PushMilestone(this.length);
                    }

                    this.observer.OnBeginSequenceItem(source, this.length);

                    this.ResetState();
                    ++this.sequenceDepth;
                    await this.ParseDatasetAsync(source).ConfigureAwait(false);
                    --this.sequenceDepth;
                    // bugfix k-pacs. there a sequence was not ended by ItemDelimitationItem>SequenceDelimitationItem, but directly with SequenceDelimitationItem
                    bool isEndSequence = (this._tag == DicomTag.SequenceDelimitationItem);
                    this.ResetState();

                    this.observer.OnEndSequenceItem();

                    if (isEndSequence)
                    {
                        // end of sequence
                        this.observer.OnEndSequence();
                        // #565 Only reset the badPrivate sequence if we're in the correct depth
                        // This prevents prematurely resetting in case of sub-sequences contained in the bad private sequence
                        if (this.badPrivateSequence && this.sequenceDepth == this.badPrivateSequenceDepth)
                        {
                            this.isExplicitVR = !this.isExplicitVR;
                            this.badPrivateSequence = false;
                        }
                        this.ResetState();
                        return false;
                    }
                }
                return true;
            }
#endif

            private void ParseItemSequencePostProcess(IByteSource source)
            {
                // end of explicit length sequence
                if (source.HasReachedMilestone())
                {
                    source.PopMilestone();
                }

                this.observer.OnEndSequence();
                // #565 Only reset the badPrivate sequence if we're in the correct depth
                // This prevents prematurely resetting in case of sub-sequences contained in the bad private sequence
                if (this.badPrivateSequence && this.sequenceDepth == this.badPrivateSequenceDepth)
                {
                    this.isExplicitVR = !this.isExplicitVR;
                    this.badPrivateSequence = false;
                }
            }

            private void ParseFragmentSequence(IByteSource source)
            {
                this.result = DicomReaderResult.Processing;

                while (!source.IsEOF)
                {
                    if (!this.ParseFragmentSequenceTag(source)) return;
                    if (!this.ParseFragmentSequenceValue(source)) return;
                }
            }

#if !NET35
            private async Task ParseFragmentSequenceAsync(IByteSource source)
            {
                this.result = DicomReaderResult.Processing;

                while (!source.IsEOF)
                {
                    if (!this.ParseFragmentSequenceTag(source)) return;
                    if (!await this.ParseFragmentSequenceValueAsync(source).ConfigureAwait(false)) return;
                }
            }
#endif

            private bool ParseFragmentSequenceTag(IByteSource source)
            {
                if (this.parseStage == ParseStage.Tag)
                {
                    source.Mark();

                    if (!source.Require(8))
                    {
                        this.result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var group = source.GetUInt16();
                    var element = source.GetUInt16();

                    DicomTag tag = new DicomTag(@group, element);

                    if (tag != DicomTag.Item && tag != DicomTag.SequenceDelimitationItem)
                    {
                        throw new DicomReaderException("Unexpected tag in DICOM fragment sequence: {0}", tag);
                    }

                    this.length = source.GetUInt32();

                    if (tag == DicomTag.SequenceDelimitationItem)
                    {
                        // end of fragment
                        this.observer.OnEndFragmentSequence();
                        this.fragmentItem = 0;
                        this.ResetState();
                        return false;
                    }

                    this.fragmentItem++;
                    this.parseStage = ParseStage.Value;
                }
                return true;
            }

            private bool ParseFragmentSequenceValue(IByteSource source)
            {
                if (this.parseStage == ParseStage.Value)
                {
                    if (!source.Require(this.length))
                    {
                        this.result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var buffer = source.GetBuffer(this.length);
                    buffer = EndianByteBuffer.Create(buffer, source.Endian, this.fragmentItem == 1 ? 4 : this._vr.UnitSize);
                    this.observer.OnFragmentSequenceItem(source, buffer);

                    this.parseStage = ParseStage.Tag;
                }
                return true;
            }

#if !NET35
            private async Task<bool> ParseFragmentSequenceValueAsync(IByteSource source)
            {
                if (this.parseStage == ParseStage.Value)
                {
                    if (!source.Require(this.length))
                    {
                        this.result = DicomReaderResult.Suspended;
                        return false;
                    }

                    var buffer = await source.GetBufferAsync(this.length).ConfigureAwait(false);
                    buffer = EndianByteBuffer.Create(buffer, source.Endian, this.fragmentItem == 1 ? 4 : this._vr.UnitSize);
                    this.observer.OnFragmentSequenceItem(source, buffer);

                    this.parseStage = ParseStage.Tag;
                }
                return true;
            }
#endif

            private void ResetState()
            {
                this.parseStage = ParseStage.Tag;
                this._tag = null;
                this._entry = null;
                this._vr = null;
                this.length = 0;
            }

            private static bool IsPrivateSequence(IByteSource source)
            {
                source.Mark();

                try
                {
                    var group = source.GetUInt16();
                    var element = source.GetUInt16();
                    var tag = new DicomTag(group, element);

                    if (tag == DicomTag.Item || tag == DicomTag.SequenceDelimitationItem) return true;
                }
                finally
                {
                    source.Rewind();
                }

                return false;
            }

            private static bool IsPrivateSequenceBad(IByteSource source, uint count, bool isExplicitVR)
            {
                source.Mark();

                try
                {
                    // Skip "item" tags; continue skipping until length is non-zero (#223)
                    // Using & instead of && enforces RHS to be evaluated regardless of LHS
                    uint length;
                    while (source.GetUInt16() == DicomTag.Item.Group &
                           source.GetUInt16() == DicomTag.Item.Element &
                           (length = source.GetUInt32()) < uint.MaxValue)   // Dummy condition to ensure that length is included in parsing
                    {
                        // Length non-zero, end skipping (#223)
                        if (length > 0)
                            break;

                        // Handle scenario where last tag is private sequence with empty items (#487)
                        count -= 8;
                        if (count <= 0)
                            return false;
                    }

                    source.GetUInt16(); // group
                    source.GetUInt16(); // element

                    var bytes = source.GetBytes(2);
                    var vr = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    DicomVR dummy;
                    if (DicomVR.TryParse(vr, out dummy)) return !isExplicitVR;
                    // unable to parse VR
                    if (isExplicitVR) return true;
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
