// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Dicom.IO.Buffer;

using Dicom.Imaging.Mathematics;

namespace Dicom.IO.Reader
{
    /// <summary>
    /// DICOM reader implementation.
    /// </summary>
    public class DicomReader : IDicomReader
    {
        #region FIELDS

        /// <summary>
        /// Defined value for undefined length.
        /// </summary>
        private const uint UndefinedLength = 0xffffffff;

        private ParseState _state;

        private DicomTag _tag;

        private DicomDictionaryEntry _entry;

        private DicomVR _vr;

        private uint _length;

        private bool _implicit;

        private int _fragmentItem;

        private DicomTag _stop;

        private IDicomReaderObserver _observer;

        private EventAsyncResult _async;

        private Exception _exception;

        private volatile DicomReaderResult _result;

        private bool _badPrivateSequence;

        private readonly Dictionary<uint, string> _private;

        private readonly Stack<object> _stack;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of <see cref="DicomReader"/>.
        /// </summary>
        public DicomReader()
        {
            _private = new Dictionary<uint, string>();
            _stack = new Stack<object>();
            this.Dictionary = DicomDictionary.Default;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the DICOM dictionary to be used by the reader..
        /// </summary>
        public DicomDictionary Dictionary { get; set; }

        /// <summary>
        /// Gets or sets whether value representation is explicit or not.
        /// </summary>
        public bool IsExplicitVR { get; set; }

        /// <summary>
        /// Gets the current reader status.
        /// </summary>
        public DicomReaderResult Status
        {
            get
            {
                return _result;
            }
        }

        #endregion

        #region INNER TYPES

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
            return EndRead(BeginRead(source, observer, stop, null, null));
        }

        public IAsyncResult BeginRead(
            IByteSource source,
            IDicomReaderObserver observer,
            DicomTag stop,
            AsyncCallback callback,
            object state)
        {
            _stop = stop;
            _observer = observer;
            _result = DicomReaderResult.Processing;
            _exception = null;
            _async = new EventAsyncResult(callback, state);
            ThreadPool.QueueUserWorkItem(ParseProc, source);
            return _async;
        }

        public DicomReaderResult EndRead(IAsyncResult result)
        {
            _async.AsyncWaitHandle.WaitOne();
            if (_exception != null) throw _exception;
            return _result;
        }

        private void ParseProc(object state)
        {
            ParseDataset(state as IByteSource, null);
        }

