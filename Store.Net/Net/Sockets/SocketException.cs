// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using Windows.Networking.Sockets;

namespace System.Net.Sockets
{
	public class SocketException : Exception
	{
		#region CONSTRUCTORS

		public SocketException(int errorCode)
		{
			ErrorCode = errorCode;
		}

		public SocketException(string format, params object[] args) : base(String.Format(format, args))
		{
		}

		#endregion

		#region PROPERTIES

		public int ErrorCode { get; private set; }

		public SocketErrorStatus SocketErrorCode
		{
			get { return (SocketErrorStatus)ErrorCode; }
		}

		#endregion
	}
}