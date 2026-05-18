// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Numerics;
using System.Text;

namespace FellowOakDicom.Imaging.Mathematics
{

    public class Matrix
    {
        #region Members

        private readonly int[,] _matrix;

        #endregion

        #region Constructors

        public Matrix(int rows, int cols)
        {
            _matrix = new int[rows, cols];
        }

        internal Matrix(int[,] matrix)
        {
            _matrix = (int[,])matrix.Clone();
        }

        #endregion

        #region Properties

        public int Rows => _matrix.GetLength(0);

        public int Columns => _matrix.GetLength(1);

        public bool IsSquare => Rows == Columns;

        public bool IsIdentity
        {
            get
            {
                if (!IsSquare)
                {
                    return false;
                }

                for (int r = 0, rows = Rows; r < rows; r++)
                {
                    for (int c = 0, cols = Columns; c < cols; c++)
                    {
                        if (_matrix[r, c] != ((r == c) ? 1 : 0))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public int this[int row, int col]
        {
            get => _matrix[row, col];
            set => _matrix[row, col] = value;
        }

        public int Determinant
        {
            get
            {
                if (!IsSquare)
                {
                    throw new InvalidOperationException("Cannot calculate determinant of non-square matrix.");
                }

                int dimensions = Rows;

                if (dimensions == 1)
                {
                    return _matrix[0, 0];
                }

                if (dimensions == 2)
                {
                    return (_matrix[0, 0] * _matrix[1, 1]) - (_matrix[0, 1] * _matrix[1, 0]);
                }

                if (dimensions == 3)
                {
                    int aei = _matrix[0, 0] * _matrix[1, 1] * _matrix[2, 2];
                    int bfg = _matrix[0, 1] * _matrix[1, 2] * _matrix[2, 0];
                    int cdh = _matrix[0, 2] * _matrix[1, 0] * _matrix[2, 1];
                    int hfa = _matrix[2, 1] * _matrix[1, 2] * _matrix[0, 0];
                    int idb = _matrix[2, 2] * _matrix[1, 0] * _matrix[0, 1];
                    int gec = _matrix[2, 0] * _matrix[1, 1] * _matrix[0, 2];
                    return (aei + bfg + cdh) - (hfa + idb + gec);
                }

                int pos = 0;
                for (int c = 0; c < dimensions; c++)
                {
                    int k = c;
                    int diag = 0;
                    for (int r = 0; r < dimensions; r++, k = ((k + 1) % dimensions))
                    {
                        diag *= _matrix[r, k];
                    }
                    pos += diag;
                }

                int neg = 0;
                for (int c = 0; c < dimensions; c++)
                {
                    int k = (c + 1) % dimensions;
                    int diag = 0;
                    for (int r = dimensions - 1; r >= 0; r--, k = ((k + 1) % dimensions))
                    {
                        diag *= _matrix[r, k];
                    }
                    neg += diag;
                }

                return pos - neg;
            }
        }

        public int Trace
        {
            get
            {
                if (!IsSquare)
                {
                    throw new InvalidOperationException("Cannot calc trace of non-square matrix.");
                }

                int t = 0;

                for (int x = 0, count = Rows; x < count; x++)
                {
                    t += _matrix[x, x];
                }

                return t;
            }
        }

        #endregion

        #region Methods

        public void Row(int row, params int[] values)
        {
            if (values.Length != Columns)
            {
                throw new ArgumentOutOfRangeException("values.Length");
            }

            for (int col = 0, cols = Columns; col < cols; col++)
            {
                _matrix[row, col] = values[col];
            }
        }

        public int[] Row(int row)
        {
            int[] values = new int[Columns];

            for (int col = 0, cols = Columns; col < cols; col++)
            {
                values[col] = _matrix[row, col];
            }

            return values;
        }

        public void Column(int col, params int[] values)
        {
            if (values.Length != Rows)
            {
                throw new ArgumentOutOfRangeException("values.Length");
            }

            for (int row = 0, rows = Rows; row < rows; row++)
            {
                _matrix[row, col] = values[row];
            }
        }

        public int[] Column(int col)
        {
            int[] values = new int[Rows];

            for (int row = 0, rows = Rows; row < rows; row++)
            {
                values[row] = _matrix[row, col];
            }

            return values;
        }

        public Matrix Clone()
        {
            return new Matrix(_matrix);
        }

        public Matrix Transpose()
        {
            var t = new Matrix(Columns, Rows);

            for (int r = 0, rows = Rows; r < rows; r++)
            {
                for (int c = 0, cols = Columns; c < cols; c++)
                {
                    t[c, r] = _matrix[r, c];
                }
            }

            return t;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('[');
            for (int r = 0, rows = Rows; r < rows; r++)
            {
                if (r > 0)
                {
                    sb.Append("; ");
                }

                for (int c = 0, cols = Columns; c < cols; c++)
                {
                    if (c > 0)
                    {
                        sb.Append(',');
                    }

                    sb.Append(_matrix[r, c]);
                }
            }
            sb.Append(']');
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix other && other == this;
        }

        public override int GetHashCode() => _matrix.GetHashCode();

        #endregion

        #region Static

        public static Matrix Zero(int rows, int columns)
        {
            return new Matrix(rows, columns);
        }

        public static Matrix One(int rows, int columns)
        {
            var m = new Matrix(rows, columns);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    m[r, c] = 1;
                }
            }

            return m;
        }

        public static Matrix Identity(int dimensions)
        {
            var m = new Matrix(dimensions, dimensions);

            for (int r = 0, rows = dimensions; r < rows; r++)
            {
                for (int c = 0, cols = dimensions; c < cols; c++)
                {
                    m[r, c] = (r == c) ? 1 : 0;
                }
            }

            return m;
        }

        public static bool operator ==(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
            {
                return false;
            }

            for (int r = 0, rows = a.Rows; r < rows; r++)
            {
                for (int c = 0, cols = a.Columns; c < cols; c++)
                {
                    if (a[r, c] != b[r, c])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(Matrix a, Matrix b)
        {
            return !(a == b);
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
            {
                throw new ArgumentException("Unable to add matrices of different dimensions");
            }

            Matrix x = a.Clone();

            for (int r = 0, rows = x.Rows; r < rows; r++)
            {
                for (int c = 0, cols = x.Columns; c < cols; c++)
                {
                    x[r, c] += b[r, c];
                }
            }

            return x;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
            {
                throw new ArgumentException("Unable to subtract matrices of different dimensions");
            }

            Matrix x = a.Clone();

            for (int r = 0, rows = x.Rows; r < rows; r++)
            {
                for (int c = 0, cols = x.Columns; c < cols; c++)
                {
                    x[r, c] -= b[r, c];
                }
            }

            return x;
        }

        public static Matrix operator -(Matrix a)
        {
            Matrix x = a.Clone();

            for (int r = 0, rows = a.Rows; r < rows; r++)
            {
                for (int c = 0, cols = a.Columns; c < cols; c++)
                {
                    x[r, c] = -x[r, c];
                }
            }

            return x;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.Columns != b.Rows)
            {
                throw new ArgumentException("Unable to multiply matrices of different inner dimensions");
            }

            var x = new Matrix(a.Rows, b.Columns);

            int inner = a.Columns;
            for (int r = 0, rows = x.Rows; r < rows; r++)
            {
                for (int c = 0, cols = x.Columns; c < cols; c++)
                {
                    int d = 0;
                    for (int i = 0; i < inner; i++)
                    {
                        d += a[r, i] * b[i, c];
                    }

                    x[r, c] = d;
                }
            }

            return x;
        }

        public static Matrix operator *(Matrix a, int d)
        {
            Matrix x = a.Clone();

            for (int r = 0, rows = x.Rows; r < rows; r++)
            {
                for (int c = 0, cols = x.Columns; c < cols; c++)
                {
                    x[r, c] *= d;
                }
            }

            return x;
        }

        public static Matrix operator *(int d, Matrix a)
        {
            Matrix x = a.Clone();

            for (int r = 0, rows = x.Rows; r < rows; r++)
            {
                for (int c = 0, cols = x.Columns; c < cols; c++)
                {
                    x[r, c] *= d;
                }
            }

            return x;
        }

        public static Matrix operator /(Matrix a, int d)
        {
            return a * (1 / d);
        }

        public static Matrix operator ^(Matrix a, int e)
        {
            Matrix m = a.Clone();
            for (int i = 1; i < e; i++)
            {
                m *= a;
            }
            return m;
        }

        #endregion
    }

    public class Matrix<T> where T : INumber<T>
    {
        #region Members

        private readonly T[,] _matrix;

        #endregion

        #region Constructors

        public Matrix(int rows, int cols)
        {
            _matrix = new T[rows, cols];
        }

        internal Matrix(T[,] matrix)
        {
            _matrix = (T[,])matrix.Clone();
        }

        #endregion

        #region Properties

        public int Rows => _matrix.GetLength(0);

        public int Columns => _matrix.GetLength(1);

        public bool IsSquare => Rows == Columns;

        public bool IsIdentity
        {
            get
            {
                if (!IsSquare)
                {
                    return false;
                }

                for (int r = 0, rows = Rows; r < rows; r++)
                {
                    for (int c = 0, cols = Columns; c < cols; c++)
                    {
                        if (_matrix[r, c] != ((r == c) ? T.One : T.Zero))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public T this[int row, int col]
        {
            get => _matrix[row, col];
            set => _matrix[row, col] = value;
        }

        public T Determinant
        {
            get
            {
                if (!IsSquare)
                {
                    throw new InvalidOperationException("Cannot calculate determinant of non-square matrix.");
                }

                int dimensions = Rows;

                if (dimensions == 1)
                {
                    return _matrix[0, 0];
                }

                if (dimensions == 2)
                {
                    return (_matrix[0, 0] * _matrix[1, 1]) - (_matrix[0, 1] * _matrix[1, 0]);
                }

                if (dimensions == 3)
                {
                    T aei = _matrix[0, 0] * _matrix[1, 1] * _matrix[2, 2];
                    T bfg = _matrix[0, 1] * _matrix[1, 2] * _matrix[2, 0];
                    T cdh = _matrix[0, 2] * _matrix[1, 0] * _matrix[2, 1];
                    T hfa = _matrix[2, 1] * _matrix[1, 2] * _matrix[0, 0];
                    T idb = _matrix[2, 2] * _matrix[1, 0] * _matrix[0, 1];
                    T gec = _matrix[2, 0] * _matrix[1, 1] * _matrix[0, 2];
                    return aei + bfg + cdh - (hfa + idb + gec);
                }

                if (dimensions == 4)
                {
                    var s0 = _matrix[0, 0] * _matrix[1, 1] - _matrix[1, 0] * _matrix[0, 1];
                    var s1 = _matrix[0, 0] * _matrix[1, 2] - _matrix[1, 0] * _matrix[0, 2];
                    var s2 = _matrix[0, 0] * _matrix[1, 3] - _matrix[1, 0] * _matrix[0, 3];
                    var s3 = _matrix[0, 1] * _matrix[1, 2] - _matrix[1, 1] * _matrix[0, 2];
                    var s4 = _matrix[0, 1] * _matrix[1, 3] - _matrix[1, 1] * _matrix[0, 3];
                    var s5 = _matrix[0, 2] * _matrix[1, 3] - _matrix[1, 2] * _matrix[0, 3];

                    var c5 = _matrix[2, 2] * _matrix[3, 3] - _matrix[3, 2] * _matrix[2, 3];
                    var c4 = _matrix[2, 1] * _matrix[3, 3] - _matrix[3, 1] * _matrix[2, 3];
                    var c3 = _matrix[2, 1] * _matrix[3, 2] - _matrix[3, 1] * _matrix[2, 2];
                    var c2 = _matrix[2, 0] * _matrix[3, 3] - _matrix[3, 0] * _matrix[2, 3];
                    var c1 = _matrix[2, 0] * _matrix[3, 2] - _matrix[3, 0] * _matrix[2, 2];
                    var c0 = _matrix[2, 0] * _matrix[3, 1] - _matrix[3, 0] * _matrix[2, 1];

                    return s0 * c5 - s1 * c4 + s2 * c3 + s3 * c2 - s4 * c1 + s5 * c0;
                }

                T pos = T.Zero;
                for (int c = 0; c < dimensions; c++)
                {
                    int k = c;
                    T diag = T.One;
                    for (int r = 0; r < dimensions; r++, k = ((k + 1) % dimensions))
                    {
                        diag *= _matrix[r, k];
                    }
                    pos += diag;
                }

                T neg = T.Zero;
                for (int c = 0; c < dimensions; c++)
                {
                    int k = (c + 1) % dimensions;
                    T diag = T.Zero;
                    for (int r = dimensions - 1; r >= 0; r--, k = ((k + 1) % dimensions))
                    {
                        diag *= _matrix[r, k];
                    }
                    neg += diag;
                }

                return pos - neg;
            }
        }

        public T Trace
        {
            get
            {
                if (!IsSquare)
                {
                    throw new InvalidOperationException("Cannot calc trace of non-square matrix.");
                }

                T t = T.Zero;

                for (int x = 0, count = Rows; x < count; x++)
                {
                    t += _matrix[x, x];
                }

                return t;
            }
        }

        #endregion

        #region Methods

        public void Row(int row, params T[] values)
        {
            if (values.Length != Columns)
            {
                throw new ArgumentOutOfRangeException("values.Length");
            }

            for (int col = 0, cols = Columns; col < cols; col++)
            {
                _matrix[row, col] = values[col];
            }
        }

        public T[] Row(int row)
        {
            var values = new T[Columns];

            for (int col = 0, cols = Columns; col < cols; col++)
            {
                values[col] = _matrix[row, col];
            }

            return values;
        }

        public void Column(int col, params T[] values)
        {
            if (values.Length != Rows)
            {
                throw new ArgumentOutOfRangeException("values.Length");
            }

            for (int row = 0, rows = Rows; row < rows; row++)
            {
                _matrix[row, col] = values[row];
            }
        }

        public T[] Column(int col)
        {
            var values = new T[Rows];

            for (int row = 0, rows = Rows; row < rows; row++)
            {
                values[row] = _matrix[row, col];
            }

            return values;
        }

        public Matrix<T> Clone()
        {
            return new Matrix<T>(_matrix);
        }

        public Matrix<T> Transpose()
        {
            var t = new Matrix<T>(Columns, Rows);

            for (int r = 0, rows = Rows; r < rows; r++)
            {
                for (int c = 0, cols = Columns; c < cols; c++)
                {
                    t[c, r] = _matrix[r, c];
                }
            }

            return t;
        }

        public Matrix<T> Invert()
        {
            int rows = Rows;
            int cols = Columns;

            if (!IsSquare)
            {
                throw new InvalidOperationException("Unable to invert non-square matrix");
            }

            // for dimensions 3 and 4 there are optimized methods to calculate the invert matrix. 
            if (rows == 3)
            {
                return Invert3();
            }
            if (rows == 4)
            {
                return Invert4();
            }

            if (Determinant == T.Zero)
            {
                throw new InvalidOperationException("Unable to invert matrix where determinant equals 0");
            }

            Matrix<T> x = Clone();

            T e;
            for (int k = 0; k < rows; k++)
            {
                e = x[k, k];
                x[k, k] = T.One;

                for (int j = 0; j < cols; j++)
                {
                    x[k, j] = x[k, j] / e;
                }

                for (int i = 0; i < cols; i++)
                {
                    if (i != k)
                    {
                        e = x[i, k];
                        x[i, k] = T.Zero;

                        for (int j = 0; j < cols; j++)
                        {
                            x[i, j] = x[i, j] - e * x[k, j];
                        }
                    }
                }
            }

            return x;
        }

        private Matrix<T> Invert3()
        {
            var b0 = _matrix[1, 1] * _matrix[2, 2] - _matrix[1, 2] * _matrix[2, 1];
            var b1 = _matrix[1, 0] * _matrix[2, 2] - _matrix[1, 2] * _matrix[2, 0];
            var b2 = _matrix[1, 0] * _matrix[2, 1] - _matrix[1, 1] * _matrix[2, 0];

            var c0 = _matrix[0, 1] * _matrix[2, 2] - _matrix[0, 2] * _matrix[2, 1];
            var s0 = _matrix[0, 1] * _matrix[1, 2] - _matrix[0, 2] * _matrix[1, 1];
            var c1 = _matrix[0, 0] * _matrix[2, 2] - _matrix[0, 2] * _matrix[2, 0];

            var s1 = _matrix[0, 0] * _matrix[1, 2] - _matrix[0, 2] * _matrix[1, 0];
            var c2 = _matrix[0, 0] * _matrix[2, 1] - _matrix[0, 1] * _matrix[2, 0];
            var s2 = _matrix[0, 0] * _matrix[1, 1] - _matrix[0, 1] * _matrix[1, 0];

            var det = _matrix[0, 0] * b0 - _matrix[0, 1] * b1 + _matrix[0, 2] * b2;
            if (T.IsZero(det))
            {
                throw new InvalidOperationException("Unable to invert matrix where determinant equals 0");
            }

            var result = new Matrix<T>(3, 3);
            det = T.One / det;

            result[0, 0] = b0 * det;
            result[0, 1] = -c0 * det;
            result[0, 2] = s0 * det;
            result[1, 0] = -b1 * det;
            result[1, 1] = c1 * det;
            result[1, 2] = -s1 * det;
            result[2, 0] = b2 * det;
            result[2, 1] = -c2 * det;
            result[2, 2] = s2 * det;

            return result;
        }

        private Matrix<T> Invert4()
        {
            var s0 = _matrix[0, 0] * _matrix[1, 1] - _matrix[1, 0] * _matrix[0, 1];
            var s1 = _matrix[0, 0] * _matrix[1, 2] - _matrix[1, 0] * _matrix[0, 2];
            var s2 = _matrix[0, 0] * _matrix[1, 3] - _matrix[1, 0] * _matrix[0, 3];
            var s3 = _matrix[0, 1] * _matrix[1, 2] - _matrix[1, 1] * _matrix[0, 2];
            var s4 = _matrix[0, 1] * _matrix[1, 3] - _matrix[1, 1] * _matrix[0, 3];
            var s5 = _matrix[0, 2] * _matrix[1, 3] - _matrix[1, 2] * _matrix[0, 3];

            var c5 = _matrix[2, 2] * _matrix[3, 3] - _matrix[3, 2] * _matrix[2, 3];
            var c4 = _matrix[2, 1] * _matrix[3, 3] - _matrix[3, 1] * _matrix[2, 3];
            var c3 = _matrix[2, 1] * _matrix[3, 2] - _matrix[3, 1] * _matrix[2, 2];
            var c2 = _matrix[2, 0] * _matrix[3, 3] - _matrix[3, 0] * _matrix[2, 3];
            var c1 = _matrix[2, 0] * _matrix[3, 2] - _matrix[3, 0] * _matrix[2, 2];
            var c0 = _matrix[2, 0] * _matrix[3, 1] - _matrix[3, 0] * _matrix[2, 1];

            var det = s0 * c5 - s1 * c4 + s2 * c3 + s3 * c2 - s4 * c1 + s5 * c0;
            if (T.IsZero(det))
            {
                throw new InvalidOperationException("Unable to invert matrix where determinant equals 0");
            }

            var result = new Matrix<T>(4, 4);
            det = T.One / det;

            result[0, 0] = (_matrix[1, 1] * c5 - _matrix[1, 2] * c4 + _matrix[1, 3] * c3) * det;
            result[0, 1] = (-_matrix[0, 1] * c5 + _matrix[0, 2] * c4 - _matrix[0, 3] * c3) * det;
            result[0, 2] = (_matrix[3, 1] * s5 - _matrix[3, 2] * s4 + _matrix[3, 3] * s3) * det;
            result[0, 3] = (-_matrix[2, 1] * s5 + _matrix[2, 2] * s4 - _matrix[2, 3] * s3) * det;
            result[1, 0] = (-_matrix[1, 0] * c5 + _matrix[1, 2] * c2 - _matrix[1, 3] * c1) * det;
            result[1, 1] = (_matrix[0, 0] * c5 - _matrix[0, 2] * c2 + _matrix[0, 3] * c1) * det;
            result[1, 2] = (-_matrix[3, 0] * s5 + _matrix[3, 2] * s2 - _matrix[3, 3] * s1) * det;
            result[1, 3] = (_matrix[2, 0] * s5 - _matrix[2, 2] * s2 + _matrix[2, 3] * s1) * det;
            result[2, 0] = (_matrix[1, 0] * c4 - _matrix[1, 1] * c2 + _matrix[1, 3] * c0) * det;
            result[2, 1] = (-_matrix[0, 0] * c4 + _matrix[0, 1] * c2 - _matrix[0, 3] * c0) * det;
            result[2, 2] = (_matrix[3, 0] * s4 - _matrix[3, 1] * s2 + _matrix[3, 3] * s0) * det;
            result[2, 3] = (-_matrix[2, 0] * s4 + _matrix[2, 1] * s2 - _matrix[2, 3] * s0) * det;
            result[3, 0] = (-_matrix[1, 0] * c3 + _matrix[1, 1] * c1 - _matrix[1, 2] * c0) * det;
            result[3, 1] = (_matrix[0, 0] * c3 - _matrix[0, 1] * c1 + _matrix[0, 2] * c0) * det;
            result[3, 2] = (-_matrix[3, 0] * s3 + _matrix[3, 1] * s1 - _matrix[3, 2] * s0) * det;
            result[3, 3] = (_matrix[2, 0] * s3 - _matrix[2, 1] * s1 + _matrix[2, 2] * s0) * det;

            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('[');
            for (int r = 0, rows = Rows; r < rows; r++)
            {
                if (r > 0)
                {
                    sb.Append("; ");
                }

                for (int c = 0, cols = Columns; c < cols; c++)
                {
                    if (c > 0)
                    {
                        sb.Append(',');
                    }

                    sb.Append(_matrix[r, c]);
                }
            }
            sb.Append(']');
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix<T> mat && mat == this;
        }

        public override int GetHashCode()
        {
            return _matrix.GetHashCode();
        }

        #endregion

        #region Static

        public static Matrix<T> Zero(int rows, int columns)
        {
            return new Matrix<T>(rows, columns);
        }

        public static Matrix<T> One(int rows, int columns)
        {
            var m = new Matrix<T>(rows, columns);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    m[r, c] = T.One;
                }
            }

            return m;
        }

        public static Matrix<T> Identity(int dimensions)
        {
            var m = new Matrix<T>(dimensions, dimensions);

            for (int r = 0, rows = dimensions; r < rows; r++)
            {
                for (int c = 0, cols = dimensions; c < cols; c++)
                {
                    m[r, c] = (r == c) ? T.One : T.Zero;
                }
            }

            return m;
        }

        public static bool operator ==(Matrix<T> a, Matrix<T> b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
            {
                return false;
            }

            for (int r = 0, rows = a.Rows; r < rows; r++)
            {
                for (int c = 0, cols = a.Columns; c < cols; c++)
                {
                    if (a[r, c] != b[r, c])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(Matrix<T> a, Matrix<T> b)
        {
            return !(a == b);
        }

        public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
            {
                throw new ArgumentException("Unable to add matrices of different dimensions");
            }

            var x = a.Clone();

            for (int r = 0, rows = x.Rows; r < rows; r++)
            {
                for (int c = 0, cols = x.Columns; c < cols; c++)
                {
                    x[r, c] += b[r, c];
                }
            }

            return x;
        }

        public static Matrix<T> operator -(Matrix<T> a, Matrix<T> b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
            {
                throw new ArgumentException("Unable to subtract matrices of different dimensions");
            }

            var x = a.Clone();

            for (int r = 0, rows = x.Rows; r < rows; r++)
            {
                for (int c = 0, cols = x.Columns; c < cols; c++)
                {
                    x[r, c] -= b[r, c];
                }
            }

            return x;
        }

        public static Matrix<T> operator -(Matrix<T> a)
        {
            var x = a.Clone();

            for (int r = 0, rows = a.Rows; r < rows; r++)
            {
                for (int c = 0, cols = a.Columns; c < cols; c++)
                {
                    x[r, c] = -x[r, c];
                }
            }

            return x;
        }

        public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b)
        {
            if (a.Columns != b.Rows)
            {
                throw new ArgumentException("Unable to multiply matrices of different inner dimensions");
            }

            var x = new Matrix<T>(a.Rows, b.Columns);

            int inner = a.Columns;
            for (int r = 0, rows = x.Rows; r < rows; r++)
            {
                for (int c = 0, cols = x.Columns; c < cols; c++)
                {
                    T d = T.Zero;
                    for (int i = 0; i < inner; i++)
                    {
                        d += a[r, i] * b[i, c];
                    }

                    x[r, c] = d;
                }
            }

            return x;
        }

        public static T[] operator *(Matrix<T> a, T[] b)
        {
            if (a.Columns == 4)
            {
                return Multiply4(a, b);
            }

            if (a.Columns != b.Length)
            {
                throw new ArgumentException("Unable to multiply matrix and vector of different inner dimensions");
            }

            var x = new T[a.Rows];

            var inner = a.Columns;
            for (int r = 0, rows = a.Rows; r < rows; r++)
            {
                T d = T.Zero;
                for (int i = 0; i < inner; i++)
                {
                    d += a[r, i] * b[i];
                }

                x[r] = d;
            }

            return x;
        }


        private static T[] Multiply4(Matrix<T> a, T[] b)
            =>
            [
                a[0, 0]*b[0] + a[0, 1]*b[1] + a[0, 2]*b[2] + a[0, 3]*b[3],
                a[1, 0]*b[0] + a[1, 1]*b[1] + a[1, 2]*b[2] + a[1, 3]*b[3],
                a[2, 0]*b[0] + a[2, 1]*b[1] + a[2, 2]*b[2] + a[2, 3]*b[3],
                a[3, 0]*b[0] + a[3, 1]*b[1] + a[3, 2]*b[2] + a[3, 3]*b[3]
            ];


        public static Matrix<T> operator *(Matrix<T> a, T d)
        {
            Matrix<T> x = a.Clone();

            for (int r = 0, rows = x.Rows; r < rows; r++)
            {
                for (int c = 0, cols = x.Columns; c < cols; c++)
                {
                    x[r, c] *= d;
                }
            }

            return x;
        }

        public static Matrix<T> operator *(T d, Matrix<T> a)
        {
            Matrix<T> x = a.Clone();

            for (int r = 0, rows = x.Rows; r < rows; r++)
            {
                for (int c = 0, cols = x.Columns; c < cols; c++)
                {
                    x[r, c] *= d;
                }
            }

            return x;
        }

        public static Matrix<T> operator /(Matrix<T> a, T d)
        {
            return a * (T.One / d);
        }

        public static Matrix<T> operator ^(Matrix<T> a, int e)
        {
            Matrix<T> m = a.Clone();
            for (int i = 1; i < e; i++)
            {
                m *= a;
            }
            return m;
        }

        #endregion
    }
}
