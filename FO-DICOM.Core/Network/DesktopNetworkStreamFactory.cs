using FellowOakDicom.Log;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{
    public interface IDesktopNetworkStreamFactory
    {
        /// <summary>
        /// Initializes a server instance of <see cref="DesktopNetworkStream"/>.
        /// </summary>
        /// <param name="tcpClient">TCP client.</param>
        /// <param name="certificate">Certificate for authenticated connection.</param>
        /// <param name="ownsTcpClient">Whether or not the TCP client should be disposed when this instance is disposed</param>
        /// <param name="options">The network listener options</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Ownership of <paramref name="tcpClient"/> is controlled by <paramref name="ownsTcpClient"/>.
        /// 
        /// if <paramref name="ownsTcpClient"/> is false, <paramref name="tcpClient"/> must be disposed by caller.
        /// this is default so that compatible with older versions.
        /// 
        /// if <paramref name="ownsTcpClient"/> is true, <paramref name="tcpClient"/> will be disposed altogether on DesktopNetworkStream's disposal.
        /// </remarks>
        Task<DesktopNetworkStream> CreateAsServerAsync(
            TcpClient tcpClient,
            X509Certificate certificate,
            bool ownsTcpClient,
            NetworkListenerCreationOptions options,
            CancellationToken cancellationToken);

        /// <summary>
        /// Initializes a client instance of <see cref="DesktopNetworkStream"/>.
        /// </summary>
        /// <param name="options">The options that specify how to open a connection</param>
        /// <param name="cancellationToken">The cancellation token that cancels the connection</param>
        Task<DesktopNetworkStream> CreateAsClientAsync(NetworkStreamCreationOptions options, CancellationToken cancellationToken);
    }

    public class DesktopNetworkStreamFactory : IDesktopNetworkStreamFactory
    {
        private static readonly TimeSpan _sslHandshakeTimeout = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Initializes a server instance of <see cref="DesktopNetworkStream"/>.
        /// </summary>
        /// <param name="tcpClient">TCP client.</param>
        /// <param name="certificate">Certificate for authenticated connection.</param>
        /// <param name="ownsTcpClient">Whether or not the TCP client should be disposed when this instance is disposed</param>
        /// <param name="options">The network listener options</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Ownership of <paramref name="tcpClient"/> is controlled by <paramref name="ownsTcpClient"/>.
        /// 
        /// if <paramref name="ownsTcpClient"/> is false, <paramref name="tcpClient"/> must be disposed by caller.
        /// this is default so that compatible with older versions.
        /// 
        /// if <paramref name="ownsTcpClient"/> is true, <paramref name="tcpClient"/> will be disposed altogether on DesktopNetworkStream's disposal.
        /// </remarks>
        public async Task<DesktopNetworkStream> CreateAsServerAsync(
            TcpClient tcpClient,
            X509Certificate certificate,
            bool ownsTcpClient,
            NetworkListenerCreationOptions options,
            CancellationToken cancellationToken)
        {
            var localEndpoint = (IPEndPoint) tcpClient.Client.LocalEndPoint;
            var remoteEndpoint = ((IPEndPoint)tcpClient.Client.RemoteEndPoint);
            var logger = options.Logger;
            var localHost = localEndpoint.Address.ToString();
            var localPort = localEndpoint.Port;
            var remoteHost = remoteEndpoint.Address.ToString();
            var remotePort = remoteEndpoint.Port;

            Stream stream = tcpClient.GetStream();
            if (certificate != null)
            {
                logger?.Debug("Setting up server SSL network stream with certificate {ServerCertificateSubject}", certificate.Subject);
                
                var requireMutualAuthentication = options.ServiceOptions.RequireMutualAuthentication;
                var userCertificateValidationCallback = options.ServiceOptions.ServerCertificateValidationCallback
                                                        ?? ((sender, _, chain, errors) =>
                                                        {
                                                            if (!requireMutualAuthentication)
                                                            {
                                                                errors &= ~SslPolicyErrors.RemoteCertificateNotAvailable;
                                                            }

                                                            return errors == SslPolicyErrors.None || options.ServiceOptions.IgnoreSslPolicyErrors;
                                                        });
                
                var ssl = new SslStream(stream, false, userCertificateValidationCallback);
                var sslHandshake =
                    Task.Run(
                        () => ssl.AuthenticateAsServerAsync(certificate, requireMutualAuthentication, SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, false),
                        cancellationToken);
                var sslHandshakeTimeout = Task.Delay(_sslHandshakeTimeout, cancellationToken);

                if (await Task.WhenAny(sslHandshake, sslHandshakeTimeout).ConfigureAwait(false) == sslHandshakeTimeout)
                {
                    throw new DicomNetworkException($"SSL server authentication took longer than {_sslHandshakeTimeout.TotalSeconds}s");
                }

                try
                {
                    await sslHandshake.ConfigureAwait(false);
                }
                catch (AuthenticationException e)
                {
                    throw new DicomNetworkException("Server SSL authentication failed", e);
                }

                if (requireMutualAuthentication)
                {
                    if (!ssl.IsMutuallyAuthenticated)
                    {
                        throw new DicomNetworkException("Client SSL authentication failed");
                    }

                    logger?.Debug("Mutual server and client SSL authentication succeeded");
                }
                else
                {
                    logger?.Debug("Server SSL authentication succeeded");
                }

                stream = ssl;
            }

            return new DesktopNetworkStream(localHost, localPort, remoteHost, remotePort, stream, ownsTcpClient ? tcpClient : null);
        }

        /// <summary>
        /// Initializes a client instance of <see cref="DesktopNetworkStream"/>.
        /// </summary>
        /// <param name="options">The options that specify how to open a connection</param>
        /// <param name="cancellationToken">The cancellation token that cancels the connection</param>
        public async Task<DesktopNetworkStream> CreateAsClientAsync(NetworkStreamCreationOptions options, CancellationToken cancellationToken)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var remoteHost = options.Host;
            var remotePort = options.Port;
            var logger = options.Logger;

            var tcpClient = new TcpClient { NoDelay = options.NoDelay };

            await tcpClient.ConnectAsync(options.Host, options.Port).ConfigureAwait(false);

            Stream stream = tcpClient.GetStream();
            if (options.UseTls)
            {
                if (options.ClientCertificates != null && options.ClientCertificates.Count > 0)
                {
                    var clientCertificateSubjects = string.Join(" and ", options.ClientCertificates.OfType<X509Certificate>().Select(c => c.Subject));
                    logger?.Debug("Setting up client SSL authentication with certificates: {ClientCertificateSubjects}", Environment.NewLine, clientCertificateSubjects);
                }
                else
                {
                    logger?.Debug("Setting up client SSL authentication");
                }

                var optionsIgnoreSslPolicyErrors = options.IgnoreSslPolicyErrors;
                var userCertificateValidationCallback = options.ClientCertificateValidationCallback
                                                        ?? ((sender, certificate, chain, errors) => errors == SslPolicyErrors.None || optionsIgnoreSslPolicyErrors);
                var ssl = new SslStream(
                    stream,
                    false,
                    userCertificateValidationCallback);
                ssl.ReadTimeout = (int)options.Timeout.TotalMilliseconds;
                ssl.WriteTimeout = (int)options.Timeout.TotalMilliseconds;

                var sslProtocols = options.ClientSslProtocols ?? (SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12);
                var checkClientCertificateRevocation = options.CheckClientCertificateRevocation ?? false;
                var sslHandshake = options.ClientCertificates != null && options.ClientCertificates.Count > 0
                    ? Task.Run(() => ssl.AuthenticateAsClientAsync(options.Host, options.ClientCertificates, sslProtocols, checkClientCertificateRevocation), cancellationToken)
                    : Task.Run(() => ssl.AuthenticateAsClientAsync(options.Host), cancellationToken);

                var sslHandshakeTimeout = Task.Delay(_sslHandshakeTimeout, cancellationToken);

                if (await Task.WhenAny(sslHandshake, sslHandshakeTimeout).ConfigureAwait(false) == sslHandshakeTimeout)
                {
                    throw new DicomNetworkException($"Client SSL authentication failed because it took longer than {_sslHandshakeTimeout.TotalSeconds}s");
                }

                await sslHandshake.ConfigureAwait(false);
                
                logger?.Debug("Client SSL authentication succeeded");

                stream = ssl;
            }

            var localHost = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.ToString();
            var localPort = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Port;

            return new DesktopNetworkStream(localHost, localPort, remoteHost, remotePort, stream, tcpClient);
        }
    }
}