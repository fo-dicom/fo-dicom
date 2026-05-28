// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Mathematics;
using System;
using System.Collections.Generic;
using System.Net;

namespace FellowOakDicom.Imaging
{

    public enum FrameOrientation
    {
        None,
        Axial,
        Sagittal,
        Coronal
    }

    public enum FrameGeometryType
    {
        None,
        Plane,
        Volume
    }

    public class FrameGeometry
    {

        #region properties

        public FrameGeometryType GeometryType { get; private set; } = FrameGeometryType.None;

        public bool HasGeometryData => GeometryType != FrameGeometryType.None;

        public string FrameOfReferenceUid { get; private set; }

        public Vector3<decimal> DirectionRow { get; private set; }

        public Vector3<decimal> DirectionColumn { get; private set; }

        public Vector3<decimal> DirectionNormal { get; private set; }

        public Point2 FrameSize { get; private set; }

        public decimal PixelSpacingBetweenColumns { get; private set; } = 0m;
        public decimal PixelSpacingBetweenRows { get; private set; } = 0m;

        public Point3<decimal> PointTopLeft { get; private set; }
        public Point3<decimal> PointTopRight { get; private set; }
        public Point3<decimal> PointBottomLeft { get; private set; }
        public Point3<decimal> PointBottomRight { get; private set; }

        public FrameOrientation Orientation { get; private set; }

        private Matrix<decimal> ImageToPatientSpace { get; set; }
        private Matrix<decimal> PatientToImageSpace { get; set; }

        #endregion

        #region Constructor


        /// <summary>
        /// A convenience class that extracts all data from a DicomDataset, that is relevant for geometry calculations.
        /// </summary>
        /// <param name="image">The DicomDataset where the information is extracted from</param>
        /// <param name="frame">An optional zero-based frame index. If not provided, then frame 0 is taken. In case of EnhancedCT or EnhancedMR the geometry data will be different for each frame</param>
        public FrameGeometry(DicomDataset image, int frame = 0)
        {
            var functionalItems = image.FunctionalGroupValues(frame);

            FrameOfReferenceUid = image.GetSingleValueOrDefault(DicomTag.FrameOfReferenceUID, string.Empty);

            FrameSize = new Point2(image.GetSingleValueOrDefault<int>(DicomTag.Columns, 0), image.GetSingleValueOrDefault<int>(DicomTag.Rows, 0));

            if (image.TryGetValues<decimal>(DicomTag.ImagerPixelSpacing, out var imagerPixelSpacing) && imagerPixelSpacing.Length == 2)
            {
                PixelSpacingBetweenRows = imagerPixelSpacing[0];
                PixelSpacingBetweenColumns = imagerPixelSpacing[1];
            }
            else if (image.TryGetValues<decimal>(DicomTag.PixelSpacing, out var pixelSpacing) && pixelSpacing.Length == 2)
            {
                PixelSpacingBetweenRows = pixelSpacing[0];
                PixelSpacingBetweenColumns = pixelSpacing[1];
            }
            else if (image.TryGetValues<decimal>(DicomTag.NominalScannedPixelSpacing, out var nominalPixelSpacing) && nominalPixelSpacing.Length == 2)
            {
                PixelSpacingBetweenRows = nominalPixelSpacing[0];
                PixelSpacingBetweenColumns = nominalPixelSpacing[1];
            }
            else if (functionalItems.TryGetValues<decimal>(DicomTag.PixelSpacing, out var functionalPixelSpacing) && functionalPixelSpacing.Length == 2)
            {
                PixelSpacingBetweenRows = functionalPixelSpacing[0];
                PixelSpacingBetweenColumns = functionalPixelSpacing[1];
            }
            else if (functionalItems.TryGetValues<decimal>(DicomTag.ImagerPixelSpacing, out var functionalImagerPixelSpacing) && functionalImagerPixelSpacing.Length == 2)
            {
                PixelSpacingBetweenRows = functionalImagerPixelSpacing[0];
                PixelSpacingBetweenColumns = functionalImagerPixelSpacing[1];
            }

            var patientPosition = FindInDatasetOrFunctional(DicomTag.ImagePositionPatient);
            var patientOrientation = FindInDatasetOrFunctional(DicomTag.ImageOrientationPatient);
            InitializeCalcualtedVolumeData(patientPosition, patientOrientation);

            InitializeTranformationMatrizes();

            decimal[] FindInDatasetOrFunctional(DicomTag tag)
            {
                if (image.TryGetValues<decimal>(tag, out var result))
                {
                    return result;
                }
                if (functionalItems.TryGetValues<decimal>(tag, out var functionalResult))
                {
                    return functionalResult;
                }
                return [];
            }
        }


