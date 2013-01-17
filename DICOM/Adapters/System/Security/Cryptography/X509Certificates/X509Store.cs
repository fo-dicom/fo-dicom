// ReSharper disable CheckNamespace
namespace System.Security.Cryptography.X509Certificates
// ReSharper restore CheckNamespace
{
	internal class X509Store
	{
		#region CONSTRUCTORS

		internal X509Store(StoreName storeName, StoreLocation storeLocation)
		{
			Certificates = new X509Certificate2Collection();
		}

		#endregion

		#region PROPERTIES

		internal X509Certificate2Collection Certificates { get; private set; }

		#endregion

		#region METHODS

		internal void Open(OpenFlags openFlags)
		{
			
		}

		internal void Close()
		{
			
		}

		#endregion
	}
}