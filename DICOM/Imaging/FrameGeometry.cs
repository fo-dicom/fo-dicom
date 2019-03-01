// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Imaging.Mathematics;
using System;
using System.Collections.Generic;

namespace Dicom.Imaging
{

    public enum FrameOrientation
    {
        None,
        Axial,
        Sagittal,
        Coronal
    }

    public class FrameGeometry
    {

        #region properties

        public string FrameOfReferenceUid { get; private set; }

        public Vector3D DirectionRow { get; private set; }

        public Vector3D DirectionColumn { get; private set; }

        public Vector3D DirectionNormal { get; private set; }

        public Point2 FrameSize { get; private set; }

        public double PixelSpacingX { get; private set; }

        public double PixelSpacingY { get; private set; }

        public Point3D PointTopLeft { get; private set; }
        public Point3D PointTopRight { get; private set; }
        public Point3D PointBottomLeft { get; private set; }
        public Point3D PointBottomRight { get; private set; }

        public FrameOrientation Orientation { get; private set; }

        private MatrixD ImageToPatientSpace { get; set; }
        private MatrixD PatientToImageSpace { get; set; }

        #endregion

        #region Constructor


        public FrameGeometry(DicomDataset image)
            : this(image.GetString(DicomTag.FrameOfReferenceUID),
                  image.GetValues<double>(DicomTag.ImagePositionPatient),
                  image.GetValues<double>(DicomTag.ImageOrientationPatient),
                  image.GetValues<double>(DicomTag.PixelSpacing),
                  image.GetSingleValue<int>(DicomTag.Columns),
                  image.GetSingleValue<int>(DicomTag.Rows))
        {
            // TODO: this constructor only works for single-frame images. Also handle multiframe like EnhancedCT or EnhancedMR
        }


        public FrameGeometry(string frameOfReferenceUid, double[] imagePatientPosition, double[] imagePatientOrientation, double[] pixelSpacing, int width, int height)
        {
            // copy provided values

            FrameOfReferenceUid = frameOfReferenceUid;
            PointTopLeft = new Point3D(imagePatientPosition);
            DirectionRow = new Vector3D(imagePatientOrientation, 0);
            DirectionColumn = new Vector3D(imagePatientOrientation, 3);
            FrameSize = new Point2(width, height);
            PixelSpacingX = pixelSpacing[0];
            PixelSpacingY = pixelSpacing[1];

            // calculate some additional values

            DirectionNormal = DirectionRow.CrossProduct(DirectionColumn);
            if (DirectionNormal.IsZero)
            {
                Orientation = FrameOrientation.None;
            }
            else
            {
                var axis = DirectionNormal.NearestAxis();
                if (axis.X != 0)
                    Orientation = FrameOrientation.Sagittal;
                else if (axis.Y != 0)
                    Orientation = FrameOrientation.Coronal;
                else if (axis.Z != 0)
                    Orientation = FrameOrientation.Axial;
                else
                    Orientation = FrameOrientation.None;
            }

            PointTopRight = PointTopLeft + DirectionRow * PixelSpacingX * FrameSize.X;
            PointBottomLeft = PointTopLeft + DirectionColumn * PixelSpacingY * FrameSize.Y;
            PointBottomRight = PointBottomLeft + (PointTopRight - PointTopLeft);

            if (DirectionNormal.IsZero)
            {
                ImageToPatientSpace = MatrixD.Identity(4);
                ImageToPatientSpace[0, 0] = PixelSpacingX;
                ImageToPatientSpace[1, 1] = PixelSpacingY;
            }
            else
            {
                ImageToPatientSpace = MatrixD.Identity(4);
                ImageToPatientSpace.Column(0, DirectionRow.X * PixelSpacingX, DirectionRow.Y * PixelSpacingX, DirectionRow.Z * PixelSpacingX, 0);
                ImageToPatientSpace.Column(1, DirectionColumn.X * PixelSpacingY, DirectionColumn.Y * PixelSpacingY, DirectionColumn.Z * PixelSpacingY, 0);
                ImageToPatientSpace.Column(2, DirectionNormal.X, DirectionNormal.Y, DirectionNormal.Z, 0);
                ImageToPatientSpace.Column(3, PointTopLeft.X, PointTopLeft.Y, PointTopLeft.Z, 1);
            }
            PatientToImageSpace = ImageToPatientSpace.Invert();
        }

