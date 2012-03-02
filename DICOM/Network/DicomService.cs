using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Dicom.Log;
using Dicom.Imaging.Codec;
using Dicom.IO;
using Dicom.IO.Reader;
using Dicom.IO.Writer;

namespace Dicom.Network {
	public abstract class DicomService {
		private Stream _network;
		private object _lock;
		private volatile bool _writing;
		private volatile bool _sending;
		private Queue<PDU> _pduQueue;
		private Queue<DicomMessage> _msgQueue;
		private List<DicomRequest> _pending;
		private int _asyncOpsWindow;
		private DicomMessage _dimse;
		private Stream _dimseStream;
		private int _readLength;
		private bool _isConnected;

		protected DicomService(Stream stream) {
			_network = stream;
			_lock = new object();
			_pduQueue = new Queue<PDU>();
			_msgQueue = new Queue<DicomMessage>();
			_pending = new List<DicomRequest>();
			_asyncOpsWindow = 1;
			_isConnected = true;
			Logger = DicomLog.DefaultLogger;
			BeginReadPDUHeader();
		}

		private DicomLogger Logger {
			get;
			set;
		}

		private string LogID {
			get;
			set;
		}

		public DicomAssociation Association {
			get;
			internal set;
		}

		public bool IsConnected {
			get {
				return _isConnected;
			}
		}

		private void BeginReadPDUHeader() {
			_readLength = 6;

			byte[] buffer = new byte[6];
			_network.BeginRead(buffer, 0, 6, EndReadPDUHeader, buffer);
		}

		private void EndReadPDUHeader(IAsyncResult result) {
			try {
				byte[] buffer = (byte[])result.AsyncState;

				int count = _network.EndRead(result);
				if (count == 0) {
					// disconnected
					_network.Close();
					_isConnected = false;
					return;
				}

				_readLength -= count;

				if (_readLength > 0) {
					_network.BeginRead(buffer, 6 - _readLength, _readLength, EndReadPDUHeader, buffer);
					return;
				}

				int length = BitConverter.ToInt32(buffer, 2);
				length = Endian.Swap(length);

				_readLength = length;

				Array.Resize(ref buffer, length + 6);

				_network.BeginRead(buffer, 6, length, EndReadPDU, buffer);
			} catch (ObjectDisposedException) {
				// silently ignore
				_network.Close();
				_isConnected = false;
			} catch (IOException) {
				// object disposed
				_network.Close();
				_isConnected = false;
			} catch (Exception e) {
				Logger.Log(DicomLogLevel.Error, "Exception processing PDU header: {0}", e.ToString());
			}
		}

