// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Mathematics;
using System.Linq;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{

    [Collection(TestCollections.General)]
    public class FrameGeometryTest
    {

        [Fact]
        public void FrameGeometry_CalculateLocalizer_OrthogonalIntersecting()
        {
            var frameofreferenceuid = DicomUIDGenerator.GenerateDerivedFromUUID();
            var source = new DicomDataset();
            source.AddOrUpdate(DicomTag.ImagePositionPatient, 0.0m, 0.0m, 0.0m);
            source.AddOrUpdate(DicomTag.ImageOrientationPatient, 1.0m, 0.0m, 0.0m, 0.0m, 1.0m, 0.0m);
            source.AddOrUpdate(DicomTag.PixelSpacing, 0.5m, 0.5m);
            source.AddOrUpdate(DicomTag.Rows, (ushort)500);
            source.AddOrUpdate(DicomTag.Columns, (ushort)500);
            source.AddOrUpdate(DicomTag.FrameOfReferenceUID, frameofreferenceuid);
            var destination = new DicomDataset();
            destination.AddOrUpdate(DicomTag.ImagePositionPatient, 50.0m, 100.0m, -50.0m);
            destination.AddOrUpdate(DicomTag.ImageOrientationPatient, 1.0m, 0.0m, 0.0m, 0.0m, 0.0m, 1.0m);
            destination.AddOrUpdate(DicomTag.PixelSpacing, 0.25m, 0.25m);
            destination.AddOrUpdate(DicomTag.Rows, (ushort)600);
            destination.AddOrUpdate(DicomTag.Columns, (ushort)600);
            destination.AddOrUpdate(DicomTag.FrameOfReferenceUID, frameofreferenceuid);

            // CalculateIntersectionLocalizer
            var sourcegeometry = new FrameGeometry(source);
            var destinationgeometry = new FrameGeometry(destination);
            Assert.True(ImageLocalizer.CanDrawLocalizer(sourcegeometry, destinationgeometry));
            Assert.False(ImageLocalizer.CanDrawLocalizer(sourcegeometry, sourcegeometry));

            var ok = ImageLocalizer.CalcualteIntersectionLocalizer(sourcegeometry, destinationgeometry, out var startPoint, out var endPoint);
            Assert.True(ok);
            Assert.Equal(new Point2(800, 200), startPoint);
            Assert.Equal(new Point2(-200, 200), endPoint);

            ok = ImageLocalizer.CalcualteIntersectionLocalizer(destinationgeometry, sourcegeometry, out startPoint, out endPoint);
            Assert.True(ok);
            Assert.Equal(new Point2(400, 200), startPoint);
            Assert.Equal(new Point2(100, 200), endPoint);

            // calculateProjectionLocalizer
            ImageLocalizer.CalcualteProjectionLocalizer(source, destination, out var points);
            Assert.Equal(4, points.Count);
            Assert.Equal(new Point2(-200, 200), points[0]);
            Assert.Equal(new Point2(796, 200), points[1]);
            Assert.Equal(new Point2(796, 200), points[2]);
            Assert.Equal(new Point2(-200, 200), points[3]);

            ImageLocalizer.CalcualteProjectionLocalizer(destination, source, out points);
            Assert.Equal(4, points.Count);
            Assert.Equal(new Point2(100, 200), points[0]);
            Assert.Equal(new Point2(398, 200), points[1]);
            Assert.Equal(new Point2(398, 200), points[2]);
            Assert.Equal(new Point2(100, 200), points[3]);
        }


        [Fact]
        public void FrameGeometry_CalculateLocalizer_OrthogonalNotIntersecting()
        {
            var frameofreferenceuid = DicomUIDGenerator.GenerateDerivedFromUUID();
            var source = new DicomDataset();
            source.AddOrUpdate(DicomTag.ImagePositionPatient, 0.0m, 0.0m, 0.0m);
            source.AddOrUpdate(DicomTag.ImageOrientationPatient, 1.0m, 0.0m, 0.0m, 0.0m, 1.0m, 0.0m);
            source.AddOrUpdate(DicomTag.PixelSpacing, 0.5m, 0.5m);
            source.AddOrUpdate(DicomTag.Rows, (ushort)500);
            source.AddOrUpdate(DicomTag.Columns, (ushort)500);
            source.AddOrUpdate(DicomTag.FrameOfReferenceUID, frameofreferenceuid);
            var destination = new DicomDataset();
            destination.AddOrUpdate(DicomTag.ImagePositionPatient, 50.0m, 100.0m, 50.0m);
            destination.AddOrUpdate(DicomTag.ImageOrientationPatient, 1.0m, 0.0m, 0.0m, 0.0m, 0.0m, 1.0m);
            destination.AddOrUpdate(DicomTag.PixelSpacing, 0.25m, 0.25m);
            destination.AddOrUpdate(DicomTag.Rows, (ushort)600);
            destination.AddOrUpdate(DicomTag.Columns, (ushort)600);
            destination.AddOrUpdate(DicomTag.FrameOfReferenceUID, frameofreferenceuid);

            // CalculateIntersectionLocalizer
            var sourcegeometry = new FrameGeometry(source);
            var destinationgeometry = new FrameGeometry(destination);
            Assert.True(ImageLocalizer.CanDrawLocalizer(sourcegeometry, destinationgeometry));
            Assert.False(ImageLocalizer.CanDrawLocalizer(sourcegeometry, sourcegeometry));

            var ok = ImageLocalizer.CalcualteIntersectionLocalizer(sourcegeometry, destinationgeometry, out var startPoint, out var endPoint);
            Assert.True(ok);
            Assert.Equal(new Point2(800, -200), startPoint);
            Assert.Equal(new Point2(-200, -200), endPoint);

            ok = ImageLocalizer.CalcualteIntersectionLocalizer(destinationgeometry, sourcegeometry, out startPoint, out endPoint);
            Assert.False(ok);

            // calculateProjectionLocalizer
            ImageLocalizer.CalcualteProjectionLocalizer(source, destination, out var points);
            Assert.Equal(4, points.Count);
            Assert.Equal(new Point2(-200, -200), points[0]);
            Assert.Equal(new Point2(796, -200), points[1]);
            Assert.Equal(new Point2(796, -200), points[2]);
            Assert.Equal(new Point2(-200, -200), points[3]);

            ImageLocalizer.CalcualteProjectionLocalizer(destination, source, out points);
            Assert.Equal(4, points.Count);
            Assert.Equal(new Point2(100, 200), points[0]);
            Assert.Equal(new Point2(398, 200), points[1]);
            Assert.Equal(new Point2(398, 200), points[2]);
            Assert.Equal(new Point2(100, 200), points[3]);
        }


        [Fact]
        public void FrameGeometry_CalculateLocalizer_ObliqueIntersecting()
        {
            var frameofreferenceuid = DicomUIDGenerator.GenerateDerivedFromUUID();
            var source = new DicomDataset();
            source.AddOrUpdate(DicomTag.ImagePositionPatient, 0.0m, 0.0m, 0.0m);
            source.AddOrUpdate(DicomTag.ImageOrientationPatient, 1.0m, 0.0m, 0.0m, 0.0m, 1.0m, 0.0m);
            source.AddOrUpdate(DicomTag.PixelSpacing, 0.5m, 0.5m);
            source.AddOrUpdate(DicomTag.Rows, (ushort)500);
            source.AddOrUpdate(DicomTag.Columns, (ushort)500);
            source.AddOrUpdate(DicomTag.FrameOfReferenceUID, frameofreferenceuid);
            var destination = new DicomDataset();
            destination.AddOrUpdate(DicomTag.ImagePositionPatient, 50.0m, 100.0m, -50.0m);
            destination.AddOrUpdate(DicomTag.ImageOrientationPatient, 1.0m, 0.0m, 0.0m, 0.0m, 0.70710678m, 0.70710678m);
            destination.AddOrUpdate(DicomTag.PixelSpacing, 0.25m, 0.25m);
            destination.AddOrUpdate(DicomTag.Rows, (ushort)600);
            destination.AddOrUpdate(DicomTag.Columns, (ushort)600);
            destination.AddOrUpdate(DicomTag.FrameOfReferenceUID, frameofreferenceuid);

            // CalculateIntersectionLocalizer
            var sourcegeometry = new FrameGeometry(source);
            var destinationgeometry = new FrameGeometry(destination);
            Assert.True(ImageLocalizer.CanDrawLocalizer(sourcegeometry, destinationgeometry));
            Assert.False(ImageLocalizer.CanDrawLocalizer(sourcegeometry, sourcegeometry));

            var ok = ImageLocalizer.CalcualteIntersectionLocalizer(sourcegeometry, destinationgeometry, out var startPoint, out var endPoint);
            Assert.True(ok);
            Assert.Equal(new Point2(800, 283), startPoint);
            Assert.Equal(new Point2(-200, 283), endPoint);

            ok = ImageLocalizer.CalcualteIntersectionLocalizer(destinationgeometry, sourcegeometry, out startPoint, out endPoint);
            Assert.True(ok);
            Assert.Equal(new Point2(400, 300), startPoint);
            Assert.Equal(new Point2(100, 300), endPoint);

            // calculateProjectionLocalizer
            ImageLocalizer.CalcualteProjectionLocalizer(source, destination, out var points);
            Assert.Equal(4, points.Count);
            Assert.Equal(new Point2(-200, -141), points[0]);
            Assert.Equal(new Point2(796, -141), points[1]);
            Assert.Equal(new Point2(796, 563), points[2]);
            Assert.Equal(new Point2(-200, 563), points[3]);

            ImageLocalizer.CalcualteProjectionLocalizer(destination, source, out points);
            Assert.Equal(4, points.Count);
            Assert.Equal(new Point2(100, 200), points[0]);
            Assert.Equal(new Point2(398, 200), points[1]);
            Assert.Equal(new Point2(398, 411), points[2]);
            Assert.Equal(new Point2(100, 411), points[3]);
        }


        [Fact]
        public void FrameGeometry_CalculateLocalizer_ObliqueNotIntersecting()
        {
            var frameofreferenceuid = DicomUIDGenerator.GenerateDerivedFromUUID();
            var source = new DicomDataset();
            source.AddOrUpdate(DicomTag.ImagePositionPatient, 0.0m, 0.0m, 0.0m);
            source.AddOrUpdate(DicomTag.ImageOrientationPatient, 1.0m, 0.0m, 0.0m, 0.0m, 1.0m, 0.0m);
            source.AddOrUpdate(DicomTag.PixelSpacing, 0.5m, 0.5m);
            source.AddOrUpdate(DicomTag.Rows, (ushort)500);
            source.AddOrUpdate(DicomTag.Columns, (ushort)500);
            source.AddOrUpdate(DicomTag.FrameOfReferenceUID, frameofreferenceuid);
            var destination = new DicomDataset();
            destination.AddOrUpdate(DicomTag.ImagePositionPatient, 50.0m, 100.0m, 50.0m);
            destination.AddOrUpdate(DicomTag.ImageOrientationPatient, 1.0m, 0.0m, 0.0m, 0.0m, 0.70710678m, 0.70710678m);
            destination.AddOrUpdate(DicomTag.PixelSpacing, 0.25m, 0.25m);
            destination.AddOrUpdate(DicomTag.Rows, (ushort)600);
            destination.AddOrUpdate(DicomTag.Columns, (ushort)600);
            destination.AddOrUpdate(DicomTag.FrameOfReferenceUID, frameofreferenceuid);

            // CalculateIntersectionLocalizer
            var sourcegeometry = new FrameGeometry(source);
            var destinationgeometry = new FrameGeometry(destination);
            Assert.True(ImageLocalizer.CanDrawLocalizer(sourcegeometry, destinationgeometry));
            Assert.False(ImageLocalizer.CanDrawLocalizer(sourcegeometry, sourcegeometry));

            var ok = ImageLocalizer.CalcualteIntersectionLocalizer(sourcegeometry, destinationgeometry, out var startPoint, out var endPoint);
            Assert.True(ok);
            Assert.Equal(new Point2(800, -283), startPoint);
            Assert.Equal(new Point2(-200, -283), endPoint);

            ok = ImageLocalizer.CalcualteIntersectionLocalizer(destinationgeometry, sourcegeometry, out startPoint, out endPoint);
            Assert.False(ok);

            // calculateProjectionLocalizer
            ImageLocalizer.CalcualteProjectionLocalizer(source, destination, out var points);
            Assert.Equal(4, points.Count);
            Assert.Equal(new Point2(-200, -424), points[0]);
            Assert.Equal(new Point2(796, -424), points[1]);
            Assert.Equal(new Point2(796, 281), points[2]);
            Assert.Equal(new Point2(-200, 281), points[3]);

            ImageLocalizer.CalcualteProjectionLocalizer(destination, source, out points);
            Assert.Equal(4, points.Count);
            Assert.Equal(new Point2(100, 200), points[0]);
            Assert.Equal(new Point2(398, 200), points[1]);
            Assert.Equal(new Point2(398, 411), points[2]);
            Assert.Equal(new Point2(100, 411), points[3]);
        }


        [Theory]
        [InlineData("CR-ModalitySequenceLUT.dcm", FrameGeometryType.Plane, 7.5)]
        [InlineData("CT1_J2KI", FrameGeometryType.Volume, 33.0734)]
        [InlineData("GH645.dcm", FrameGeometryType.None, 0.0)]
        public void FrameGeometryMeassureDistance(string filename, FrameGeometryType expectedType, decimal expectedMeassure)
        {
            var image = DicomFile.Open(TestData.Resolve(filename));
            var geometry = new FrameGeometry(image.Dataset);
            Assert.Equal(expectedType, geometry.GeometryType);
            Assert.Equal(expectedType != FrameGeometryType.None, geometry.HasGeometryData);
            if (geometry.HasGeometryData)
            {
                var point1 = geometry.TransformImagePointToPatient(new Point2(0, 0));
                var point2 = geometry.TransformImagePointToPatient(new Point2(30, 40));
                var distance = point1.Distance(point2);
                Assert.Equal(expectedMeassure, distance, 4);
            }
        }


        [Theory]
        [InlineData("1,2", 0x0018, 0x1164)] // DicomTag.ImagerPixelSpacing
        [InlineData("", 0x0018, 0x1164)]    // DicomTag.ImagerPixelSpacing
        [InlineData("1,2", 0x0028, 0x0030)] // DicomTag.PixelSpacing
        [InlineData("", 0x0028, 0x0030)]    // DicomTag.PixelSpacing
        [InlineData("1,2", 0x0018, 0x2010)] // DicomTag.NominalScannedPixelSpacing
        [InlineData("", 0x0018, 0x2010)]    // DicomTag.NominalScannedPixelSpacing
        public void FrameGeometry_InstantiatesWithVariousPixelSpacingLengths(string values, ushort group, ushort element)
        {
            var pixelSpacingValues = values == "" ? System.Array.Empty<decimal>() : values.Split(',').Select(decimal.Parse).ToArray();

            var dataset = new DicomDataset
            {
                { DicomTag.ImagePositionPatient, new decimal[] { 0.0m, 0.0m, 0.0m } },
                { DicomTag.ImageOrientationPatient, new decimal[] { 1.0m, 0.0m, 0.0m, 0.0m, 1.0m, 0.0m } },
                { DicomTag.Rows, (ushort)500 },
                { DicomTag.Columns, (ushort)500 },
                { new DicomTag(group, element), pixelSpacingValues }
            };

            var exception = Record.Exception(() => new FrameGeometry(dataset));
            Assert.Null(exception);
        }


        [Theory]
        [InlineData("1,2", 0x0028, 0x0030)] // DicomTag.PixelSpacing in functional groups
        [InlineData("", 0x0028, 0x0030)]    // DicomTag.PixelSpacing in functional groups
        [InlineData("1,2", 0x0018, 0x1164)] // DicomTag.ImagerPixelSpacing in functional groups
        [InlineData("", 0x0018, 0x1164)]    // DicomTag.ImagerPixelSpacing in functional groups
        public void FrameGeometry_InstantiatesWithFunctionalGroupPixelSpacing(string values, ushort group, ushort element)
        {
            var pixelSpacingValues = values == "" ? System.Array.Empty<double>() : values.Split(',').Select(double.Parse).ToArray();

            var pixelMeasuresSequenceItem = new DicomDataset { ValidateItems = false };
            pixelMeasuresSequenceItem.Add(new DicomTag(group, element), pixelSpacingValues);

            var pixelMeasuresSequence = new DicomSequence(DicomTag.PixelMeasuresSequence, pixelMeasuresSequenceItem);
            var sharedFunctionalGroup = new DicomDataset { ValidateItems = false };
            sharedFunctionalGroup.Add(pixelMeasuresSequence);
            var sharedFunctionalGroupsSequence = new DicomSequence(DicomTag.SharedFunctionalGroupsSequence, sharedFunctionalGroup);

            var dataset = new DicomDataset
            {
                { DicomTag.ImagePositionPatient, new decimal[] { 0.0m, 0.0m, 0.0m } },
                { DicomTag.ImageOrientationPatient, new decimal[] { 1.0m, 0.0m, 0.0m, 0.0m, 1.0m, 0.0m } },
                { DicomTag.Rows, (ushort)500 },
                { DicomTag.Columns, (ushort)500 },
                sharedFunctionalGroupsSequence
            };

            var exception = Record.Exception(() => new FrameGeometry(dataset));
            Assert.Null(exception);
        }


        [Fact]
        public void ThrowWhenTransformingWithoutGeometryData()
        {
            // load a dicom image that does not contain pixel spacing (removed when anonymization was done)
            var image = DicomFile.Open(TestData.Resolve("CR-MONO1-10-chest"));
            var geometry = new FrameGeometry(image.Dataset);
            Assert.Equal(FrameGeometryType.None, geometry.GeometryType);
            var exception = Record.Exception(() => geometry.TransformImagePointToPatient(new Point2(10, 10)));
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<DicomException>(exception);
        }


        [Fact]
        public void InitialState()
        {
            // load a dicom image that does not contain pixel spacing (removed when anonymization was done)
            var geometry = new FrameGeometry(new DicomDataset());
            Assert.Equal(FrameGeometryType.None, geometry.GeometryType);
            var exception = Record.Exception(() => geometry.TransformImagePointToPatient(new Point2(10, 10)));
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<DicomException>(exception);
        }


        [Theory]
        [InlineData(0, 0, false)]  // Both missing
        [InlineData(1, 0, false)]  // Position has 1 value, orientation missing
        [InlineData(2, 0, false)]  // Position has 2 values, orientation missing
        [InlineData(3, 0, false)]  // Position complete, orientation missing
        [InlineData(0, 3, false)]  // Position missing, orientation has 3 values
        [InlineData(0, 5, false)]  // Position missing, orientation has 5 values
        [InlineData(0, 6, false)]  // Position missing, orientation complete
        [InlineData(3, 3, false)]  // Position complete, orientation has only row direction
        [InlineData(3, 5, false)]  // Position complete, orientation missing one value for column direction
        [InlineData(2, 5, false)]  // Both incomplete
        [InlineData(3, 6, true)]   // complete
        public void FrameGeometry_HandlesIncompletePositionAndOrientationArrays(int positionLength, int orientationLength, bool valid)
        {
            var positionValues = (new decimal[] { 0, 1, 2 }).Take(positionLength).ToArray();
            var orientationValues = (new decimal[] { 1, 0, 0, 0, 1, 0 }).Take(orientationLength).ToArray();

            var dataset = new DicomDataset { ValidateItems = false };
            dataset.Add(DicomTag.PixelSpacing, 0.5m, 0.5m);
            dataset.Add(DicomTag.Rows, (ushort)500);
            dataset.Add(DicomTag.Columns, (ushort)500);

            dataset.AddOrUpdate(DicomTag.ImagePositionPatient, positionValues);
            dataset.AddOrUpdate(DicomTag.ImageOrientationPatient, orientationValues);

            FrameGeometry geometry = null;
            var exception = Record.Exception(() => geometry = new FrameGeometry(dataset));
            Assert.Null(exception);
            Assert.NotNull(geometry);
            if (valid)
            {
                Assert.NotEqual(FrameOrientation.None, geometry.Orientation);
            }
            else
            {
                Assert.Equal(FrameOrientation.None, geometry.Orientation);
            }
        }

    }
}
