// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using System.Helpers;
using Windows.Storage;

// ReSharper disable CheckNamespace
namespace Override.System.IO
// ReSharper restore CheckNamespace
{
    public static class Path
	{
		#region FIELDS

	    private static readonly string DefaultFileExtension = AppxManifest.SupportedFileExtensions.Length > 0
		                                                          ? AppxManifest.SupportedFileExtensions[0]
		                                                          : ".txt";

		#endregion

		#region METHODS

		public static string GetTempPath()
        {
            return ApplicationData.Current.TemporaryFolder.Path;
        }

        public static string GetTempFileName()
        {
            return global::System.IO.Path.ChangeExtension(global::System.IO.Path.Combine(GetTempPath(),
												  global::System.IO.Path.GetRandomFileName()), DefaultFileExtension);
        }

        public static string GetFullPath(string path)
        {
            return path;
        }

        public static string Combine(params string[] paths)
        {
            return global::System.IO.Path.Combine(paths);
		}

		#endregion
	}
}