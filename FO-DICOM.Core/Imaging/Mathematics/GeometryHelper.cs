// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Linq;

namespace FellowOakDicom.Imaging.Mathematics
{

    public static class GeometryHelper
    {

        /// <summary>
        /// Returns the minimal value of all the values
        /// </summary>
        /// <param name="values">A list of values</param>
        public static double Min(params double[] values) => values.Min();

        /// <summary>
        /// Finds the bounding box of the geometry by finding the bounding box of the 4 corners
        /// </summary>
        public static (Point3D min, Point3D max) GetBoundingBox(this FrameGeometry geometry)
            => GetBoundingBox(geometry.PointTopLeft, geometry.PointTopRight, geometry.PointBottomLeft, geometry.PointBottomRight);

        /// <summary>
        /// Finds the bounding box of a list of points in space.
        /// </summary>
        public static (Point3D min, Point3D max) GetBoundingBox(this IEnumerable<Point3D> points)
            => GetBoundingBox(points.ToArray());

        /// <summary>
        /// Finds the bounding box of a list of points in space.
        /// </summary>
        private static (Point3D min, Point3D max) GetBoundingBox(params Point3D[] points)
        {
            IEnumerable<double> xvalues = points.Select(p => p.X);
            IEnumerable<double> yvalues = points.Select(p => p.Y);
            IEnumerable<double> zvalues = points.Select(p => p.Z);
            return (new Point3D(xvalues.Min(), yvalues.Min(), zvalues.Min()), new Point3D(xvalues.Max(), yvalues.Max(), zvalues.Max()));
        }

    }
}
