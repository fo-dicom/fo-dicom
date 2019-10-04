// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO.Buffer;

namespace FellowOakDicom.IO.Reader
{

    public interface IDicomReaderObserver
    {

        void OnElement(IByteSource source, DicomTag tag, DicomVR vr, IByteBuffer data);

        void OnBeginSequence(IByteSource source, DicomTag tag, uint length);

        void OnBeginSequenceItem(IByteSource source, uint length);

        void OnEndSequenceItem();

        void OnEndSequence();

        void OnBeginFragmentSequence(IByteSource source, DicomTag tag, DicomVR vr);

        void OnFragmentSequenceItem(IByteSource source, IByteBuffer data);

        void OnEndFragmentSequence();
    }
}
