using System;
using System.IO;
using System.Net.Security;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Tls
{
    public class DefaultTlsInitiator : ITlsInitiator
    {

        /// <summary>
        /// Whether or not to ignore any certificate validation errors that occur when authenticating as a client over SSL
        /// </summary>
        public bool IgnoreSslPolicyErrors { get; set; }

        public TimeSpan SslHandshakeTimeout { get; set; } = TimeSpan.FromMinutes(1);

        public DefaultTlsInitiator() { }


        public Stream InitiateTls(Stream plainStream, string remoteAddress, int remotePort)
        {
            var ssl = new SslStream(
                   plainStream,
                   false,

                   (sender, certificate, chain, errors) => errors == SslPolicyErrors.None || IgnoreSslPolicyErrors);

            var authenticationSucceeded = Task.Run(async () => await ssl.AuthenticateAsClientAsync(remoteAddress).ConfigureAwait(false)).Wait(SslHandshakeTimeout);

            if (!authenticationSucceeded)
            {
                throw new DicomNetworkException($"SSL client authentication took longer than {SslHandshakeTimeout.TotalSeconds}s");
            }

            return ssl;
        }

    }
}
