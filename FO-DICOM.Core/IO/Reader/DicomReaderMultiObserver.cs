// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;

namespace FellowOakDicom.IO.Reader
{

    internal class DicomReaderMultiObserver : IDicomReaderObserver
    {

        private readonly IDicomReaderObserver[] _observers;

        public DicomReaderMultiObserver(params IDicomReaderObserver[] observers)
        {
            _observers = observers;
        }

        public void OnElement(IByteSource source, long position, DicomTag tag, DicomVR vr, IByteBuffer data)
            => _observers.Each(observer => observer.OnElement(source, position, tag, vr, data));

        public void OnBeginSequence(IByteSource source, long position, DicomTag tag, uint length)
            => _observers.Each(observer => observer.OnBeginSequence(source, position, tag, length));

        public void OnBeginSequenceItem(IByteSource source, long position, uint length)
            => _observers.Each(observer => observer.OnBeginSequenceItem(source, position, length));

        public void OnEndSequenceItem()
            => _observers.Each(observer => observer.OnEndSequenceItem());

        public void OnEndSequence()
            => _observers.Each(observer => observer.OnEndSequence());

        public void OnBeginFragmentSequence(IByteSource source, long position, DicomTag tag, DicomVR vr)
            => _observers.Each(observer => observer.OnBeginFragmentSequence(source, position, tag, vr));

        public void OnFragmentSequenceItem(IByteSource source, long position, IByteBuffer data)
            => _observers.Each(observer => observer.OnFragmentSequenceItem(source, position, data));

        public void OnEndFragmentSequence()
            => _observers.Each(observer => observer.OnEndFragmentSequence());

    }
}
