// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

namespace System.Drawing
{
    public struct Point
    {
        #region FIELDS

        public static readonly Point Empty = new Point(0, 0);

        private int _x;
        private int _y;

        #endregion

        #region CONSTRUCTORS

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        #endregion

        #region PROPERTIES

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        #endregion

        #region METHODS

        public bool Equals(Point other)
        {
            return _x == other._x && _y == other._y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point && Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_x * 397) ^ _y;
            }
        }

        #endregion

        #region OPERATORS

        public static bool operator ==(Point lhs, Point rhs)
        {
            return rhs.Equals(lhs);
        }

        public static bool operator !=(Point lhs, Point rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
    }
}