// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace System.Net.Sockets
{
	public class TcpListener
	{
		private readonly string _port;
		private AsyncCallback _callback;
		private StreamSocketListener _listener;

		#region CONSTRUCTORS

		public TcpListener(IPAddress address, int port)
		{
			_port = port.ToString();
		}

		#endregion

		#region METHODS

		public void Start()
		{
			_listener = new StreamSocketListener();
			_listener.ConnectionReceived += OnConnectionReceived;
		}

		public IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state)
		{
			_callback = callback;
			return Task.Run(async () => await _listener.BindServiceNameAsync(_port));
		}

		public TcpClient EndAcceptTcpClient(IAsyncResult asyncResult)
		{
			return new TcpClient(asyncResult.AsyncState as StreamSocket);
		}

		public void Stop()
		{
			_listener.ConnectionReceived -= OnConnectionReceived;
			_listener.Dispose();
		}

		private void OnConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
		{
			_callback(new TcpListenerAsyncResult(args.Socket));
		}

		private class TcpListenerAsyncResult : IAsyncResult
		{
			#region FIELDS

			private readonly StreamSocket _socket;
			private readonly ManualResetEventSlim _event;

			#endregion

			#region CONSTRUCTORS

			public TcpListenerAsyncResult(StreamSocket socket)
			{
				_socket = socket;
				_event = new ManualResetEventSlim();
			}

			#endregion

			#region PROPERTIES

			public object AsyncState
			{
				get { return _socket; }
			}

			public WaitHandle AsyncWaitHandle
			{
				get { return _event.WaitHandle; }
			}

			public bool CompletedSynchronously
			{
				get { return false; }
			}

			public bool IsCompleted
			{
				get { return _event.IsSet; }
			}
			
			#endregion
		}

		#endregion
	}
}