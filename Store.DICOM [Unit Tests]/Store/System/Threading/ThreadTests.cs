using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

// ReSharper disable CheckNamespace
namespace System.Threading
// ReSharper restore CheckNamespace
{
    [TestClass]
    public class ThreadTests
    {
        private int _value;

        [TestInitialize]
        public void Init()
        {
            _value = 0;
        }

        [TestMethod]
        public void Start_ParameterlessAction_SuccessfulUponCompletion()
        {
            var thread = new Thread(() => _value = 1);
            thread.Start();
            Thread.Sleep(100);

            var expected = 1;
            var actual = _value;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Start_ParameterizedAction_SuccessfulUponCompletion()
        {
            var thread = new Thread(value => _value = (int)value);
            thread.Start(25);
            Thread.Sleep(100);

            var expected = 25;
            var actual = _value;
            Assert.AreEqual(expected, actual);
        }
    }
}