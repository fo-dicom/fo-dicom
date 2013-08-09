using System.IO;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace System.Net.Sockets
{
	public class TcpClient
	{
		#region FIELDS

		private readonly Stream _stream;

		#endregion

		#region CONSTRUCTORS

		public TcpClient(string host, int port)
		{
			try
			{
				_stream = Task.Run(async () =>
					                         {
						                         var socket = new StreamSocket();
						                         await socket.ConnectAsync(new HostName(host), port.ToString());
						                         return new Stream(socket);
					                         }).Result;
			}
			catch (Exception e)
			{
				throw e.InnerException ?? e;
			}
		}

		public TcpClient(StreamSocket socket)
		{
			if (socket == null) throw new ArgumentNullException("socket");
			_stream = new Stream(socket);
		}

		#endregion

		#region PROPERTIES

		public bool NoDelay { get; set; }

		#endregion
		
		#region METHODS

		public global::System.IO.Stream GetStream()
		{
			return _stream;
		}

		public void Close()
		{
			if (_stream != null) _stream.Dispose();
		}

		#endregion

		internal class Stream : global::System.IO.Stream
		{
			#region FIELDS

			private readonly StreamSocket _socket;

			#endregion

			#region CONSTRUCTORS

			internal Stream(StreamSocket socket)
			{
				_socket = socket;
			}

			#endregion

			#region METHODS

			internal void UpgradeToSsl(string validationHost)
			{
				if (!Task.Run(async () => await _socket.UpgradeToSslAsync(SocketProtectionLevel.Ssl, new HostName(validationHost))).Wait(10000))
					throw new SocketException("Could not authenticate '{0}' as SSL server", validationHost);
			}

			public override void Flush()
			{
			}

			public override int Read(byte[] buffer, int offset, int count)
			{
				try
				{
					Task.Run(async () =>
						               {
							               var reader = new DataReader(_socket.InputStream);
							               await reader.LoadAsync((uint)count);
							               var buf = new byte[count];
							               reader.ReadBytes(buf);
							               reader.DetachStream();
							               Array.Copy(buf, 0, buffer, offset, count);
						               }).Wait();
					return count;
				}
				catch (Exception e)
				{
					throw e.InnerException ?? e;
				}
			}

			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			public override void Write(byte[] buffer, int offset, int count)
			{
				try
				{
					Task.Run(async () =>
					{
						var buf = new byte[count];
						Array.Copy(buffer, offset, buf, 0, count);
						var writer = new DataWriter(_socket.OutputStream);
						writer.WriteBytes(buf);
						await writer.StoreAsync();
						writer.DetachStream();
					}).Wait();
				}
				catch (Exception e)
				{
					throw e.InnerException ?? e;
				}
			}

			protected override void Dispose(bool disposing)
			{
				_socket.Dispose();
			}

			#endregion

			#region PROPERTIES

			public override bool CanRead
			{
				get { return true; }
			}

			public override bool CanSeek
			{
				get { return false; }
			}

			public override bool CanWrite
			{
				get { return true; }
			}

			public override long Length
			{
				get { throw new NotSupportedException(); }
			}

			public override long Position
			{
				get { throw new NotSupportedException(); }
				set { throw new NotSupportedException(); }
			}

			#endregion
		}
	}
}