// // Copyright (c) 2012-2017 fo-dicom contributors.
// // Licensed under the Microsoft Public License (MS-PL).
// 

using Xunit;

namespace Dicom.Imaging
{
    public class DicomPixelDataTest
    {
        [Fact]
        public void Create_TransferSyntaxImplicitLE_ReturnsOtherWordPixelDataObject()
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ImplicitVRLittleEndian);
            dataset.Add(DicomTag.BitsAllocated, (ushort)8);
            var pixelData = DicomPixelData.Create(dataset, true);

            Assert.Equal("OtherWordPixelData", pixelData.GetType().Name);
        }

        [Fact]
        public void Create_TransferSyntaxExplicitLEBitsAllocatedGreaterThan16_Throws()
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian);
            dataset.Add(DicomTag.BitsAllocated, (ushort)17);
            var exception = Record.Exception(() => DicomPixelData.Create(dataset, true));

            Assert.NotNull(exception);
        }

        [Theory]
        [InlineData(9)]
        [InlineData(11)]
        [InlineData(14)]
        [InlineData(16)]
        public void Create_TransferSyntaxExplicitLEBitsAllocatedGreaterThan8_ReturnsOtherWordPixelDataObject(ushort bitsAllocated)
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian);
            dataset.Add(DicomTag.BitsAllocated, bitsAllocated);
            var pixelData = DicomPixelData.Create(dataset, true);

            Assert.Equal("OtherWordPixelData", pixelData.GetType().Name);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(4)]
        [InlineData(2)]
        [InlineData(1)]
        public void Create_TransferSyntaxExplicitLEBitsAllocatedLessThanOrEqualTo8_ReturnsOtherBytePixelDataObject(ushort bitsAllocated)
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian);
            dataset.Add(DicomTag.BitsAllocated, bitsAllocated);
            var pixelData = DicomPixelData.Create(dataset, true);

            Assert.Equal("OtherBytePixelData", pixelData.GetType().Name);
        }

        [Theory]
        [InlineData(8, 8)]
        [InlineData(8, 7)]
        [InlineData(16, 16)]
        [InlineData(16, 12)]
        public void BitsStored_Setter_SmallerThanOrEqualToBitsAllocatedIsAllowed(ushort bitsAllocated, ushort bitsStored)
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian);
            dataset.Add(DicomTag.BitsAllocated, bitsAllocated);
            var pixelData = DicomPixelData.Create(dataset, true);

            var exception = Record.Exception(() => pixelData.BitsStored = bitsStored);
            Assert.Null(exception);
            Assert.Equal(bitsStored, pixelData.BitsStored);
        }

        [Theory]
        [InlineData(8, 9)]
        [InlineData(16, 17)]
        public void BitsStored_Setter_GreaterThanBitsAllocatedIsNotAllowed(ushort bitsAllocated, ushort bitsStored)
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian);
            dataset.Add(DicomTag.BitsAllocated, bitsAllocated);
            var pixelData = DicomPixelData.Create(dataset, true);

            var exception = Record.Exception(() => pixelData.BitsStored = bitsStored);
            Assert.NotNull(exception);
        }

        [Theory]
        [InlineData(8, 7)]
        [InlineData(16, 12)]
        public void HighBit_Setter_SmallerThanBitsAllocatedIsAllowed(ushort bitsAllocated, ushort highBit)
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian);
            dataset.Add(DicomTag.BitsAllocated, bitsAllocated);
            var pixelData = DicomPixelData.Create(dataset, true);

            var exception = Record.Exception(() => pixelData.HighBit = highBit);
            Assert.Null(exception);
            Assert.Equal(highBit, pixelData.HighBit);
        }

        [Theory]
        [InlineData(8, 8)]
        [InlineData(8, 9)]
        [InlineData(16, 16)]
        [InlineData(16, 17)]
        public void HighBit_Setter_GreaterThanOrEqualToBitsAllocatedIsNotAllowed(ushort bitsAllocated, ushort highBit)
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian);
            dataset.Add(DicomTag.BitsAllocated, bitsAllocated);
            var pixelData = DicomPixelData.Create(dataset, true);

            var exception = Record.Exception(() => pixelData.HighBit = highBit);
            Assert.NotNull(exception);
        }
    }
}
