using System;
using System.Text;
using System.Threading;

using Dicom.IO;

namespace Dicom.IO.Reader {
	public class DicomFileReader {
		private readonly static DicomTag FileMetaInfoStopTag = new DicomTag(0x0002, 0xffff);

		private IDicomReader _reader;
		private EventAsyncResult _async;
		private DicomReaderResult _result;
		private Exception _exception;

		private IByteSource _source;
		private IDicomReaderObserver _fmiObserver;
		private IDicomReaderObserver _dataObserver;

		private DicomTransferSyntax _syntax;

		public DicomFileReader() {
			_reader = new DicomReader();
			_syntax = DicomTransferSyntax.ExplicitVRLittleEndian;
		}

		public DicomReaderResult Read(IByteSource source, IDicomReaderObserver fileMetaInfo, IDicomReaderObserver dataset) {
			return EndRead(BeginRead(source, fileMetaInfo, dataset, null, null));
		}

		public IAsyncResult BeginRead(IByteSource source, IDicomReaderObserver fileMetaInfo, IDicomReaderObserver dataset, AsyncCallback callback, object state) {
			_result = DicomReaderResult.Processing;
			_source = source;
			_fmiObserver = fileMetaInfo;
			_dataObserver = dataset;
			_async = new EventAsyncResult(callback, state);
			ParsePreamble(source, null); // ThreadPool?
			return _async;
		}

		public DicomReaderResult EndRead(IAsyncResult result) {
			_async.AsyncWaitHandle.WaitOne();
			if (_exception != null)
				throw _exception;
			return _reader.Status;
		}

		private void ParsePreamble(IByteSource source, object state) {
			try {
				if (!source.Require(132, ParsePreamble, state))
					return;

				_source.Skip(128);
				if (_source.GetUInt8() != 'D' ||
					_source.GetUInt8() != 'I' ||
					_source.GetUInt8() != 'C' ||
					_source.GetUInt8() != 'M')
					throw new DicomReaderException("Invalid preamble found in DICOM file parser");

				DicomReaderCallbackObserver obs = new DicomReaderCallbackObserver();
				obs.Add(DicomTag.TransferSyntaxUID, delegate(object sender, DicomReaderEventArgs ea) {
					try {
						string uid = Encoding.ASCII.GetString(ea.Data.Data);
						_syntax = DicomTransferSyntax.Parse(uid);
					} catch {
					}
				});

				_source.Endian = _syntax.Endian;
				_reader.IsExplicitVR = _syntax.IsExplicitVR;
				_reader.BeginRead(_source, new DicomReaderMultiObserver(obs, _fmiObserver), FileMetaInfoStopTag, OnFileMetaInfoParseComplete, null);
			} catch (Exception e) {
				if (_exception == null)
					_exception = e;
				_result = DicomReaderResult.Error;
			} finally {
				if (_result != DicomReaderResult.Processing && _result != DicomReaderResult.Suspended) {
					_async.Set();
				}
			}
		}

		private void OnFileMetaInfoParseComplete(IAsyncResult result) {
			try {
				if (_reader.EndRead(result) != DicomReaderResult.Stopped)
					throw new DicomReaderException("DICOM File Meta Info ended prematurely");

				// rewind to last marker (start of previous tag)... ugly because 
				// it requires knowledge of how the parser is implemented
				_source.Rewind();

				_source.Endian = _syntax.Endian;
				_reader.IsExplicitVR = _syntax.IsExplicitVR;
				_reader.BeginRead(_source, _dataObserver, null, OnDatasetParseComplete, null);
			} catch (Exception e) {
				if (_exception == null)
					_exception = e;
				_result = DicomReaderResult.Error;
			} finally {
				if (_result == DicomReaderResult.Error) {
					_async.Set();
				}
			}
		}

		private void OnDatasetParseComplete(IAsyncResult result) {
			try {
				_result = _reader.EndRead(result);
			} catch (Exception e) {
				if (_exception == null)
					_exception = e;
				_result = DicomReaderResult.Error;
			} finally {
				_async.Set();
			}
		}
	}
}
