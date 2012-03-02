using System;
using System.Text;

namespace Dicom.Imaging.Mathematics {
	public class Matrix {
		#region Members
		private int[,] _matrix;
		#endregion

		#region Constructors
		public Matrix(int rows, int cols) {
			_matrix = new int[rows, cols];
		}

		public Matrix(int[,] matrix) {
			_matrix = (int[,])matrix.Clone();
		}
		#endregion

		#region Properties
		public int Rows { get { return _matrix.GetLength(0); } }
		public int Columns { get { return _matrix.GetLength(1); } }
		public bool IsSquare { get { return Rows == Columns; } }

		public bool IsIdentity {
			get {
				if (!IsSquare)
					return false;

				for (int r = 0, rows = Rows; r < rows; r++) {
					for (int c = 0, cols = Columns; c < cols; c++) {
						if (_matrix[r, c] != ((r == c) ? 1.0 : 0.0))
							return false;
					}
				}

				return true;
			}
		}

		public int this[int row, int col] {
			get { return _matrix[row, col]; }
			set { _matrix[row, col] = value; }
		}

		public int Determinant {
			get {
				if (!IsSquare)
					throw new InvalidOperationException("Cannot calculate determinant of non-square matrix.");

				int dimensions = Rows;

				if (dimensions == 1)
					return _matrix[0, 0];

				if (dimensions == 2) {
					return (_matrix[0, 0] * _matrix[1, 1]) - (_matrix[0, 1] * _matrix[1, 0]);
				}

				if (dimensions == 3) {
					int aei = _matrix[0, 0] * _matrix[1, 1] * _matrix[2, 2];
					int bfg = _matrix[0, 1] * _matrix[1, 2] * _matrix[2, 0];
					int cdh = _matrix[0, 2] * _matrix[1, 0] * _matrix[2, 1];
					int hfa = _matrix[2, 1] * _matrix[1, 2] * _matrix[0, 0];
					int idb = _matrix[2, 2] * _matrix[1, 0] * _matrix[0, 1];
					int gec = _matrix[2, 0] * _matrix[1, 1] * _matrix[0, 2];
					return (aei + bfg + cdh) - (hfa + idb + gec);
				}

				int pos = 0;
				for (int c = 0; c < dimensions; c++) {
					int k = c;
					int diag = 0;
					for (int r = 0; r < dimensions; r++, k = ((k + 1) % dimensions)) {
						diag *= _matrix[r, k];
					}
					pos += diag;
				}

				int neg = 0;
				for (int c = 0; c < dimensions; c++) {
					int k = (c + 1) % dimensions;
					int diag = 0;
					for (int r = dimensions - 1; r >= 0; r--, k = ((k + 1) % dimensions)) {
						diag *= _matrix[r, k];
					}
					neg += diag;
				}

				return pos - neg;
			}
		}

		public int Trace {
			get {
				if (!IsSquare)
					throw new InvalidOperationException("Cannot calc trace of non-square matrix.");

				int t = 0;

				for (int x = 0, count = Rows; x < count; x++) {
					t += _matrix[x, x];
				}

				return t;
			}
		}
		#endregion

		#region Methods
		public void Row(int row, params int[] values) {
			if (values.Length != Columns)
				throw new ArgumentOutOfRangeException("values.Length");

			for (int col = 0, cols = Columns; col < cols; col++) {
				_matrix[row, col] = values[col];
			}
		}

		public int[] Row(int row) {
			int[] values = new int[Columns];

			for (int col = 0, cols = Columns; col < cols; col++) {
				values[col] = _matrix[row, col];
			}

			return values;
		}

		public void Column(int col, params int[] values) {
			if (values.Length != Rows)
				throw new ArgumentOutOfRangeException("values.Length");

			for (int row = 0, rows = Rows; row < rows; row++) {
				_matrix[row, col] = values[row];
			}
		}

