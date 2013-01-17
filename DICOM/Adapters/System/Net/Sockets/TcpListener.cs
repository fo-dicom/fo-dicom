using System.Threading.Tasks;
using Dicom;
using Windows.Networking.Sockets;

// ReSharper disable CheckNamespace
namespace System.Net.Sockets
// ReSharper restore CheckNamespace
{
	internal class TcpListener
	{
		private readonly string _port;
		private AsyncCallback _callback;
		private StreamSocketListener _listener;

		#region CONSTRUCTORS

		internal TcpListener(IPAddress address, int port)
		{
			_port = port.ToString();
		}

		#endregion

		#region METHODS

		internal void Start()
		{
			_listener = new StreamSocketListener();
			_listener.ConnectionReceived += OnConnectionReceived;
		}

		internal IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state)
		{
			_callback = callback;
			return Task.Run(async () => await _listener.BindServiceNameAsync(_port));
		}

		internal TcpClient EndAcceptTcpClient(IAsyncResult asyncResult)
		{
			return new TcpClient(asyncResult.AsyncState as StreamSocket);
		}

		internal void Stop()
		{
			_listener.ConnectionReceived -= OnConnectionReceived;
			_listener.Dispose();
		}

		private void OnConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
		{
			_callback(new EventAsyncResult(null, args.Socket));
		}

		#endregion
	}
}