		private void EndReadPDU(IAsyncResult result) {
			try {
				byte[] buffer = (byte[])result.AsyncState;

				int count = _network.EndRead(result);
				if (count == 0) {
					// disconnected
					_network.Close();
					_isConnected = false;
					return;
				}

				_readLength -= count;

				if (_readLength > 0) {
					_network.BeginRead(buffer, buffer.Length - _readLength, _readLength, EndReadPDU, buffer);
					return;
				}

				var raw = new RawPDU(buffer);

				switch (raw.Type) {
				case 0x01: {
						Association = new DicomAssociation();
						var pdu = new AAssociateRQ(Association);
						pdu.Read(raw);
						LogID = Association.CallingAE;
						Logger.Log(DicomLogLevel.Info, "{0} <- Association request:\n{1}", LogID, Association.ToString());
						if (this is IDicomServiceProvider)
							(this as IDicomServiceProvider).OnReceiveAssociationRequest(Association);
						break;
					}
				case 0x02: {
						var pdu = new AAssociateAC(Association);
						pdu.Read(raw);
						LogID = Association.CalledAE;
						Logger.Log(DicomLogLevel.Info, "{0} <- Association accept:\n{1}", LogID, Association.ToString());
						if (this is IDicomServiceUser)
							(this as IDicomServiceUser).OnReceiveAssociationAccept(Association);
						break;
					}
				case 0x03: {
						var pdu = new AAssociateRJ();
						pdu.Read(raw);
						Logger.Log(DicomLogLevel.Info, "{0} <- Association reject [result: {1}; source: {2}; reason: {3}]", LogID, pdu.Result, pdu.Source, pdu.Reason);
						if (this is IDicomServiceUser)
							(this as IDicomServiceUser).OnReceiveAssociationReject(pdu.Result, pdu.Source, pdu.Reason);
						break;
					}
				case 0x04: {
						var pdu = new PDataTF();
						pdu.Read(raw);
						ProcessPDataTF(pdu);
						break;
					}
				case 0x05: {
						var pdu = new AReleaseRQ();
						pdu.Read(raw);
						Logger.Log(DicomLogLevel.Info, "{0} <- Association release request", LogID);
						if (this is IDicomServiceProvider)
							(this as IDicomServiceProvider).OnReceiveAssociationReleaseRequest();
						break;
					}
				case 0x06: {
						var pdu = new AReleaseRP();
						pdu.Read(raw);
						Logger.Log(DicomLogLevel.Info, "{0} <- Association release response", LogID);
						if (this is IDicomServiceUser)
							(this as IDicomServiceUser).OnReceiveAssociationReleaseResponse();
						_network.Close();
						_isConnected = false;
						break;
					}
				case 0x07: {
						var pdu = new AAbort();
						pdu.Read(raw);
						Logger.Log(DicomLogLevel.Info, "{0} <- Abort: {1} - {2}", LogID, pdu.Source, pdu.Reason);
						if (this is IDicomServiceProvider)
							(this as IDicomServiceProvider).OnReceiveAbort(pdu.Source, pdu.Reason);
						else if (this is IDicomServiceUser)
							(this as IDicomServiceUser).OnReceiveAbort(pdu.Source, pdu.Reason);
						_network.Close();
						_isConnected = false;
						break;
					}
				case 0xFF: {
						break;
					}
				default:
					throw new DicomNetworkException("Unknown PDU type");
				}

				BeginReadPDUHeader();
			} catch (Exception e) {
				Logger.Log(DicomLogLevel.Error, "Exception processing PDU: {0}", e.ToString());
				_network.Close();
				_isConnected = false;
			}
		}

