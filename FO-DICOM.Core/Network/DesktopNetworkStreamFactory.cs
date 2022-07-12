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
        /// <param name="tlsOptions">The options that configure TLS authentication</param>
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
            DicomServerTlsOptions tlsOptions,
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
        /// <inheritdoc cref="IDesktopNetworkStreamFactory.CreateAsServerAsync" />
        public async Task<DesktopNetworkStream> CreateAsServerAsync(
            TcpClient tcpClient,
            DicomServerTlsOptions tlsOptions,
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
            
            if (tlsOptions?.Certificate != null)
            {
                var certificate = tlsOptions.Certificate;
                var requireMutualAuthentication = tlsOptions.RequireMutualAuthentication;
                var userCertificateValidationCallback = tlsOptions.CertificateValidationCallback
                                                        ?? ((sender, _, chain, errors) =>
                                                        {
                                                            if (!requireMutualAuthentication)
                                                            {
                                                                errors &= ~SslPolicyErrors.RemoteCertificateNotAvailable;
                                                            }
                                                            return errors == SslPolicyErrors.None;
                                                        });
                var protocols = tlsOptions.Protocols;
                var checkCertificateRevocation = tlsOptions.CheckCertificateRevocation;
                var timeout = tlsOptions.Timeout;
                
                logger?.Debug("Setting up server SSL network stream with certificate {ServerCertificateSubject}", certificate.Subject);

                var ssl = new SslStream(stream, false, userCertificateValidationCallback);
                var sslHandshake =
                    Task.Run(
                        () => ssl.AuthenticateAsServerAsync(certificate, requireMutualAuthentication, protocols, checkCertificateRevocation),
                        cancellationToken);
                var sslHandshakeTimeout = Task.Delay(timeout, cancellationToken);

                if (await Task.WhenAny(sslHandshake, sslHandshakeTimeout).ConfigureAwait(false) == sslHandshakeTimeout)
                {
                    throw new DicomNetworkException($"SSL server authentication took longer than {timeout.TotalSeconds}s");
                }

                try
                {
                    await sslHandshake.ConfigureAwait(false);
                }
                catch (AuthenticationException e)
                {
                    throw new DicomNetworkException("Server TLS authentication failed", e);
                }

                if (requireMutualAuthentication)
                {
                    if (!ssl.IsMutuallyAuthenticated)
                    {
                        throw new DicomNetworkException("Client TLS authentication failed");
                    }

                    logger?.Debug("Mutual server and client TLS authentication succeeded");
                }
                else
                {
                    logger?.Debug("Server TLS authentication succeeded");
                }

                stream = ssl;
            }

            return new DesktopNetworkStream(localHost, localPort, remoteHost, remotePort, stream, ownsTcpClient ? tcpClient : null);
        }

        /// <inheritdoc cref="IDesktopNetworkStreamFactory.CreateAsClientAsync"/>
        public async Task<DesktopNetworkStream> CreateAsClientAsync(NetworkStreamCreationOptions options, CancellationToken cancellationToken)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var remoteHost = options.Host;
            var remotePort = options.Port;
            var logger = options.Logger;
            var tlsOptions = options.TlsOptions;
            var noDelay = options.NoDelay;
            var useTls = options.UseTls;
            var timeout = options.Timeout;

            var tcpClient = new TcpClient { NoDelay = noDelay };

            await tcpClient.ConnectAsync(remoteHost, remotePort).ConfigureAwait(false);

            var networkStream = tcpClient.GetStream();
            Stream stream;
            if (useTls && tlsOptions != null)
            {
                var certificates = tlsOptions.Certificates;
                var protocols = tlsOptions.Protocols;
                var checkCertificateRevocation = tlsOptions.CheckCertificateRevocation;
                var userCertificateValidationCallback = tlsOptions.CertificateValidationCallback
                                                        ?? ((sender, certificate, chain, errors) => errors == SslPolicyErrors.None);
                var tlsTimeout = tlsOptions.Timeout;
                
                if (certificates?.Count > 0)
                {
                    var clientCertificateSubjects = string.Join(" and ", certificates.OfType<X509Certificate>().Select(c => c.Subject));
                    logger?.Debug("Setting up client TLS authentication with certificates: {CertificateSubjects}", clientCertificateSubjects);
                }
                else
                {
                    logger?.Debug("Setting up client TLS authentication");
                }

                var ssl = new SslStream(networkStream, false, userCertificateValidationCallback);
                if (timeout != null)
                {
                    ssl.ReadTimeout = (int)timeout.Value.TotalMilliseconds;
                    ssl.WriteTimeout = (int)timeout.Value.TotalMilliseconds;
                }

                var sslHandshake = certificates?.Count > 0
                    ? Task.Run(() => ssl.AuthenticateAsClientAsync(remoteHost, certificates, protocols, checkCertificateRevocation), cancellationToken)
                    : Task.Run(() => ssl.AuthenticateAsClientAsync(remoteHost), cancellationToken);
                var sslHandshakeTimeout = Task.Delay(tlsTimeout, cancellationToken);

                if (await Task.WhenAny(sslHandshake, sslHandshakeTimeout).ConfigureAwait(false) == sslHandshakeTimeout)
                {
                    throw new DicomNetworkException($"Client TLS authentication failed because it took longer than {tlsTimeout.TotalSeconds}s");
                }

                await sslHandshake.ConfigureAwait(false);
                
                logger?.Debug("Client TLS authentication succeeded");

                stream = ssl;
            }
            else
            {
                if (timeout != null)
                {
                    networkStream.ReadTimeout = (int)timeout.Value.TotalMilliseconds;
                    networkStream.WriteTimeout = (int)timeout.Value.TotalMilliseconds;
                }

                stream = networkStream;
            }

            var localHost = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.ToString();
            var localPort = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Port;

            return new DesktopNetworkStream(localHost, localPort, remoteHost, remotePort, stream, tcpClient);
        }
    }
}