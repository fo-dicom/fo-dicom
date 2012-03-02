using Dicom.IO;
using Dicom.IO.Buffer;

namespace Dicom.IO.Reader {
	public interface IDicomReaderObserver {
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