        public FrameGeometry(string frameOfReferenceUid, decimal[] imagePatientPosition, decimal[] imagePatientOrientation, decimal[] pixelSpacing, int width, int height)
        {
            // copy provided values

            FrameOfReferenceUid = frameOfReferenceUid;

            FrameSize = new Point2(width, height);
            PixelSpacingBetweenRows = pixelSpacing[0];
            PixelSpacingBetweenColumns = pixelSpacing[1];

            InitializeCalcualtedVolumeData(imagePatientPosition, imagePatientOrientation);

            InitializeTranformationMatrizes();
        }

        #endregion


        #region private methods

        private void InitializeCalcualtedVolumeData(decimal[] imagePatientPosition, decimal[] imagePatientOrientation)
        {
            if (imagePatientPosition.Length < 3 || imagePatientOrientation.Length < 6)
            {
                // in case there are no or only incomplete data, then no 3d-initialization can be done
                // these are the default-values for some 2d-data like CR. they are used for measurements of lengths and angles in 2d, but not for 3d
                Orientation = FrameOrientation.None;
                PointTopLeft = new Point3<decimal>(0, 0, 0);
                DirectionRow = new Vector3<decimal>(1, 0, 0);
                DirectionColumn = new Vector3<decimal>(0, 1, 0);
                DirectionNormal = Vector3<decimal>.Zero;
                PointTopRight = PointTopLeft + DirectionRow * PixelSpacingBetweenColumns * FrameSize.X;
                PointBottomLeft = PointTopLeft + DirectionColumn * PixelSpacingBetweenRows * FrameSize.Y;
                PointBottomRight = PointBottomLeft + (PointTopRight - PointTopLeft);

                return;
            }

            PointTopLeft = new Point3<decimal>(imagePatientPosition);
            DirectionRow = new Vector3<decimal>(imagePatientOrientation, 0);
            DirectionColumn = new Vector3<decimal>(imagePatientOrientation, 3);

            DirectionNormal = DirectionRow.CrossProduct(DirectionColumn);
            if (DirectionNormal.IsZero)
            {
                Orientation = FrameOrientation.None;
            }
            else
            {
                var axis = DirectionNormal.NearestAxis();
                Orientation = axis switch
                {
                    Vector3<decimal> xaxis when xaxis.X != 0 => FrameOrientation.Sagittal,
                    Vector3<decimal> yaxis when yaxis.Y != 0 => FrameOrientation.Coronal,
                    Vector3<decimal> zaxis when zaxis.Z != 0 => FrameOrientation.Axial,
                    _ => FrameOrientation.None
                };
            }

            PointTopRight = PointTopLeft + DirectionRow * PixelSpacingBetweenColumns * FrameSize.X;
            PointBottomLeft = PointTopLeft + DirectionColumn * PixelSpacingBetweenRows * FrameSize.Y;
            PointBottomRight = PointBottomLeft + (PointTopRight - PointTopLeft);
        }

        private void InitializeTranformationMatrizes()
        {
            GeometryType = FrameGeometryType.None;

            if (!PixelSpacingBetweenColumns.IsNearlyZero() && !PixelSpacingBetweenRows.IsNearlyZero())
            {
                // at least pixel spacing is present
                GeometryType = FrameGeometryType.Plane;

                if (DirectionNormal.IsZero)
                {
                    ImageToPatientSpace = Matrix<decimal>.Identity(4);
                    ImageToPatientSpace[0, 0] = PixelSpacingBetweenColumns;
                    ImageToPatientSpace[1, 1] = PixelSpacingBetweenRows;
                }
                else
                {
                    ImageToPatientSpace = Matrix<decimal>.Identity(4);
                    ImageToPatientSpace.Column(0, DirectionRow.X * PixelSpacingBetweenColumns, DirectionRow.Y * PixelSpacingBetweenColumns, DirectionRow.Z * PixelSpacingBetweenColumns, 0);
                    ImageToPatientSpace.Column(1, DirectionColumn.X * PixelSpacingBetweenRows, DirectionColumn.Y * PixelSpacingBetweenRows, DirectionColumn.Z * PixelSpacingBetweenRows, 0);
                    ImageToPatientSpace.Column(2, DirectionNormal.X, DirectionNormal.Y, DirectionNormal.Z, 0);
                    ImageToPatientSpace.Column(3, PointTopLeft.X, PointTopLeft.Y, PointTopLeft.Z, 1);
                }

                PatientToImageSpace = ImageToPatientSpace.Invert();

                if (PointTopLeft != Point3<decimal>.Zero || DirectionRow != Vector3<decimal>.AxisX || DirectionColumn != Vector3<decimal>.AxisY)
                {
                    GeometryType = FrameGeometryType.Volume;
                }
            }
        }

