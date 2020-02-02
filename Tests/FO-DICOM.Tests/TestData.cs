using System.IO;

namespace FellowOakDicom.Tests
{
    internal static class TestData
    {
        private static DirectoryInfo TestDataDirectory => new DirectoryInfo(Path.Combine(".", "Test Data"));

        public static string Resolve(string path) => Path.Combine(TestDataDirectory.FullName, path);
    }
}