        #endregion

        #region Methods


        public Point3D TransformImagePointToPatient(Point2 imagePoint)
        {
            var transformed = ImageToPatientSpace * new double[] { imagePoint.X, imagePoint.Y, 0, 1 };
            return new Point3D(transformed, 0);
        }

        public Point2 TransformPatientPointToImage(Point3D patientPoint)
        {
            var transformed = PatientToImageSpace * new double[] { patientPoint.X, patientPoint.Y, patientPoint.Z, 1 };
            // validation, if the point is within the image plane, then the z-component of the transformed point should be zero
            return new Point2((int)Math.Round(transformed[0]), (int)Math.Round(transformed[1]));
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
            if (sourceFrame == null) return false;
            if (destinationFrame == null) return false;

            // if either of the two frames is not a 3D image then there are no localizers
            if (sourceFrame.Orientation == FrameOrientation.None || destinationFrame.Orientation == FrameOrientation.None) return false;
            // localizers shall only be drawn on orthogonal images, so if they both are of the same orientation, then do not draw localizers
            if (sourceFrame.Orientation == destinationFrame.Orientation) return false;

            // in order to apply calculations on both frames, they both must be within the same FrameOfReferenceUid
            if (string.IsNullOrEmpty(sourceFrame.FrameOfReferenceUid) || string.IsNullOrEmpty(destinationFrame.FrameOfReferenceUid)) return false;
            if (sourceFrame.FrameOfReferenceUid != destinationFrame.FrameOfReferenceUid) return false;

            return true;
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
            localizerPoints = new List<Point2>();

            GetPositionOrientationSpacingAndSize(destinationFrame, out double dst_row_dircos_x, out double dst_row_dircos_y, out double dst_row_dircos_z,
                    out double dst_col_dircos_x, out double dst_col_dircos_y, out double dst_col_dircos_z,
                    out double dst_nrm_dircos_x, out double dst_nrm_dircos_y, out double dst_nrm_dircos_z,
                    out double dst_pos_x, out double dst_pos_y, out double dst_pos_z,
                    out ulong dst_rows, out ulong dst_cols,
                    out double dst_row_spacing, out double dst_col_spacing,
                    out double dst_row_length, out double dst_col_length);

            GetPositionOrientationSpacingAndSize(sourceFrame, out double src_row_dircos_x, out double src_row_dircos_y, out double src_row_dircos_z,
                        out double src_col_dircos_x, out double src_col_dircos_y, out double src_col_dircos_z,
                        out double src_nrm_dircos_x, out double src_nrm_dircos_y, out double src_nrm_dircos_z,
                        out double src_pos_x, out double src_pos_y, out double src_pos_z,
                        out ulong src_rows, out ulong src_cols,
                        out double src_row_spacing, out double src_col_spacing,
                        out double src_row_length, out double src_col_length);

            // Build a square to project with 4 corners TLHC, TRHC, BRHC, BLHC ...
            double[] pos_x = new double[4];
            double[] pos_y = new double[4];
            double[] pos_z = new double[4];

            // TLHC is what is in ImagePositionPatient
            pos_x[0] = src_pos_x;
            pos_y[0] = src_pos_y;
            pos_z[0] = src_pos_z;

            // TRHC
            pos_x[1] = src_pos_x + src_row_dircos_x * (src_row_length - 1);
            pos_y[1] = src_pos_y + src_row_dircos_y * (src_row_length - 1);
            pos_z[1] = src_pos_z + src_row_dircos_z * (src_row_length - 1);

            // BRHC
            pos_x[2] = src_pos_x + src_row_dircos_x * (src_row_length - 1) + src_col_dircos_x * (src_col_length - 1);
            pos_y[2] = src_pos_y + src_row_dircos_y * (src_row_length - 1) + src_col_dircos_y * (src_col_length - 1);
            pos_z[2] = src_pos_z + src_row_dircos_z * (src_row_length - 1) + src_col_dircos_z * (src_col_length - 1);

            // BLHC
            pos_x[3] = src_pos_x + src_col_dircos_x * (src_col_length - 1);
            pos_y[3] = src_pos_y + src_col_dircos_y * (src_col_length - 1);
            pos_z[3] = src_pos_z + src_col_dircos_z * (src_col_length - 1);

            int[] row_pixel = new int[4];
            int[] col_pixel = new int[4];

            for (int i = 0; i < 4; ++i)
            {            
                // move everything to origin of target
                pos_x[i] -= dst_pos_x;
                pos_y[i] -= dst_pos_y;
                pos_z[i] -= dst_pos_z;

                // The rotation is easy ... just rotate by the row, col and normal vectors ...
                Rotate(dst_row_dircos_x, dst_row_dircos_y, dst_row_dircos_z,
                    dst_col_dircos_x, dst_col_dircos_y, dst_col_dircos_z,
                    dst_nrm_dircos_x, dst_nrm_dircos_y, dst_nrm_dircos_z,
                    pos_x[i], pos_y[i], pos_z[i],
                    out pos_x[i], out pos_y[i], out pos_z[i]);

                // DICOM coordinates are center of pixel 1\1
                col_pixel[i] = Convert.ToInt32(pos_x[i] / dst_col_spacing + 0.5);
                row_pixel[i] = Convert.ToInt32(pos_y[i] / dst_row_spacing + 0.5);

            }            

            localizerPoints.Add(new Point2(col_pixel[0], row_pixel[0]));
            localizerPoints.Add(new Point2(col_pixel[1], row_pixel[1]));
            localizerPoints.Add(new Point2(col_pixel[2], row_pixel[2]));
            localizerPoints.Add(new Point2(col_pixel[3], row_pixel[3]));
        }

