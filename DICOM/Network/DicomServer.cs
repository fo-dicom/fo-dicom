// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

using Dicom.Log;

namespace Dicom.Network
{
    public class DicomServer<T> : IDisposable
        where T : DicomService, IDicomServiceProvider
    {
        private X509Certificate _cert;

        private TcpListener _listener;

        private List<T> _clients;

        private Timer _timer;

        private object _synchRoot = new object();

        public DicomServer(int port, string certificateName = null)
        {
            _clients = new List<T>();

            if (certificateName != null)
            {
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);

                var certs = store.Certificates.Find(X509FindType.FindBySubjectName, certificateName, false);

                if (certs.Count == 0) throw new DicomNetworkException("Unable to find certificate for " + certificateName);

                _cert = certs[0];

                store.Close();
            }

            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _listener.BeginAcceptTcpClient(OnAcceptTcpClient, null);

            _timer = new Timer(OnTimerTick, false, 1000, 1000);
        }

        public Logger Logger { get; set; }

        /// <summary>
        /// Options to control behavior of <see cref="DicomService"/> base class.
        /// </summary>
        public DicomServiceOptions Options { get; set; }

        private void OnAcceptTcpClient(IAsyncResult result)
        {
            try
            {

                TcpClient client = null;
                lock (_synchRoot)
                {
                    if (_listener == null)
                    {
                        return;
                    }
                    client = _listener.EndAcceptTcpClient(result);
                }



                if (Options != null) client.NoDelay = Options.TcpNoDelay;
                else client.NoDelay = DicomServiceOptions.Default.TcpNoDelay;

                Stream stream = client.GetStream();

                if (_cert != null)
                {
                    var ssl = new SslStream(stream, false);
                    ssl.AuthenticateAsServer(_cert, false, SslProtocols.Tls, false);

                    stream = ssl;
                }

                T scp = CreateScp(stream);

                if (Options != null) scp.Options = Options;

                _clients.Add(scp);
            }
            catch (Exception e)
            {
                if (Logger == null) Logger = LogManager.GetLogger("Dicom.Network");
                Logger.Error("Exception accepting client {@error}", e);
            }
            finally
            {
                lock (_synchRoot)
                {
                    if (_listener != null) _listener.BeginAcceptTcpClient(OnAcceptTcpClient, null);

                }
            }
        }

        private void OnTimerTick(object state)
        {
            try
            {
                for (int i = 0; i < _clients.Count; i++) if (!_clients[i].IsConnected) _clients.RemoveAt(i--);
            }
            catch
            {
            }
        }

        public void Dispose()
        {
            lock (_synchRoot)
            {
                _listener.Stop();
                _listener = null;
            }
        }

        protected virtual T CreateScp(Stream stream)
        {
            return (T)Activator.CreateInstance(typeof(T), stream, Logger);
        }
    }
}
