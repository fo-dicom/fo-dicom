#if !NETFX_CORE && !WINDOWS_PHONE
using System.Drawing;
#endif

namespace Dicom.Imaging {
	public class SpatialTransform {

#if NETFX_CORE || WINDOWS_PHONE
        #region INNER TYPES

        public struct Point
        {
            private int _x;
            private int _y;

            public Point(int x, int y)
            {
                _x = x;
                _y = y;
            }

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
        }

        #endregion
#endif

        #region Private Members
        private double _scale;
		private int _rotate;
		private bool _flipx;
		private bool _flipy;
		private Point _pan;
		#endregion

		#region Public Constructors
		public SpatialTransform() {
			_pan = new Point(0, 0);
			Reset();
		}
		#endregion

		#region Public Properties
		public double Scale {
			get { return _scale; }
			set { _scale = value; }
		}

		public int Rotation {
			get { return _rotate; }
			set { _rotate = value; }
		}

		public bool FlipX {
			get { return _flipx; }
			set { _flipx = value; }
		}

		public bool FlipY {
			get { return _flipy; }
			set { _flipy = value; }
		}

		public Point Pan {
			get { return _pan; }
			set { _pan = value; }
		}

		public bool IsTransformed {
			get {
				return _scale != 1.0f ||
					_rotate != 0.0f ||
					_pan.X != 0 || 
                    _pan.Y != 0;
			}
		}
		#endregion

		#region Public Members
		public void Rotate(int angle) {
			_rotate += angle;
		}

		public void Reset() {
			_scale = 1.0;
			_rotate = 0;
			_flipx = false;
			_flipy = false;
			_pan.X = 0;
			_pan.Y = 0;
		}
		#endregion
	}
}
