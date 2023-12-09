// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using System;
using FellowOakDicom.Memory;
using Xunit;

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network)]
    public class PDVTest
    {
        private readonly IMemoryProvider _memoryProvider;

        #region Unit tests

        public PDVTest()
        {
            _memoryProvider = new ArrayPoolMemoryProvider();
        }

        [Fact]
        public void Constructor_EvenLengthValue_Unmodified()
        {
            var value = _memoryProvider.Provide(6);
            var contents = new byte[] { 1, 2, 3, 4, 5, 6 };
            contents.CopyTo(value.Span);

            using var pdv = new PDV(1, value, 6, true, true);
            var actual = pdv.Value.Span.ToArray();

            Assert.Equal(contents, actual);
        }

        [Fact]
        public void Constructor_OddLengthValue_ThrowsArgumentException()
        {
            var value = _memoryProvider.Provide(5);
            var contents = new byte[] { 1, 2, 3, 4, 5 };
            contents.CopyTo(value.Span);

            Assert.Throws<ArgumentException>(() => new PDV(1, value, 5, true, true));
        }

        #endregion
    }
}