		public int[] Column(int col) {
			int[] values = new int[Rows];

			for (int row = 0, rows = Rows; row < rows; row++) {
				values[row] = _matrix[row, col];
			}

			return values;
		}

		public Matrix Clone() {
			return new Matrix(_matrix);
		}

		public Matrix Transpose() {
			Matrix t = new Matrix(Columns, Rows);

			for (int r = 0, rows = Rows; r < rows; r++) {
				for (int c = 0, cols = Columns; c < cols; c++) {
					t[c, r] = _matrix[r, c];
				}
			}

			return t;
		}

		public Matrix Invert() {
			int rows = Rows;
			int cols = Columns;

			if (rows != cols)
				throw new InvalidOperationException("Unable to invert non-square matrix");

			if (Determinant == 0.0)
				throw new InvalidOperationException("Unable to invert matrix where determinant equals 0");

			Matrix x = Clone();

			int e;
			for (int k = 0; k < rows; k++) {
				e = x[k, k];
				x[k, k] = 1;

				for (int j = 0; j < cols; j++) {
					x[k, j] = x[k, j] / e;
				}

				for (int i = 0; i < cols; i++) {
					if (i != k) {
						e = x[i, k];
						x[i, k] = 0;

						for (int j = 0; j < cols; j++) {
							x[i, j] = x[i, j] - e * x[k, j];
						}
					}
				}
			}

			return x;
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.Append("[");
			for (int r = 0, rows = Rows; r < rows; r++) {
				if (r > 0)
					sb.Append("; ");
				for (int c = 0, cols = Columns; c < cols; c++) {
					if (c > 0)
						sb.Append(",");
					sb.Append(_matrix[r, c]);
				}
			}
			sb.Append("]");
			return sb.ToString();
		}

		public override bool Equals(object obj) {
			if (obj is Matrix)
				return (obj as Matrix) == this;
			return false;
		}

		public override int GetHashCode() {
			return _matrix.GetHashCode();
		}
		#endregion

		#region Static
		public static Matrix Zero(int rows, int columns) {
			return new Matrix(rows, columns);
		}

		public static Matrix One(int rows, int columns) {
			Matrix m = new Matrix(rows, columns);

			for (int r = 0; r < rows; r++) {
				for (int c = 0; c < columns; c++) {
					m[r, c] = 1;
				}
			}

			return m;
		}

		public static Matrix Identity(int dimensions) {
			Matrix m = new Matrix(dimensions, dimensions);

			for (int r = 0, rows = dimensions; r < rows; r++) {
				for (int c = 0, cols = dimensions; c < cols; c++) {
					m[r, c] = (r == c) ? 1 : 0;
				}
			}

			return m;
		}

		public static bool operator ==(Matrix a, Matrix b) {
			if (a.Rows != b.Rows || a.Columns != b.Columns)
				return false;

			for (int r = 0, rows = a.Rows; r < rows; r++) {
				for (int c = 0, cols = a.Columns; c < cols; c++) {
					if (a[r, c] != b[r, c])
						return false;
				}
			}

			return true;
		}

		public static bool operator !=(Matrix a, Matrix b) {
			return !(a == b);
		}

		public static Matrix operator +(Matrix a, Matrix b) {
			if (a.Rows != b.Rows || a.Columns != b.Columns)
				throw new ArgumentException("Unable to add matrices of different dimensions");

			Matrix x = a.Clone();

			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					x[r, c] += b[r, c];
				}
			}

