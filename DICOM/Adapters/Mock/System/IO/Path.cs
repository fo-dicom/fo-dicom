using System;
using Windows.Storage;

// ReSharper disable CheckNamespace
namespace Mock.System.IO
// ReSharper restore CheckNamespace
{
    public static class Path
    {
        public static string GetTempPath()
        {
            return ApplicationData.Current.LocalFolder.Name;
        }

        public static string GetTempFileName()
        {
            return global::System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Name,
                                                  global::System.IO.Path.GetRandomFileName());
        }

        public static string GetFullPath(string path)
        {
            return path;
        }

        public static string Combine(string path1, string path2)
        {
            return global::System.IO.Path.Combine(path1, path2);
        }
    }
}