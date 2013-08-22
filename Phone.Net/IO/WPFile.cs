// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

namespace System.IO
{
// ReSharper disable InconsistentNaming
	public static class WPFile
// ReSharper restore InconsistentNaming
	{
		public static byte[] ReadAllBytes(string path)
		{
			using (var stream = new FileStream(path, FileMode.Open))
			{
				var bytes = new byte[stream.Length];
				stream.Read(bytes, 0, (int)stream.Length);
				return bytes;
			}
		}

		public static void WriteAllBytes(string path, byte[] bytes)
		{
			using (var stream = new FileStream(path, FileMode.Create))
			{
				stream.Write(bytes, 0, bytes.Length);
			}
		}

		public static FileAttributes GetAttributes(string path)
		{
			return new FileInfo(path).Attributes;
		}
	}
}