#include "Dicom.Imaging.Codec.Rle.h"

#include <algorithm>
#include "ppltasks.h"

using namespace std;
using namespace Concurrency;
using namespace Platform;
using namespace Windows::Storage::Streams;

namespace Dicom {
namespace Imaging {
namespace Codec {

private ref class RLEEncoder sealed {
private:
	int _count;
	Array<unsigned int>^ _offsets;
	IRandomAccessStream^ _stream;
	IDataWriter^ _writer;
	Array<unsigned char>^ _buffer;

	int _prevByte;
	int _repeatCount;
	int _bufferPos;

internal:
	RLEEncoder() {
		_count = 0;
		_offsets = ref new Array<unsigned int>(15);
		_stream = ref new InMemoryRandomAccessStream();
		_writer = ref new DataWriter(_stream);
		_writer->ByteOrder = ByteOrder::LittleEndian;
		_buffer = ref new Array<unsigned char>(132);
		WriteHeader();

		_prevByte = -1;
		_repeatCount = 0;
		_bufferPos = 0;
	}

	property int NumberOfSegments {
		int get() { return _count; }
	}

	property int64 Length {
		int64 get() { return (int64)_stream->Size; }
	}

	Array<unsigned char>^ GetBuffer() {
		Flush();
		WriteHeader();

		Array<unsigned char>^ bytes = ref new Array<unsigned char>(_stream->Size);
		DataReader^ reader = ref new DataReader(_stream->GetInputStreamAt(0));

		create_task(_writer->StoreAsync()).then([reader] (unsigned int bytesStored) {
			return reader->LoadAsync(bytesStored);
		}).then([reader, bytes] (task<unsigned int> bytesLoaded) {
			reader->ReadBytes(bytes);
		}).wait();
		return bytes;
	}

	void NextSegment() {
		Flush();
		if ((Length & 1) == 1)
			_writer->WriteByte(0x00);
		_offsets[_count++] = (unsigned int)_stream->Size;
	}

	void Encode(unsigned char b) {
		if (b == _prevByte) {
			_repeatCount++;

			if (_repeatCount > 2 && _bufferPos > 0) {
				// We're starting a run, flush out the buffer
				while (_bufferPos > 0) {
					int count = min(128, _bufferPos);
					_writer->WriteByte((unsigned char)(count - 1));
					MoveBuffer(count);
				}
			}
			else if (_repeatCount > 128) {
				int count = min(_repeatCount, 128);
				_writer->WriteByte((unsigned char)(257 - count));
				_writer->WriteByte((unsigned char)_prevByte);
				_repeatCount -= count;
			}
		}
		else {
			switch (_repeatCount) {
				case 0:
					break;
				case 1: {
						_buffer[_bufferPos++] = (unsigned char)_prevByte;
						break;
					}
				case 2: {
						_buffer[_bufferPos++] = (unsigned char)_prevByte;
						_buffer[_bufferPos++] = (unsigned char)_prevByte;
						break;
					}
				default: {
						while (_repeatCount > 0) {
							int count = min(_repeatCount, 128);
							_writer->WriteByte((unsigned char)(257 - count));
							_writer->WriteByte((unsigned char)_prevByte);
							_repeatCount -= count;
						}

						break;
					}
			}

			while (_bufferPos > 128) {
				int count = min(128, _bufferPos);
				_writer->WriteByte((unsigned char)(count - 1));
				MoveBuffer(count);
			}

			_prevByte = b;
			_repeatCount = 1;
		}
	}

	void MakeEvenLength() {
		// Make even length
		if (_stream->Size % 2 == 1)
			_writer->WriteByte(0);
	}

	void Flush() {
		if (_repeatCount < 2) {
			while (_repeatCount > 0) {
				_buffer[_bufferPos++] = (unsigned char)_prevByte;
				_repeatCount--;
			}
		}

		while (_bufferPos > 0) {
			int count = min(128, _bufferPos);
			_writer->WriteByte((unsigned char)(count - 1));
			MoveBuffer(count);
		}

		if (_repeatCount >= 2) {
			while (_repeatCount > 0) {
				int count = min(_repeatCount, 128);
				_writer->WriteByte((unsigned char)(257 - count));
				_writer->WriteByte((unsigned char)_prevByte);
				_repeatCount -= count;
			}
		}

		_prevByte = -1;
		_repeatCount = 0;
		_bufferPos = 0;
	}

private:
	void MoveBuffer(int count) {
		for (int i = 0; i < count; ++i)
			_writer->WriteByte(_buffer[i]);
		for (int i = count, n = 0; i < _bufferPos; i++, n++) {
			_buffer[n] = _buffer[i];
		}
		_bufferPos = _bufferPos - count;
	}

	void WriteHeader() {
		_stream->Seek(0);
		_writer->WriteUInt32((unsigned int)_count);
		for (int i = 0; i < 15; i++) {
			_writer->WriteUInt32(_offsets[i]);
		}
	}
};

void DicomRleNativeCodec::Encode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData) {
	int pixelCount = oldPixelData->Width * oldPixelData->Height;
	int numberOfSegments = oldPixelData->BytesAllocated * oldPixelData->SamplesPerPixel;

	for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
		Array<unsigned char>^ frameData = oldPixelData->GetFrame(frame);

		RLEEncoder^ encoder = ref new RLEEncoder();

		for (int s = 0; s < numberOfSegments; s++) {
			encoder->NextSegment();

			int sample = s / oldPixelData->BytesAllocated;
			int sabyte = s % oldPixelData->BytesAllocated;

			int pos;
			int offset;

			if (newPixelData->PlanarConfiguration == PlanarConfiguration::Interleaved) {
				pos = sample * oldPixelData->BytesAllocated;
				offset = numberOfSegments;
			}
			else {
				pos = sample * oldPixelData->BytesAllocated * pixelCount;
				offset = oldPixelData->BytesAllocated;
			}

			pos += oldPixelData->BytesAllocated - sabyte - 1;

			for (int p = 0; p < pixelCount; p++) {
				if ((unsigned int)pos >= frameData->Length)
					throw ref new FailureException("Read position is past end of frame buffer");
				encoder->Encode(frameData[pos]);
				pos += offset;
			}
			encoder->Flush();
		}

		encoder->MakeEvenLength();

		Array<unsigned char>^ data = encoder->GetBuffer();
		newPixelData->AddFrame(data);
	}
}

private ref class RLEDecoder sealed {
private:
			int _count;
			Array<int>^ _offsets;
			Array<unsigned char>^ _data;

public:
	RLEDecoder(const Array<unsigned char>^ data) {
		IRandomAccessStream^ stream = ref new InMemoryRandomAccessStream();
		DataWriter^ writer = ref new DataWriter(stream);
		DataReader^ reader = ref new DataReader(stream->GetInputStreamAt(0));
		reader->ByteOrder = ByteOrder::LittleEndian;
		writer->WriteBytes(data);
		create_task(writer->StoreAsync()).then([stream, reader] (unsigned int bytesStored) {
			return reader->LoadAsync(stream->Size);
		}).wait();
		_count = (int)reader->ReadUInt32();
		_offsets = ref new Array<int>(15);
		for (int i = 0; i < 15; i++) {
			_offsets[i] = reader->ReadInt32();
		}
		_data = ref new Array<unsigned char>(data);
	}

	property int NumberOfSegments {
		int get() { return _count; }
	}

	void DecodeSegment(int segment, Array<unsigned char>^* buffer, int start, int sampleOffset) {
		if (segment < 0 || segment >= _count)
			throw ref new OutOfBoundsException("Segment number out of range");

		int offset = GetSegmentOffset(segment);
		int length = GetSegmentLength(segment);

		Decode(*buffer, start, sampleOffset, _data, offset, length);
	}

private:
	void Decode(Array<unsigned char>^ buffer, int start, int sampleOffset, Array<unsigned char>^ rleData, int offset, int count) {
		int pos = start;
		int end = offset + count;
		int bufferLength = buffer->Length;

		for (int i = offset; i < end && pos < bufferLength; ) {
			char control = (char)rleData[i++];

			if (control >= 0) {
				int length = control + 1;

				if ((end - i) < length)
					throw ref new FailureException("RLE literal run exceeds input buffer length.");
				if ((pos + ((length - 1) * sampleOffset)) >= bufferLength)
					throw ref new FailureException("RLE literal run exceeds output buffer length.");

				if (sampleOffset == 1) {
					for (int j = 0; j < length; ++j, ++i, ++pos)
						buffer[pos] = rleData[i];
				}
				else {
					while (length-- > 0) {
						buffer[pos] = rleData[i++];
						pos += sampleOffset;
					}
				}
			}
			else if (control >= -127) {
				int length = -control;

				if ((pos + ((length - 1) * sampleOffset)) >= bufferLength)
					throw ref new FailureException("RLE repeat run exceeds output buffer length.");

				unsigned char b = rleData[i++];

				if (sampleOffset == 1) {
					while (length-- >= 0)
						buffer[pos++] = b;
				}
				else {
					while (length-- >= 0) {
						buffer[pos] = b;
						pos += sampleOffset;
					}
				}
			}

			if ((i + 2) >= end)
				break;
		}
	}

	int GetSegmentOffset(int segment) {
		return _offsets[segment];
	}

	int GetSegmentLength(int segment) {
		int offset = GetSegmentOffset(segment);
		if (segment < (_count - 1)) {
			int next = GetSegmentOffset(segment + 1);
			return next - offset;
		}
		else {
			return _data->Length - offset;
		}
	}
};

void DicomRleNativeCodec::Decode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData) {
	for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
		Array<unsigned char>^ rleData = oldPixelData->GetFrame(frame);
		Array<unsigned char>^ frameData = ref new Array<unsigned char>(newPixelData->UncompressedFrameSize);

		int pixelCount = oldPixelData->Width * oldPixelData->Height;
		int numberOfSegments = oldPixelData->BytesAllocated * oldPixelData->SamplesPerPixel;

		RLEDecoder^ decoder = ref new RLEDecoder(rleData);

		if (decoder->NumberOfSegments != numberOfSegments)
			throw ref new FailureException("Unexpected number of RLE segments!");

		for (int s = 0; s < numberOfSegments; s++) {
			int sample = s / newPixelData->BytesAllocated;
			int sabyte = s % newPixelData->BytesAllocated;

			int pos, offset;

			if (newPixelData->PlanarConfiguration == PlanarConfiguration::Interleaved) {
				pos = sample * newPixelData->BytesAllocated;
				offset = newPixelData->SamplesPerPixel * newPixelData->BytesAllocated;
			}
			else {
				pos = sample * newPixelData->BytesAllocated * pixelCount;
				offset = newPixelData->BytesAllocated;
			}

			pos += newPixelData->BytesAllocated - sabyte - 1;

			decoder->DecodeSegment(s, &frameData, pos, offset);
		}

		//buffer = EvenLengthBuffer::Create(buffer);
		newPixelData->AddFrame(frameData);
	}
}

} // Codec
} // Imaging
} // Dicom