// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
        public static (Point3<decimal> min, Point3<decimal> max) GetBoundingBox(this FrameGeometry geometry)
            => GetBoundingBox(geometry.PointTopLeft, geometry.PointTopRight, geometry.PointBottomLeft, geometry.PointBottomRight);

        /// <summary>
        /// Finds the bounding box of a list of points in space.
        /// </summary>
        public static (Point3<T> min, Point3<T> max) GetBoundingBox<T>(this IEnumerable<Point3<T>> points) where T : INumber<T>
            => GetBoundingBox(points.ToArray());

        /// <summary>
        /// Finds the bounding box of a list of points in space.
        /// </summary>
        private static (Point3<T> min, Point3<T> max) GetBoundingBox<T>(params Point3<T>[] points) where T : INumber<T>
        {
            IEnumerable<T> xvalues = points.Select(p => p.X);
            IEnumerable<T> yvalues = points.Select(p => p.Y);
            IEnumerable<T> zvalues = points.Select(p => p.Z);
            return (new Point3<T>(xvalues.Min(), yvalues.Min(), zvalues.Min()), new Point3<T>(xvalues.Max(), yvalues.Max(), zvalues.Max()));
        }

    }
}
