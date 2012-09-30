using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using NLog;

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
		private DicomMessage _dimse;
		private Stream _dimseStream;
		private int _readLength;
		private bool _isConnected;

		protected DicomService(Stream stream, Logger log) {
			_network = stream;
			_lock = new object();
			_pduQueue = new Queue<PDU>();
			MaximumPDUsInQueue = 16;
			_msgQueue = new Queue<DicomMessage>();
			_pending = new List<DicomRequest>();
			_isConnected = true;
			Logger = log ?? LogManager.GetLogger("Dicom.Network");
			BeginReadPDUHeader();
		}

		public Logger Logger {
			get;
			set;
		}

		private string LogID {
			get;
			set;
		}

		public object UserState {
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

		public bool IsSendQueueEmpty {
			get {
				lock (_lock)
					return _pending.Count == 0;
			}
		}

		public int MaximumPDUsInQueue {
			get;
			set;
		}

		private void CloseConnection(int errorCode) {
			_isConnected = false;
			try { _network.Close(); } catch { }

			if (this is IDicomServiceProvider)
				(this as IDicomServiceProvider).OnConnectionClosed(errorCode);
			else if (this is IDicomServiceUser)
				(this as IDicomServiceUser).OnConnectionClosed(errorCode);
		}

		private void BeginReadPDUHeader() {
			try {
				_readLength = 6;

				byte[] buffer = new byte[6];
				_network.BeginRead(buffer, 0, 6, EndReadPDUHeader, buffer);
			} catch (ObjectDisposedException) {
				// silently ignore
				CloseConnection(0);
			} catch (IOException e) {
				int error = 0;
				if (e.InnerException is SocketException) {
					error = (e.InnerException as SocketException).ErrorCode;
					Logger.Error("Socket error while reading PDU: {0} [{1}]", (e.InnerException as SocketException).SocketErrorCode, (e.InnerException as SocketException).ErrorCode);
				} else if (!(e.InnerException is ObjectDisposedException))
					Logger.Error("IO exception while reading PDU: {0}", e.ToString());

				CloseConnection(error);
			}
		}

		private void EndReadPDUHeader(IAsyncResult result) {
			try {
				byte[] buffer = (byte[])result.AsyncState;

				int count = _network.EndRead(result);
				if (count == 0) {
					// disconnected
					CloseConnection(0);
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
				CloseConnection(0);
			} catch (IOException e) {
				int error = 0;
				if (e.InnerException is SocketException) {
					error = (e.InnerException as SocketException).ErrorCode;
					Logger.Error("Socket error while reading PDU: {0} [{1}]", (e.InnerException as SocketException).SocketErrorCode, (e.InnerException as SocketException).ErrorCode);
				} else if (!(e.InnerException is ObjectDisposedException))
					Logger.Error("IO exception while reading PDU: {0}", e.ToString());

				CloseConnection(error);
			} catch (Exception e) {
				Logger.Log(LogLevel.Error, "Exception processing PDU header: {0}", e.ToString());
			}
		}

		private void EndReadPDU(IAsyncResult result) {
			try {
				byte[] buffer = (byte[])result.AsyncState;

				int count = _network.EndRead(result);
				if (count == 0) {
					// disconnected
					CloseConnection(0);
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
						Logger.Log(LogLevel.Info, "{0} <- Association request:\n{1}", LogID, Association.ToString());
						if (this is IDicomServiceProvider)
							(this as IDicomServiceProvider).OnReceiveAssociationRequest(Association);
						break;
					}
				case 0x02: {
						var pdu = new AAssociateAC(Association);
						pdu.Read(raw);
						LogID = Association.CalledAE;
						Logger.Log(LogLevel.Info, "{0} <- Association accept:\n{1}", LogID, Association.ToString());
						if (this is IDicomServiceUser)
							(this as IDicomServiceUser).OnReceiveAssociationAccept(Association);
						break;
					}
				case 0x03: {
						var pdu = new AAssociateRJ();
						pdu.Read(raw);
						Logger.Log(LogLevel.Info, "{0} <- Association reject [result: {1}; source: {2}; reason: {3}]", LogID, pdu.Result, pdu.Source, pdu.Reason);
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
						Logger.Log(LogLevel.Info, "{0} <- Association release request", LogID);
						if (this is IDicomServiceProvider)
							(this as IDicomServiceProvider).OnReceiveAssociationReleaseRequest();
						break;
					}
				case 0x06: {
						var pdu = new AReleaseRP();
						pdu.Read(raw);
						Logger.Log(LogLevel.Info, "{0} <- Association release response", LogID);
						if (this is IDicomServiceUser)
							(this as IDicomServiceUser).OnReceiveAssociationReleaseResponse();
						CloseConnection(0);
						break;
					}
				case 0x07: {
						var pdu = new AAbort();
						pdu.Read(raw);
						Logger.Log(LogLevel.Info, "{0} <- Abort: {1} - {2}", LogID, pdu.Source, pdu.Reason);
						if (this is IDicomServiceProvider)
							(this as IDicomServiceProvider).OnReceiveAbort(pdu.Source, pdu.Reason);
						else if (this is IDicomServiceUser)
							(this as IDicomServiceUser).OnReceiveAbort(pdu.Source, pdu.Reason);
						CloseConnection(0);
						break;
					}
				case 0xFF: {
						break;
					}
				default:
					throw new DicomNetworkException("Unknown PDU type");
				}

				BeginReadPDUHeader();
			} catch (IOException e) {
				int error = 0;
				if (e.InnerException is SocketException) {
					error = (e.InnerException as SocketException).ErrorCode;
					Logger.Error("Socket error while reading PDU: {0} [{1}]", (e.InnerException as SocketException).SocketErrorCode, (e.InnerException as SocketException).ErrorCode);
				} else if (!(e.InnerException is ObjectDisposedException))
					Logger.Error("IO exception while reading PDU: {0}", e.ToString());

				CloseConnection(error);
			} catch (Exception e) {
				Logger.Log(LogLevel.Error, "Exception processing PDU: {0}", e.ToString());
				CloseConnection(0);
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

								var fileName = TemporaryFile.Create();

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

								try {
									request.File = DicomFile.Open(fileName);
								} catch (Exception e) {
									// failed to parse received DICOM file; send error response instead of aborting connection
									SendResponse(new DicomCStoreResponse(request, new DicomStatus(DicomStatus.ProcessingFailure, e.Message)));
									Logger.Error("Error parsing C-Store dataset: " + e.ToString());
									(this as IDicomCStoreProvider).OnCStoreRequestException(fileName, e);
									return;
								}

								request.File.File.IsTempFile = true;
								request.Dataset = request.File.Dataset;
							}

							ThreadPool.QueueUserWorkItem(PerformDimseCallback, _dimse);
							_dimse = null;
						}
					}
				}
			} catch (Exception e) {
				SendAbort(DicomAbortSource.ServiceUser, DicomAbortReason.NotSpecified);
				Logger.Error(e.ToString());
			} finally {
				SendNextMessage();
			}
		}

		private void PerformDimseCallback(object state) {
			var dimse = state as DicomMessage;

			try {
				Logger.Log(LogLevel.Info, "{0} <- {1}", LogID, dimse);

				if (!DicomMessage.IsRequest(dimse.Type)) {
					var rsp = dimse as DicomResponse;
					lock (_lock) {
						var req = _pending.FirstOrDefault(x => x.MessageID == rsp.RequestMessageID);
						if (req != null) {
							rsp.UserState = req.UserState;
							(req as DicomRequest).PostResponse(this, rsp);
							if (rsp.Status.State != DicomState.Pending)
								_pending.Remove(req);
						}
					}
					return;
				}

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
				SendNextMessage();
			}
		}

		protected void SendPDU(PDU pdu) {
			// throttle queueing of PDUs to prevent out of memory errors for very large datasets
			do {
				if (_pduQueue.Count >= MaximumPDUsInQueue) {
					Thread.Sleep(10);
					continue;
				}

				lock (_lock)
					_pduQueue.Enqueue(pdu);

				break;
			} while (true);

			SendNextPDU();
		}

		private void SendNextPDU() {
			if (!_isConnected)
				return;

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

			try {
				_network.BeginWrite(buffer, 0, (int)ms.Length, OnEndSendPDU, buffer);
			} catch (IOException e) {
				int error = 0;
				if (e.InnerException is SocketException) {
					error = (e.InnerException as SocketException).ErrorCode;
					Logger.Error("Socket error while writing PDU: {0} [{1}]", (e.InnerException as SocketException).SocketErrorCode, (e.InnerException as SocketException).ErrorCode);
				} else if (!(e.InnerException is ObjectDisposedException))
					Logger.Error("IO exception while writing PDU: {0}", e.ToString());

				CloseConnection(error);
			}
		}

		private void OnEndSendPDU(IAsyncResult ar) {
			byte[] buffer = (byte[])ar.AsyncState;

			try {
				_network.EndWrite(ar);
			} catch (IOException e) {
				int error = 0;
				if (e.InnerException is SocketException) {
					error = (e.InnerException as SocketException).ErrorCode;
					Logger.Error("Socket error while writing PDU: {0} [{1}]", (e.InnerException as SocketException).SocketErrorCode, (e.InnerException as SocketException).ErrorCode);
				} else if (!(e.InnerException is ObjectDisposedException))
					Logger.Error("IO exception while writing PDU: {0}", e.ToString());

				CloseConnection(error);
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

				if (Association.MaxAsyncOpsInvoked > 0 && _pending.Count >= Association.MaxAsyncOpsInvoked)
					return;

				_sending = true;

				msg = _msgQueue.Dequeue();
			}

			Logger.Log(LogLevel.Info, "{0} -> {1}", LogID, msg);

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

				// remove group lengths as suggested in PS 3.5 7.2
				//
				//	2. It is recommended that Group Length elements be removed during storage or transfer 
				//	   in order to avoid the risk of inconsistencies arising during coercion of data 
				//	   element values and changes in transfer syntax.
				dataset.RemoveGroupLengths();

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
			private int _pduMax;
			private int _max;
			private byte _pcid;
			private PDataTF _pdu;
			private byte[] _bytes;
			private int _length;
			#endregion

			#region Public Constructors
			public PDataTFStream(DicomService service, byte pcid, int max) {
				_service = service;
				_command = true;
				_pcid = pcid;
				_pduMax = max;
				_max = (max == 0) ? MaxCommandBuffer : Math.Min(max, MaxCommandBuffer);

				_pdu = new PDataTF();

				// Max PDU Size - Current Size - Size of PDV header
				_bytes = new byte[_max - CurrentPduSize() - 6];
			}
			#endregion

			#region Public Properties
			private const int MaxCommandBuffer = 1 * 1024; // 1KB
			private const int MaxDataBuffer = 1 * 1024 * 1024; // 1MB

			public bool IsCommand {
				get { return _command; }
				set {
					// recalculate maximum PDU buffer size
					if (value)
						_max = (_pduMax == 0) ? MaxCommandBuffer : Math.Min(_pduMax, MaxCommandBuffer);
					else
						_max = (_pduMax == 0) ? MaxDataBuffer : Math.Min(_pduMax, MaxDataBuffer);

					CreatePDV(true);
					_command = value;
				}
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

			private void CreatePDV(bool last) {
				if (_bytes == null)
					_bytes = new byte[0];

				if (_length < _bytes.Length)
					Array.Resize(ref _bytes, _length);

				PDV pdv = new PDV(_pcid, _bytes, _command, last);
				_pdu.PDVs.Add(pdv);

				// is the current PDU at its maximum size or do we have room for another PDV?
				if ((CurrentPduSize() + 6) >= _max || last)
					WritePDU(last);

				// Max PDU Size - Current Size - Size of PDV header
				int max = _max - CurrentPduSize() - 6;

				_bytes = last ? null : new byte[max];
				_length = 0;
			}

			private void WritePDU(bool last) {
				if (_pdu.PDVs.Count == 0 && last)
					CreatePDV(true);
				else if (_pdu.PDVs.Count > 0) {
					if (last)
						_pdu.PDVs[_pdu.PDVs.Count - 1].IsLastFragment = true;

					_service.SendPDU(_pdu);

					_pdu = new PDataTF();
				}
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
				if (_bytes == null || _bytes.Length == 0) {
					// Max PDU Size - Current Size - Size of PDV header
					int max = _max - CurrentPduSize() - 6;
					_bytes = new byte[max];
				}

				while (count >= (_bytes.Length - _length)) {
					int c = Math.Min(count, _bytes.Length - _length);

					Array.Copy(buffer, offset, _bytes, _length, c);

					_length += c;
					offset += c;
					count -= c;

					CreatePDV(false);
				}

				if (count > 0) {
					Array.Copy(buffer, offset, _bytes, _length, count);
					_length += count;

					if (_bytes.Length == _length)
						CreatePDV(false);
				}
			}
			#endregion
		}

		#region Send Methods
		protected void SendAssociationRequest(DicomAssociation association) {
			LogID = association.CalledAE;
			Logger.Log(LogLevel.Info, "{0} -> Association request:\n{1}", LogID, association.ToString());
			Association = association;
			SendPDU(new AAssociateRQ(Association));
		}

		protected void SendAssociationAccept(DicomAssociation association) {
			Association = association;

			// reject all presentation contexts that have not already been accepted or rejected
			foreach (var pc in Association.PresentationContexts) {
				if (pc.Result == DicomPresentationContextResult.Proposed)
					pc.SetResult(DicomPresentationContextResult.RejectNoReason);
			}

			Logger.Log(LogLevel.Info, "{0} -> Association accept:\n{1}", LogID, association.ToString());
			SendPDU(new AAssociateAC(Association));
		}

		protected void SendAssociationReject(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason) {
			Logger.Log(LogLevel.Info, "{0} -> Association reject [result: {1}; source: {2}; reason: {3}]", LogID, result, source, reason);
			SendPDU(new AAssociateRJ(result, source, reason));
		}

		protected void SendAssociationReleaseRequest() {
			Logger.Log(LogLevel.Info, "{0} -> Association release request", LogID);
			SendPDU(new AReleaseRQ());
		}

		protected void SendAssociationReleaseResponse() {
			Logger.Log(LogLevel.Info, "{0} -> Association release response", LogID);
			SendPDU(new AReleaseRP());
		}

		protected void SendAbort(DicomAbortSource source, DicomAbortReason reason) {
			Logger.Log(LogLevel.Info, "{0} -> Abort [source: {1}; reason: {2}]", LogID, source, reason);
			SendPDU(new AAbort(source, reason));
		}
		#endregion

		#region Override Methods
		protected virtual void OnSendQueueEmpty() {
		}
		#endregion
	}
}
