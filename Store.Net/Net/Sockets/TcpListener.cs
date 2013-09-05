// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace System.Net.Sockets
{
	public class TcpListener
	{
		private readonly ManualResetEventSlim _event;
		private readonly IPAddress _address;
		private readonly string _port;
		private StreamSocketListener _listener;
		private TcpClient _client;

		#region CONSTRUCTORS

		public TcpListener(IPAddress address, int port)
		{
			_event = new ManualResetEventSlim();
			_address = address;
			_port = port.ToString(CultureInfo.InvariantCulture);
		}

		#endregion

		#region METHODS

		public void Start()
		{
			_client = null;

			_listener = new StreamSocketListener();
			_listener.ConnectionReceived += OnConnectionReceived;

			// Binding to specific endpoint is currently not supported.
			_event.Reset();
			Task.Run(async () => await _listener.BindServiceNameAsync(_port));
		}

		public IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state)
		{
			return
				new TaskFactory().StartNew(new Action<object>(asyncState => _event.Wait()), state)
				                 .ContinueWith(task => callback(task));
		}

		private void OnConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
		{
			_event.Set();
			_client = new TcpClient(args.Socket);
		}

		public TcpClient EndAcceptTcpClient(IAsyncResult asyncResult)
		{
			_event.Reset();
			return _client;
		}

		public void Stop()
		{
			_listener.ConnectionReceived -= OnConnectionReceived;
			_listener.Dispose();
		}

		#endregion
	}
}