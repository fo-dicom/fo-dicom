using System.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

// ReSharper disable CheckNamespace
namespace System.Collections.Generic
// ReSharper restore CheckNamespace
{
    [TestClass]
    public class SortedListTests
    {
        [TestMethod]
        public void SortedListEnumeration_LinqSelect_ReturnsCorrectlySortedList()
        {
            var sortedList = new SortedList<int, string> { { 3, "three" }, { 1, "one" }, { 4, "four" }, { 2, "two" } };

            var expected = new[] { "one", "two", "three", "four" };
            var actual = sortedList.Select(kv => kv.Value).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SortedListEnumeration_Foreach_ReturnsCorrectlySortedList()
        {
            var sortedList = new SortedList<int, string> { { 3, "three" }, { 1, "one" }, { 4, "four" }, { 2, "two" } };

            var expected = new[] { "one", "two", "three", "four" };
            var actual = new List<string>();
            foreach (var kv in sortedList) actual.Add(kv.Value);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Values_Accessor_CorrectlySorted()
        {
            var sortedList = new SortedList<int, string> { { 3, "three" }, { 1, "one" }, { 4, "four" }, { 2, "two" } };

            var expected = new[] { "one", "two", "three", "four" };
            var actual = sortedList.Values;
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}