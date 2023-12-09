// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{

    [Collection(TestCollections.General)]
    public class DicomOverlayDataTest
    {

        [Fact]
        public void Constructor_GroupSpecified_GroupTransferredToDescription()
        {
            const string expected = "Description 6002";
            const ushort group = 0x6002;

            var dataset = new DicomDataset();
            _ = new DicomOverlayData(dataset, group) { Description = expected };

            var actual = dataset.GetString(new DicomTag(group, 0x0022));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Constructor_GroupSpecified_GroupTransferredToSubtype()
        {
            const string expected = "Subtype 6005";
            const ushort group = 0x6005;

            var dataset = new DicomDataset();
            _ = new DicomOverlayData(dataset, group) { Subtype = expected };

            var actual = dataset.GetString(new DicomTag(group, 0x0045));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Constructor_GroupSpecified_GroupTransferredToLabel()
        {
            const string expected = "Label 6003";
            const ushort group = 0x6003;

            var dataset = new DicomDataset();
            _ = new DicomOverlayData(dataset, group) { Label = expected };

            var actual = dataset.GetString(new DicomTag(group, 0x1500));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OverlayTypeSetter_SetToGraphics_ReturnsG()
        {
            const string expected = "G";
            const ushort group = 0x6011;

            var dataset = new DicomDataset();
            _ = new DicomOverlayData(dataset, group) { Type = DicomOverlayType.Graphics };

            var actual = dataset.GetString(new DicomTag(group, 0x0040));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OverlayTypeSetter_SetToROI_ReturnsR()
        {
            const string expected = "R";
            const ushort group = 0x6011;

            var dataset = new DicomDataset();
            _ = new DicomOverlayData(dataset, group) { Type = DicomOverlayType.ROI };

            var actual = dataset.GetString(new DicomTag(group, 0x0040));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OverlayTypeGetter_TypeNotSet_ShouldThrow()
        {
            const ushort group = 0x6008;

            var dataset = new DicomDataset();
            var od = new DicomOverlayData(dataset, group);

            var exception = Record.Exception(() => od.Type);
            Assert.NotNull(exception);
        }

        [Fact]
        public void OverlayTypeGetter_TypeSetToG_ReturnsGraphics()
        {
            const DicomOverlayType expected = DicomOverlayType.Graphics;
            const ushort group = 0x6012;

            var dataset = new DicomDataset();
            var od = new DicomOverlayData(dataset, group);
            dataset.AddOrUpdate(new DicomTag(group, 0x0040), "G");

            var actual = od.Type;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OverlayTypeGetter_TypeSetToR_ReturnsROI()
        {
            const DicomOverlayType expected = DicomOverlayType.ROI;
            const ushort group = 0x6012;

            var dataset = new DicomDataset();
            var od = new DicomOverlayData(dataset, group);
            dataset.AddOrUpdate(new DicomTag(group, 0x0040), "R");

            var actual = od.Type;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OverlayTypeGetter_TypeSetToOtherThanRAndG_ShouldThrow()
        {
            const ushort group = 0x6001;

            var dataset = new DicomDataset();
            var od = new DicomOverlayData(dataset, group);
            dataset.AddOrUpdate(new DicomTag(group, 0x0040), "O");

            var exception = Record.Exception(() => od.Type);
            Assert.NotNull(exception);
        }

        [Fact]
        public void OriginXGetter_ReturnsValueAtIndex1()
        {
            const int expected = 42;
            const ushort group = 0x6012;

            var dataset = new DicomDataset();
            var od = new DicomOverlayData(dataset, group);
            dataset.AddOrUpdate(new DicomTag(group, 0x0050), "0", "42");

            var actual = od.OriginX;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OriginYGetter_ReturnsValueAtIndex0()
        {
            const int expected = 42;
            const ushort group = 0x6012;

            var dataset = new DicomDataset();
            var od = new DicomOverlayData(dataset, group);
            dataset.AddOrUpdate(new DicomTag(group, 0x0050), "42", "0");

            var actual = od.OriginY;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OriginXSetter_ReturnsValueAtIndex1()
        {
            const int expected = 96;
            const ushort group = 0x6012;

            var dataset = new DicomDataset();
            _ = new DicomOverlayData(dataset, group)
            {
                OriginX = expected
            };

            int originX = dataset.GetValueOrDefault<short>(new DicomTag(group, 0x0050), 1, 1);
            Assert.Equal(expected, originX);
        }

        [Fact]
        public void OriginYSetter_ReturnsValueAtIndex0()
        {
            const int expected = 96;
            const ushort group = 0x6012;

            var dataset = new DicomDataset();
            _ = new DicomOverlayData(dataset, group)
            {
                OriginY = expected
            };

            int originY = dataset.GetValueOrDefault<short>(new DicomTag(group, 0x0050), 0, 1);
            Assert.Equal(expected, originY);
        }

    }
}
