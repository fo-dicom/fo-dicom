using System;
using System.Collections.Generic;
using System.Text;

namespace Dicom.IO.Buffer {
	public class ByteBufferByteSource : IByteSource {
		private List<IByteBuffer> _buffers;
		private Stack<long> _milestones;

		private long _expired;
		private long _marker;
		private long _position;
		private long _length;

		private int _current;
		private long _currentPos;
		private byte[] _currentData;

		private bool _fixed;
		private uint _required;
		private ByteSourceCallback _callback;
		private object _callbackState;

		private object _lock;

		private Endian _endian;

		public ByteBufferByteSource() {
			_expired = 0;
			_marker = 0;
			_position = 0;
			_length = 0;
			_endian = Endian.LocalMachine;

			_milestones = new Stack<long>();
			_buffers = new List<IByteBuffer>();
			_fixed = false;

			_current = -1;
			_lock = new object();
		}

		public ByteBufferByteSource(params IByteBuffer[] buffers) {
			_expired = 0;
			_marker = 0;
			_position = 0;
			_length = 0;
			_endian = Endian.LocalMachine;

			_milestones = new Stack<long>();
			_buffers = new List<IByteBuffer>(buffers);
			foreach (var x in _buffers) _length += x.Size;
			_fixed = true;

			_current = -1;
			_lock = new object();
		}

		public Endian Endian {
			get { return _endian; }
			set {
				_endian = value;
				SwapBuffers();
			}
		}

		public long Position {
			get { return _position; }
		}

		public long Marker {
			get { return _marker; }
		}

		public bool IsEOF {
			get { lock (_lock) { return _fixed && (_position >= _length); } }
		}

		public bool CanRewind {
			get { return true; }
		}

		public byte GetUInt8() {
			return NextByte();
		}

		public short GetInt16() {
			if (Endian == Endian.LocalMachine) {
				return unchecked((short)(
						(NextByte() << 0) |
						(NextByte() << 8)
					));
			} else {
				return unchecked((short)(
						(NextByte() << 8) |
						(NextByte() << 0)
					));
			}
		}

		public ushort GetUInt16() {
			if (Endian == Endian.LocalMachine) {
				return unchecked((ushort)(
						(NextByte() << 0) |
						(NextByte() << 8)
					));
			} else {
				return unchecked((ushort)(
						(NextByte() << 8) |
						(NextByte() << 0)
					));
			}
		}

		public int GetInt32() {
			if (Endian == Endian.LocalMachine) {
				return unchecked((int)(
						(NextByte() <<  0) |
						(NextByte() <<  8) |
						(NextByte() << 16) |
						(NextByte() << 24)
					));
			} else {
				return unchecked((int)(
						(NextByte() << 24) |
						(NextByte() << 16) |
						(NextByte() <<  8) |
						(NextByte() <<  0)
					));
			}
		}

		public uint GetUInt32() {
			if (Endian == Endian.LocalMachine) {
				return unchecked((uint)(
						(NextByte() <<  0) |
						(NextByte() <<  8) |
						(NextByte() << 16) |
						(NextByte() << 24)
					));
			} else {
				return unchecked((uint)(
						(NextByte() << 24) |
						(NextByte() << 16) |
						(NextByte() <<  8) |
						(NextByte() <<  0)
					));
			}
		}

		public long GetInt64() {
			byte[] b = GetBytes(8);
			if (Endian != Endian.LocalMachine)
				Array.Reverse(b);
			return BitConverter.ToInt64(b, 0);
		}

		public ulong GetUInt64() {
			byte[] b = GetBytes(8);
			if (Endian != Endian.LocalMachine)
				Array.Reverse(b);
			return BitConverter.ToUInt64(b, 0);
		}

		public float GetSingle() {
			byte[] b = GetBytes(4);
			if (Endian != Endian.LocalMachine)
				Array.Reverse(b);
			return BitConverter.ToSingle(b, 0);
		}

