using System;

// ReSharper disable CheckNamespace
namespace Mock.System.IO
// ReSharper restore CheckNamespace
{
    public static class Path
    {
        public static string GetTempPath()
        {
            return String.Empty;
        }

        public static string GetTempFileName()
        {
            return String.Empty;
        }

        public static string GetFullPath(string path)
        {
            return String.Empty;
        }

        public static string Combine(string path1, string path2)
        {
            return global::System.IO.Path.Combine(path1, path2);
        }
    }
}