using System.IO;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Dicom.Network;

// ReSharper disable CheckNamespace
namespace System.Net.Security
// ReSharper restore CheckNamespace
{
	internal class SslStream : MemoryStream
	{
		#region FIELDS

		private TcpClient.Stream _innerStream;
		private bool _leaveInnerStreamOpen;
		
		#endregion

		#region CONSTRUCTORS

		internal SslStream(Stream innerStream, bool leaveInnerStreamOpen)
		{
			var tcpClientStream = innerStream as TcpClient.Stream;
			if (tcpClientStream == null)
				throw new ArgumentException("Stream type not associated with TCP client", "innerStream");

			_innerStream = tcpClientStream;
			_leaveInnerStreamOpen = leaveInnerStreamOpen;
		}

		internal SslStream(Stream innerStream) : this(innerStream, true)
		{
		}
		
		#endregion

		#region METHODS

		internal void AuthenticateAsServer(X509Certificate serverCertificate, 
			bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			throw new DicomNetworkException("SSL server support is not implemented");
		}

		internal void AuthenticateAsClient(string targetHost)
		{
			_innerStream.UpgradeToSsl(targetHost);
		}
		
		#endregion
	}
}