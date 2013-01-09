// ReSharper disable CheckNamespace
namespace System.Drawing
// ReSharper restore CheckNamespace
{
    public class Point
    {
        #region FIELDS

        public static readonly Point Empty = new Point(0, 0);

        #endregion

        #region CONSTRUCTORS

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region PROPERTIES

        public int X { get; set; }

        public int Y { get; set; }

        #endregion
    }
}