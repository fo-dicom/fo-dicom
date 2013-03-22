// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

namespace System.IO
{
	public class DirectoryInfo
	{
		#region FIELDS

		private readonly string _path;

		#endregion

		#region CONSTRUCTORS

		public DirectoryInfo(string path)
		{
			_path = path;
		}

		#endregion

		#region METHODS

		public bool Exists
		{
			get { return Directory.Exists(_path); }
		}

		public void Create()
		{
			Directory.CreateDirectory(_path);
		}
 
		#endregion
	}
}