		private void ProcessPDataTF(PDataTF pdu) {
			try {
				foreach (var pdv in pdu.PDVs) {
					if (_dimse == null) {
						// create stream for receiving command
						if (_dimseStream == null)
							_dimseStream = new MemoryStream();
					} else {
						// create stream for receiving dataset
						if (_dimseStream == null) {
							if (_dimse.Type == DicomCommandField.CStoreRequest) {
								var pc = Association.PresentationContexts.FirstOrDefault(x => x.ID == pdv.PCID);

								var file = new DicomFile();
								file.FileMetaInfo.MediaStorageSOPClassUID = pc.AbstractSyntax;
								file.FileMetaInfo.MediaStorageSOPInstanceUID = _dimse.Command.Get<DicomUID>(DicomTag.AffectedSOPInstanceUID);
								file.FileMetaInfo.TransferSyntax = pc.AcceptedTransferSyntax;
								file.FileMetaInfo.ImplementationClassUID = Association.RemoteImplemetationClassUID;
								file.FileMetaInfo.ImplementationVersionName = Association.RemoteImplementationVersion;
								file.FileMetaInfo.SourceApplicationEntityTitle = Association.CallingAE;

								string fileName;
								if (this is IDicomCStoreProvider)
									fileName = (this as IDicomCStoreProvider).GetTempFileName(file.FileMetaInfo.MediaStorageSOPInstanceUID);
								else
									throw new DicomNetworkException("C-Store SCP not implemented");

								file.Save(fileName);

								_dimseStream = File.OpenWrite(fileName);
								_dimseStream.Seek(0, SeekOrigin.End);
							} else {
								_dimseStream = new MemoryStream();
							}
						}
					}

					_dimseStream.Write(pdv.Value, 0, pdv.Value.Length);

					if (pdv.IsLastFragment) {
						if (pdv.IsCommand) {
							_dimseStream.Seek(0, SeekOrigin.Begin);

							var command = new DicomDataset();

							var reader = new DicomReader();
							reader.IsExplicitVR = false;
							reader.Read(new StreamByteSource(_dimseStream), new DicomDatasetReaderObserver(command));

							_dimseStream = null;

							var type = command.Get<DicomCommandField>(DicomTag.CommandField);
							switch (type) {
							case DicomCommandField.CStoreRequest:
								_dimse = new DicomCStoreRequest(command);
								break;
							case DicomCommandField.CStoreResponse:
								_dimse = new DicomCStoreResponse(command);
								break;
							case DicomCommandField.CFindRequest:
								_dimse = new DicomCFindRequest(command);
								break;
							case DicomCommandField.CFindResponse:
								_dimse = new DicomCFindResponse(command);
								break;
							case DicomCommandField.CMoveRequest:
								_dimse = new DicomCMoveRequest(command);
								break;
							case DicomCommandField.CMoveResponse:
								_dimse = new DicomCMoveResponse(command);
								break;
							case DicomCommandField.CEchoRequest:
								_dimse = new DicomCEchoRequest(command);
								break;
							case DicomCommandField.CEchoResponse:
								_dimse = new DicomCEchoResponse(command);
								break;
							default:
								_dimse = new DicomMessage(command);
								break;
							}

							if (!_dimse.HasDataset) {
								ThreadPool.QueueUserWorkItem(PerformDimseCallback, _dimse);
								_dimse = null;
								return;
							}
						} else {
							if (_dimse.Type != DicomCommandField.CStoreRequest) {
								_dimseStream.Seek(0, SeekOrigin.Begin);

								_dimse.Dataset = new DicomDataset();
								_dimse.Dataset.InternalTransferSyntax = _dimse.Command.InternalTransferSyntax;

								var source = new StreamByteSource(_dimseStream);
								source.Endian = _dimse.Command.InternalTransferSyntax.Endian;

								var reader = new DicomReader();
								reader.IsExplicitVR = _dimse.Command.InternalTransferSyntax.IsExplicitVR;
								reader.Read(source, new DicomDatasetReaderObserver(_dimse.Dataset));

								_dimseStream = null;
							} else {
								var fileName = (_dimseStream as FileStream).Name;
								_dimseStream.Close();
								_dimseStream = null;

								var request = _dimse as DicomCStoreRequest;
								request.File = DicomFile.Open(fileName);
								request.File.IsTempFile = true;
								request.Dataset = request.File.Dataset;
							}

							ThreadPool.QueueUserWorkItem(PerformDimseCallback, _dimse);
							_dimse = null;
						}
					}
				}
			} catch (Exception e) {
				SendAbort(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified);
				Logger.Log(DicomLogLevel.Error, e.ToString());
			} finally {
				SendNextMessage();
			}
		}

