// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	public class X509Certificate2Collection : List<X509Certificate>
	{
		#region METHODS

		public X509Certificate2Collection Find(X509FindType findType, string findValue, bool validOnly)
		{
			return new X509Certificate2Collection();
		}

		#endregion
	}
}