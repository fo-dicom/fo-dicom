// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;
using Xunit;

namespace FellowOakDicom.Tests.IO.Buffer
{
    [Collection(TestCollections.General)]
    public class EndianByteBufferTest
    {
        private readonly Endian _otherEndian;

        public class TestCase
        {
            public Endian Endian { get; }
            public int UnitSize { get; }

            public TestCase(Endian endian, int unitSize)
            {
                Endian = endian;
                UnitSize = unitSize;
            }
        }

        public static TheoryData<TestCase> TestCases
        {
            get
            {
                var data = new TheoryData<TestCase>
                {
                    new TestCase(Endian.Big, 2),
                    new TestCase(Endian.Big, 4),
                    new TestCase(Endian.Big, 8),
                    new TestCase(Endian.Little, 2),
                    new TestCase(Endian.Little, 4),
                    new TestCase(Endian.Little, 8)
                };
                return data;
            }
        }

        public EndianByteBufferTest()
        {
            _otherEndian = Endian.LocalMachine == Endian.Big ? Endian.Little : Endian.Big;
        }

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Data_CompareWithInitializer_ExactMatch(TestCase testCase)
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            byte[] expected = null;
            if (testCase.Endian == Endian.LocalMachine)
            {
                expected = bytes;
            }
            else
            {
                switch (testCase.UnitSize)
                {
                    case 2:
                        expected = new byte[] { 2, 1, 4, 3, 6, 5, 8, 7 };
                        break;
                    case 4:
                        expected = new byte[] { 4, 3, 2, 1, 8, 7, 6, 5 };
                        break;
                    case 8:
                        expected = new byte[] { 8, 7, 6, 5, 4, 3, 2, 1 };
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Unsupported test case unit size: " + testCase.UnitSize);
                }
            }

            var buffer = EndianByteBuffer.Create(new MemoryByteBuffer(bytes), testCase.Endian, testCase.UnitSize);

            // Act
            var actual = buffer.Data;

            // Assert
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 8)]
        [InlineData(4, 4)]
        [InlineData(2, 4)]
        public void GetByteRange_WithOffset_ToEnd_ShouldReturnValidArray(int offset, int count)
        {
            // Arrange
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var expected = new byte[] { 2, 1, 4, 3, 6, 5, 8, 7 };
            var buffer = EndianByteBuffer.Create(new MemoryByteBuffer(bytes), _otherEndian, 2);

            // Act
            var actual = new byte[count];
            buffer.GetByteRange(offset, count, actual);

            // Assert
            Assert.Equal(count, actual.Length);
            Assert.Equal(expected.Skip(offset).Take(count).ToArray(), actual);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(5 * (1024*1024) + 256)]
        public void CopyToStream_ShouldWorkCorrectly(int length)
        {
            // Arrange
            var bytes = new byte[length];
            var expected = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bytes[i] = (byte)i;
                expected[i] = i % 2 == 0
                    ? (byte)(i + 1)
                    : (byte)(i - 1);
            }

            var buffer = EndianByteBuffer.Create(new MemoryByteBuffer(bytes), _otherEndian, 2);

            using var ms = new MemoryStream(length);

            // Act
            buffer.CopyToStream(ms);
            var actual = ms.ToArray();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(5 * (1024*1024) + 256)]
        public async Task CopyToStreamAsync_ShouldWorkCorrectly(int length)
        {
            // Arrange
            var bytes = new byte[length];
            var expected = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bytes[i] = (byte)i;
                expected[i] = i % 2 == 0
                    ? (byte)(i + 1)
                    : (byte)(i - 1);
            }

            var buffer = EndianByteBuffer.Create(new MemoryByteBuffer(bytes), _otherEndian, 2);

            using var ms = new MemoryStream(length);

            // Act
            await buffer.CopyToStreamAsync(ms, CancellationToken.None);
            var actual = ms.ToArray();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