        #endregion

        #region Methods


        public Point3<decimal> TransformImagePointToPatient(Point2 imagePoint)
        {
            if (GeometryType == FrameGeometryType.None)
            {
                throw new DicomImagingException("Cannot transform point in image without geometry data");
            }
            var transformed = ImageToPatientSpace * new decimal[] { imagePoint.X, imagePoint.Y, 0, 1 };
            return new Point3<decimal>(transformed, 0);
        }

        public Point2<decimal> TransformPatientPointToImage(Point3<decimal> patientPoint)
        {
            if (GeometryType == FrameGeometryType.None)
            {
                throw new DicomImagingException("Cannot transform point in image without geometry data");
            }
            decimal[] transformed = PatientToImageSpace * new decimal[] { patientPoint.X, patientPoint.Y, patientPoint.Z, 1 };
            // validation, if the point is within the image plane, then the z-component of the transformed point should be zero
            return new Point2<decimal>(transformed[0], transformed[1]);
        }

        #endregion

    }


    public static class ImageLocalizer
    {

        /// <summary>
        /// This method performes some checks, if it is valid or allowed that the location of the sourceFrame
        /// is drawn on the destinationFrame.
        /// This check should be called at least once before the more computation intensive method CalcualteLocalizer
        /// is called.
        /// </summary>
        /// <param name="sourceFrame"></param>
        /// <param name="destinationFrame"></param>
        /// <returns></returns>
        public static bool CanDrawLocalizer(FrameGeometry sourceFrame, FrameGeometry destinationFrame)
        {
            // first check for valid frame geometry
            if (sourceFrame == null || destinationFrame == null)
            {
                return false;
            }

            // if either of the two frames is not a 3D image then there are no localizers
            if (sourceFrame.Orientation == FrameOrientation.None || destinationFrame.Orientation == FrameOrientation.None)
            {
                return false;
            }
            // localizers shall only be drawn on orthogonal images, so if they both are of the same orientation, then do not draw localizers
            if (sourceFrame.Orientation == destinationFrame.Orientation)
            {
                return false;
            }

            // in order to apply calculations on both frames, they both must be within the same FrameOfReferenceUid
            if (string.IsNullOrEmpty(sourceFrame.FrameOfReferenceUid) || string.IsNullOrEmpty(destinationFrame.FrameOfReferenceUid))
            {
                return false;
            }

            return sourceFrame.FrameOfReferenceUid == destinationFrame.FrameOfReferenceUid;
        }

