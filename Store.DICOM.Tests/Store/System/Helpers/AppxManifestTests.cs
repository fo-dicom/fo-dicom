// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using System.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace System.Helpers
{
	[TestClass]
	public class AppxManifestTests
	{
		[TestMethod]
		public void SupportedFileExtensions_Getter_ShouldBeAtLeastThree()
		{
			Assert.IsTrue(AppxManifest.SupportedFileExtensions.Count() >= 3);
		}
	}
}