		private void PerformDimseCallback(object state) {
			var dimse = state as DicomMessage;

			try {
				Logger.Log(DicomLogLevel.Info, "{0} <- {1}", LogID, dimse);

				if (!DicomMessage.IsRequest(dimse.Type))
					return;

				if (dimse.Type == DicomCommandField.CStoreRequest) {
					if (this is IDicomCStoreProvider) {
						var response = (this as IDicomCStoreProvider).OnCStoreRequest(dimse as DicomCStoreRequest);
						SendResponse(response);
						return;
					} else
						throw new DicomNetworkException("C-Store SCP not implemented");
				}

				if (dimse.Type == DicomCommandField.CFindRequest) {
					if (this is IDicomCFindProvider) {
						var responses = (this as IDicomCFindProvider).OnCFindRequest(dimse as DicomCFindRequest);
						foreach (var response in responses)
							SendResponse(response);
						return;
					} else
						throw new DicomNetworkException("C-Find SCP not implemented");
				}

				if (dimse.Type == DicomCommandField.CMoveRequest) {
					if (this is IDicomCMoveProvider) {
						var responses = (this as IDicomCMoveProvider).OnCMoveRequest(dimse as DicomCMoveRequest);
						foreach (var response in responses)
							SendResponse(response);
						return;
					} else
						throw new DicomNetworkException("C-Move SCP not implemented");
				}

				if (dimse.Type == DicomCommandField.CEchoRequest) {
					if (this is IDicomCEchoProvider) {
						var response = (this as IDicomCEchoProvider).OnCEchoRequest(dimse as DicomCEchoRequest);
						SendResponse(response);
						return;
					} else
						throw new DicomNetworkException("C-Echo SCP not implemented");
				}

				throw new DicomNetworkException("Operation not implemented");
			} finally {
				if (dimse is DicomResponse) {
					lock (_lock) {
						var req = _pending.FirstOrDefault(x => x.MessageID == (dimse as DicomResponse).RequestMessageID);
						if (req != null) {
							(req as DicomRequest).PostResponse(this, dimse as DicomResponse);
							_pending.Remove(req);
						}
					}
				}

				SendNextMessage();
			}
		}

		protected void SendPDU(PDU pdu) {
			lock (_lock)
				_pduQueue.Enqueue(pdu);
			SendNextPDU();
		}

		private void SendNextPDU() {
			PDU pdu;

			lock (_lock) {
				if (_writing)
					return;

				if (_pduQueue.Count == 0)
					return;
				
				_writing = true;

				pdu = _pduQueue.Dequeue();
			}

			MemoryStream ms = new MemoryStream();
			pdu.Write().WritePDU(ms);

			byte[] buffer = ms.ToArray();

			_network.BeginWrite(buffer, 0, (int)ms.Length, OnEndSendPDU, buffer);
		}

		private void OnEndSendPDU(IAsyncResult ar) {
			byte[] buffer = (byte[])ar.AsyncState;

			try {
				_network.EndWrite(ar);
			} catch {
			} finally {
				lock (_lock)
					_writing = false;
				SendNextPDU();
			}
		}

		private void SendMessage(DicomMessage message) {
			lock (_lock)
				_msgQueue.Enqueue(message);
			SendNextMessage();
		}

		private class Dimse {
			public DicomMessage Message;
			public PDataTFStream Stream;
			public DicomDatasetWalker Walker;
			public DicomPresentationContext PresentationContext;
		}

		private void SendNextMessage() {
			DicomMessage msg;

			lock (_lock) {
				if (_msgQueue.Count == 0) {
					if (_pending.Count == 0)
						OnSendQueueEmpty();
					return;
				}

				if (_sending)
					return;

				if (_pending.Count == _asyncOpsWindow)
					return;

				_sending = true;

				msg = _msgQueue.Dequeue();
			}

			Logger.Log(DicomLogLevel.Info, "{0} -> {1}", LogID, msg);

			if (msg is DicomRequest)
				_pending.Add(msg as DicomRequest);

			DicomPresentationContext pc = null;
			if (msg is DicomCStoreRequest) {
				pc = Association.PresentationContexts.FirstOrDefault(x => x.Result == DicomPresentationContextResult.Accept && x.AbstractSyntax == msg.AffectedSOPClassUID && x.AcceptedTransferSyntax == (msg as DicomCStoreRequest).TransferSyntax);
				if (pc == null)
					pc = Association.PresentationContexts.FirstOrDefault(x => x.Result == DicomPresentationContextResult.Accept && x.AbstractSyntax == msg.AffectedSOPClassUID);
			} else {
				pc = Association.PresentationContexts.FirstOrDefault(x => x.Result == DicomPresentationContextResult.Accept && x.AbstractSyntax == msg.AffectedSOPClassUID);
			}

			if (pc == null)
				throw new DicomNetworkException("No accepted presentation context found for abstract syntax: {0}", msg.AffectedSOPClassUID);

			var dimse = new Dimse();
			dimse.Message = msg;
			dimse.PresentationContext = pc;

			dimse.Stream = new PDataTFStream(this, pc.ID, (int)Association.MaximumPDULength);

			var writer = new DicomWriter(DicomTransferSyntax.ImplicitVRLittleEndian, DicomWriteOptions.Default, new StreamByteTarget(dimse.Stream));

			dimse.Walker = new DicomDatasetWalker(msg.Command);
			dimse.Walker.BeginWalk(writer, OnEndSendCommand, dimse);
		}

