// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.IO.Buffer;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{

    [Collection(TestCollections.Imaging)]
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

        /// <summary>
        /// issue #716
        /// </summary>
        [Fact]
        public void Create_TransferSyntaxExplicitLEBitsAllocatedGreaterThan16_ReturnsOtherWordPixelDataObject()
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian);
            dataset.Add(DicomTag.BitsAllocated, (ushort)32);
            var pixelData = DicomPixelData.Create(dataset, true);

            Assert.Equal("OtherWordPixelData", pixelData.GetType().Name);
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

        [Fact]
        public void CheckEvenBytesInOtherBytePixelData()
        {
            var dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian)
            {
                { DicomTag.BitsAllocated, (ushort)1 }
            };
            var pixelData = DicomPixelData.Create(dataset, true);

            Assert.Equal("OtherBytePixelData", pixelData.GetType().Name);

            pixelData.AddFrame(new TempFileBuffer(new byte[1]));
            pixelData.AddFrame(new TempFileBuffer(new byte[2]));
            pixelData.AddFrame(new TempFileBuffer(new byte[1]));

            Assert.Equal(3, pixelData.NumberOfFrames);
            var pixels = pixelData.Dataset.GetDicomItem<DicomElement>(DicomTag.PixelData);
            var size = pixels.Buffer.Size;
            Assert.True(size % 2 == 0);

            dataset = new DicomDataset(DicomTransferSyntax.ExplicitVRLittleEndian)
            {
                { DicomTag.BitsAllocated, (ushort)1 }
            };
            pixelData = DicomPixelData.Create(dataset, true);

            Assert.Equal("OtherBytePixelData", pixelData.GetType().Name);

            pixelData.AddFrame(new TempFileBuffer(new byte[1]));
            pixelData.AddFrame(new TempFileBuffer(new byte[2]));

            Assert.Equal(2, pixelData.NumberOfFrames);

            pixels = pixelData.Dataset.GetDicomItem<DicomElement>(DicomTag.PixelData);
            size = pixels.Buffer.Size;
            Assert.True(size % 2 == 0);
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

        [Fact]
        public void RotateAndFlipImage()
        {
            var myDicomFile = DicomFile.Open(TestData.Resolve("CR-MONO1-10-chest"));
            var myDicomImage = new DicomImage(myDicomFile.Dataset);
            IImage myImg = myDicomImage.RenderImage(0);
            myImg.Render(3, true, true, 0);
        }


    }
}
