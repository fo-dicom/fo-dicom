using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.IO;

namespace Dicom.IO.Writer {
	public class DicomFileWriter {
		private EventAsyncResult _async;
		private Exception _exception;

		private IByteTarget _target;
		private DicomFileMetaInformation _fileMetaInfo;
		private DicomDataset _dataset;

		private DicomWriteOptions _options;

		public DicomFileWriter(DicomWriteOptions options) {
			_options = options;
		}

		public IByteTarget Target {
			get { return _target; }
		}

		public void Write(IByteTarget target, DicomFileMetaInformation fileMetaInfo, DicomDataset dataset) {
			EndWrite(BeginWrite(target, fileMetaInfo, dataset, null, null));
		}

		public IAsyncResult BeginWrite(IByteTarget target, DicomFileMetaInformation fileMetaInfo, DicomDataset dataset, AsyncCallback callback, object state) {
			_target = target;
			_fileMetaInfo = fileMetaInfo;
			_dataset = dataset;

			_async = new EventAsyncResult(callback, state);

			byte[] preamble = new byte[132];
			preamble[128] = (byte)'D';
			preamble[129] = (byte)'I';
			preamble[130] = (byte)'C';
			preamble[131] = (byte)'M';

			_target.Write(preamble, 0, 132, OnCompletePreamble, null);

			return _async;
		}

		public void EndWrite(IAsyncResult result) {
			_async.AsyncWaitHandle.WaitOne();
			if (_exception != null)
				throw _exception;
		}

		private void OnCompletePreamble(IByteTarget target, object state) {
			// recalculate FMI group length as required by standard
			_fileMetaInfo.RecalculateGroupLengths();

			DicomWriter writer = new DicomWriter(DicomTransferSyntax.ExplicitVRLittleEndian, _options, _target);
			DicomDatasetWalker walker = new DicomDatasetWalker(_fileMetaInfo);
			walker.BeginWalk(writer, OnCompleteFileMetaInfo, walker);
		}

		private void OnCompleteFileMetaInfo(IAsyncResult result) {
			try {
				DicomDatasetWalker walker;

				if (result != null) {
					walker = result.AsyncState as DicomDatasetWalker;
					walker.EndWalk(result);
				}

				//DicomTransferSyntax syntax = _fileMetaInfo.TransferSyntax;
                DicomTransferSyntax syntax = _fileMetaInfo.InternalTransferSyntax;
				if (_options.KeepGroupLengths) {
					// update transfer syntax and recalculate existing group lengths
					_dataset.InternalTransferSyntax = syntax;
					_dataset.RecalculateGroupLengths(false);
				} else {
					// remove group lengths as suggested in PS 3.5 7.2
					//
					//	2. It is recommended that Group Length elements be removed during storage or transfer 
					//	   in order to avoid the risk of inconsistencies arising during coercion of data 
					//	   element values and changes in transfer syntax.
					_dataset.RemoveGroupLengths();
				}

				DicomWriter writer = new DicomWriter(syntax, _options, _target);
				walker = new DicomDatasetWalker(_dataset);
				walker.BeginWalk(writer, OnCompleteDataset, walker);
			} catch (Exception e) {
				_exception = e;
				_async.Set();
			}
		}

		private void OnCompleteDataset(IAsyncResult result) {
			try {
				DicomDatasetWalker walker = result.AsyncState as DicomDatasetWalker;
				walker.EndWalk(result);
			} catch (Exception e) {
				_exception = e;
			} finally {
				_async.Set();
			}
		}
	}
}
