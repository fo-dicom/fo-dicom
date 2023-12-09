// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using FellowOakDicom.IO.Buffer;

namespace FellowOakDicom.IO.Reader
{

    internal class DicomReaderCallbackObserver : IDicomReaderObserver
    {

        private readonly Stack<DicomReaderEventArgs> _stack;

        private readonly IDictionary<DicomTag, EventHandler<DicomReaderEventArgs>> _callbacks;

        public DicomReaderCallbackObserver()
        {
            _stack = new Stack<DicomReaderEventArgs>();
            _callbacks = new Dictionary<DicomTag, EventHandler<DicomReaderEventArgs>>();
        }

        public void Add(DicomTag tag, EventHandler<DicomReaderEventArgs> callback)
        {
            _callbacks.Add(tag, callback);
        }

        public void OnElement(IByteSource source, DicomTag tag, DicomVR vr, IByteBuffer data)
        {
            if (_callbacks.TryGetValue(tag, out EventHandler<DicomReaderEventArgs> handler))
            {
                handler(this, new DicomReaderEventArgs(source.Marker, tag, vr, data));
            }
        }

        public void OnBeginSequence(IByteSource source, DicomTag tag, uint length)
        {
            _stack.Push(new DicomReaderEventArgs(source.Marker, tag, DicomVR.SQ, null));
        }

        public void OnBeginSequenceItem(IByteSource source, uint length)
        {
        }

        public void OnEndSequenceItem()
        {
        }

        public void OnEndSequence()
        {
            DicomReaderEventArgs args = _stack.Pop();
            if (_callbacks.TryGetValue(args.Tag, out EventHandler<DicomReaderEventArgs> handler))
            {
                handler(this, args);
            }
        }

        public void OnBeginFragmentSequence(IByteSource source, DicomTag tag, DicomVR vr)
        {
            _stack.Push(new DicomReaderEventArgs(source.Marker, tag, vr, null));
        }

        public void OnFragmentSequenceItem(IByteSource source, IByteBuffer data)
        {
        }

        public void OnEndFragmentSequence()
        {
            DicomReaderEventArgs args = _stack.Pop();
            if (_callbacks.TryGetValue(args.Tag, out EventHandler<DicomReaderEventArgs> handler))
            {
                handler(this, args);
            }
        }
    }
}
