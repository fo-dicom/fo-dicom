#include "Dicom.Imaging.Codec.Rle.h"

using namespace System;
using namespace System::IO;

using namespace Dicom;
using namespace Dicom::IO;
using namespace Dicom::IO::Buffer;

namespace Dicom {
namespace Imaging {
namespace Codec {

private ref class RLEEncoder {
private:
	int _count;
	array<unsigned int>^ _offsets;
	MemoryStream^ _stream;
	BinaryWriter^ _writer;
	array<unsigned char>^ _buffer;

	int _prevByte;
	int _repeatCount;
	int _bufferPos;

public:
	RLEEncoder() {
		_count = 0;
		_offsets = gcnew array<unsigned int>(15);
		_stream = gcnew MemoryStream();
		_writer = EndianBinaryWriter::Create(_stream, Endian::Little);
		_buffer = gcnew array<unsigned char>(132);
		WriteHeader();

		_prevByte = -1;
		_repeatCount = 0;
		_bufferPos = 0;
	}

	property int NumberOfSegments {
		int get() { return _count; }
	}

	property long Length {
		long get() { return (long)_stream->Length; }
	}

	array<unsigned char>^ GetBuffer() {
		Flush();
		WriteHeader();
		return _stream->ToArray();
	}

	void NextSegment() {
		Flush();
		if ((Length & 1) == 1)
			_stream->WriteByte(0x00);
		_offsets[_count++] = (unsigned int)_stream->Length;
	}

	void Encode(unsigned char b) {
		if (b == _prevByte) {
			_repeatCount++;

			if (_repeatCount > 2 && _bufferPos > 0) {
				// We're starting a run, flush out the buffer
				while (_bufferPos > 0) {
					int count = Math::Min(128, _bufferPos);
					_stream->WriteByte((unsigned char)(count - 1));
					MoveBuffer(count);
				}
			}
			else if (_repeatCount > 128) {
				int count = Math::Min(_repeatCount, 128);
				_stream->WriteByte((unsigned char)(257 - count));
				_stream->WriteByte((unsigned char)_prevByte);
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
							int count = Math::Min(_repeatCount, 128);
							_stream->WriteByte((unsigned char)(257 - count));
							_stream->WriteByte((unsigned char)_prevByte);
							_repeatCount -= count;
						}

						break;
					}
			}

			while (_bufferPos > 128) {
				int count = Math::Min(128, _bufferPos);
				_stream->WriteByte((unsigned char)(count - 1));
				MoveBuffer(count);
			}

			_prevByte = b;
			_repeatCount = 1;
		}
	}

	void MakeEvenLength() {
		// Make even length
		if (_stream->Length % 2 == 1)
			_stream->WriteByte(0);
	}

	void Flush() {
		if (_repeatCount < 2) {
			while (_repeatCount > 0) {
				_buffer[_bufferPos++] = (unsigned char)_prevByte;
				_repeatCount--;
			}
		}

		while (_bufferPos > 0) {
			int count = Math::Min(128, _bufferPos);
			_stream->WriteByte((unsigned char)(count - 1));
			MoveBuffer(count);
		}

		if (_repeatCount >= 2) {
			while (_repeatCount > 0) {
				int count = Math::Min(_repeatCount, 128);
				_stream->WriteByte((unsigned char)(257 - count));
				_stream->WriteByte((unsigned char)_prevByte);
				_repeatCount -= count;
			}
		}

		_prevByte = -1;
		_repeatCount = 0;
		_bufferPos = 0;
	}

private:
	void MoveBuffer(int count) {
		_stream->Write(_buffer, 0, count);
		for (int i = count, n = 0; i < _bufferPos; i++, n++) {
			_buffer[n] = _buffer[i];
		}
		_bufferPos = _bufferPos - count;
	}

