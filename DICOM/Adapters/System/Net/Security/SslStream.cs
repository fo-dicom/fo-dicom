using System.IO;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

// ReSharper disable CheckNamespace
namespace System.Net.Security
// ReSharper restore CheckNamespace
{
	internal class SslStream : MemoryStream
	{
		#region FIELDS

		private Stream _innerStream;
		private bool _leaveInnerStreamOpen;
		
		#endregion

		#region CONSTRUCTORS

		internal SslStream(Stream innerStream, bool leaveInnerStreamOpen)
		{
			_innerStream = innerStream;
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
		}

		internal void AuthenticateAsClient(string targetHost)
		{
		}
		
		#endregion
	}
}