        /// <summary>
        /// This method gets the values for the image position, orientation, spacing, and size
        /// from ImageOrientationPatient, ImagePositionPatient, PixelSpacing, Rows, and Columns.
        /// The normal direction cosines are derived from row and column direction cosines.
        ///
        /// </summary>
        /// <param name="dicomDataset">The dataset of the frame, that is viewed by the user</param>
        /// <param name="row_dircos_x">The row direction cosine for the x-axis</param>
        /// <param name="row_dircos_y">The row direction cosine for the y-axis</param>        
        /// <param name="row_dircos_z">The row direction cosine for the z-axis</param>
        /// <param name="col_dircos_x">The column direction cosine for the x-axis</param>
        /// <param name="col_dircos_y">The column direction cosine for the y-axis</param>
        /// <param name="col_dircos_z">The column direction cosine for the z-axis</param>
        /// <param name="nrm_dircos_x">The normal direction cosine for the x-axis</param>        
        /// <param name="nrm_dircos_y">The normal direction cosine for the y-axis</param>
        /// <param name="nrm_dircos_z">The normal direction cosine for the z-axis</param>
        /// <param name="pos_x">The starting pixel position on the x-axis (top lefthand corner)</param>
        /// <param name="pos_y">The starting pixel position on the y-axis (top lefthand corner)</param>
        /// <param name="pos_z">The starting pixel position on the z-axis (top lefthand corner)</param>        
        /// <param name="rows">The number of rows in the frame</param>
        /// <param name="cols">The number of columns in the frame</param>
        /// <param name="row_spacing">The Row spacing of the frame, derived from the first entry of the PixelSpacing tag</param>
        /// <param name="col_spacing">The Column spacing of the frame, derived from the second entry of the PixelSpacing tag</param>
        /// <param name="row_length">The row length of the frame, derived from multiplying the columns by the row spacing</param>        
        /// <param name="col_length">The column length of the frame, derived from multiplying the rows by the column spacing</param>        
        /// <returns></returns>
        private static bool GetPositionOrientationSpacingAndSize(DicomDataset dicomDataset,
                        out double row_dircos_x, out double row_dircos_y, out double row_dircos_z,
                        out double col_dircos_x, out double col_dircos_y, out double col_dircos_z,
                        out double nrm_dircos_x, out double nrm_dircos_y, out double nrm_dircos_z,
                        out double pos_x, out double pos_y, out double pos_z,
                        out ulong rows, out ulong cols,
                        out double row_spacing, out double col_spacing,
                        out double row_length, out double col_length)
        {
            row_dircos_x = dicomDataset.GetValue<double>(DicomTag.ImageOrientationPatient, 0);
            row_dircos_y = dicomDataset.GetValue<double>(DicomTag.ImageOrientationPatient, 1);
            row_dircos_z = dicomDataset.GetValue<double>(DicomTag.ImageOrientationPatient, 2);
            col_dircos_x = dicomDataset.GetValue<double>(DicomTag.ImageOrientationPatient, 3);
            col_dircos_y = dicomDataset.GetValue<double>(DicomTag.ImageOrientationPatient, 4);
            col_dircos_z = dicomDataset.GetValue<double>(DicomTag.ImageOrientationPatient, 5);

            // compute nrm to row and col (i.e. cross product of row and col unit vectors)
            nrm_dircos_x = row_dircos_y * col_dircos_z - row_dircos_z * col_dircos_y;
            nrm_dircos_y = row_dircos_z * col_dircos_x - row_dircos_x * col_dircos_z;
            nrm_dircos_z = row_dircos_x * col_dircos_y - row_dircos_y * col_dircos_x;

            pos_x = dicomDataset.GetValue<double>(DicomTag.ImagePositionPatient, 0);
            pos_y = dicomDataset.GetValue<double>(DicomTag.ImagePositionPatient, 1);
            pos_z = dicomDataset.GetValue<double>(DicomTag.ImagePositionPatient, 2);

            row_spacing = dicomDataset.GetValue<double>(DicomTag.PixelSpacing, 0);
            col_spacing = dicomDataset.GetValue<double>(DicomTag.PixelSpacing, 1);

            rows = dicomDataset.GetSingleValue<ulong>(DicomTag.Rows);
            cols = dicomDataset.GetSingleValue<ulong>(DicomTag.Columns);

            row_length = cols * row_spacing;
            col_length = rows * col_spacing;

            return true;
        }

