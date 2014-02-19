#if !NETFX_CORE && !WINDOWS_PHONE && !TOUCH
using System.Drawing;
#endif

namespace Dicom.Imaging {
	public class SpatialTransform {
		#region Private Members
		private double _scale;
		private int _rotate;
		private bool _flipx;
		private bool _flipy;
#if NETFX_CORE || WINDOWS_PHONE || TOUCH
	    private int _panX;
	    private int _panY;
#else
		private Point _pan;
#endif
		#endregion

		#region Public Constructors
		public SpatialTransform() {
#if !NETFX_CORE && !WINDOWS_PHONE && !TOUCH
			_pan = new Point(0, 0);
#endif
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

#if NETFX_CORE || WINDOWS_PHONE || TOUCH
        public int PanX
        {
            get { return _panX; }
            set { _panX = value; }
        }

        public int PanY
        {
            get { return _panY; }
            set { _panY = value; }
        }
#else
		public Point Pan {
			get { return _pan; }
			set { _pan = value; }
		}
#endif

		public bool IsTransformed {
			get {
				return _scale != 1.0f ||
					_rotate != 0.0f ||
#if NETFX_CORE || WINDOWS_PHONE || TOUCH
                    _panX != 0 || _panY != 0;
#else
					_pan != Point.Empty;
#endif
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
#if NETFX_CORE || WINDOWS_PHONE || TOUCH
            _panX = 0;
            _panY = 0;
#else
			_pan.X = 0;
			_pan.Y = 0;
#endif
		}
		#endregion
	}
}