        /// <summary>
        /// This method calculates the localizer rectangle of the sourceFrame that can be drawn on the destinationFrame.
        /// You should call the method CanDrawLocalizer prior to check if localizer calculation is valid on the two frames.
        /// This method will return the 4 points of a rectangle, although most image sets are orthogonal, resulting in the rectangle
        /// being presented as a straight line on the scout image. Since the source image is projected on the destination image
        /// there might me a result even if the two images do not intersect
        ///
        /// </summary>
        /// <param name="sourceFrame">The dataset of the frame, that is viewed by the user</param>
        /// <param name="destinationFrame">The dataset of the scout frame, where the localizer line should be drawn on</param>
        /// <param name="localizerPoints">This contains the points of the localizer rectangle in terms of pixels on destinationFrame</param>        
        /// <returns></returns>
        public static void CalcualteProjectionLocalizer(DicomDataset sourceFrame, DicomDataset destinationFrame, out List<Point2> localizerPoints)
        {
            localizerPoints = [];

            GetPositionOrientationSpacingAndSize(destinationFrame, out Vector3<double> dstRowDir,
                    out Vector3<double> dstColDir, out Vector3<double> dstNormal, out Point3<double> dstPos,
                    out int _, out int _,
                    out double dstRowSpacing, out double dstColSpacing,
                    out double _, out double _);

            GetPositionOrientationSpacingAndSize(sourceFrame, out Vector3<double> srcRowDir,
                    out Vector3<double> srcColDir, out Vector3<double> _, out Point3<double> srcPos,
                    out int _, out int _,
                    out double _, out double _,
                    out double srcRowLength, out double srcColLength);

            // Build a square to project with 4 corners TLHC, TRHC, BRHC, BLHC ...
            var pos = new Point3<double>[4];

            // TLHC is what is in ImagePositionPatient
            pos[0] = srcPos;
            // TRHC
            pos[1] = srcPos + srcRowDir * (srcRowLength - 1);
            // BRHC
            pos[2] = srcPos + srcRowDir * (srcRowLength - 1) + srcColDir * (srcColLength - 1);
            // BLHC
            pos[3] = srcPos + srcColDir * (srcColLength - 1);

            var pixel = new Point2[4];

            var rotation = new Matrix<double>(3, 3);
            rotation.Row(0, dstRowDir.ToArray());
            rotation.Row(1, dstColDir.ToArray());
            rotation.Row(2, dstNormal.ToArray());

            for (int i = 0; i < 4; i++)
            {
                // move everything to origin of target
                pos[i] += (Point3<double>.Zero - dstPos);

                // The rotation is easy ... just rotate by the row, col and normal vectors ...
                pos[i] = new Point3<double>(rotation * pos[i].ToArray());

                // DICOM coordinates are center of pixel 1\1
                pixel[i] = new Point2(Convert.ToInt32(pos[i].X / dstColSpacing + 0.5),
                  Convert.ToInt32(pos[i].Y / dstRowSpacing + 0.5));
            }

            localizerPoints.AddRange(pixel);
        }

        /// <summary>
        /// This method gets the values for the image position, orientation, spacing, and size
        /// from ImageOrientationPatient, ImagePositionPatient, PixelSpacing, Rows, and Columns.
        /// The normal direction cosines are derived from row and column direction cosines.
        ///
        /// </summary>
        /// <param name="dicomDataset">The dataset of the frame, that is viewed by the user</param>
        /// <param name="rowDir">The row direction cosine</param>
        /// <param name="colDir">The column direction cosine</param>
        /// <param name="normalDir">The normal direction cosine</param>
        /// <param name="pos">The starting pixel position (top lefthand corner)</param>
        /// <param name="rows">The number of rows in the frame</param>
        /// <param name="cols">The number of columns in the frame</param>
        /// <param name="row_spacing">The Row spacing of the frame, derived from the first entry of the PixelSpacing tag</param>
        /// <param name="col_spacing">The Column spacing of the frame, derived from the second entry of the PixelSpacing tag</param>
        /// <param name="row_length">The row length of the frame, derived from multiplying the columns by the row spacing</param>
        /// <param name="col_length">The column length of the frame, derived from multiplying the rows by the column spacing</param>
        /// <returns></returns>
        private static bool GetPositionOrientationSpacingAndSize(DicomDataset dicomDataset,
                        out Vector3<double> rowDir, out Vector3<double> colDir,
                        out Vector3<double> normalDir, out Point3<double> pos,
                        out int rows, out int cols,
                        out double row_spacing, out double col_spacing,
                        out double row_length, out double col_length)
        {
            var imageorientation = dicomDataset.GetValues<double>(DicomTag.ImageOrientationPatient);
            rowDir = new Vector3<double>(imageorientation, 0);
            colDir = new Vector3<double>(imageorientation, 3);
            // compute nrm to row and col (i.e. cross product of row and col unit vectors)
            normalDir = rowDir.CrossProduct(colDir);

            pos = new Point3<double>(dicomDataset.GetValues<double>(DicomTag.ImagePositionPatient));

            row_spacing = dicomDataset.GetValue<double>(DicomTag.PixelSpacing, 0);
            col_spacing = dicomDataset.GetValue<double>(DicomTag.PixelSpacing, 1);

            rows = dicomDataset.GetSingleValue<int>(DicomTag.Rows);
            cols = dicomDataset.GetSingleValue<int>(DicomTag.Columns);

            row_length = cols * row_spacing;
            col_length = rows * col_spacing;

            return true;
        }


