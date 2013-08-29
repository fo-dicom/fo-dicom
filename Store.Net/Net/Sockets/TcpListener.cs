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
		private readonly ManualResetEventSlim _event;
		private readonly IPAddress _address;
		private readonly string _port;
		private StreamSocketListener _listener;
		private StreamSocket _socket;

		#region CONSTRUCTORS

		public TcpListener(IPAddress address, int port)
		{
			_event = new ManualResetEventSlim();
			_address = address;
			_port = port.ToString();
		}

		#endregion

		#region METHODS

		public void Start()
		{
			_listener = new StreamSocketListener();
			_listener.ConnectionReceived += OnConnectionReceived;
			_socket = null;
		}

		public IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state)
		{
			return
				new TaskFactory().StartNew(new Action<object>(async asyncState =>
					                                                    {
						                                                    _event.Reset();
						                                                    var couldBind = false;
						                                                    while (!couldBind)
						                                                    {
							                                                    try
							                                                    {
																					// Binding to specific endpoint is currently not supported.
								                                                    await _listener.BindServiceNameAsync(_port);
								                                                    couldBind = true;
							                                                    }
							                                                    catch (InvalidOperationException)
							                                                    {
																					// If listening fails because port has already been bound, sleep and try again
								                                                    Thread.Sleep(1000);
							                                                    }
						                                                    }
						                                                    _event.Wait();
					                                                    }),
				                           state).ContinueWith(task => callback(task));
		}

		private void OnConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
		{
			_socket = args.Socket;
			_event.Set();
		}

		public TcpClient EndAcceptTcpClient(IAsyncResult asyncResult)
		{
			return new TcpClient(_socket);
		}

		public void Stop()
		{
			_listener.ConnectionReceived -= OnConnectionReceived;
			_listener.Dispose();
		}

		#endregion
	}
}