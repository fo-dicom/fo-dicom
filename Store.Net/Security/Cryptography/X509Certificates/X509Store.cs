// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

namespace System.Security.Cryptography.X509Certificates
{
	public class X509Store
	{
		#region CONSTRUCTORS

		public X509Store(StoreName storeName, StoreLocation storeLocation)
		{
			Certificates = new X509Certificate2Collection();
		}

		#endregion

		#region PROPERTIES

		public X509Certificate2Collection Certificates { get; private set; }

		#endregion

		#region METHODS

		public void Open(OpenFlags openFlags)
		{
			
		}

		public void Close()
		{
			
		}

		#endregion
	}
}