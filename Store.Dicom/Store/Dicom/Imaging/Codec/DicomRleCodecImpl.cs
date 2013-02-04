using System;
using System.IO;
using Dicom.IO;
using Dicom.IO.Buffer;

// ReSharper disable CheckNamespace
namespace Dicom.Imaging.Codec
// ReSharper restore CheckNamespace
{
	public class DicomRleParams : DicomCodecParams
	{
		#region Private Members

		#endregion

		#region Public Members

		public DicomRleParams(bool reverseByteOrder = false)
		{
			ReverseByteOrder = reverseByteOrder;
		}

		#endregion

		#region Public Properties

		public bool ReverseByteOrder { get; set; }

		#endregion
	}

	public class DicomRleCodecImpl : DicomRleCodec
	{

		private class RLEEncoder {
			#region Private Members
			private int _count;
			private readonly uint[] _offsets;
			private readonly MemoryStream _stream;
			private readonly BinaryWriter _writer;
			private readonly byte[] _buffer;

			private int _prevByte;
			private int _repeatCount;
			private int _bufferPos;
			#endregion

			#region Public Constructors
			public RLEEncoder() {
				_count = 0;
				_offsets = new uint[15];
				_stream = new MemoryStream();
				_writer = EndianBinaryWriter.Create(_stream, Endian.Little);
				_buffer = new byte[132];
				WriteHeader();

				_prevByte = -1;
				_repeatCount = 0;
				_bufferPos = 0;
			}
			#endregion

			#region Public Members

			private long Length {
				get { return _stream.Length; }
			}

			public IByteBuffer GetBuffer() {
				Flush();
				WriteHeader();
				return new MemoryByteBuffer(_stream.ToArray());
			}

			public void NextSegment() {
				Flush();
				if ((Length & 1) == 1)
					_stream.WriteByte(0x00);
				_offsets[_count++] = (uint)_stream.Length;
			}

			public void Encode(byte b) {
				if (b == _prevByte) {
					_repeatCount++;

					if (_repeatCount > 2 && _bufferPos > 0) {
						// We're starting a run, flush out the buffer
						while (_bufferPos > 0) {
							int count = Math.Min(128, _bufferPos);
							_stream.WriteByte((byte)(count - 1));
							MoveBuffer(count);
						}
					}
					else if (_repeatCount > 128) {
						int count = Math.Min(_repeatCount, 128);
						_stream.WriteByte((byte)(257 - count));
						_stream.WriteByte((byte)_prevByte);
						_repeatCount -= count;
					}
				}
				else {
					switch (_repeatCount) {
						case 0:
							break;
						case 1: {
								_buffer[_bufferPos++] = (byte)_prevByte;
								break;
							}
						case 2: {
								_buffer[_bufferPos++] = (byte)_prevByte;
								_buffer[_bufferPos++] = (byte)_prevByte;
								break;
							}
						default: {
								while (_repeatCount > 0) {
									int count = Math.Min(_repeatCount, 128);
									_stream.WriteByte((byte)(257 - count));
									_stream.WriteByte((byte)_prevByte);
									_repeatCount -= count;
								}

								break;
							}
					}

					while (_bufferPos > 128) {
						int count = Math.Min(128, _bufferPos);
						_stream.WriteByte((byte)(count - 1));
						MoveBuffer(count);
					}

					_prevByte = b;
					_repeatCount = 1;
				}
			}

			public void MakeEvenLength() {
				// Make even length
				if (_stream.Length % 2 == 1)
					_stream.WriteByte(0);
			}

			internal void Flush() {
				if (_repeatCount < 2) {
					while (_repeatCount > 0) {
						_buffer[_bufferPos++] = (byte)_prevByte;
						_repeatCount--;
					}
				}

				while (_bufferPos > 0) {
					int count = Math.Min(128, _bufferPos);
					_stream.WriteByte((byte)(count - 1));
					MoveBuffer(count);
				}

				if (_repeatCount >= 2) {
					while (_repeatCount > 0) {
						int count = Math.Min(_repeatCount, 128);
						_stream.WriteByte((byte)(257 - count));
						_stream.WriteByte((byte)_prevByte);
						_repeatCount -= count;
					}
				}

				_prevByte = -1;
				_repeatCount = 0;
				_bufferPos = 0;
			}
			#endregion

			#region Private Members
			private void MoveBuffer(int count) {
				_stream.Write(_buffer, 0, count);
				for (int i = count, n = 0; i < _bufferPos; i++, n++) {
					_buffer[n] = _buffer[i];
				}
				_bufferPos = _bufferPos - count;
			}

			private void WriteHeader() {
				_stream.Seek(0, SeekOrigin.Begin);
				_writer.Write((uint)_count);
				for (int i = 0; i < 15; i++) {
					_writer.Write(_offsets[i]);
				}
			}
			#endregion
		}

		public override void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters) {
			var rleParams = parameters as DicomRleParams ?? new DicomRleParams();

			var pixelCount = oldPixelData.Width * oldPixelData.Height;
			var numberOfSegments = oldPixelData.BytesAllocated * oldPixelData.SamplesPerPixel;

			for (int i = 0; i < oldPixelData.NumberOfFrames; i++) {
				var encoder = new RLEEncoder();
				var frameData = oldPixelData.GetFrame(i);

				for (var s = 0; s < numberOfSegments; s++) {
					encoder.NextSegment();

					var sample = s / oldPixelData.BytesAllocated;
					var sabyte = s % oldPixelData.BytesAllocated;

					int pos;
					int offset;

					if (newPixelData.PlanarConfiguration == 0) {
						pos = sample * oldPixelData.BytesAllocated;
						offset = numberOfSegments;
					}
					else {
						pos = sample * oldPixelData.BytesAllocated * pixelCount;
						offset = oldPixelData.BytesAllocated;
					}

					if (rleParams.ReverseByteOrder)
						pos += sabyte;
					else
						pos += oldPixelData.BytesAllocated - sabyte - 1;

					for (var p = 0; p < pixelCount; p++) {
						if (pos >= frameData.Size)
							throw new DicomCodecException("");
						encoder.Encode(frameData.Data[pos]);
						pos += offset;
					}
					encoder.Flush();
				}

				encoder.MakeEvenLength();

				newPixelData.AddFrame(encoder.GetBuffer());
			}
		}

/*		public override void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters) {
			DicomRleNativeCodec.Encode(oldPixelData.ToNativePixelData(), newPixelData.ToNativePixelData());
		}*/

		public override void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
		{
			DicomRleNativeCodec.Decode(oldPixelData.ToNativePixelData(), newPixelData.ToNativePixelData());
		}
	}
}