		private void OnEndSendCommand(IAsyncResult result) {
			var dimse = result.AsyncState as Dimse;
			try {
				dimse.Walker.EndWalk(result);

				if (!dimse.Message.HasDataset) {
					dimse.Stream.Flush(true);
					dimse.Stream.Close();
					return;
				}

				dimse.Stream.IsCommand = false;

				var dataset = dimse.Message.Dataset;

				if (dataset.InternalTransferSyntax != dimse.PresentationContext.AcceptedTransferSyntax)
					dataset = dataset.ChangeTransferSyntax(dimse.PresentationContext.AcceptedTransferSyntax);

				var writer = new DicomWriter(dimse.PresentationContext.AcceptedTransferSyntax, DicomWriteOptions.Default, new StreamByteTarget(dimse.Stream));

				dimse.Walker = new DicomDatasetWalker(dataset);
				dimse.Walker.BeginWalk(writer, OnEndSendMessage, dimse);
			} catch {
			} finally {
				if (!dimse.Message.HasDataset) {
					lock (_lock)
						_sending = false;
					SendNextMessage();
				}
			}
		}

		private void OnEndSendMessage(IAsyncResult result) {
			var dimse = result.AsyncState as Dimse;
			try {
				dimse.Walker.EndWalk(result);
			} catch {
			} finally {
				dimse.Stream.Flush(true);
				dimse.Stream.Close();

				lock (_lock)
					_sending = false;
				SendNextMessage();
			}
		}

		public void SendRequest(DicomRequest request) {
			SendMessage(request);
		}

		protected void SendResponse(DicomResponse response) {
			SendMessage(response);
		}

		private class PDataTFStream : Stream {
			#region Private Members
			private DicomService _service;
			private bool _command;
			private int _max;
			private byte _pcid;
			private PDataTF _pdu;
			private byte[] _bytes;
			private int _sent;
			private MemoryStream _buffer;
			#endregion

			#region Public Constructors
			public PDataTFStream(DicomService service, byte pcid, int max) {
				_service = service;
				_command = true;
				_pcid = pcid;
				_max = (max == 0) ? MaxPduSizeLimit : Math.Min(max, MaxPduSizeLimit);
				_pdu = new PDataTF();
				_buffer = new MemoryStream((int)_max * 2);
			}
			#endregion

			#region Public Properties
			public static int MaxPduSizeLimit = 16384;

			public bool IsCommand {
				get { return _command; }
				set {
					CreatePDV();
					_command = value;
					WritePDU(true);
				}
			}

			public int BytesSent {
				get { return _sent; }
			}
			#endregion

			#region Public Members
			public void Flush(bool last) {
				WritePDU(last);
			}
			#endregion

			#region Private Members
			private int CurrentPduSize() {
				return 6 + (int)_pdu.GetLengthOfPDVs();
			}

			private bool CreatePDV() {
				int len = Math.Min(GetBufferLength(), _max - CurrentPduSize() - 6);

				if (_bytes == null || _bytes.Length != len || _pdu.PDVs.Count > 0) {
					_bytes = new byte[len];
				}
				_sent = _buffer.Read(_bytes, 0, len);

				PDV pdv = new PDV(_pcid, _bytes, _command, false);
				_pdu.PDVs.Add(pdv);

				return pdv.IsLastFragment;
			}

