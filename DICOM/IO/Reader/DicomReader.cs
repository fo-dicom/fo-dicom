// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Reader
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Dicom.IO.Buffer;

    using Dicom.Imaging.Mathematics;

    /// <summary>
    /// DICOM reader implementation.
    /// </summary>
    public class DicomReader : IDicomReader
    {
        #region FIELDS

        private volatile DicomReaderResult _result;

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

        /// <summary>
        /// Gets or sets the DICOM dictionary to be used by the reader..
        /// </summary>
        public DicomDictionary Dictionary { get; set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Perform DICOM reading of a byte source.
        /// </summary>
        /// <param name="source">Byte source to read.</param>
        /// <param name="observer">Reader observer.</param>
        /// <param name="stop">Tag at which to stop.</param>
        /// <returns>Reader resulting status.</returns>
        public DicomReaderResult Read(IByteSource source, IDicomReaderObserver observer, DicomTag stop = null)
        {
            var worker = new DicomReaderWorker(observer, stop, this.Dictionary, this.IsExplicitVR, this._private);
            return worker.DoWork(source);
        }

        /// <summary>
        /// Asynchronously perform DICOM reading of a byte source.
        /// </summary>
        /// <param name="source">Byte source to read.</param>
        /// <param name="observer">Reader observer.</param>
        /// <param name="stop">Tag at which to stop.</param>
        /// <returns>Awaitable reader resulting status.</returns>
        public async Task<DicomReaderResult> ReadAsync(IByteSource source, IDicomReaderObserver observer, DicomTag stop = null)
        {
            var worker = new DicomReaderWorker(observer, stop, this.Dictionary, this.IsExplicitVR, this._private);
            return await worker.DoWorkAsync(source).ConfigureAwait(false);
        }

        #endregion

        #region INNER TYPES

        /// <summary>
        /// Support class performing the actual reading.
        /// </summary>
        private class DicomReaderWorker
        {
            /// <summary>
            /// Available parse states.
            /// </summary>
            private enum ParseState
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

            private readonly DicomTag stop;

            private readonly DicomDictionary dictionary;

            private readonly Dictionary<uint, string> _private;

            private bool isExplicitVR;

            private ParseState _state;

            private DicomTag _tag;

            private DicomDictionaryEntry _entry;

            private DicomVR _vr;

            private uint length;

            private DicomReaderResult result;

            private bool _implicit;

            private bool badPrivateSequence;

            private int fragmentItem;

            private readonly object locker;

            #endregion

            #region CONSTRUCTORS

            /// <summary>
            /// Initializes an instance of <see cref="DicomReaderWorker"/>.
            /// </summary>
            internal DicomReaderWorker(
                IDicomReaderObserver observer,
                DicomTag stop,
                DicomDictionary dictionary,
                bool isExplicitVR,
                Dictionary<uint, string> @private)
            {
                this.observer = observer;
                this.stop = stop;
                this.dictionary = dictionary;
                this.isExplicitVR = isExplicitVR;
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

            private void ParseDataset(IByteSource source)
            {
                this.result = DicomReaderResult.Processing;

                while (!source.IsEOF && !source.HasReachedMilestone() && this.result == DicomReaderResult.Processing)
                {
                    if (!this.ParseTag(source)) return;
                    if (!this.ParseVR(source)) return;
                    if (!this.ParseLength(source)) return;

                    if (this._state == ParseState.Value)
                    {
                        // check dictionary for VR after reading length to handle 16-bit lengths
                        // check before reading value to handle SQ elements
                        var parsedVR = this._vr;

                        // check dictionary for VR after reading length to handle 16-bit lengths
                        // check before reading value to handle SQ elements
                        if (this._vr == DicomVR.Implicit || (this._vr == DicomVR.UN && this.isExplicitVR))
                        {
                            var entry = this.dictionary[this._tag];
                            if (entry != null) this._vr = entry.ValueRepresentations.FirstOrDefault();

                            if (this._vr == null) this._vr = DicomVR.UN;
                        }

                        if (this._tag == DicomTag.ItemDelimitationItem)
                        {
                            // end of sequence item
                            return;
                        }

                        while (this._vr == DicomVR.SQ && this._tag.IsPrivate)
                        {
                            if (!IsPrivateSequence(source))
                            {
                                this._vr = DicomVR.UN;
                                break;
                            }

                            if (IsPrivateSequenceBad(source, this.isExplicitVR))
                            {
                                this.badPrivateSequence = true;
                                this.isExplicitVR = !this.isExplicitVR;
                            }
                            break;
                        }

                        if (this._vr == DicomVR.SQ)
                        {
                            // start of sequence
                            this.observer.OnBeginSequence(source, this._tag, this.length);
                            this._state = ParseState.Tag;
                            if (this.length != UndefinedLength)
                            {
                                this._implicit = false;
                                source.PushMilestone(this.length);
                            }
                            else this._implicit = true;
                            var last = source.Position;
                            this.ParseItemSequence(source);

                            // Aeric Sylvan - https://github.com/rcd/fo-dicom/issues/62#issuecomment-46248073
                            // Fix reading of SQ with parsed VR of UN
                            if (source.Position > last || this.length == 0) continue;

                            this._state = ParseState.Value;
                            this._vr = parsedVR;
                        }

                        if (this.length == UndefinedLength)
                        {
                            this.observer.OnBeginFragmentSequence(source, this._tag, this._vr);
                            this._state = ParseState.Tag;
                            this.ParseFragmentSequence(source);
                            continue;
                        }

                        if (!source.Require(this.length))
                        {
                            this.result = DicomReaderResult.Suspended;
                            return;
                        }

                        var buffer = source.GetBuffer(this.length);

                        if (!this._vr.IsString) buffer = EndianByteBuffer.Create(buffer, source.Endian, this._vr.UnitSize);
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

            private async Task ParseDatasetAsync(IByteSource source)
            {
                this.result = DicomReaderResult.Processing;

                while (!source.IsEOF && !source.HasReachedMilestone() && this.result == DicomReaderResult.Processing)
                {
                    if (!this.ParseTag(source)) return;
                    if (!this.ParseVR(source)) return;
                    if (!this.ParseLength(source)) return;

                    if (this._state == ParseState.Value)
                    {
                        // check dictionary for VR after reading length to handle 16-bit lengths
                        // check before reading value to handle SQ elements
                        var parsedVR = this._vr;

                        // check dictionary for VR after reading length to handle 16-bit lengths
                        // check before reading value to handle SQ elements
                        if (this._vr == DicomVR.Implicit || (this._vr == DicomVR.UN && this.isExplicitVR))
                        {
                            var entry = this.dictionary[this._tag];
                            if (entry != null) this._vr = entry.ValueRepresentations.FirstOrDefault();

                            if (this._vr == null) this._vr = DicomVR.UN;
                        }

                        if (this._tag == DicomTag.ItemDelimitationItem)
                        {
                            // end of sequence item
                            return;
                        }

                        while (this._vr == DicomVR.SQ && this._tag.IsPrivate)
                        {
                            if (!IsPrivateSequence(source))
                            {
                                this._vr = DicomVR.UN;
                                break;
                            }

                            if (IsPrivateSequenceBad(source, this.isExplicitVR))
                            {
                                this.badPrivateSequence = true;
                                this.isExplicitVR = !this.isExplicitVR;
                            }
                            break;
                        }

                        if (this._vr == DicomVR.SQ)
                        {
                            // start of sequence
                            this.observer.OnBeginSequence(source, this._tag, this.length);
                            this._state = ParseState.Tag;
                            if (this.length != UndefinedLength)
                            {
                                this._implicit = false;
                                source.PushMilestone(this.length);
                            }
                            else this._implicit = true;
                            var last = source.Position;
                            await this.ParseItemSequenceAsync(source).ConfigureAwait(false);

                            // Aeric Sylvan - https://github.com/rcd/fo-dicom/issues/62#issuecomment-46248073
                            // Fix reading of SQ with parsed VR of UN
                            if (source.Position > last || this.length == 0) continue;

                            this._state = ParseState.Value;
                            this._vr = parsedVR;
                        }

                        if (this.length == UndefinedLength)
                        {
                            this.observer.OnBeginFragmentSequence(source, this._tag, this._vr);
                            this._state = ParseState.Tag;
                            await this.ParseFragmentSequenceAsync(source).ConfigureAwait(false);
                            continue;
                        }

                        if (!source.Require(this.length))
                        {
                            this.result = DicomReaderResult.Suspended;
                            return;
                        }

                        var buffer = await source.GetBufferAsync(this.length).ConfigureAwait(false);

                        if (!this._vr.IsString) buffer = EndianByteBuffer.Create(buffer, source.Endian, this._vr.UnitSize);
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

            private bool ParseTag(IByteSource source)
            {
                if (this._state == ParseState.Tag)
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

                    if (this.stop != null && this._tag.CompareTo(this.stop) >= 0)
                    {
                        this.result = DicomReaderResult.Stopped;
                        return false;
                    }

                    this._state = ParseState.VR;
                }
                return true;
            }

            private bool ParseVR(IByteSource source)
            {
                while (this._state == ParseState.VR)
                {
                    if (this._tag == DicomTag.Item || this._tag == DicomTag.ItemDelimitationItem
                        || this._tag == DicomTag.SequenceDelimitationItem)
                    {
                        this._vr = DicomVR.NONE;
                        this._state = ParseState.Length;
                        break;
                    }

                    if (this.isExplicitVR)
                    {
                        if (!source.Require(2))
                        {
                            this.result = DicomReaderResult.Suspended;
                            return false;
                        }

                        source.Mark();
                        var bytes = source.GetBytes(2);
                        var vr = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                        if (!DicomVR.TryParse(vr, out this._vr))
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

                    this._state = ParseState.Length;

                    if (this._vr == DicomVR.UN)
                    {
                        if (this._tag.Element == 0x0000)
                        {
                            // Group Length to UL
                            this._vr = DicomVR.UL;
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
                while (this._state == ParseState.Length)
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

                        this._state = ParseState.Value;
                        break;
                    }

                    if (this.isExplicitVR)
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

                    this._state = ParseState.Value;
                }
                return true;
            }

            private void ParseItemSequence(IByteSource source)
            {
                this.result = DicomReaderResult.Processing;

                while (!source.IsEOF && !source.HasReachedMilestone())
                {
                    if (!this.ParseItemSequenceTag(source)) return;

                    if (this._state == ParseState.Value)
                    {
                        if (this.length != UndefinedLength)
                        {
                            if (!source.Require(this.length))
                            {
                                this.result = DicomReaderResult.Suspended;
                                return;
                            }

                            source.PushMilestone(this.length);
                        }

                        this.observer.OnBeginSequenceItem(source, this.length);

                        this.ResetState();
                        this.ParseDataset(source);
                        this.ResetState();

                        this.observer.OnEndSequenceItem();
                    }
                }

                // end of explicit length sequence
                if (source.HasReachedMilestone()) source.PopMilestone();

                this.observer.OnEndSequence();
                if (this.badPrivateSequence)
                {
                    this.isExplicitVR = !this.isExplicitVR;
                    this.badPrivateSequence = false;
                }
            }

            private async Task ParseItemSequenceAsync(IByteSource source)
            {
                this.result = DicomReaderResult.Processing;

                while (!source.IsEOF && !source.HasReachedMilestone())
                {
                    if (!this.ParseItemSequenceTag(source)) return;

                    if (this._state == ParseState.Value)
                    {
                        if (this.length != UndefinedLength)
                        {
                            if (!source.Require(this.length))
                            {
                                this.result = DicomReaderResult.Suspended;
                                return;
                            }

                            source.PushMilestone(this.length);
                        }

                        this.observer.OnBeginSequenceItem(source, this.length);

                        this.ResetState();
                        await this.ParseDatasetAsync(source).ConfigureAwait(false);
                        this.ResetState();

                        this.observer.OnEndSequenceItem();
                    }
                }

                // end of explicit length sequence
                if (source.HasReachedMilestone()) source.PopMilestone();

                this.observer.OnEndSequence();
                if (this.badPrivateSequence)
                {
                    this.isExplicitVR = !this.isExplicitVR;
                    this.badPrivateSequence = false;
                }
            }

            private bool ParseItemSequenceTag(IByteSource source)
            {
                if (this._state == ParseState.Tag)
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
                        if (this.badPrivateSequence)
                        {
                            this.isExplicitVR = !this.isExplicitVR;
                            this.badPrivateSequence = false;
                        }
                        return false;
                    }

                    this.length = source.GetUInt32();

                    if (this._tag == DicomTag.SequenceDelimitationItem)
                    {
                        // end of sequence
                        this.observer.OnEndSequence();
                        if (this.badPrivateSequence)
                        {
                            this.isExplicitVR = !this.isExplicitVR;
                            this.badPrivateSequence = false;
                        }
                        this.ResetState();
                        return false;
                    }

                    this._state = ParseState.Value;
                }
                return true;
            }

            private void ParseFragmentSequence(IByteSource source)
            {
                this.result = DicomReaderResult.Processing;

                while (!source.IsEOF)
                {
                    if (!this.ParseFragmentSequenceTag(source)) return;

                    if (this._state == ParseState.Value)
                    {
                        if (!source.Require(this.length))
                        {
                            this.result = DicomReaderResult.Suspended;
                            return;
                        }

                        var buffer = source.GetBuffer(this.length);
                        buffer = EndianByteBuffer.Create(
                            buffer,
                            source.Endian,
                            this.fragmentItem == 1 ? 4 : this._vr.UnitSize);
                        this.observer.OnFragmentSequenceItem(source, buffer);

                        this._state = ParseState.Tag;
                    }
                }
            }

            private async Task ParseFragmentSequenceAsync(IByteSource source)
            {
                this.result = DicomReaderResult.Processing;

                while (!source.IsEOF)
                {
                    if (!this.ParseFragmentSequenceTag(source)) return;

                    if (this._state == ParseState.Value)
                    {
                        if (!source.Require(this.length))
                        {
                            this.result = DicomReaderResult.Suspended;
                            return;
                        }

                        var buffer = await source.GetBufferAsync(this.length).ConfigureAwait(false);
                        buffer = EndianByteBuffer.Create(
                            buffer,
                            source.Endian,
                            this.fragmentItem == 1 ? 4 : this._vr.UnitSize);
                        this.observer.OnFragmentSequenceItem(source, buffer);

                        this._state = ParseState.Tag;
                    }
                }
            }

            private bool ParseFragmentSequenceTag(IByteSource source)
            {
                if (this._state == ParseState.Tag)
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
                        this.ParseDataset(source);
                        return false;
                    }

                    this.fragmentItem++;
                    this._state = ParseState.Value;
                }
                return true;
            }

            private void ResetState()
            {
                this._state = ParseState.Tag;
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

            private static bool IsPrivateSequenceBad(IByteSource source, bool isExplicitVR)
            {
                source.Mark();

                try
                {
                    source.GetUInt16(); // group
                    source.GetUInt16(); // element
                    source.GetUInt32(); // length

                    source.GetUInt16(); // group
                    source.GetUInt16(); // element

                    var bytes = source.GetBytes(2);
                    var vr = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    DicomVR dummy;
                    if (DicomVR.TryParse(vr, out dummy)) return isExplicitVR;
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
