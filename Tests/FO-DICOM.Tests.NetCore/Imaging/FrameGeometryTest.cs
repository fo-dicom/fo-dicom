// // Copyright (c) 2012-2019 fo-dicom contributors.
// // Licensed under the Microsoft Public License (MS-PL).
// 

using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.Mathematics;
using Xunit;

namespace FellowOakDicom.Tests.Imaging
{

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


    }
}
