using System;
using System.Collections.Generic;
using System.IO;

using Dicom.IO.Buffer;

namespace Dicom.IO {
	public class FileByteSource : IByteSource, IDisposable {
		private FileReference _file;
		private Stream _stream;
		private Endian _endian;
		private BinaryReader _reader;
		private long _mark;

		private int _largeObjectSize;

		private Stack<long> _milestones;
		private object _lock;

		public FileByteSource(FileReference file) {
			_file = file;
			_stream = _file.OpenRead();
			_endian = Endian.LocalMachine;
			_reader = EndianBinaryReader.Create(_stream, _endian);
			_mark = 0;

			_largeObjectSize = 64 * 1024;

			_milestones = new Stack<long>();
			_lock = new object();
		}

		public Endian Endian {
			get { return _endian; }
			set {
				if (_endian != value) {
					lock (_lock) {
						_endian = value;
						_reader = EndianBinaryReader.Create(_stream, _endian);
					}
				}
			}
		}

		public long Position {
			get { return _stream.Position; }
		}

		public long Marker {
			get { return _mark; }
		}

		public bool IsEOF {
			get { return _stream.Position >= _stream.Length; }
		}

		public bool CanRewind {
			get { return _stream.CanSeek; }
		}

		public int LargeObjectSize {
			get { return _largeObjectSize; }
			set { _largeObjectSize = value; }
		}

		public byte GetUInt8() {
			return _reader.ReadByte();
		}

		public short GetInt16() {
			return _reader.ReadInt16();
		}

		public ushort GetUInt16() {
			return _reader.ReadUInt16();
		}

		public int GetInt32() {
			return _reader.ReadInt32();
		}

		public uint GetUInt32() {
			return _reader.ReadUInt32();
		}

		public long GetInt64() {
			return _reader.ReadInt64();
		}

		public ulong GetUInt64() {
			return _reader.ReadUInt64();
		}

		public float GetSingle() {
			return _reader.ReadSingle();
		}

		public double GetDouble() {
			return _reader.ReadDouble();
		}

		public byte[] GetBytes(int count) {
			return _reader.ReadBytes(count);
		}

		public IByteBuffer GetBuffer(uint count) {
			IByteBuffer buffer = null;
			if (count == 0)
				buffer = EmptyBuffer.Value;
			else if (count >= _largeObjectSize) {
				buffer = new FileByteBuffer(_file, _stream.Position, count);
				_stream.Seek((int)count, SeekOrigin.Current);
			} else
				buffer = new MemoryByteBuffer(GetBytes((int)count));
			return buffer;
		}

		public void Skip(int count) {
			_stream.Seek(count, SeekOrigin.Current);
		}

		public void Mark() {
			_mark = _stream.Position;
		}

		public void Rewind() {
			_stream.Position = _mark;
		}

		public void PushMilestone(uint count) {
			lock (_lock)
				_milestones.Push(_stream.Position + count);
		}

		public void PopMilestone() {
			lock (_lock)
				_milestones.Pop();
		}

		public bool HasReachedMilestone() {
			lock (_lock) {
				if (_milestones.Count > 0 && _stream.Position >= _milestones.Peek())
					return true;
				return false;
			}
		}

		public bool Require(uint count) {
			return Require(count, null, null);
		}

		public bool Require(uint count, ByteSourceCallback callback, object state) {
			lock (_lock) {
				if ((_stream.Length - _stream.Position) >= count)
					return true;

				throw new DicomIoException("Requested {0} bytes past end of file.", count);
			}
		}

		public void Dispose() {
			try {
				_reader.Close();

				// closing binary reader should close this
				_stream.Close();
			} catch {
			}

			GC.SuppressFinalize(this);
		}
	}
}
