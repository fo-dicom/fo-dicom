using System.IO;
using Windows.Storage;

// ReSharper disable CheckNamespace
namespace Override.System.IO
// ReSharper restore CheckNamespace
{
    internal static class Path
	{
		#region METHODS

		internal static string GetTempPath()
        {
            return ApplicationData.Current.TemporaryFolder.Path;
        }

        internal static string GetTempFileName()
        {
            return global::System.IO.Path.ChangeExtension(global::System.IO.Path.Combine(GetTempPath(),
                                                  global::System.IO.Path.GetRandomFileName()), File.DicomFileExtensions[0]);
        }

        internal static string GetFullPath(string path)
        {
            return path;
        }

        internal static string Combine(string path1, string path2)
        {
            return global::System.IO.Path.Combine(path1, path2);
		}

		#endregion
	}
}