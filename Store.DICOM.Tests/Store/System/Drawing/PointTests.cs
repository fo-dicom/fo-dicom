using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

// ReSharper disable CheckNamespace
namespace System.Drawing
// ReSharper restore CheckNamespace
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void EqualityOperator_ComparingEqualObjects_YieldsTrue()
        {
            var pnt1 = new Point(3, 5);
            var pnt2 = new Point { X = 3, Y = 5 };
            Assert.IsTrue(pnt1 == pnt2);
        }

        [TestMethod]
        public void EqualityOperator_ComparingNonEqualObjects_YieldsFalse()
        {
            var pnt1 = new Point(3, 4);
            var pnt2 = new Point { X = 3, Y = 5 };
            Assert.IsFalse(pnt1 == pnt2);
        }
    }
}