			private void WritePDU(bool last) {
				if (_pdu.PDVs.Count == 0 || ((CurrentPduSize() + 6) < _max && GetBufferLength() > 0)) {
					CreatePDV();
				}
				if (_pdu.PDVs.Count > 0) {
					if (last)
						_pdu.PDVs[_pdu.PDVs.Count - 1].IsLastFragment = true;

					_service.SendPDU(_pdu);

					_pdu = new PDataTF();
				}
			}

			private void AppendBuffer(byte[] buffer, int offset, int count) {
				long pos = _buffer.Position;
				_buffer.Seek(0, SeekOrigin.End);
				_buffer.Write(buffer, offset, count);
				_buffer.Position = pos;
			}

			private int GetBufferLength() {
				return (int)(_buffer.Length - _buffer.Position);
			}
			#endregion

			#region Stream Members
			public override bool CanRead {
				get { return false; }
			}

			public override bool CanSeek {
				get { return false; }
			}

			public override bool CanWrite {
				get { return true; }
			}

			public override void Flush() {
			}

			public override long Length {
				get { throw new NotImplementedException(); }
			}

			public override long Position {
				get {
					throw new NotImplementedException();
				}
				set {
					throw new NotImplementedException();
				}
			}

			public override int Read(byte[] buffer, int offset, int count) {
				throw new NotImplementedException();
			}

			public override long Seek(long offset, SeekOrigin origin) {
				throw new NotImplementedException();
			}

			public override void SetLength(long value) {
				throw new NotImplementedException();
			}

			public override void Write(byte[] buffer, int offset, int count) {
				AppendBuffer(buffer, offset, count);
				while ((CurrentPduSize() + 6 + GetBufferLength()) > _max) {
					WritePDU(false);
				}
			}

			public void Write(Stream stream) {
				int max = _max - 12;
				int length = (int)stream.Length;
				int position = (int)stream.Position;
				byte[] buffer = new byte[max];
				while (position < length) {
					int count = Math.Min(max, length - position);
					count = stream.Read(buffer, 0, count);
					AppendBuffer(buffer, 0, count);
					position += count;
					WritePDU(position == length);
				}
			}
			#endregion
		}

		#region Send Methods
		protected void SendAssociationRequest(DicomAssociation association) {
			LogID = association.CalledAE;
			Logger.Log(DicomLogLevel.Info, "{0} -> Association request:\n{1}", LogID, association);
			Association = association;
			SendPDU(new AAssociateRQ(Association));
		}

		protected void SendAssociationAccept(DicomAssociation association) {
			Association = association;
			Logger.Log(DicomLogLevel.Info, "{0} -> Association accept:\n{1}", LogID, association);
			SendPDU(new AAssociateAC(Association));
		}

		protected void SendAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason) {
			Logger.Log(DicomLogLevel.Info, "{0} -> Association reject [result: {1}; source: {2}; reason: {3}]", LogID, result, source, reason);
			SendPDU(new AAssociateRJ(result, source, reason));
		}

		protected void SendAssociationReleaseRequest() {
			Logger.Log(DicomLogLevel.Info, "{0} -> Association release request", LogID);
			SendPDU(new AReleaseRQ());
		}

		protected void SendAssociationReleaseResponse() {
			Logger.Log(DicomLogLevel.Info, "{0} -> Association release response", LogID);
			SendPDU(new AReleaseRQ());
		}

		protected void SendAbort(DicomAbortSource source, DicomAbortReason reason) {
			Logger.Log(DicomLogLevel.Info, "{0} -> Abort: {1} - {2}", LogID, source.ToString(), reason.ToString());
			SendPDU(new AAbort(source, reason));
		}
		#endregion

		#region Override Methods
		protected virtual void OnSendQueueEmpty() {
		}
		#endregion
	}
}
