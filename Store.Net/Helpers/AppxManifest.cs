// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace System.Helpers
{
	public static class AppxManifest
	{
		#region CONSTRUCTORS

		static AppxManifest()
		{
			try
			{
				var document = XDocument.Load("AppxManifest.xml");
				var xname = XNamespace.Get("http://schemas.microsoft.com/appx/2010/manifest");
				var fileTypeElements = document.Descendants(xname + "FileTypeAssociation").Descendants().Descendants();

				SupportedFileExtensions = fileTypeElements.Select(element => element.Value).ToArray();
			}
			catch (Exception)
			{
				SupportedFileExtensions = new string[0];
			}
		}

		#endregion

		#region PROPERTIES

		public static string[] SupportedFileExtensions { get; private set; }

		#endregion
	}
}