using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Dicom.Network
{
	[TestClass]
	public class DicomClientTests
	{
		[TestMethod]
		public void Send_ToDicomServer_ReturnsSuccess()
		{
			Task.Run(() => new DicomServer<DicomCEchoProvider>(104)).Wait(100);

			var client = new DicomClient();
			client.NegotiateAsyncOps();

			DicomStatus status = null;
			var request = new DicomCEchoRequest { OnResponseReceived = (echoRequest, response) => status = response.Status };
			client.AddRequest(request);
			client.Send("localhost", 104, false, "ECHOSCU", "ECHOSCP");

			while (status == null) Thread.Sleep(10);
			Assert.AreEqual(DicomStatus.Success, status);
		}
	}
}
