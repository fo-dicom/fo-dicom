using Windows.Networking.Sockets;

// ReSharper disable CheckNamespace
namespace System.Net.Sockets
// ReSharper restore CheckNamespace
{
	internal class SocketException : Exception
	{
		#region CONSTRUCTORS

		internal SocketException(int errorCode)
		{
			ErrorCode = errorCode;
			SocketErrorCode = (SocketErrorStatus)errorCode;
		}

		#endregion

		#region PROPERTIES

		internal int ErrorCode { get; private set; }

		internal SocketErrorStatus SocketErrorCode { get; private set; }

		#endregion
	}
}