        private void ParseDataset(IByteSource source, object state)
        {
            try
            {
                _result = DicomReaderResult.Processing;

                while (!source.IsEOF && !source.HasReachedMilestone() && _result == DicomReaderResult.Processing)
                {
                    if (_state == ParseState.Tag)
                    {
                        source.Mark();

                        if (!source.Require(4, ParseDataset, state))
                        {
                            _result = DicomReaderResult.Suspended;
                            return;
                        }

                        ushort group = source.GetUInt16();
                        ushort element = source.GetUInt16();
                        DicomPrivateCreator creator = null;

                        if (group.IsOdd() && element > 0x00ff)
                        {
                            string pvt = null;
                            uint card = (uint)(group << 16) + (uint)(element >> 8);
                            if (_private.TryGetValue(card, out pvt)) creator = Dictionary.GetPrivateCreator(pvt);
                        }

                        _tag = new DicomTag(group, element, creator);
                        _entry = Dictionary[_tag];

                        if (!_tag.IsPrivate && _entry != null && _entry.MaskTag == null) _tag = _entry.Tag; // Use dictionary tag

                        if (_stop != null && _tag.CompareTo(_stop) >= 0)
                        {
                            _result = DicomReaderResult.Stopped;
                            return;
                        }

                        _state = ParseState.VR;
                    }

                    while (_state == ParseState.VR)
                    {
                        if (_tag == DicomTag.Item || _tag == DicomTag.ItemDelimitationItem
                            || _tag == DicomTag.SequenceDelimitationItem)
                        {
                            _vr = DicomVR.NONE;
                            _state = ParseState.Length;
                            break;
                        }

                        if (IsExplicitVR)
                        {
                            if (!source.Require(2, ParseDataset, state))
                            {
                                _result = DicomReaderResult.Suspended;
                                return;
                            }

                            source.Mark();
                            byte[] bytes = source.GetBytes(2);
                            string vr = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                            if (!DicomVR.TryParse(vr, out _vr))
                            {
                                // unable to parse VR; rewind VR bytes for continued attempt to interpret the data.
                                _vr = DicomVR.Implicit;
                                source.Rewind();
                            }
                        }
                        else
                        {
                            if (_entry != null)
                            {
                                if (_entry == DicomDictionary.UnknownTag) _vr = DicomVR.UN;
                                else if (_entry.ValueRepresentations.Contains(DicomVR.OB)
                                         && _entry.ValueRepresentations.Contains(DicomVR.OW)) _vr = DicomVR.OW; // ???
                                else _vr = _entry.ValueRepresentations.FirstOrDefault();
                            }
                        }

                        if (_vr == null) _vr = DicomVR.UN;

                        _state = ParseState.Length;

                        if (_vr == DicomVR.UN)
                        {
                            if (_tag.Element == 0x0000)
                            {
                                // Group Length to UL
                                _vr = DicomVR.UL;
                                break;
                            }
                            else if (IsExplicitVR)
                            {
                                break;
                            }
                        }

                        if (_tag.IsPrivate)
                        {
                            if (_tag.Element != 0x0000 && _tag.Element <= 0x00ff && _vr == DicomVR.UN) _vr = DicomVR.LO; // force private creator to LO
                        }
                    }

                    while (_state == ParseState.Length)
                    {
                        if (_tag == DicomTag.Item || _tag == DicomTag.ItemDelimitationItem
                            || _tag == DicomTag.SequenceDelimitationItem)
                        {
                            if (!source.Require(4, ParseDataset, state))
                            {
                                _result = DicomReaderResult.Suspended;
                                return;
                            }

                            _length = source.GetUInt32();

                            _state = ParseState.Value;
                            break;
                        }

                        if (IsExplicitVR)
                        {
                            if (_vr == DicomVR.Implicit)
                            {
                                if (!source.Require(4, ParseDataset, state))
                                {
                                    _result = DicomReaderResult.Suspended;
                                    return;
                                }

                                _length = source.GetUInt32();

                                // assume that undefined length in implicit VR element is SQ
                                if (_length == UndefinedLength) _vr = DicomVR.SQ;
                            }
                            else if (_vr.Is16bitLength)
                            {
                                if (!source.Require(2, ParseDataset, state))
                                {
                                    _result = DicomReaderResult.Suspended;
                                    return;
                                }

                                _length = source.GetUInt16();
                            }
                            else
                            {
                                if (!source.Require(6, ParseDataset, state))
                                {
                                    _result = DicomReaderResult.Suspended;
                                    return;
                                }

                                source.Skip(2);
                                _length = source.GetUInt32();
                            }
                        }
                        else
                        {
                            if (!source.Require(4, ParseDataset, state))
                            {
                                _result = DicomReaderResult.Suspended;
                                return;
                            }

                            _length = source.GetUInt32();

                            // assume that undefined length in implicit dataset is SQ
                            if (_length == UndefinedLength && _vr == DicomVR.UN) _vr = DicomVR.SQ;
                        }

                        _state = ParseState.Value;
                    }

                    if (_state == ParseState.Value)
                    {
                        // check dictionary for VR after reading length to handle 16-bit lengths
                        // check before reading value to handle SQ elements
                        var parsedVR = _vr;

                        // check dictionary for VR after reading length to handle 16-bit lengths
                        // check before reading value to handle SQ elements
                        if (_vr == DicomVR.Implicit || (_vr == DicomVR.UN && IsExplicitVR))
                        {
                            var entry = Dictionary[_tag];
                            if (entry != null) _vr = entry.ValueRepresentations.FirstOrDefault();

                            if (_vr == null) _vr = DicomVR.UN;
                        }

                        if (_tag == DicomTag.ItemDelimitationItem)
                        {
                            // end of sequence item
                            return;
                        }

                        while (_vr == DicomVR.SQ && _tag.IsPrivate)
                        {
                            if (!IsPrivateSequence(source))
                            {
                                _vr = DicomVR.UN;
                                break;
                            }

                            if (IsPrivateSequenceBad(source, this.IsExplicitVR))
                            {
                                _badPrivateSequence = true;
                                this.IsExplicitVR = !this.IsExplicitVR;
                            }
                            break;
                        }

                        if (_vr == DicomVR.SQ)
                        {
                            // start of sequence
                            _observer.OnBeginSequence(source, _tag, _length);
                            _state = ParseState.Tag;
                            if (_length != UndefinedLength)
                            {
                                _implicit = false;
                                source.PushMilestone(_length);
                            }
                            else _implicit = true;
                            this._stack.Push(state);
                            var last = source.Position;
                            ParseItemSequence(source, null);

                            // Aeric Sylvan - https://github.com/rcd/fo-dicom/issues/62#issuecomment-46248073
                            // Fix reading of SQ with parsed VR of UN
                            if (source.Position > last || _length == 0) continue;
                            else
                            {
                                _state = ParseState.Value;
                                _vr = parsedVR;
                            }
                        }

                        if (_length == UndefinedLength)
                        {
                            _observer.OnBeginFragmentSequence(source, _tag, _vr);
                            _state = ParseState.Tag;
                            this._stack.Push(state);
                            ParseFragmentSequence(source, null);
                            continue;
                        }

                        if (!source.Require(_length, ParseDataset, state))
                        {
                            _result = DicomReaderResult.Suspended;
                            return;
                        }

                        IByteBuffer buffer = source.GetBuffer(_length);

                        if (!_vr.IsString) buffer = EndianByteBuffer.Create(buffer, source.Endian, _vr.UnitSize);
                        _observer.OnElement(source, _tag, _vr, buffer);

                        // parse private creator value and add to lookup table
                        if (_tag.IsPrivate && _tag.Element != 0x0000 && _tag.Element <= 0x00ff)
                        {
                            var creator =
                                DicomEncoding.Default.GetString(buffer.Data, 0, buffer.Data.Length)
                                    .TrimEnd((char)DicomVR.LO.PaddingValue);
                            var card = (uint)(_tag.Group << 16) + (uint)(_tag.Element);
                            _private[card] = creator;
                        }

                        ResetState();
                    }
                }

                if (source.HasReachedMilestone())
                {
                    // end of explicit length sequence item
                    source.PopMilestone();
                    return;
                }

                if (_result != DicomReaderResult.Processing) return;

                // end of processing
                _result = DicomReaderResult.Success;
            }
            catch (Exception e)
            {
                _exception = e;
                _result = DicomReaderResult.Error;
            }
            finally
            {
                if (_result != DicomReaderResult.Processing)
                {
                    _async.Set();
                }
            }
        }

