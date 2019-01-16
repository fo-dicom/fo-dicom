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


        public FrameGeometry(DicomImage image)
            : this(image.Dataset.GetString(DicomTag.FrameOfReferenceUID),
                  image.Dataset.GetValues<double>(DicomTag.ImagePositionPatient),
                  image.Dataset.GetValues<double>(DicomTag.ImageOrientationPatient),
                  image.Dataset.GetValues<double>(DicomTag.PixelSpacing),
                  image.Width,
                  image.Height)
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
        /// This method calculates the localizer line of the sourceFrame that can be drawn on the destinationFrame.
        /// You should call the method CanDrawLocalizer prior to check if localizer calculation is valid on the two frames.
        /// 
        /// If the two images intersect, then it returns <code>true</code> and the out parameters are filled with values. Otherwise the method returns <code>false</code>
        /// </summary>
        /// <param name="sourceFrame">The geometry of the frame, that is viewed by the user</param>
        /// <param name="destinationFrame">The geometry of the scout frame, where the localizer line should be drawn on</param>
        /// <param name="startPoint">If the frames intersect, then this contains the start point of the localizer line in terms of pixels on destinationFrame</param>
        /// <param name="endPoint">If the frames intersect, then this contains the end point of the localizer lin in terms of pixels on destinationFrame</param>
        /// <returns></returns>
        public static bool CalcualteLocalizer(FrameGeometry sourceFrame, FrameGeometry destinationFrame, out Point2 startPoint, out Point2 endPoint)
        {
            double t; // coeficient of the plane-equation
            double nA, nB, nC, nD, nP;
            var lstProj = new List<Point3D>();

            // initialize
            startPoint = Point2.Origin;
            endPoint = Point2.Origin;

            // validation
            if (destinationFrame.PointTopLeft.ToVector().IsZero)
                return false;
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