			return x;
		}

		public static Matrix operator -(Matrix a, Matrix b) {
			if (a.Rows != b.Rows || a.Columns != b.Columns)
				throw new ArgumentException("Unable to subtract matrices of different dimensions");

			Matrix x = a.Clone();

			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					x[r, c] -= b[r, c];
				}
			}

			return x;
		}

		public static Matrix operator -(Matrix a) {
			Matrix x = a.Clone();

			for (int r = 0, rows = a.Rows; r < rows; r++) {
				for (int c = 0, cols = a.Columns; c < cols; c++) {
					x[r, c] = -x[r, c];
				}
			}

			return x;
		}

		public static Matrix operator *(Matrix a, Matrix b) {
			if (a.Columns != b.Rows)
				throw new ArgumentException("Unable to multiply matrices of different inner dimensions");

			Matrix x = new Matrix(a.Rows, b.Columns);

			int inner = a.Columns;
			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					int d = 0;
					for (int i = 0; i < inner; i++)
						d += a[r, i] * b[i, c];
					x[r, c] = d;
				}
			}

			return x;
		}

		public static Matrix operator *(Matrix a, int d) {
			Matrix x = a.Clone();

			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					x[r, c] *= d;
				}
			}

			return x;
		}

		public static Matrix operator *(int d, Matrix a) {
			Matrix x = a.Clone();

			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					x[r, c] *= d;
				}
			}

			return x;
		}

		public static Matrix operator /(Matrix a, int d) {
			return a * (1 / d);
		}

		public static Matrix operator ^(Matrix a, int e) {
			Matrix m = a.Clone();
			for (int i = 1; i < e; i++) {
				m *= a;
			}
			return m;
		}
		#endregion
	}

	public class MatrixF {
		#region Members
		private float[,] _matrix;
		#endregion

		#region Constructors
		public MatrixF(int rows, int cols) {
			_matrix = new float[rows, cols];
		}

		public MatrixF(float[,] matrix) {
			_matrix = (float[,])matrix.Clone();
		}
		#endregion

		#region Properties
		public int Rows { get { return _matrix.GetLength(0); } }
		public int Columns { get { return _matrix.GetLength(1); } }
		public bool IsSquare { get { return Rows == Columns; } }

		public bool IsIdentity {
			get {
				if (!IsSquare)
					return false;

				for (int r = 0, rows = Rows; r < rows; r++) {
					for (int c = 0, cols = Columns; c < cols; c++) {
						if (_matrix[r, c] != ((r == c) ? 1.0 : 0.0))
							return false;
					}
				}

				return true;
			}
		}

		public float this[int row, int col] {
			get { return _matrix[row, col]; }
			set { _matrix[row, col] = value; }
		}

		public float Determinant {
			get {
				if (!IsSquare)
					throw new InvalidOperationException("Cannot calculate determinant of non-square matrix.");

				int dimensions = Rows;

				if (dimensions == 1)
					return _matrix[0, 0];

				if (dimensions == 2) {
					return (_matrix[0, 0] * _matrix[1, 1]) - (_matrix[0, 1] * _matrix[1, 0]);
				}

				if (dimensions == 3) {
					float aei = _matrix[0, 0] * _matrix[1, 1] * _matrix[2, 2];
					float bfg = _matrix[0, 1] * _matrix[1, 2] * _matrix[2, 0];
					float cdh = _matrix[0, 2] * _matrix[1, 0] * _matrix[2, 1];
					float hfa = _matrix[2, 1] * _matrix[1, 2] * _matrix[0, 0];
					float idb = _matrix[2, 2] * _matrix[1, 0] * _matrix[0, 1];
					float gec = _matrix[2, 0] * _matrix[1, 1] * _matrix[0, 2];
					return (aei + bfg + cdh) - (hfa + idb + gec);
				}

				float pos = 0.0f;
				for (int c = 0; c < dimensions; c++) {
					int k = c;
					float diag = 0.0f;
					for (int r = 0; r < dimensions; r++, k = ((k + 1) % dimensions)) {
						diag *= _matrix[r, k];
					}
					pos += diag;
				}

				float neg = 0.0f;
				for (int c = 0; c < dimensions; c++) {
					int k = (c + 1) % dimensions;
					float diag = 0.0f;
					for (int r = dimensions - 1; r >= 0; r--, k = ((k + 1) % dimensions)) {
						diag *= _matrix[r, k];
					}
					neg += diag;
				}

				return pos - neg;
			}
		}

		public float Trace {
			get {
				if (!IsSquare)
					throw new InvalidOperationException("Cannot calc trace of non-square matrix.");

				float t = 0.0f;

				for (int x = 0, count = Rows; x < count; x++) {
					t += _matrix[x, x];
				}

				return t;
			}
		}
		#endregion

		#region Methods
		public void Row(int row, params float[] values) {
			if (values.Length != Columns)
				throw new ArgumentOutOfRangeException("values.Length");

			for (int col = 0, cols = Columns; col < cols; col++) {
				_matrix[row, col] = values[col];
			}
		}

		public float[] Row(int row) {
			float[] values = new float[Columns];

			for (int col = 0, cols = Columns; col < cols; col++) {
				values[col] = _matrix[row, col];
			}

			return values;
		}

		public void Column(int col, params float[] values) {
			if (values.Length != Rows)
				throw new ArgumentOutOfRangeException("values.Length");

			for (int row = 0, rows = Rows; row < rows; row++) {
				_matrix[row, col] = values[row];
			}
		}

		public float[] Column(int col) {
			float[] values = new float[Rows];

			for (int row = 0, rows = Rows; row < rows; row++) {
				values[row] = _matrix[row, col];
			}

			return values;
		}

		public MatrixF Clone() {
			return new MatrixF(_matrix);
		}

		public MatrixF Transpose() {
			MatrixF t = new MatrixF(Columns, Rows);

			for (int r = 0, rows = Rows; r < rows; r++) {
				for (int c = 0, cols = Columns; c < cols; c++) {
					t[c, r] = _matrix[r, c];
				}
			}

			return t;
		}

		public MatrixF Invert() {
			int rows = Rows;
			int cols = Columns;

			if (rows != cols)
				throw new InvalidOperationException("Unable to invert non-square matrix");

			if (Determinant == 0.0f)
				throw new InvalidOperationException("Unable to invert matrix where determinant equals 0");

			MatrixF x = Clone();

			float e;
			for (int k = 0; k < rows; k++) {
				e = x[k, k];
				x[k, k] = 1.0f;

				for (int j = 0; j < cols; j++) {
					x[k, j] = x[k, j] / e;
				}

				for (int i = 0; i < cols; i++) {
					if (i != k) {
						e = x[i, k];
						x[i, k] = 0.0f;

						for (int j = 0; j < cols; j++) {
							x[i, j] = x[i, j] - e * x[k, j];
						}
					}
				}
			}

			return x;
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.Append("[");
			for (int r = 0, rows = Rows; r < rows; r++) {
				if (r > 0)
					sb.Append("; ");
				for (int c = 0, cols = Columns; c < cols; c++) {
					if (c > 0)
						sb.Append(",");
					sb.Append(_matrix[r, c]);
				}
			}
			sb.Append("]");
			return sb.ToString();
		}

		public override bool Equals(object obj) {
			if (obj is MatrixF)
				return (obj as MatrixF) == this;
			return false;
		}

		public override int GetHashCode() {
			return _matrix.GetHashCode();
		}
		#endregion

		#region Static
		public static MatrixF Zero(int rows, int columns) {
			return new MatrixF(rows, columns);
		}

		public static MatrixF One(int rows, int columns) {
			MatrixF m = new MatrixF(rows, columns);

			for (int r = 0; r < rows; r++) {
				for (int c = 0; c < columns; c++) {
					m[r, c] = 1.0f;
				}
			}

			return m;
		}

		public static MatrixF Identity(int dimensions) {
			MatrixF m = new MatrixF(dimensions, dimensions);

			for (int r = 0, rows = dimensions; r < rows; r++) {
				for (int c = 0, cols = dimensions; c < cols; c++) {
					m[r, c] = (r == c) ? 1.0f : 0.0f;
				}
			}

			return m;
		}

		public static bool operator ==(MatrixF a, MatrixF b) {
			if (a.Rows != b.Rows || a.Columns != b.Columns)
				return false;

			for (int r = 0, rows = a.Rows; r < rows; r++) {
				for (int c = 0, cols = a.Columns; c < cols; c++) {
					if (a[r, c] != b[r, c])
						return false;
				}
			}

			return true;
		}

		public static bool operator !=(MatrixF a, MatrixF b) {
			return !(a == b);
		}

		public static MatrixF operator +(MatrixF a, MatrixF b) {
			if (a.Rows != b.Rows || a.Columns != b.Columns)
				throw new ArgumentException("Unable to add matrices of different dimensions");

			MatrixF x = a.Clone();

			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					x[r, c] += b[r, c];
				}
			}

			return x;
		}

		public static MatrixF operator -(MatrixF a, MatrixF b) {
			if (a.Rows != b.Rows || a.Columns != b.Columns)
				throw new ArgumentException("Unable to subtract matrices of different dimensions");

			MatrixF x = a.Clone();

			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					x[r, c] -= b[r, c];
				}
			}

			return x;
		}

		public static MatrixF operator -(MatrixF a) {
			MatrixF x = a.Clone();

			for (int r = 0, rows = a.Rows; r < rows; r++) {
				for (int c = 0, cols = a.Columns; c < cols; c++) {
					x[r, c] = -x[r, c];
				}
			}

			return x;
		}

		public static MatrixF operator *(MatrixF a, MatrixF b) {
			if (a.Columns != b.Rows)
				throw new ArgumentException("Unable to multiply matrices of different inner dimensions");

			MatrixF x = new MatrixF(a.Rows, b.Columns);

			int inner = a.Columns;
			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					float d = 0.0f;
					for (int i = 0; i < inner; i++)
						d += a[r, i] * b[i, c];
					x[r, c] = d;
				}
			}

			return x;
		}

		public static MatrixF operator *(MatrixF a, float d) {
			MatrixF x = a.Clone();

			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					x[r, c] *= d;
				}
			}

			return x;
		}

		public static MatrixF operator *(float d, MatrixF a) {
			MatrixF x = a.Clone();

			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					x[r, c] *= d;
				}
			}

			return x;
		}

		public static MatrixF operator /(MatrixF a, float d) {
			return a * (1 / d);
		}

		public static MatrixF operator ^(MatrixF a, int e) {
			MatrixF m = a.Clone();
			for (int i = 1; i < e; i++) {
				m *= a;
			}
			return m;
		}
		#endregion
	}

	public class MatrixD {
		#region Members
		private double[,] _matrix;
		#endregion

		#region Constructors
		public MatrixD(int rows, int cols) {
			_matrix = new double[rows, cols];
		}

		public MatrixD(double[,] matrix) {
			_matrix = (double[,])matrix.Clone();
		}
		#endregion

		#region Properties
		public int Rows { get { return _matrix.GetLength(0); } }
		public int Columns { get { return _matrix.GetLength(1); } }
		public bool IsSquare { get { return Rows == Columns; } }

		public bool IsIdentity {
			get {
				if (!IsSquare)
					return false;

				for (int r = 0, rows = Rows; r < rows; r++) {
					for (int c = 0, cols = Columns; c < cols; c++) {
						if (_matrix[r, c] != ((r == c) ? 1.0 : 0.0))
							return false;
					}
				}

				return true;
			}
		}

		public double this[int row, int col] {
			get { return _matrix[row, col]; }
			set { _matrix[row, col] = value; }
		}

		public double Determinant {
			get {
				if (!IsSquare)
					throw new InvalidOperationException("Cannot calculate determinant of non-square matrix.");

				int dimensions = Rows;

				if (dimensions == 1)
					return _matrix[0, 0];

				if (dimensions == 2) {
					return (_matrix[0, 0] * _matrix[1, 1]) - (_matrix[0, 1] * _matrix[1, 0]);
				}

				if (dimensions == 3) {
					double aei = _matrix[0, 0] * _matrix[1, 1] * _matrix[2, 2];
					double bfg = _matrix[0, 1] * _matrix[1, 2] * _matrix[2, 0];
					double cdh = _matrix[0, 2] * _matrix[1, 0] * _matrix[2, 1];
					double hfa = _matrix[2, 1] * _matrix[1, 2] * _matrix[0, 0];
					double idb = _matrix[2, 2] * _matrix[1, 0] * _matrix[0, 1];
					double gec = _matrix[2, 0] * _matrix[1, 1] * _matrix[0, 2];
					return (aei + bfg + cdh) - (hfa + idb + gec);
				}

				double pos = 0.0;
				for (int c = 0; c < dimensions; c++) {
					int k = c;
					double diag = 0.0;
					for (int r = 0; r < dimensions; r++, k = ((k + 1) % dimensions)) {
						diag *= _matrix[r, k];
					}
					pos += diag;
				}

				double neg = 0.0;
				for (int c = 0; c < dimensions; c++) {
					int k = (c + 1) % dimensions;
					double diag = 0.0;
					for (int r = dimensions - 1; r >= 0; r--, k = ((k + 1) % dimensions)) {
						diag *= _matrix[r, k];
					}
					neg += diag;
				}

				return pos - neg;
			}
		}

		public double Trace {
			get {
				if (!IsSquare)
					throw new InvalidOperationException("Cannot calc trace of non-square matrix.");

				double t = 0.0;

				for (int x = 0, count = Rows; x < count; x++) {
					t += _matrix[x, x];
				}

				return t;
			}
		}
		#endregion

		#region Methods
		public void Row(int row, params double[] values) {
			if (values.Length != Columns)
				throw new ArgumentOutOfRangeException("values.Length");

			for (int col = 0, cols = Columns; col < cols; col++) {
				_matrix[row, col] = values[col];
			}
		}

		public double[] Row(int row) {
			double[] values = new double[Columns];

			for (int col = 0, cols = Columns; col < cols; col++) {
				values[col] = _matrix[row, col];
			}

			return values;
		}

		public void Column(int col, params double[] values) {
			if (values.Length != Rows)
				throw new ArgumentOutOfRangeException("values.Length");

			for (int row = 0, rows = Rows; row < rows; row++) {
				_matrix[row, col] = values[row];
			}
		}

		public double[] Column(int col) {
			double[] values = new double[Rows];

			for (int row = 0, rows = Rows; row < rows; row++) {
				values[row] = _matrix[row, col];
			}

			return values;
		}

		public MatrixD Clone() {
			return new MatrixD(_matrix);
		}

		public MatrixD Transpose() {
			MatrixD t = new MatrixD(Columns, Rows);

			for (int r = 0, rows = Rows; r < rows; r++) {
				for (int c = 0, cols = Columns; c < cols; c++) {
					t[c, r] = _matrix[r, c];
				}
			}

			return t;
		}

		public MatrixD Invert() {
			int rows = Rows;
			int cols = Columns;

			if (rows != cols)
				throw new InvalidOperationException("Unable to invert non-square matrix");

			if (Determinant == 0.0)
				throw new InvalidOperationException("Unable to invert matrix where determinant equals 0");

			MatrixD x = Clone();

			double e;
			for (int k = 0; k < rows; k++) {
				e = x[k, k];
				x[k, k] = 1.0;

				for (int j = 0; j < cols; j++) {
					x[k, j] = x[k, j] / e;
				}

				for (int i = 0; i < cols; i++) {
					if (i != k) {
						e = x[i, k];
						x[i, k] = 0.0;

						for (int j = 0; j < cols; j++) {
							x[i, j] = x[i, j] - e * x[k, j];
						}
					}
				}
			}

			return x;
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.Append("[");
			for (int r = 0, rows = Rows; r < rows; r++) {
				if (r > 0)
					sb.Append("; ");
				for (int c = 0, cols = Columns; c < cols; c++) {
					if (c > 0)
						sb.Append(",");
					sb.Append(_matrix[r, c]);
				}
			}
			sb.Append("]");
			return sb.ToString();
		}

		public override bool Equals(object obj) {
			if (obj is MatrixD)
				return (obj as MatrixD) == this;
			return false;
		}

		public override int GetHashCode() {
			return _matrix.GetHashCode();
		}
		#endregion

		#region Static
		public static MatrixD Zero(int rows, int columns) {
			return new MatrixD(rows, columns);
		}

		public static MatrixD One(int rows, int columns) {
			MatrixD m = new MatrixD(rows, columns);

			for (int r = 0; r < rows; r++) {
				for (int c = 0; c < columns; c++) {
					m[r, c] = 1.0;
				}
			}

			return m;
		}

		public static MatrixD Identity(int dimensions) {
			MatrixD m = new MatrixD(dimensions, dimensions);

			for (int r = 0, rows = dimensions; r < rows; r++) {
				for (int c = 0, cols = dimensions; c < cols; c++) {
					m[r, c] = (r == c) ? 1.0 : 0.0;
				}
			}

			return m;
		}

		public static bool operator ==(MatrixD a, MatrixD b) {
			if (a.Rows != b.Rows || a.Columns != b.Columns)
				return false;

			for (int r = 0, rows = a.Rows; r < rows; r++) {
				for (int c = 0, cols = a.Columns; c < cols; c++) {
					if (a[r, c] != b[r, c])
						return false;
				}
			}

			return true;
		}

		public static bool operator !=(MatrixD a, MatrixD b) {
			return !(a == b);
		}

		public static MatrixD operator +(MatrixD a, MatrixD b) {
			if (a.Rows != b.Rows || a.Columns != b.Columns)
				throw new ArgumentException("Unable to add matrices of different dimensions");

			MatrixD x = a.Clone();

			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					x[r, c] += b[r, c];
				}
			}

			return x;
		}

		public static MatrixD operator -(MatrixD a, MatrixD b) {
			if (a.Rows != b.Rows || a.Columns != b.Columns)
				throw new ArgumentException("Unable to subtract matrices of different dimensions");

			MatrixD x = a.Clone();

			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					x[r, c] -= b[r, c];
				}
			}

			return x;
		}

		public static MatrixD operator -(MatrixD a) {
			MatrixD x = a.Clone();

			for (int r = 0, rows = a.Rows; r < rows; r++) {
				for (int c = 0, cols = a.Columns; c < cols; c++) {
					x[r, c] = -x[r, c];
				}
			}

			return x;
		}

		public static MatrixD operator *(MatrixD a, MatrixD b) {
			if (a.Columns != b.Rows)
				throw new ArgumentException("Unable to multiply matrices of different inner dimensions");

			MatrixD x = new MatrixD(a.Rows, b.Columns);

			int inner = a.Columns;
			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					double d = 0.0;
					for (int i = 0; i < inner; i++)
						d += a[r, i] * b[i, c];
					x[r, c] = d;
				}
			}

			return x;
		}

		public static MatrixD operator *(MatrixD a, double d) {
			MatrixD x = a.Clone();

			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					x[r, c] *= d;
				}
			}

			return x;
		}

		public static MatrixD operator *(double d, MatrixD a) {
			MatrixD x = a.Clone();

			for (int r = 0, rows = x.Rows; r < rows; r++) {
				for (int c = 0, cols = x.Columns; c < cols; c++) {
					x[r, c] *= d;
				}
			}

			return x;
		}

		public static MatrixD operator /(MatrixD a, double d) {
			return a * (1 / d);
		}

		public static MatrixD operator ^(MatrixD a, int e) {
			MatrixD m = a.Clone();
			for (int i = 1; i < e; i++) {
				m *= a;
			}
			return m;
		}
		#endregion
	}
}