	void WriteHeader() {
		_stream->Seek(0, SeekOrigin::Begin);
		_writer->Write((unsigned int)_count);
		for (int i = 0; i < 15; i++) {
			_writer->Write(_offsets[i]);
		}
	}
};

void DicomRleNativeCodec::Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) {
	int pixelCount = oldPixelData->Width * oldPixelData->Height;
	int numberOfSegments = oldPixelData->BytesAllocated * oldPixelData->SamplesPerPixel;

	for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
		IByteBuffer^ frameData = oldPixelData->GetFrame(frame);
		PinnedByteArray^ frameArray = gcnew PinnedByteArray(frameData->Data);

		RLEEncoder^ encoder = gcnew RLEEncoder();

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
				if ((unsigned int)pos >= frameData->Size)
					throw gcnew DicomCodecException("Read position is past end of frame buffer");
				encoder->Encode(frameArray[pos]);
				pos += offset;
			}
			encoder->Flush();
		}

		encoder->MakeEvenLength();

		array<unsigned char>^ data = encoder->GetBuffer();

		IByteBuffer^ buffer;
		if (data->Length >= (1 * 1024 * 1024) || oldPixelData->NumberOfFrames > 1)
			buffer = gcnew TempFileBuffer(data);
		else
			buffer = gcnew MemoryByteBuffer(data);
		buffer = EvenLengthBuffer::Create(buffer);
		newPixelData->AddFrame(buffer);
	}
}

private ref class RLEDecoder {
private:
			int _count;
			array<int>^ _offsets;
			array<unsigned char>^ _data;

public:
	RLEDecoder(array<unsigned char>^ data) {
		MemoryStream^ stream = gcnew MemoryStream(data);
		BinaryReader^ reader = EndianBinaryReader::Create(stream, Endian::Little);
		_count = (int)reader->ReadUInt32();
		_offsets = gcnew array<int>(15);
		for (int i = 0; i < 15; i++) {
			_offsets[i] = reader->ReadInt32();
		}
		_data = data;
	}

	property int NumberOfSegments {
		int get() { return _count; }
	}

	void DecodeSegment(int segment, array<unsigned char>^ buffer, int start, int sampleOffset) {
		if (segment < 0 || segment >= _count)
			throw gcnew IndexOutOfRangeException("Segment number out of range");

		int offset = GetSegmentOffset(segment);
		int length = GetSegmentLength(segment);

		Decode(buffer, start, sampleOffset, _data, offset, length);
	}

private:
	void Decode(array<unsigned char>^ buffer, int start, int sampleOffset, array<unsigned char>^ rleData, int offset, int count) {
		int pos = start;
		int end = offset + count;
		int bufferLength = buffer->Length;

		for (int i = offset; i < end && pos < bufferLength; ) {
			char control = (char)rleData[i++];

			if (control >= 0) {
				int length = control + 1;

				if ((end - i) < length)
					throw gcnew DicomCodecException("RLE literal run exceeds input buffer length.");
				if ((pos + ((length - 1) * sampleOffset)) >= bufferLength)
					throw gcnew DicomCodecException("RLE literal run exceeds output buffer length.");

				if (sampleOffset == 1) {
					// ANTS says this is faster than Array.Copy!
					Array::Copy(rleData, i, buffer, pos, length);
					pos += length;
					i += length;
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

				//if (i >= end) // never happens due to check below
				//    throw new DicomCodecException("RLE repeat run exceeds input buffer length.");
				if ((pos + ((length - 1) * sampleOffset)) >= bufferLength)
					throw gcnew DicomCodecException("RLE repeat run exceeds output buffer length.");

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

void DicomRleNativeCodec::Decode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) {
	for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
		IByteBuffer^ rleData = oldPixelData->GetFrame(frame);
		PinnedByteArray^ rleArray = gcnew PinnedByteArray(rleData->Data);

		array<unsigned char>^ frameData = gcnew array<unsigned char>(newPixelData->UncompressedFrameSize);
		PinnedByteArray^ frameArray = gcnew PinnedByteArray(frameData);

		int pixelCount = oldPixelData->Width * oldPixelData->Height;
		int numberOfSegments = oldPixelData->BytesAllocated * oldPixelData->SamplesPerPixel;

		RLEDecoder^ decoder = gcnew RLEDecoder(rleArray->Data);

		if (decoder->NumberOfSegments != numberOfSegments)
			throw gcnew DicomCodecException("Unexpected number of RLE segments!");

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

			decoder->DecodeSegment(s, frameData, pos, offset);
		}

		IByteBuffer^ buffer;
		if (frameData->Length >= (1 * 1024 * 1024) || oldPixelData->NumberOfFrames > 1)
			buffer = gcnew TempFileBuffer(frameData);
		else
			buffer = gcnew MemoryByteBuffer(frameData);
		buffer = EvenLengthBuffer::Create(buffer);
		newPixelData->AddFrame(buffer);
	}
}

} // Codec
} // Imaging
} // Dicom