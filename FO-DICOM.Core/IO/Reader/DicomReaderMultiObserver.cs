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

        public void OnElement(IByteSource source, DicomTag tag, DicomVR vr, IByteBuffer data)
            => _observers.Each(observer => observer.OnElement(source, tag, vr, data));

        public void OnBeginSequence(IByteSource source, DicomTag tag, uint length)
            => _observers.Each(observer => observer.OnBeginSequence(source, tag, length));

        public void OnBeginSequenceItem(IByteSource source, uint length)
            => _observers.Each(observer => observer.OnBeginSequenceItem(source, length));

        public void OnEndSequenceItem()
            => _observers.Each(observer => observer.OnEndSequenceItem());

        public void OnEndSequence()
            => _observers.Each(observer => observer.OnEndSequence());

        public void OnBeginFragmentSequence(IByteSource source, DicomTag tag, DicomVR vr)
            => _observers.Each(observer => observer.OnBeginFragmentSequence(source, tag, vr));

        public void OnFragmentSequenceItem(IByteSource source, IByteBuffer data)
            => _observers.Each(observer => observer.OnFragmentSequenceItem(source, data));

        public void OnEndFragmentSequence()
            => _observers.Each(observer => observer.OnEndFragmentSequence());

    }
}
