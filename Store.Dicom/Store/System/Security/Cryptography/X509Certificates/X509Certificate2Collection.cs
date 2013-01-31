using System.Collections.Generic;

// ReSharper disable CheckNamespace
namespace System.Security.Cryptography.X509Certificates
// ReSharper restore CheckNamespace
{
	internal class X509Certificate2Collection : List<X509Certificate>
	{
		#region METHODS

		internal X509Certificate2Collection Find(X509FindType findType, string findValue, bool validOnly)
		{
			return new X509Certificate2Collection();
		}

		#endregion
	}
}