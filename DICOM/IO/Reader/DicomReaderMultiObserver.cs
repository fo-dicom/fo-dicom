using System.Collections.Generic;

using Dicom.IO;
using Dicom.IO.Buffer;

namespace Dicom.IO.Reader {
	public class DicomReaderMultiObserver : IDicomReaderObserver {
		private IDicomReaderObserver[] _observers;

		public DicomReaderMultiObserver(params IDicomReaderObserver[] observers) {
			_observers = observers;
		}

		public void OnElement(IByteSource source, DicomTag tag, DicomVR vr, IByteBuffer data) {
			foreach (IDicomReaderObserver observer in _observers)
				observer.OnElement(source, tag, vr, data);
		}

		public void OnBeginSequence(IByteSource source, DicomTag tag, uint length) {
			foreach (IDicomReaderObserver observer in _observers)
				observer.OnBeginSequence(source, tag, length);
		}

		public void OnBeginSequenceItem(IByteSource source, uint length) {
			foreach (IDicomReaderObserver observer in _observers)
				observer.OnBeginSequenceItem(source, length);
		}

		public void OnEndSequenceItem() {
			foreach (IDicomReaderObserver observer in _observers)
				observer.OnEndSequenceItem();
		}

		public void OnEndSequence() {
			foreach (IDicomReaderObserver observer in _observers)
				observer.OnEndSequence();
		}

		public void OnBeginFragmentSequence(IByteSource source, DicomTag tag, DicomVR vr) {
			foreach (IDicomReaderObserver observer in _observers)
				observer.OnBeginFragmentSequence(source, tag, vr);
		}

		public void OnFragmentSequenceItem(IByteSource source, IByteBuffer data) {
			foreach (IDicomReaderObserver observer in _observers)
				observer.OnFragmentSequenceItem(source, data);
		}

		public void OnEndFragmentSequence() {
			foreach (IDicomReaderObserver observer in _observers)
				observer.OnEndFragmentSequence();
		}
	}
}