		public double GetDouble() {
			byte[] b = GetBytes(8);
			if (Endian != Endian.LocalMachine)
				Array.Reverse(b);
			return BitConverter.ToDouble(b, 0);
		}

		public byte[] GetBytes(int count) {
			lock (_lock) {
				int p = 0;
				byte[] bytes = new byte[count];
				while (count > 0) {
					if (_current == -1 || _currentPos >= _currentData.Length) {
						if (!SwapBuffers())
							throw new DicomIoException("Tried to retrieve {0} bytes past end of source.", count);
					}

					int n = (int)System.Math.Min(_currentData.Length - _currentPos, count);
					Array.Copy(_currentData, (int)_currentPos, bytes, p, n);

					count -= n;
					p += n;
					_position += n;
					_currentPos += n;
				}
				return bytes;
			}
		}

		public IByteBuffer GetBuffer(uint count) {
			return new MemoryByteBuffer(GetBytes((int)count));
		}

		public void Skip(int count) {
			lock (_lock) {
				_position += count;
				_currentPos += count;
				SwapBuffers();
			}
		}

		public void Mark() {
			lock (_lock) {
				_marker = _position;

				while (_buffers.Count > 0 && (_expired + _buffers[0].Size) < _marker) {
					_expired += _buffers[0].Size;
					_buffers.RemoveAt(0);
				}
			}
		}

		public void Rewind() {
			lock (_lock) {
				_position = _marker;
				SwapBuffers();
			}
		}

		public void PushMilestone(uint count) {
			lock (_lock)
				_milestones.Push(_position + count);
		}

		public void PopMilestone() {
			lock (_lock)
				_milestones.Pop();
		}

		public bool HasReachedMilestone() {
			lock (_lock) {
				if (_milestones.Count > 0 && _position >= _milestones.Peek())
					return true;
				return false;
			}
		}

		public bool Require(uint count) {
			return Require(count, null, null);
		}

		public bool Require(uint count, ByteSourceCallback callback, object state) {
			lock (_lock) {
				if ((_position + count) <= _length)
					return true;

				if (_fixed)
					throw new DicomIoException("Requested {0} bytes past end of byte source.", count);

				if (callback == null)
					throw new DicomIoException("Requested {0} bytes past end of byte source without providing a callback.", count);

				_required = count;
				_callback = callback;
				_callbackState = state;

				return false;
			}
		}

		public void Add(IByteBuffer buffer, bool last) {
			lock (_lock) {
				if (_fixed)
					throw new DicomIoException("Tried to extend fixed length byte source.");

				if (buffer != null && buffer.Size > 0) {
					_buffers.Add(buffer);
					_length += buffer.Size;

					if (_callback != null) {
						if ((_length - _position) >= _required) {
							_callback.BeginInvoke(this, _callbackState, Callback, _callback);
							_callback = null;
							_callbackState = null;
							_required = 0;
						}
					}
				}

				_fixed = last;
			}
		}

		private void Callback(IAsyncResult result) {
			try {
				ByteSourceCallback cb = (ByteSourceCallback)result.AsyncState;
				cb.EndInvoke(result);
			} catch {
			}
		}

		private bool SwapBuffers() {
			lock (_lock) {
				long pos = _position - _expired;

				for (int i = 0; i < _buffers.Count; i++) {
					if (pos < _buffers[i].Size) {
						_current = i;
						_currentPos = pos;
						_currentData = _buffers[i].Data;
						return true;
					}
					pos -= _buffers[i].Size;
				}

				return false;
			}
		}

		private byte NextByte() {
			lock (_lock) {
				if (_current == -1 || _currentPos >= _currentData.Length) {
					if (!SwapBuffers())
						throw new DicomIoException("Tried to retrieve byte past end of source.");
				}

				_position++;
				return _currentData[_currentPos++];
			}
		}
	}
}