        /// <summary>
        /// This method calculates the localizer line of the sourceFrame that can be drawn on the destinationFrame.
        /// You should call the method CanDrawLocalizer prior to check if localizer calculation is valid on the two frames.
        ///
        /// This method returns the line of common pixels where the two images intersect.
        /// If the two images intersect, then it returns <code>true</code> and the out parameters are filled with values. Otherwise the method returns <code>false</code>
        /// </summary>
        /// <param name="sourceFrame">The geometry of the frame, that is viewed by the user</param>
        /// <param name="destinationFrame">The geometry of the scout frame, where the localizer line should be drawn on</param>
        /// <param name="startPoint">If the frames intersect, then this contains the start point of the localizer line in terms of pixels on destinationFrame</param>
        /// <param name="endPoint">If the frames intersect, then this contains the end point of the localizer lin in terms of pixels on destinationFrame</param>
        /// <returns></returns>
        public static bool CalcualteIntersectionLocalizer(FrameGeometry sourceFrame, FrameGeometry destinationFrame, out Point2 startPoint, out Point2 endPoint)
        {
            decimal t; // coeficient of the plane-equation
            decimal nA, nB, nC, nD, nP;
            var lstProj = new List<Point3<decimal>>();

            // initialize
            startPoint = Point2.Origin;
            endPoint = Point2.Origin;

            // validation
            if (destinationFrame.DirectionNormal.IsZero)
            {
                return false;
            }

            nP = destinationFrame.DirectionNormal * destinationFrame.PointTopLeft;
            nA = destinationFrame.DirectionNormal * sourceFrame.PointTopLeft;
            nB = destinationFrame.DirectionNormal * sourceFrame.PointTopRight;
            nC = destinationFrame.DirectionNormal * sourceFrame.PointBottomRight;
            nD = destinationFrame.DirectionNormal * sourceFrame.PointBottomLeft;

            // segment AB
            if (Math.Abs(nB - nA) > Constants.EpsilonM)
            {
                t = (nP - nA) / (nB - nA);
                if (t > 0 && t <= 1)
                {
                    lstProj.Add(sourceFrame.PointTopLeft + t * (sourceFrame.PointTopRight - sourceFrame.PointTopLeft));
                }
            }

            // segment BC
            if (Math.Abs(nC - nB) > Constants.EpsilonM)
            {
                t = (nP - nB) / (nC - nB);
                if (t > 0 && t <= 1)
                {
                    lstProj.Add(sourceFrame.PointTopRight + t * (sourceFrame.PointBottomRight - sourceFrame.PointTopRight));
                }
            }

            // segment CD
            if (Math.Abs(nD - nC) > Constants.EpsilonM)
            {
                t = (nP - nC) / (nD - nC);
                if (t > 0 && t <= 1)
                {
                    lstProj.Add(sourceFrame.PointBottomRight + t * (sourceFrame.PointBottomLeft - sourceFrame.PointBottomRight));
                }
            }

            // segment DA
            if (Math.Abs(nA - nD) > Constants.EpsilonM)
            {
                t = (nP - nD) / (nA - nD);
                if (t > 0 && t <= 1)
                {
                    lstProj.Add(sourceFrame.PointBottomLeft + t * (sourceFrame.PointTopLeft - sourceFrame.PointBottomLeft));
                }
            }

            // the destinationplane should have been crossed exactly two times
            if (lstProj.Count != 2)
            {
                return false;
            }

            // now back from 3D patient space to 2D pixel space
            var startPointExact = destinationFrame.TransformPatientPointToImage(lstProj[0]);
            startPoint = new Point2((int)Math.Round(startPointExact.X), (int)Math.Round(startPointExact.Y));
            var endPointExact = destinationFrame.TransformPatientPointToImage(lstProj[1]);
            endPoint = new Point2((int)Math.Round(endPointExact.X), (int)Math.Round(endPointExact.Y));
            return true;
        }

    }
}
