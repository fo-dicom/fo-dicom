using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

// ReSharper disable CheckNamespace
namespace System.Net.Sockets
// ReSharper restore CheckNamespace
{
	internal class TcpClient
	{
		#region FIELDS

		private readonly Stream _stream;

		#endregion

		#region CONSTRUCTORS

		internal TcpClient(string host, int port)
		{
			_stream = Task.Run(async () =>
				                         {
					                         var socket = new StreamSocket();
					                         await socket.ConnectAsync(new HostName(host), port.ToString());
					                         return new Stream(socket);
				                         }).Result;
		}

		internal TcpClient(StreamSocket socket)
		{
			_stream = new Stream(socket);
		}

		#endregion

		#region METHODS

		internal Stream GetStream()
		{
			return _stream;
		}

		internal void Close()
		{
			_stream.Dispose();
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

			public override void Flush()
			{
				Task.Run(async () => await _socket.OutputStream.FlushAsync()).Wait();
			}

			public override int Read(byte[] buffer, int offset, int count)
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