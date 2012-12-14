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
		private DicomFileFormat _fileFormat;

		private DicomTransferSyntax _syntax;

		public DicomFileReader() {
			_reader = new DicomReader();
			_fileFormat = DicomFileFormat.Unknown;
			_syntax = DicomTransferSyntax.ExplicitVRLittleEndian;
		}

		public IByteSource Source {
			get { return _source; }
		}

		public DicomFileFormat FileFormat {
			get { return _fileFormat; }
		}

		public DicomTransferSyntax Syntax {
			get { return _syntax; }
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

				// mark file origin
				_source.Mark();

				// test for DICM preamble
				_source.Skip(128);
				if (_source.GetUInt8() == 'D' &&
					_source.GetUInt8() == 'I' &&
					_source.GetUInt8() == 'C' &&
					_source.GetUInt8() == 'M')
					_fileFormat = DicomFileFormat.DICOM3;

				// test for incorrect syntax in file meta info
				 do {
					 if (_fileFormat == DicomFileFormat.DICOM3) {
						 // move milestone to after preamble
						 _source.Mark();
					 } else {
						 // rewind to origin milestone
						 _source.Rewind();
					 }

					// test for file meta info
					var group = _source.GetUInt16();

					if (group > 0x00ff) {
						_source.Endian = Endian.Big;
						_syntax = DicomTransferSyntax.ExplicitVRBigEndian;

						group = Endian.Swap(group);
					}

					if (group > 0x00ff) {
						// invalid starting tag
						_fileFormat = DicomFileFormat.Unknown;
						_source.Rewind();
						break;
					}

					if (_fileFormat == DicomFileFormat.Unknown) {
						if (group == 0x0002)
							_fileFormat = DicomFileFormat.DICOM3NoPreamble;
						else
							_fileFormat = DicomFileFormat.DICOM3NoFileMetaInfo;
					}

					var element = _source.GetUInt16();
					var tag = new DicomTag(group, element);

					// test for explicit VR
					var vrt = Encoding.UTF8.GetBytes(tag.DictionaryEntry.ValueRepresentations[0].Code);
					var vrs = _source.GetBytes(2);

					if (vrt[0] != vrs[0] || vrt[1] != vrs[1]) {
						// implicit VR
						if (_syntax.Endian == Endian.Little)
							_syntax = DicomTransferSyntax.ImplicitVRLittleEndian;
						else
							_syntax = DicomTransferSyntax.ImplicitVRBigEndian;
					}

					_source.Rewind();
				} while (_fileFormat == DicomFileFormat.Unknown);

				if (_fileFormat == DicomFileFormat.Unknown)
					throw new DicomReaderException("Attempted to read invalid DICOM file");

				var obs = new DicomReaderCallbackObserver();
				if (_fileFormat != DicomFileFormat.DICOM3) {
					obs.Add(DicomTag.RecognitionCodeRETIRED, (object sender, DicomReaderEventArgs ea) => {
						try {
							string code = Encoding.UTF8.GetString(ea.Data.Data, 0, ea.Data.Data.Length);
							if (code == "ACR-NEMA 1.0")
								_fileFormat = DicomFileFormat.ACRNEMA1;
							else if (code == "ACR-NEMA 2.0")
								_fileFormat = DicomFileFormat.ACRNEMA2;
						} catch {
						}
					});
				}
				obs.Add(DicomTag.TransferSyntaxUID, (object sender, DicomReaderEventArgs ea) => {
					try {
						string uid = Encoding.UTF8.GetString(ea.Data.Data, 0, ea.Data.Data.Length);
						_syntax = DicomTransferSyntax.Parse(uid);
					} catch {
					}
				});

				_source.Endian = _syntax.Endian;
				_reader.IsExplicitVR = _syntax.IsExplicitVR;

				if (_fileFormat == DicomFileFormat.DICOM3NoFileMetaInfo)
					_reader.BeginRead(_source, new DicomReaderMultiObserver(obs, _dataObserver), null, OnDatasetParseComplete, null);
				else
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
