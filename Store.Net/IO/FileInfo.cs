// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

namespace System.IO
{
	public class FileInfo
	{
		#region FIELDS

		private readonly string _name;
		
		#endregion

		#region CONSTRUCTORS

		public FileInfo(string name)
		{
			_name = name;
		}
		
		#endregion

		#region PROPERTIES

		public DirectoryInfo Directory
		{
			get { return new DirectoryInfo(Path.GetDirectoryName(_name)); }
		}

		#endregion

		#region METHODS

		public FileStream OpenWrite()
		{
			return new FileStream(_name, FileMode.Create);
		}
		
		#endregion
	}
}