using System.Threading;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Dicom.Network
{
	[TestClass]
	public class DicomClientTests
	{
		[TestMethod]
		public void Send_ToDicomServer_ReturnsSuccess()
		{
			var client = new DicomClient();
			client.NegotiateAsyncOps();

			DicomStatus status = null;
			var request = new DicomCEchoRequest { OnResponseReceived = (echoRequest, response) => status = response.Status };
			client.AddRequest(request);
			client.Send("server", 104, false, "cureos", "cureos");

			while (status == null) Thread.Sleep(10);
			Assert.AreEqual(DicomStatus.Success, status);
		}
	}
}