        /// <summary>
        /// This method rotates the positions of the source pixels to the orientation of the destination frame
        ///
        /// </summary>
        /// <param name="dst_row_dircos_x">The row direction cosine for the x-axis of the destination image</param>
        /// <param name="dst_row_dircos_y">The row direction cosine for the y-axis of the destination image</param>        
        /// <param name="dst_row_dircos_z">The row direction cosine for the z-axis of the destination image</param>
        /// <param name="dst_col_dircos_x">The column direction cosine for the x-axis of the destination image</param>
        /// <param name="dst_col_dircos_y">The column direction cosine for the y-axis of the destination image</param>
        /// <param name="dst_col_dircos_z">The column direction cosine for the z-axis of the destination image</param>
        /// <param name="dst_nrm_dircos_x">The normal direction cosine for the x-axis of the destination image</param>        
        /// <param name="dst_nrm_dircos_y">The normal direction cosine for the y-axis of the destination image</param>
        /// <param name="dst_nrm_dircos_z">The normal direction cosine for the z-axis of the destination image</param>
        /// <param name="src_pos_x">The starting pixel position on the x-axis of the source image</param>
        /// <param name="src_pos_y">The starting pixel position on the y-axis of the source image</param>
        /// <param name="src_pos_z">The starting pixel position on the z-axis of the source image</param> 
        /// <param name="dst_pos_x">The resulting rotated pixel position on the x-axis of the destination image (top lefthand corner)</param>
        /// <param name="dst_pos_y">The resulting rotated pixel position on the y-axis of the destination image (top lefthand corner)</param>
        /// <param name="dst_pos_z">The resulting rotated pixel position on the z-axis of the destination image (top lefthand corner)</param>
        /// <returns></returns>
        private static void Rotate(double dst_row_dircos_x, double dst_row_dircos_y, double dst_row_dircos_z,
                    double dst_col_dircos_x, double dst_col_dircos_y, double dst_col_dircos_z,
                    double dst_nrm_dircos_x, double dst_nrm_dircos_y, double dst_nrm_dircos_z,
                    double src_pos_x, double src_pos_y, double src_pos_z,
                    out double dst_pos_x, out double dst_pos_y, out double dst_pos_z)
        {
            dst_pos_x = dst_row_dircos_x * src_pos_x
                + dst_row_dircos_y * src_pos_y
                + dst_row_dircos_z * src_pos_z;

            dst_pos_y = dst_col_dircos_x * src_pos_x
                + dst_col_dircos_y * src_pos_y
                + dst_col_dircos_z * src_pos_z;

            dst_pos_z = dst_nrm_dircos_x * src_pos_x
                + dst_nrm_dircos_y * src_pos_y
                + dst_nrm_dircos_z * src_pos_z;
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
            double t; // coeficient of the plane-equation
            double nA, nB, nC, nD, nP;
            var lstProj = new List<Point3D>();

            // initialize
            startPoint = Point2.Origin;
            endPoint = Point2.Origin;

            // validation
            if (destinationFrame.DirectionNormal.IsZero)
                return false;

            nP = destinationFrame.DirectionNormal * destinationFrame.PointTopLeft;
            nA = destinationFrame.DirectionNormal * sourceFrame.PointTopLeft;
            nB = destinationFrame.DirectionNormal * sourceFrame.PointTopRight;
            nC = destinationFrame.DirectionNormal * sourceFrame.PointBottomRight;
            nD = destinationFrame.DirectionNormal * sourceFrame.PointBottomLeft;

            // segment AB
            if (Math.Abs(nB - nA) > Constants.Epsilon)
            {
                t = (nP - nA) / (nB - nA);
                if (t > 0 && t <= 1)
                    lstProj.Add(sourceFrame.PointTopLeft + t * (sourceFrame.PointTopRight - sourceFrame.PointTopLeft));
            }

            // segment BC
            if (Math.Abs(nC - nB) > Constants.Epsilon)
            {
                t = (nP - nB) / (nC - nB);
                if (t > 0 && t <= 1)
                    lstProj.Add(sourceFrame.PointTopRight + t * (sourceFrame.PointBottomRight - sourceFrame.PointTopRight));
            }

            // segment CD
            if (Math.Abs(nD - nC) > Constants.Epsilon)
            {
                t = (nP - nC) / (nD - nC);
                if (t > 0 && t <= 1)
                    lstProj.Add(sourceFrame.PointBottomRight + t * (sourceFrame.PointBottomLeft - sourceFrame.PointBottomRight));
            }

            // segment DA
            if (Math.Abs(nA - nD) > Constants.Epsilon)
            {
                t = (nP - nD) / (nA - nD);
                if (t > 0 && t <= 1)
                    lstProj.Add(sourceFrame.PointBottomLeft + t * (sourceFrame.PointTopLeft - sourceFrame.PointBottomLeft));
            }

            // the destinationplane should have been crossed exactly two times
            if (lstProj.Count != 2)
                return false;

            // now back from 3D patient space to 2D pixel space
            startPoint = destinationFrame.TransformPatientPointToImage(lstProj[0]);
            endPoint = destinationFrame.TransformPatientPointToImage(lstProj[1]);
            return true;
        }

    }
}
