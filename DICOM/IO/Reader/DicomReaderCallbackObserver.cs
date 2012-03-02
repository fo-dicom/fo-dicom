using System;
using System.Collections.Generic;

using Dicom.IO;
using Dicom.IO.Buffer;

namespace Dicom.IO.Reader {
	public class DicomReaderCallbackObserver : IDicomReaderObserver {
		private Stack<DicomReaderEventArgs> _stack;
		private IDictionary<DicomTag, EventHandler<DicomReaderEventArgs>> _callbacks;

		public DicomReaderCallbackObserver() {
			_stack = new Stack<DicomReaderEventArgs>();
			_callbacks = new Dictionary<DicomTag, EventHandler<DicomReaderEventArgs>>();
		}

		public void Add(DicomTag tag, EventHandler<DicomReaderEventArgs> callback) {
			_callbacks.Add(tag, callback);
		}

		public void OnElement(IByteSource source, DicomTag tag, DicomVR vr, IByteBuffer data) {
			EventHandler<DicomReaderEventArgs> handler;
			if (_callbacks.TryGetValue(tag, out handler))
				handler(this, new DicomReaderEventArgs(source.Marker, tag, vr, data));
		}

		public void OnBeginSequence(IByteSource source, DicomTag tag, uint length) {
			_stack.Push(new DicomReaderEventArgs(source.Marker, tag, DicomVR.SQ, null));
		}

		public void OnBeginSequenceItem(IByteSource source, uint length) {
		}

		public void OnEndSequenceItem() {
		}

		public void OnEndSequence() {
			DicomReaderEventArgs args = _stack.Pop();
			EventHandler<DicomReaderEventArgs> handler;
			if (_callbacks.TryGetValue(args.Tag, out handler))
				handler(this, args);
		}

		public void OnBeginFragmentSequence(IByteSource source, DicomTag tag, DicomVR vr) {
			_stack.Push(new DicomReaderEventArgs(source.Marker, tag, vr, null));
		}

		public void OnFragmentSequenceItem(IByteSource source, IByteBuffer data) {
		}

		public void OnEndFragmentSequence() {
			DicomReaderEventArgs args = _stack.Pop();
			EventHandler<DicomReaderEventArgs> handler;
			if (_callbacks.TryGetValue(args.Tag, out handler))
				handler(this, args);
		}
	}
}
