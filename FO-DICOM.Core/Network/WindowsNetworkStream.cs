// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Threading;
    using System.Threading.Tasks;

    using Dicom.IO;

    using Windows.Networking;
    using Windows.Networking.Sockets;
    using Windows.Security.Cryptography.Certificates;
    using Windows.Storage.Streams;

    /// <summary>
    /// Universal Windows Platform implementation of <see cref="INetworkStream"/>.
    /// </summary>
    public sealed class WindowsNetworkStream : Stream, INetworkStream
    {
        #region FIELDS

        private bool disposed = false;

        private readonly StreamSocket socket;

        private readonly bool canDisposeSocket;

        private readonly bool isConnected;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a client instance of <see cref="WindowsNetworkStream"/>.
        /// </summary>
        /// <param name="host">Network host.</param>
        /// <param name="port">Network port.</param>
        /// <param name="useTls">Use TLS layer?</param>
        /// <param name="noDelay">No delay?</param>
        /// <param name="ignoreSslPolicyErrors">Ignore SSL policy errors?</param>
        internal WindowsNetworkStream(string host, int port, bool useTls, bool noDelay, bool ignoreSslPolicyErrors, int millisecondsTimeout)
        {
            this.RemoteHost = host;
            this.RemotePort = port;
            this.socket = new StreamSocket();
            this.canDisposeSocket = true;

            this.socket.Control.NoDelay = noDelay;

            if (ignoreSslPolicyErrors)
            {
                foreach (var value in Enum.GetValues(typeof(ChainValidationResult)))
                {
                    this.socket.Control.IgnorableServerCertificateErrors.Add((ChainValidationResult)value);
                }
            }

            this.isConnected = this.EstablishConnectionAsync(host, port, useTls).Result;

            this.LocalHost = this.socket.Information.LocalAddress.DisplayName;
            this.LocalPort = int.Parse(this.socket.Information.LocalPort, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Initializes a server instance of <see cref="WindowsNetworkStream"/>.
        /// </summary>
        /// <param name="socket">TCP socket.</param>
        /// <param name="ownsSocket">dispose <paramref name="socket"/> on Dispose</param>
        /// <remarks>
        /// Ownership of <paramref name="socket"/> is controlled by <paramref name="ownsSocket"/>.
        /// 
        /// if <paramref name="ownsSocket"/> is false, <paramref name="socket"/> must be disposed by caller.
        /// this is default so that compatible with older versions.
        /// 
        /// if <paramref name="ownsSocket"/> is true, <paramref name="socket"/> will be disposed altogether on WindowsNetworkStream's disposal.
        /// </remarks>
        internal WindowsNetworkStream(StreamSocket socket, bool ownsSocket = false)
        {
            this.LocalHost = socket.Information.LocalAddress.DisplayName;
            this.LocalPort = int.Parse(socket.Information.LocalPort, CultureInfo.InvariantCulture);
            this.RemoteHost = socket.Information.RemoteAddress.DisplayName;
            this.RemotePort = int.Parse(socket.Information.RemotePort, CultureInfo.InvariantCulture);

            this.socket = socket;
            this.canDisposeSocket = ownsSocket;
            this.isConnected = true;
        }

        /// <summary>
        /// Destrutor.
        /// </summary>
        ~WindowsNetworkStream()
        {
            this.Dispose(false);
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the remote host of the network stream.
        /// </summary>
        public string RemoteHost { get; }

        /// <summary>
        /// Gets the local host of the network stream
        /// </summary>
        public string LocalHost { get; }

        /// <summary>
        /// Gets the remote port of the network stream.
        /// </summary>
        public int RemotePort { get; }

        /// <summary>
        /// Gets the local port of the network stream.
        /// </summary>
        public int LocalPort { get; }

        #endregion

        #region METHODS

        /// <summary>
        /// Get corresponding <see cref="Stream"/> object.
        /// </summary>
        /// <returns>Network stream as <see cref="Stream"/> object.</returns>
        public Stream AsStream()
        {
            return this;
        }

        /// <summary>
        /// Do the actual disposal.
        /// </summary>
        /// <param name="disposing">True if called from <see cref="Dispose"/>, false otherwise.</param>
        /// <remarks>The underlying stream is normally passed on to a <see cref="DicomService"/> implementation that
        /// is responsible for disposing the stream when appropriate. Therefore, the stream should not be disposed here.</remarks>
        protected override void Dispose(bool disposing)
        {
            if (this.disposed) return;

            if (this.canDisposeSocket && this.socket != null)
            {
                this.socket.Dispose();
            }

            this.disposed = true;
        }

        /// <summary>
        /// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <filterpriority>2</filterpriority>
        public override async void Flush()
        {
            if (this.isConnected)
            {
                await this.socket.OutputStream.FlushAsync().AsTask().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <returns>
        /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
        /// </returns>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source. </param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream. </param>
        /// <param name="count">The maximum number of bytes to be read from the current stream. </param>
        /// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset"/> and <paramref name="count"/> is larger than the buffer length. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="buffer"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        /// <filterpriority>1</filterpriority>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!this.isConnected)
            {
                throw new DicomIoException("Cannot write; no socket connection is established.");
            }

            return DoReadAsync(this.socket.InputStream, buffer, offset, count).Result;
        }

        /// <summary>
        /// Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The value of the <paramref name="count"/> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached. 
        /// </returns>
        /// <param name="buffer">The buffer to write the data into.</param>
        /// <param name="offset">The byte offset in <paramref name="buffer"/> at which to begin writing data from the stream.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="buffer"/> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative.</exception>
        /// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset"/> and <paramref name="count"/> is larger than the buffer length.</exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
        /// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation. </exception>
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (!this.isConnected)
            {
                throw new DicomIoException("Cannot read; no socket connection is established.");
            }

            return DoReadAsync(this.socket.InputStream, buffer, offset, count);
        }

        /// <summary>
        /// When overridden in a derived class, sets the position within the current stream.
        /// </summary>
        /// <returns>
        /// The new position within the current stream.
        /// </returns>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter. 
        /// </param><param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"/> indicating the reference point used to obtain the new position. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        /// <filterpriority>1</filterpriority>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// When overridden in a derived class, sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        /// <filterpriority>2</filterpriority>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream. </param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream. </param>
        /// <param name="count">The number of bytes to be written to the current stream. </param>
        /// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset"/> and <paramref name="count"/> is greater than the buffer length.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="buffer"/>  is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occured, such as the specified file cannot be found.</exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
        /// <exception cref="T:System.ObjectDisposedException"><see cref="M:System.IO.Stream.Write(System.Byte[],System.Int32,System.Int32)"/> was called after the stream was closed.</exception>
        /// <filterpriority>1</filterpriority>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!this.isConnected)
            {
                throw new DicomIoException("Cannot write; no socket connection is established.");
            }

            DoWriteAsync(this.socket.OutputStream, buffer, offset, count).Wait();
        }

        /// <summary>
        /// Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous write operation.
        /// </returns>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> from which to begin copying bytes to the stream.</param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None"/>.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="buffer"/> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative.</exception>
        /// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset"/> and <paramref name="count"/> is larger than the buffer length.</exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception><exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
        /// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation. </exception>
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (!this.isConnected)
            {
                throw new DicomIoException("Cannot write; no socket connection is established.");
            }

            return DoWriteAsync(this.socket.OutputStream, buffer, offset, count);
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
        /// </summary>
        /// <returns>
        /// true if the stream supports reading; otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
        /// </summary>
        /// <returns>
        /// true if the stream supports seeking; otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
        /// </summary>
        /// <returns>
        /// true if the stream supports writing; otherwise, false.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets the length in bytes of the stream.
        /// </summary>
        /// <returns>
        /// A long value representing the length of the stream in bytes.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        /// <filterpriority>1</filterpriority>
        public override long Length
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets or sets the position within the current stream.
        /// </summary>
        /// <returns>
        /// The current position within the stream.
        /// </returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
        /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        /// <filterpriority>1</filterpriority>
        public override long Position
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        private async Task<bool> EstablishConnectionAsync(string host, int port, bool useTls)
        {
            try
            {
                await
                    this.socket.ConnectAsync(new HostName(host), port.ToString(CultureInfo.InvariantCulture))
                        .AsTask()
                        .ConfigureAwait(false);

                if (useTls)
                {
                    await
                        this.socket.UpgradeToSslAsync(SocketProtectionLevel.Tls10, new HostName(host))
                            .AsTask()
                            .ConfigureAwait(false);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static async Task<int> DoReadAsync(IInputStream stream, byte[] buffer, int offset, int count)
        {
            try
            {
                using (var reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)count).AsTask().ConfigureAwait(false);
                    var length = Math.Min((int)reader.UnconsumedBufferLength, count);
                    if (length > 0)
                    {
                        reader.ReadBuffer((uint)length).CopyTo(0, buffer, offset, length);
                    }
                    reader.DetachStream();
                    return length;
                }
            }
            catch
            {
                return 0;
            }
        }

        private static async Task DoWriteAsync(IOutputStream stream, byte[] buffer, int offset, int count)
        {
            try
            {
                using (var writer = new DataWriter(stream))
                {
                    byte[] tmp;
                    if (offset == 0)
                    {
                        tmp = buffer;
                    }
                    else
                    {
                        tmp = new byte[count];
                        Array.Copy(buffer, offset, tmp, 0, count);
                    }

                    writer.WriteBytes(tmp);
                    await writer.StoreAsync().AsTask().ConfigureAwait(false);
                    writer.DetachStream();
                }
            }
            catch (Exception e)
            {
                throw new IOException("Socket write failure.", e.InnerException ?? e);
            }
        }

        #endregion
    }
}