        private void ParseItemSequence(IByteSource source, object state)
        {
            try
            {
                _result = DicomReaderResult.Processing;

                while (!source.IsEOF && !source.HasReachedMilestone())
                {
                    if (_state == ParseState.Tag)
                    {
                        source.Mark();

                        if (!source.Require(8, ParseItemSequence, state))
                        {
                            _result = DicomReaderResult.Suspended;
                            return;
                        }

                        ushort group = source.GetUInt16();
                        ushort element = source.GetUInt16();

                        _tag = new DicomTag(group, element);

                        if (_tag != DicomTag.Item && _tag != DicomTag.SequenceDelimitationItem)
                        {
                            // assume invalid sequence
                            source.Rewind();
                            if (!_implicit) source.PopMilestone();
                            _observer.OnEndSequence();
                            if (_badPrivateSequence)
                            {
                                this.IsExplicitVR = !this.IsExplicitVR;
                                _badPrivateSequence = false;
                            }
                            return;
                        }

                        _length = source.GetUInt32();

                        if (_tag == DicomTag.SequenceDelimitationItem)
                        {
                            // end of sequence
                            _observer.OnEndSequence();
                            if (_badPrivateSequence)
                            {
                                this.IsExplicitVR = !this.IsExplicitVR;
                                _badPrivateSequence = false;
                            }
                            ResetState();
                            return;
                        }

                        _state = ParseState.Value;
                    }

                    if (_state == ParseState.Value)
                    {
                        if (_length != UndefinedLength)
                        {
                            if (!source.Require(_length, ParseItemSequence, state))
                            {
                                _result = DicomReaderResult.Suspended;
                                return;
                            }

                            source.PushMilestone(_length);
                        }

                        _observer.OnBeginSequenceItem(source, _length);

                        ResetState();
                        ParseDataset(source, state);
                        ResetState();

                        _observer.OnEndSequenceItem();
                        continue;
                    }
                }

                // end of explicit length sequence
                if (source.HasReachedMilestone()) source.PopMilestone();

                _observer.OnEndSequence();
                if (_badPrivateSequence)
                {
                    this.IsExplicitVR = !this.IsExplicitVR;
                    _badPrivateSequence = false;
                }
            }
            catch (Exception e)
            {
                _exception = e;
                _result = DicomReaderResult.Error;
            }
            finally
            {
                if (_result != DicomReaderResult.Processing)
                {
                    _async.Set();
                }
            }
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

                byte[] bytes = source.GetBytes(2);
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

        private void ParseFragmentSequence(IByteSource source, object state)
        {
            try
            {
                _result = DicomReaderResult.Processing;

                while (!source.IsEOF)
                {
                    if (_state == ParseState.Tag)
                    {
                        source.Mark();

                        if (!source.Require(8, ParseFragmentSequence, state))
                        {
                            _result = DicomReaderResult.Suspended;
                            return;
                        }

                        ushort group = source.GetUInt16();
                        ushort element = source.GetUInt16();

                        DicomTag tag = new DicomTag(group, element);

                        if (tag != DicomTag.Item && tag != DicomTag.SequenceDelimitationItem) throw new DicomReaderException("Unexpected tag in DICOM fragment sequence: {0}", tag);

                        _length = source.GetUInt32();

                        if (tag == DicomTag.SequenceDelimitationItem)
                        {
                            // end of fragment
                            _observer.OnEndFragmentSequence();
                            _fragmentItem = 0;
                            ResetState();
                            ParseDataset(source, this._stack.Count > 0 ? this._stack.Pop() : null);
                            return;
                        }

                        _fragmentItem++;
                        _state = ParseState.Value;
                    }

                    if (_state == ParseState.Value)
                    {
                        if (!source.Require(_length, ParseFragmentSequence, state))
                        {
                            _result = DicomReaderResult.Suspended;
                            return;
                        }

                        IByteBuffer buffer = source.GetBuffer(_length);
                        if (_fragmentItem == 1) buffer = EndianByteBuffer.Create(buffer, source.Endian, 4);
                        else buffer = EndianByteBuffer.Create(buffer, source.Endian, _vr.UnitSize);
                        _observer.OnFragmentSequenceItem(source, buffer);

                        _state = ParseState.Tag;
                    }
                }
            }
            catch (Exception e)
            {
                _exception = e;
                _result = DicomReaderResult.Error;
            }
            finally
            {
                if (_result != DicomReaderResult.Processing)
                {
                    _async.Set();
                }
            }
        }

        private void ResetState()
        {
            _state = ParseState.Tag;
            _tag = null;
            _entry = null;
            _vr = null;
            _length = 0;
        }

        #endregion
    }
}
